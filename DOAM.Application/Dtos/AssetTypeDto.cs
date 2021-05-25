using System.Collections.Generic;

namespace DOAM.Application.Dtos
{
    public class AssetTypeDto
    {
        public int AssetTypeId { get; set; }
        public string TypeLabel { get; set; }
        public string TypeDescription { get; set; }
        public List<AssetTypeSupportedMetadataDto> AssetTypeSupportedMetadatas { get; set; }
        public string ImageUrl { get; set; }
    }
}