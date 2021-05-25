using DOAM.Application.Services.Interfaces;
using DOAM.Application.Services.Validation;
using DOAM.Application.ViewModels.AssetTypes;
using DOAM.Domain.Services;
using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DOAM.Application.Services
{
    public class AssetTypesControllerService : IAssetTypesControllerService
    {
        private readonly AssetTypeService _assetTypeService;
        private readonly MetadataGroupService _metadataGroupService;
        private readonly AssetTypeSupportedMetadataService _assetTypeSupportedMetadataService;
        private readonly IValidationDictionary _validatonDictionary;
        private readonly Validate _validate;

        public AssetTypesControllerService(IValidationDictionary validationDictionary)
        {
            _assetTypeService = new AssetTypeService();
            _metadataGroupService = new MetadataGroupService();
            _assetTypeSupportedMetadataService = new AssetTypeSupportedMetadataService();
            _validatonDictionary = validationDictionary;
            _validate = new Validate(_validatonDictionary);
        }

        public IndexViewModel GetIndexViewModel()
        {
            List<AssetType> assetTypes = _assetTypeService.GetAll().ToList();

            if (assetTypes.Count == 0) return null;

            var indexViewModel = new IndexViewModel()
            {
                AssetTypes = assetTypes
            };

            return indexViewModel;
        }

        public FormViewModel GetAssetTypeFormViewModel(int? id)
        {
            var vm = new FormViewModel();
            vm.AssetType = id.HasValue ? _assetTypeService.Get(id.Value) : new AssetType();

            vm.Metadatas = GetMetadatasSelectListItemsList();

            vm.AssetTypeSupportedMetadatas = id.HasValue ? _assetTypeSupportedMetadataService.GetAssetTypeSupportedMetadasIdsForGivenAssetType(id.Value).ToList() : new List<int>();

            return vm;
        }

        private List<SelectListItem> GetMetadatasSelectListItemsList() {

            var selectListItemsList = new List<SelectListItem>();
            var metadataGroups = _metadataGroupService.GetFullMetadataGroups().ToList();
            foreach (var metadataGroup in metadataGroups)
            {
                var optionGroup = new SelectListGroup() { Name = metadataGroup.Name };
                foreach (var metadata in metadataGroup.Metadatas)
                {
                    selectListItemsList.Add(new SelectListItem()
                    {
                        Value = metadata.MetadataId.ToString(),
                        Text = $"{metadata.Name} | {metadataGroup.Name}",
                        Group = optionGroup
                    });
                }
            }

            return selectListItemsList;
        }
        
        public bool CreateAssetType(FormViewModel formViewModel)
        {
            if (!_validate.ValidateAssetType(formViewModel.AssetType, _assetTypeService.GetAll()))
            {
                formViewModel.Metadatas = GetMetadatasSelectListItemsList();
                return false;
            }

            _assetTypeService.AddAssetType(formViewModel.AssetType, formViewModel.AssetTypeSupportedMetadatas);

            return true;
        }


        public bool UpdateAssetType(FormViewModel formViewModel)
        {
            if (!_validate.ValidateAssetType(formViewModel.AssetType, _assetTypeService.GetAll()))
            {
                formViewModel.Metadatas = GetMetadatasSelectListItemsList();
                return false;
            }

            _assetTypeService.UpdateAssetType(formViewModel.AssetType.AssetTypeId, formViewModel.AssetType, formViewModel.AssetTypeSupportedMetadatas);

            return true;
        }
        

        public void DeleteAssetType(int id)
        {
            _assetTypeService.DeleteAssetType(id);
        }
    }
}
