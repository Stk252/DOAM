using DOAM.Application.Services.Interfaces;
using DOAM.Application.Services.Validation;
using DOAM.Application.ViewModels.MimeTypes;
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
    public class MimeTypesControllerService : IMimeTypesControllerService
    {
        private readonly MimeTypeService _mimeTypeService;
        private readonly AssetTypeService _assetTypeService;
        private readonly IValidationDictionary _validatonDictionary;
        private readonly Validate _validate;


        public MimeTypesControllerService(IValidationDictionary validationDictionary)
        {
            _mimeTypeService = new MimeTypeService();
            _assetTypeService = new AssetTypeService();
            _validatonDictionary = validationDictionary;
            _validate = new Validate(_validatonDictionary);
        }

        public IndexViewModel GetIndexViewModel()
        {
            List<MimeType> mimeTypes = _mimeTypeService.GetFullMimeTypes().ToList();

            if (mimeTypes.Count == 0) return null;

            var indexViewModel = new IndexViewModel()
            {
                MimeTypes = mimeTypes
            };

            return indexViewModel;
        }

        public FormViewModel GetMimeTypeFormViewModel(int? id)
        {
            var vm = new FormViewModel();

            vm.MimeType = id.HasValue ? _mimeTypeService.Get(id.Value) : new MimeType();
            vm.AssetTypes = GetAssetTypesSelectListItemsList();

            return vm;
        }

        private List<SelectListItem> GetAssetTypesSelectListItemsList()
        {
            var selectListItem = new List<SelectListItem>();
            var assetTypes = _assetTypeService.GetAll();

            foreach (var assetType in assetTypes)
            {
                selectListItem.Add(new SelectListItem()
                {
                    Value = assetType.AssetTypeId.ToString(),
                    Text = assetType.TypeLabel
                });
            }

            return selectListItem;
        }

        public bool CreateMimeType(FormViewModel formViewModel)
        {
            if (!_validate.ValidateMimeType(formViewModel.MimeType, _mimeTypeService.GetAll()))
            {
                formViewModel.AssetTypes = GetAssetTypesSelectListItemsList();
                return false;
            }

            _mimeTypeService.Add(formViewModel.MimeType);
            return true;
        }
        public bool UpdateMimeType(FormViewModel formViewModel)
        {
            if (!_validate.ValidateMimeType(formViewModel.MimeType, _mimeTypeService.GetAll()))
            {
                formViewModel.AssetTypes = GetAssetTypesSelectListItemsList();
                return false;
            }

            _mimeTypeService.UpdateMimeType(formViewModel.MimeType.MimeTypeId, formViewModel.MimeType);
            return true;
        }

        public void DeleteMimeType(int id)
        {
            _mimeTypeService.DeleteMimeType(id);
        }

    }
}
