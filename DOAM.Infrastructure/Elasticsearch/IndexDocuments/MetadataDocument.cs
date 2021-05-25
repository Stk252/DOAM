using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace DOAM.Infrastructure.Elasticsearch.IndexDocuments
{
    [ElasticsearchType(IdProperty = nameof(MetadataId))]
    public class MetadataDocument
    {
        [Keyword(Name = nameof(MetadataId))]
        public int MetadataId { get; set; }

        public string Name { get; set; }

        public string MetadataGroup { get; set; }

        public string Value { get; set; }
    }
}
