using DOAM.Application.Dtos;
using DOAM.Application.ViewModels.Assets;
using DOAM.Domain.Models;
using DOAM.Application.ViewModels.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Services.Interfaces
{
    public interface IAssetsControllerService
    {
        IndexViewModel GetIndexViewModel();

        SearchViewModel GetSearchViewModel(Domain.Models.SearchForm form);

        DetailsViewModel GetDetailsViewModel(int id);
        FormViewModel GetAssetFormViewModel(int? id);

        AssetMetadatasViewModel GetAssetMetadatasViewModel(int? assetId, int mimeTypeId);

        IssueViewModel GetIssueViewModel(int? id);
        SuggestionViewModel GetSuggestionViewModel(int? id);

        bool CreateAsset(FormViewModel formViewModel, AssetMetadatasViewModel assetMetadatasViewModel);
        bool UpdateAsset(FormViewModel formViewModel, AssetMetadatasViewModel assetMetadatasViewModel);

        void DeleteAsset(int id);
    }
}
