using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Dtos
{
    public class MimeTypeDto
    {
        public int MimeTypeId { get; set; }
        public string Name { get; set; }
        public string Template { get; set; }
        public string FileExtension { get; set; }
        public AssetTypeDto AssetType { get; set; }
    }
}
