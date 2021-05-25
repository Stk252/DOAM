using DOAM.Domain.Models;
using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.ViewModels.Assets
{
    public class AssetMetadatasViewModel
    {

        public List<string> MetadataGroupNames { get; set; }
        public List<MetadataWithValue> MetadatasWithValue { get; set; }
    }
}
