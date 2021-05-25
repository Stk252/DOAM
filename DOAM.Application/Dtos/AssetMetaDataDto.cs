using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Dtos
{
    public class AssetMetadataDto
    {
        public int AssetMetadataId { get; set; }
        public MetadataDto metadata { get; set; }
        public int AssetId { get; set; }
        public string Value { get; set; }
    }
}
