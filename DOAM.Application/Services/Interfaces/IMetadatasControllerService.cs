using System;
using System.Collections.Generic;
using System.Linq;
using DOAM.Application.ViewModels.Metadatas;

using System.Text;
using System.Threading.Tasks;
using DOAM.Infrastructure.DB;

namespace DOAM.Application.Services.Interfaces
{
    public interface IMetadatasControllerService
    {
        IndexViewModel GetIndexViewModel();
        FormViewModel GetMetadataFormViewModel(int? id);

        bool CreateMetadata(FormViewModel formViewModel);
        bool UpdateMetadata(FormViewModel formViewModel);

        void DeleteMetadata(int id);
    }
}
