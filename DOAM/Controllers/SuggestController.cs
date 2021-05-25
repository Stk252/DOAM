using DOAM.Application.ViewModels.Search;
using DOAM.Domain.Models;
using DOAM.Infrastructure.Elasticsearch;
using DOAM.Infrastructure.Elasticsearch.IndexDocuments;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DOAM.Controllers
{
    public class SuggestController : Controller
    {
        private readonly IElasticClient _client;

        public SuggestController() => _client = DOAMSearchConfiguration.GetClient();


        [System.Web.Mvc.HttpPost]
        public JsonResult Index([FromBody]SearchForm form)
        {
            var result = _client.Search<AssetDocument>(s => s
                .Index<AssetDocument>()
                .Source(sf => sf
                    .Includes(f => f
                        .Field(ff => ff.AssetId)
                        .Field(ff => ff.Name)
                        .Field(ff => ff.ImageUrl)
                        .Field(ff => ff.Type.Name)
                        .Field(ff => ff.Type.AssetType)
                        .Field(ff => ff.Type.AssetTypeImage)
                        .Field(ff => ff.Tags)
                    )
                )
                .Suggest(su => su
                    .Completion("asset-suggestions", c => c
                        .Prefix(form.Query)
                        .Field(a => a.Suggest)
                    )
                )
            );


            var suggestions = result.Suggest["asset-suggestions"]
                .FirstOrDefault()
                .Options
                .Select(suggest => new 
                {
                    name = suggest.Source.Name,
                    assetId = suggest.Source.AssetId,
                    imageUrl = suggest.Source.ImageUrl,
                    assetType = suggest.Source.Type.AssetType,
                    assetTypeImage = suggest.Source.Type.AssetTypeImage,
                    mimeType = suggest.Source.Type.Name,
                    tags = suggest.Source.Tags
                });



            return Json(suggestions);
        }
    }
}