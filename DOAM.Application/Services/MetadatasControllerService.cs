using DOAM.Application.Services.Validation;
using DOAM.Application.Services.Interfaces;
using DOAM.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOAM.Infrastructure.DB;
using DOAM.Application.ViewModels.Metadatas;
using System.Web.Mvc;

namespace DOAM.Application.Services
{
    public class MetadatasControllerService : IMetadatasControllerService
    {
        private readonly MetadataService _metadataService;
        private readonly MetadataGroupService _metadataGroupService;
        private readonly IValidationDictionary _validatonDictionary;
        private readonly Validate _validate;


        public MetadatasControllerService(IValidationDictionary validationDictionary)
        {
            _metadataService = new MetadataService();
            _metadataGroupService = new MetadataGroupService();
            _validatonDictionary = validationDictionary;
            _validate = new Validate(_validatonDictionary);
        }

        public IndexViewModel GetIndexViewModel()
        {
            List<Metadata> metadatas = _metadataService.GetFullMetadatas().ToList();

            if (metadatas.Count == 0) return null;

            var indexViewModel = new IndexViewModel()
            {
                Metadatas = metadatas
            };

            return indexViewModel;
        }


        public FormViewModel GetMetadataFormViewModel(int? id)
        {
            var vm = new FormViewModel();
            vm.Metadata = id.HasValue ? _metadataService.Get(id.Value) : new Metadata();
            vm.MetadataGroups = GetMetadataGroupsSelectListItemsList();

            return vm;
        }

        private List<SelectListItem> GetMetadataGroupsSelectListItemsList()
        {
            var selectListItem = new List<SelectListItem>();
            var metadataGroups = _metadataGroupService.GetAll();

            foreach (var metadataGroup in metadataGroups)
            {
                selectListItem.Add(new SelectListItem() 
                {
                    Value = metadataGroup.MetadataGroupId.ToString(),
                    Text = metadataGroup.Name
                });
            }

            return selectListItem;
        }

        public bool CreateMetadata(FormViewModel formViewModel)
        {
            if (!_validate.ValidateMetadata(formViewModel.Metadata, _metadataService.GetAll()))
            {
                formViewModel.MetadataGroups = GetMetadataGroupsSelectListItemsList();
                return false;
            }

            _metadataService.Add(formViewModel.Metadata);
            return true;
        }

        public bool UpdateMetadata(FormViewModel formViewModel)
        {
            if (!_validate.ValidateMetadata(formViewModel.Metadata, _metadataService.GetAll()))
            {
                formViewModel.MetadataGroups = GetMetadataGroupsSelectListItemsList();
                return false;
            }

            _metadataService.UpdateMetadata(formViewModel.Metadata.MetadataId, formViewModel.Metadata);
            return true;
        }

        public void DeleteMetadata(int id)
        {
            _metadataService.DeleteMetadata(id);
        }


    }
}
