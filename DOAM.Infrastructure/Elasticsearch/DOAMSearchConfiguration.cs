using DOAM.Infrastructure.Elasticsearch.IndexDocuments;
using Nest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Elasticsearch
{
    public static class DOAMSearchConfiguration
    {
        const string assetsIndexName = "assets-kummert-santiago";
        const string elasticAddress = "http://localhost:9200";


        public static ElasticClient GetClient() => new ElasticClient(_connectionSettings);

        static DOAMSearchConfiguration()
        {
            _connectionSettings = new ConnectionSettings(new Uri(elasticAddress))
                .DefaultIndex(assetsIndexName)
                .DefaultMappingFor<AssetDocument>(i => i.IndexName(assetsIndexName))
                .PrettyJson();
        }

        private static readonly ConnectionSettings _connectionSettings;

        public static string LiveIndexAlias => assetsIndexName;
        public static string OldIndexAlias => $"{assetsIndexName}-old";

        public static string CreateIndexName() => $"{LiveIndexAlias}-{DateTime.UtcNow:dd-MM-yyyy-HH-mm-ss}";
    }
}
