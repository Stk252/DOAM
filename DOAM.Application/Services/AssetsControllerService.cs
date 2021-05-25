using AutoMapper;
using DOAM.Application.Dtos;
using DOAM.Application.Services.Interfaces;
using DOAM.Application.Services.Validation;
using DOAM.Application.ViewModels.Assets;
using DOAM.Application.ViewModels.Search;
using DOAM.Domain.Models;
using DOAM.Domain.Services;
using DOAM.Domain.Services.Elasticsearch;
using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SearchForm = DOAM.Domain.Models.SearchForm;

namespace DOAM.Application.Services
{
    public class AssetsControllerService : IAssetsControllerService
    {
        private readonly AssetService _assetService;
        private readonly AssetTypeSupportedMetadataService _assetTypeSupportedMetadataService;
        private readonly TagService _tagService;
        private readonly MimeTypeService _mimeTypeService;
        private readonly AssetTypeService _assetTypeService;
        private readonly IValidationDictionary _validatonDictionary;
        private readonly Validate _validate;
        private readonly ElasticsearchSearchAgent _agent;


        public AssetsControllerService(IValidationDictionary validationDictionary)
        {
            _assetService = new AssetService();
            _tagService = new TagService();
            _assetTypeSupportedMetadataService = new AssetTypeSupportedMetadataService();
            _mimeTypeService = new MimeTypeService();
            _assetTypeService = new AssetTypeService();
            _validatonDictionary = validationDictionary;
            _validate = new Validate(_validatonDictionary);
            _agent = new ElasticsearchSearchAgent();
        }


        public IndexViewModel GetIndexViewModel()
        {
            List<Asset> assets = _assetService.GetFullAssets().ToList();

            if (assets.Count == 0) return null;

            var indexViewModel = new IndexViewModel()
            {
                Assets = assets
            };

            return indexViewModel;
        }

        public SearchViewModel GetSearchViewModel(SearchForm form)
        {
            var result = _agent.GetSearchResult(form);

            var assetTypes = result.Aggregations.Nested("Type")
                .Terms("AssetType")
                .Buckets
                .ToDictionary(k => k.Key, v => v.DocCount);


            var tags = result.Aggregations.Nested("Tags")
                .Terms("Labels")
                .Buckets
                .ToDictionary(k => k.Key, v => v.DocCount ?? 0);


            var searchViewModel = new SearchViewModel()
            {
                Hits = result.Hits,
                Total = result.Total,
                Form = form,
                TotalPages = (int)Math.Ceiling(result.Total / (double)form.PageSize),
                AssetTypes = assetTypes,
                Tags = tags
            };

            return searchViewModel;
        }


        public FormViewModel GetAssetFormViewModel(int? id)
        {
            var vm = new FormViewModel();
            vm.Asset = id.HasValue ? _assetService.GetFullAsset(id.Value) : new Asset();

            vm.MimeTypes = GetMimeTypesSelectListItemsList();

            vm.AssetTagIds = id.HasValue ? _assetService.GetAssetTagIdsForGivenAsset(id.Value).ToList() : new List<int>();
            vm.Tags = GetTagsSelectListItemsList();

            return vm;
        }

        public AssetMetadatasViewModel GetAssetMetadatasViewModel(int? assetId, int mimeTypeId)
        {
            var vm = new AssetMetadatasViewModel();

            var mimeType = _mimeTypeService.Get(mimeTypeId);

            vm.MetadatasWithValue = _assetTypeSupportedMetadataService.GetAssetTypeSupportedMetadatasForGivenAssetType(assetId, mimeType.AssetTypeId);
            vm.MetadataGroupNames = new List<string>();

            foreach(var metadataGroupName in vm.MetadatasWithValue.Select(atsm => atsm.MetadataGroupName).Distinct())
            {
                vm.MetadataGroupNames.Add(metadataGroupName);
            }

            return vm;
        }
        

        private List<SelectListItem> GetMimeTypesSelectListItemsList()
        {

            var selectListItemsList = new List<SelectListItem>();
            var assetTypes = _assetTypeService.GetFullAssetTypes().ToList();
            foreach (var assetType in assetTypes)
            {
                var optionGroup = new SelectListGroup() { Name = assetType.TypeLabel };
                foreach (var mimeType in assetType.MimeTypes)
                {
                    selectListItemsList.Add(new SelectListItem()
                    {
                        Value = mimeType.MimeTypeId.ToString(),
                        Text = $"{mimeType.Name}  |  {mimeType.Template}",
                        Group = optionGroup
                    });

                }
            }

            return selectListItemsList;
        }

        private List<SelectListItem> GetTagsSelectListItemsList()
        {

            var selectListItemsList = new List<SelectListItem>();
            var tags = _tagService.GetFullTags().ToList();
            foreach (var tag in tags)
            {
                if (tag.Status)
                {
                    selectListItemsList.Add(new SelectListItem()
                    {
                        Value = tag.TagId.ToString(),
                        Text = tag.Label
                    });
                }
            }

            return selectListItemsList;
        }


        public bool CreateAsset(FormViewModel formViewModel, AssetMetadatasViewModel assetMetadatasViewModel)
        {
            if (!_validate.ValidateAsset(formViewModel.Asset))
            {
                formViewModel.MimeTypes = GetMimeTypesSelectListItemsList();
                formViewModel.Tags = GetTagsSelectListItemsList();
                return false;
            }

            _assetService.AddAsset(formViewModel.Asset, formViewModel.AssetTagIds, assetMetadatasViewModel.MetadatasWithValue);

            return true;
        }

        public bool UpdateAsset(FormViewModel formViewModel, AssetMetadatasViewModel assetMetadatasViewModel)
        {
            if (!_validate.ValidateAsset(formViewModel.Asset))
            {
                formViewModel.MimeTypes = GetMimeTypesSelectListItemsList();
                formViewModel.Tags = GetTagsSelectListItemsList();
                return false;
            }

            _assetService.UpdateAsset(formViewModel.Asset.AssetId, formViewModel.Asset, formViewModel.AssetTagIds, assetMetadatasViewModel.MetadatasWithValue);

            return true;
        }


        public void DeleteAsset(int id)
        {
            _assetService.DeleteAsset(id);
        }


        public DetailsViewModel GetDetailsViewModel(int id)
        {
            var asset = _assetService.GetFullAsset(id);
            var vm = new DetailsViewModel
            {
                Asset = asset
            };

            return vm;
        }

        public IssueViewModel GetIssueViewModel(int? id)
        {
            throw new NotImplementedException();
        }

        public SuggestionViewModel GetSuggestionViewModel(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
