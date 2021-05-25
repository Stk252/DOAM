using DOAM.Application.Services.Interfaces;
using DOAM.Application.ViewModels.Home;
using DOAM.Domain.Constants;
using DOAM.Domain.Models;
using DOAM.Domain.Services;
using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Services
{
    public class HomeControllerService : IHomeControllerService
    {
        private readonly AssetService _assetService;

        public HomeControllerService()
        {
            _assetService = new AssetService();
        }

        public IndexViewModel GetIndexViewModel(int latestAssetsCount)
        {
            List<Asset> latestAssets = _assetService.GetLatestAssets(latestAssetsCount).ToList();


            if (latestAssets.Count == 0) return null;

            var indexViewModel = new IndexViewModel()
            {
                LatestAssets = latestAssets,
                Form = new SearchForm(),
            };

            return indexViewModel;
        }
    }
}
