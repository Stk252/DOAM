using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Dtos
{
    public class AssetDto
    {
        public int AssetId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public MimeTypeDto MimeType { get; set; }
        public List<AssetMetadataDto> AssetMetadatas { get; set; }
        public List<AssetTagDto> AssetTags { get; set; }
    }
}
