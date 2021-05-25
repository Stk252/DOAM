using DOAM.Application.ViewModels.MimeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Services.Interfaces
{
    public interface IMimeTypesControllerService
    {
        IndexViewModel GetIndexViewModel();
        FormViewModel GetMimeTypeFormViewModel(int? id);

        bool CreateMimeType(FormViewModel formViewModel);
        bool UpdateMimeType(FormViewModel formViewModel);

        void DeleteMimeType(int id);
    }
}
