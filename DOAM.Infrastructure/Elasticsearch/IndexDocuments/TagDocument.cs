using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace DOAM.Infrastructure.Elasticsearch.IndexDocuments
{
    [ElasticsearchType(IdProperty = nameof(TagId))]
    public class TagDocument
    {
        [Keyword(Name = nameof(TagId))]
        public int TagId { get; set; }
        public string Label { get; set; }
    }
}
