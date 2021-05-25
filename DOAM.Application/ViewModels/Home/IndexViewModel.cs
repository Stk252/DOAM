using DOAM.Domain.Models;
using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.ViewModels.Home
{
    public class IndexViewModel
    {
        public List<Asset> LatestAssets { get; set; }

        /// <summary>
        /// The current state of the form that was submitted
        /// </summary>
        public SearchForm Form { get; set; }
    }
}
