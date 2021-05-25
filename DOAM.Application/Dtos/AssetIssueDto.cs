using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Dtos
{
    public class AssetIssueDto
    {
        public int AssetIssueId { get; set; }
        public AssetDto Asset { get; set; }
        public UserDto RaisedBy { get; set; }
        public DateTime DateRaised { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }
    }
}
