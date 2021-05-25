using DOAM.Domain.Services.Elasticsearch;
using DOAM.Domain.Models;
using DOAM.Infrastructure.Elasticsearch.IndexDocuments;
using Nest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.ViewModels.Search
{
    public class SearchViewModel
    {
        private static readonly ReadOnlyCollection<IHit<AssetDocument>> EmptyHits =
            new ReadOnlyCollection<IHit<AssetDocument>>(new List<IHit<AssetDocument>>());

        /// <summary>
        /// The current state of the form that was submitted
        /// </summary>
        public SearchForm Form { get; set; }

        /// <summary>
        /// The total number of matching results
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// The total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// The current page of asset results
        /// </summary>
        public IReadOnlyCollection<IHit<AssetDocument>> Hits { get; set; }

        /// <summary>
        /// Returns how long the elasticsearch query took in milliseconds
        /// </summary>
        public int ElapsedMilliseconds { get; set; }

        /// <summary>
        /// Returns the top tags for the curren search
        /// </summary>
        public Dictionary<string, long> Tags { get; set; }

        /// <summary>
        /// Returns the top mime types with the most assets
        /// </summary>
        public Dictionary<string, long?> MimeTypes { get; set; }

        /// <summary>
        /// Returns the top asset types with the most assets
        /// </summary>
        public Dictionary<string, long?> AssetTypes { get; set; }

        public SearchViewModel()
        {
            this.Hits = EmptyHits;
            this.Form = new SearchForm();
            this.Tags = new Dictionary<string, long>();
            this.MimeTypes = new Dictionary<string, long?>();
            this.AssetTypes = new Dictionary<string, long?>();
        }

        public string UrlForFirstPage(Action<SearchForm> alter) => this.UrlFor(form =>
        {
            alter(form);
            form.Page = 1;
        });

        public string UrlFor(Action<SearchForm> alter)
        {
            var clone = this.Form.Clone();
            alter(clone);
            return clone.ToQueryString();
        }
    }
}
