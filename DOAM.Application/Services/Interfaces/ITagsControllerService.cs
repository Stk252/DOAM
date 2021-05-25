using DOAM.Application.ViewModels.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Services.Interfaces
{
    public interface ITagsControllerService
    {
        IndexViewModel GetIndexViewModel();
        FormViewModel GetTagFormViewModel(int? id);

        bool CreateTag(FormViewModel formViewModel);
        bool UpdateTag(FormViewModel formViewModel);

        void DeleteTag(int id);
    }
}
