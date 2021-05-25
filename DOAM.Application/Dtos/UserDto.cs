using System.Collections.Generic;

namespace DOAM.Application.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<AssetIssueDto> RaisedIssues { get; set; }
        public List<AssetSuggestionDto> SuggestedAssets { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}