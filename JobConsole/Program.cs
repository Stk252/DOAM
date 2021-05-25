using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using DOAM.Infrastructure;
using DOAM.Infrastructure.Elasticsearch.IndexDocuments;
using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Elasticsearch;
using System.Threading;
using System.Runtime.ExceptionServices;

namespace JobConsole
{
    class Program
    {
        private static ElasticClient Client { get; set; }
        private static string CurrentIndexName { get; set; }


        static void Main(string[] args)
        {
            Client = DOAMSearchConfiguration.GetClient();
            CurrentIndexName = DOAMSearchConfiguration.CreateIndexName();

            CreateIndex();
            IndexDumps();
            SwapAlias();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void CreateIndex()
        {
            Client.Indices.Create(
                CurrentIndexName,
                c => c.Map<AssetDocument>(m => m.AutoMap())
            );
        }


        static void IndexDumps()
        {
            Console.WriteLine("Yieling asset documents...");
            var assets = GetAssetDocuments();

            Console.Write("Indexing assets into Elasticsearch...");
            var waitHandle = new CountdownEvent(1);

            var bulkAll = Client.BulkAll(assets, b => b
                .Index(CurrentIndexName)
                .BackOffRetries(2)
                .BackOffTime("30s")
                .RefreshOnCompleted(true)
                .MaxDegreeOfParallelism(4)
                .Size(1000)
            );

            ExceptionDispatchInfo captureInfo = null;

            bulkAll.Subscribe(new BulkAllObserver(
                onNext: b => Console.Write("."),
                onError: e =>
                {
                    captureInfo = ExceptionDispatchInfo.Capture(e);
                    waitHandle.Signal();
                },
                onCompleted: () => waitHandle.Signal()
            ));

            waitHandle.Wait();
            captureInfo?.Throw();
            Console.WriteLine("Done.");
        }


        private static void SwapAlias()
        {
            var indexExists = Client.Indices.Exists(DOAMSearchConfiguration.LiveIndexAlias).Exists;

            Client.Indices.BulkAlias(aliases => 
            {
                if (indexExists)
                    aliases.Add(a => a
                        .Alias(DOAMSearchConfiguration.OldIndexAlias)
                        .Index(Client.GetIndicesPointingToAlias(DOAMSearchConfiguration.LiveIndexAlias).First())
                    );

                return aliases
                    .Remove(a => a.Alias(DOAMSearchConfiguration.LiveIndexAlias).Index("*"))
                    .Add(a => a.Alias(DOAMSearchConfiguration.LiveIndexAlias).Index(CurrentIndexName));
            });

            var oldIndices = Client.GetIndicesPointingToAlias(DOAMSearchConfiguration.OldIndexAlias)
                .OrderByDescending(name => name)
                .Skip(2);

            foreach (var oldIndex in oldIndices)
                Client.Indices.Delete(oldIndex);
        }

        static IEnumerable<AssetDocument> GetAssetDocuments()
        {
            using (DOAMDbContext db = new DOAMDbContext())
            {
                foreach (var asset in db.Assets)
                {
                    var assetDocument = new AssetDocument()
                    {
                        AssetId = asset.AssetId,
                        Name = asset.Name,
                        Type = new MimeTypeDocument()
                        {
                            MimeTypeId = asset.MimeType.MimeTypeId,
                            Name = asset.MimeType.Name,
                            Template = asset.MimeType.Template,
                            FileExtension = asset.MimeType.FileExtension,
                            AssetType = asset.MimeType.AssetType.TypeLabel,
                            AssetTypeImage = asset.MimeType.AssetType.ImageUrl
                        },
                        Description = asset.Description,
                        ImageUrl = asset.ImageUrl,
                        DateAdded = asset.DateAdded,
                        Tags = new List<TagDocument>(),
                        Metadatas = new List<MetadataDocument>()
                    };

                    var assetTags = asset.AssetTags.Where(at => at.Tag.Status).Select(at => at.Tag);

                    foreach (var tag in assetTags)
                    {
                        assetDocument.Tags.Add(new TagDocument()
                        {
                            TagId = tag.TagId,
                            Label = tag.Label
                        });
                    }

                    var assetMetadatas = asset.AssetMetadatas;

                    foreach (var metadata in assetMetadatas)
                    {
                        assetDocument.Metadatas.Add(new MetadataDocument()
                        {
                            MetadataId = metadata.MetadataId,
                            Name = metadata.Metadata.Name,
                            MetadataGroup = metadata.Metadata.MetadataGroup.Name,
                            Value = metadata.Value
                        });
                    }

                    yield return assetDocument;
                }
            }
        }
    }
}
