using DOAM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Domain.Services.Elasticsearch
{
    public static class ExtensionMethods
    {
        public static string ToQueryString(this object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             let value = WebUtility.UrlEncode(p.GetValue(obj, null).ToString())
                             where !string.IsNullOrEmpty(value)
                             select p.Name.ToLowerInvariant() + "=" + value;

            return "?" + string.Join("&", properties.ToArray());
        }

        public static string ToQueryString(this SearchForm form)
        {
            Func<string, string> u = WebUtility.UrlEncode;

            var properties = new List<string>();
            if (form.Page != SearchForm.DefaultPage) properties.Add($"page={form.Page}");
            if (form.PageSize != SearchForm.DefaultPageSize) properties.Add($"pagesize={form.PageSize}");
            if (form.Sort != SearchForm.DefaultSort)
                properties.Add($"sort={u(form.Sort.ToString().ToLowerInvariant())}");
            if (!string.IsNullOrEmpty(form.Query)) properties.Add($"query={u(form.Query)}");
            if (!string.IsNullOrEmpty(form.MimeType)) properties.Add($"MimeType={u(form.MimeType)}");
            if (!string.IsNullOrEmpty(form.AssetType)) properties.Add($"AssetType={u(form.AssetType)}");
            if (form.Tags != null && form.Tags.Length > 0)
                properties.AddRange(form.Tags.Select(t => $"tags={u(t)}"));

            return "?" + string.Join("&", properties.ToArray());
        }
    }
}
