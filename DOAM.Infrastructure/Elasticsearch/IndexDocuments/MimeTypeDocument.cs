using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Elasticsearch.IndexDocuments
{
    [ElasticsearchType(IdProperty = nameof(MimeTypeId))]
    public class MimeTypeDocument
    {
        [Keyword(Name = nameof(MimeTypeId))]
        public int MimeTypeId { get; set; }

        public string Name { get; set; }

        public string Template { get; set; }

        public string FileExtension { get; set; }

        public string AssetType { get; set; }

        public string AssetTypeImage { get; set; }
    }
}
