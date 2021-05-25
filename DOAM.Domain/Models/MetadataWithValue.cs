using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Domain.Models
{
    public class MetadataWithValue
    {
        public int Id { get; set; }
        public string MetadataGroupName { get; set; }
        public string MetadataName { get; set; }
        public int MetadataId { get; set; }
        public int AssetId { get; set; }
        public string Value { get; set; }
    }
}
