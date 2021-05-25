//using DOAM.Application.ViewModels.Search;
using DOAM.Domain.Models;
using DOAM.Infrastructure.Elasticsearch;
using DOAM.Infrastructure.Elasticsearch.IndexDocuments;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Domain.Services.Elasticsearch
{
    public class ElasticsearchSearchAgent
    {
        private readonly IElasticClient _client;

        public ElasticsearchSearchAgent()
        {
            _client = DOAMSearchConfiguration.GetClient();
        }

        public ISearchResponse<AssetDocument> GetSearchResult(SearchForm form)
        {
            var result = _client.Search<AssetDocument>(s => s
                    .From((form.Page - 1) * form.PageSize)
                    .Size(form.PageSize)
                    .Sort(sort =>
                    {
                        if (form.Sort == SearchSort.Name)
                            return sort.Ascending(a => a.Name.Suffix("keyword"));
                        if (form.Sort == SearchSort.Type)
                            return sort.Ascending(a => a.Type.AssetType.Suffix("keyword"));
                        return sort.Descending(a => a.DateAdded);
                    })
                    .Aggregations(a => a
                        .Nested("Type", n => n
                            .Path(p => p.Type)
                            .Aggregations(aa => aa
                                .Terms("AssetType", ts => ts
                                    .Field(p => p.Type.AssetType.Suffix("keyword"))
                                )
                            )
                        )
                        .Nested("Tags", n => n
                            .Path(p => p.Tags)
                            .Aggregations(aa => aa
                                .Terms("Labels", ts => ts
                                    .Field(p => p.Tags.First().Label.Suffix("keyword"))
                                )
                            )
                        )
                    )
                    .Query(q => q
                        .QueryString(qs => qs
                            .Query(form.Query)
                        )
                        && +q.Nested(n => n
                            .Path(p => p.Type)
                            .Query(nq => +nq
                                .Term(p => p.Type.AssetType.Suffix("keyword"), form.AssetType)
                            )
                        )
                        && +q.Nested(n => n
                            .Path(p => p.Tags)
                            .Query(nq => +nq
                                .Terms(t => t
                                    .Field(f => f.Tags.First().Label.Suffix("keyword"))
                                    .Terms(form.Tags)
                                )
                            )
                        )
                    )

                );

            return result;
        }
    }
}
