using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Dtos
{
    public class AssetSuggestionDto
    {
        public int AssetSuggestionId { get; set; }
        public string SuggestedAssetUrl { get; set; }
        public string Description { get; set; }
        public DateTime DateSuggested { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }
        public UserDto User { get; set; }
    }
}
