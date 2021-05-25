using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DOAM.Application.ViewModels.MimeTypes
{
    public class FormViewModel
    {
        public List<SelectListItem> AssetTypes { get; set; }
        public MimeType MimeType { get; set; }
    }
}
