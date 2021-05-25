using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Elasticsearch.IndexDocuments;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Elasticsearch.Services
{
    public class ElasticsearchServiceAgent
    {
        private static ElasticClient Client { get; set; }
        private static string CurrentIndexName { get; set; }

        public ElasticsearchServiceAgent()
        {
            Client = DOAMSearchConfiguration.GetClient();
            CurrentIndexName = DOAMSearchConfiguration.CreateIndexName();
        }
        public void CreateOrUpdtateAssetDocument(Asset asset)
        {
            var assetDocument = MakeAssetDocument(asset);

            Client.IndexDocument(assetDocument);
        }

        public void UpdateAssetDocuments(List<Asset> assets)
        {
            List<AssetDocument> assetDocuments = new List<AssetDocument>();
            if (assets != null && assets.Count > 0)
                assets.ForEach(asset => assetDocuments.Add(MakeAssetDocument(asset)));

            Client.IndexMany(assetDocuments);
        }

        public void DeleteAssetDocuments(List<int> assetIds)
        {
            // TODO Bulk Delete
            if (assetIds != null && assetIds.Count > 0)
                assetIds.ForEach(id => DeleteAssetDocument(id));

        }

        public void DeleteAssetDocument(int id)
        {
            Client.Delete<AssetDocument>(id);
        }



        public AssetDocument MakeAssetDocument(Asset asset)
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

            return assetDocument;
        }
    }
}
