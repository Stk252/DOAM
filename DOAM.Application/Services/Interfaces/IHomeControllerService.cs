using DOAM.Application.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Services.Interfaces
{
    public interface IHomeControllerService
    {
        IndexViewModel GetIndexViewModel(int latestAssetsCount);
    }
}
