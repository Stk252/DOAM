using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DOAM.Application.ViewModels.AssetTypes
{
    public class FormViewModel
    {
        public AssetType AssetType { get; set; }
        public List<int> AssetTypeSupportedMetadatas { get; set; }
        public List<SelectListItem> Metadatas { get; set; }
    }
}
