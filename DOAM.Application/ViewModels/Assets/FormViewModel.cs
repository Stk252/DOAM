using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DOAM.Application.ViewModels.Assets
{
    public class FormViewModel
    {
        public Asset Asset { get; set; }
        public List<SelectListItem> MimeTypes { get; set; }


        public List<int> AssetTagIds { get; set; }
        public List<SelectListItem> Tags { get; set; }

        public List<AssetTypeSupportedMetadata> assetTypeSupportedMetadatas { get; set; }
    }
}
