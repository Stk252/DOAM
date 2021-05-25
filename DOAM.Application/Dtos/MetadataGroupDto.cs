using System.Collections.Generic;

namespace DOAM.Application.Dtos
{
    public class MetadataGroupDto
    {
        public int MetadataGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MetadataDto> metadatas { get; set; }
    }
}