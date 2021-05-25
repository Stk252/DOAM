using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Domain.Models
{
	public class SearchForm
	{
		public const int DefaultPageSize = 10;
		public const int DefaultPage = 1;
		public const SearchSort DefaultSort = SearchSort.Latest;

		public int Page { get; set; }
		public string Query { get; set; }
		public string MimeType { get; set; }
		public string AssetType { get; set; }
		public string[] Tags { get; set; }
		public int PageSize { get; set; }
		public SearchSort Sort { get; set; }

		public SearchForm()
		{
			this.PageSize = DefaultPageSize;
			this.Page = DefaultPage;
			this.Sort = DefaultSort;
			this.Tags = Array.Empty<string>();
		}

		public SearchForm Clone() => new SearchForm
		{
			Page = this.Page,
			Query = this.Query,
			MimeType = this.MimeType,
			AssetType = this.AssetType,
			Tags = this.Tags,
			PageSize = this.PageSize,
			Sort = this.Sort,
		};
	}
}
