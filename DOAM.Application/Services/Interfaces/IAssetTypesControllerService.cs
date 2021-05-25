using DOAM.Application.ViewModels.AssetTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Services.Interfaces
{
    public interface IAssetTypesControllerService
    {
        IndexViewModel GetIndexViewModel();
        FormViewModel GetAssetTypeFormViewModel(int? id);
        bool CreateAssetType(FormViewModel formViewModel);
        bool UpdateAssetType(FormViewModel formViewModel);

        void DeleteAssetType(int id);
    }
}
