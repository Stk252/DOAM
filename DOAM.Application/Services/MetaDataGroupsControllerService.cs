using System;
using System.Collections.Generic;
using System.Linq;
using DOAM.Application.ViewModels.MetadataGroups;
using DOAM.Infrastructure.DB;
using DOAM.Domain.Services;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DOAM.Application.Dtos;
using DOAM.Application.Services.Validation;
using DOAM.Application.Services.Interfaces;
using AutoMapper;

namespace DOAM.Application.Services
{
    public class MetadataGroupsControllerService : IMetadataGroupsControllerService
    {
        private readonly MetadataGroupService _metadataGroupService;
        private readonly IValidationDictionary _validatonDictionary;
        private readonly Validate _validate;


        public MetadataGroupsControllerService(IValidationDictionary validationDictionary)
        {
            _metadataGroupService = new MetadataGroupService();
            _validatonDictionary = validationDictionary;
            _validate = new Validate(_validatonDictionary);
        }


        public IndexViewModel GetIndexViewModel()
        {
            List<MetadataGroup> metadataGroups = _metadataGroupService.GetAll().ToList();

            if (metadataGroups.Count == 0) return null;

            var indexViewModel = new IndexViewModel()
            {
                MetadataGroups = metadataGroups
            };

            return indexViewModel;
        }


        public FormViewModel GetMetadataGroupFormViewModel(int? id)
        {
            var vm = new FormViewModel()
            {
                MetadataGroup = id.HasValue ? _metadataGroupService.Get(id.Value) : new MetadataGroup()
            };

            return vm;
        }


        

        public bool CreateMetadataGroup(FormViewModel formViewModel)
        {
            if (!_validate.ValidateMetadataGroup(formViewModel.MetadataGroup, _metadataGroupService.GetAll()))
                return false;

            _metadataGroupService.Add(formViewModel.MetadataGroup);
            return true;
        }

        public bool UpdateMetadataGroup(FormViewModel formViewModel)
        {
            if (!_validate.ValidateMetadataGroup(formViewModel.MetadataGroup, _metadataGroupService.GetAll()))
                return false;

            _metadataGroupService.UpdateMetadataGroup(formViewModel.MetadataGroup.MetadataGroupId, formViewModel.MetadataGroup);
            return true;
        }


        public void DeleteMetadataGroup(int id)
        {
             _metadataGroupService.DeleteMetadataGroup(id);
        }

        
    }
}
