using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace DOAM.Infrastructure.Elasticsearch.IndexDocuments
{
    [ElasticsearchType(IdProperty = nameof(AssetId))]
    public class AssetDocument
    {
        [Keyword(Name = nameof(AssetId))]
        public int AssetId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [Nested(Name = nameof(Type), IncludeInRoot = true)]
        public MimeTypeDocument Type { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        [Nested(Name = nameof(Tags), IncludeInRoot = true)]
        public List<TagDocument> Tags { get; set; }

        [Nested(Name = nameof(Metadatas), IncludeInRoot = true)]
        public List<MetadataDocument> Metadatas { get; set; }

        [Completion(Name = nameof(Suggest))]
        public CompletionField Suggest 
        {
            get
            {
                List<string> suggestions = new List<string>();
                suggestions.AddRange(new List<string>(Name.Split(' ')) { Name });
                return new CompletionField()
                {
                    Input = suggestions
                };
            }
            set { } 
        }

        public AssetDocument()
        {
            Tags = new List<TagDocument>();
            Metadatas = new List<MetadataDocument>();
        }
    }
}
