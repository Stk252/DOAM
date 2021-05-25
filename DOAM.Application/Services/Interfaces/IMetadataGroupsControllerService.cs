using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOAM.Application.ViewModels.MetadataGroups;
using DOAM.Infrastructure.DB;
using System.Threading.Tasks;
using DOAM.Application.Dtos;

namespace DOAM.Application.Services.Interfaces
{
    public interface IMetadataGroupsControllerService
    {
        IndexViewModel GetIndexViewModel();
        FormViewModel GetMetadataGroupFormViewModel(int? id);
        bool CreateMetadataGroup(FormViewModel formViewModel);
        bool UpdateMetadataGroup(FormViewModel formViewModel);

        void DeleteMetadataGroup(int id);
    }
}
