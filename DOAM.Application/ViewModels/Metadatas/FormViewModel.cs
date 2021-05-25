using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DOAM.Application.ViewModels.Metadatas
{
    public class FormViewModel
    {
        public List<SelectListItem> MetadataGroups { get; set; }
        public Metadata Metadata { get; set; }
    }
}
