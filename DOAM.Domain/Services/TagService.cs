using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DOAM.Infrastructure.DB;
using System.Threading.Tasks;
using DOAM.Infrastructure.Repositories;
using DOAM.Infrastructure.Elasticsearch.Services;

namespace DOAM.Domain.Services
{
    public class TagService : EntityService<Tag>
    {
        private readonly ElasticsearchServiceAgent _elastisearchServiceAgent;

        public TagService()
        {
            _elastisearchServiceAgent = new ElasticsearchServiceAgent();
        }

        public IEnumerable<Tag> GetFullTags()
        {
            using (UnitOfWork<Tag> unitOfWork = new UnitOfWork<Tag>())
            {
                return unitOfWork.Tags.GetAll();
            }

        }

        public void UpdateTag(int id, Tag tag)
        {
            List<Asset> assets;

            using (UnitOfWork<Tag> unitOfWork = new UnitOfWork<Tag>())
            {
                var tagInDb = unitOfWork.Tags.Get(id);
                tagInDb.Label = tag.Label;
                tagInDb.Status = tag.Status;

                unitOfWork.Complete();

                assets = unitOfWork.Assets.GetFullAssetsForGivenTag(id).ToList();
            }

            if (assets.Count > 0)
                _elastisearchServiceAgent.UpdateAssetDocuments(assets);
        }

        public void DeleteTag(int id)
        {
            List<Asset> assets;
            using (UnitOfWork<Tag> unitOfWork = new UnitOfWork<Tag>())
            {
                var tag = unitOfWork.Tags.Get(id);
                assets = unitOfWork.Assets.GetFullAssetsForGivenTag(id).ToList();
                if (tag != null)
                {
                    unitOfWork.Tags.Remove(tag);
                    unitOfWork.Complete();
                }
            }

            if (assets.Count > 0)
            {
                assets.ForEach(asset => 
                {
                    if (asset.AssetTags.Any(at => at.TagId == id))
                    {
                        var tagToRemove = asset.AssetTags.SingleOrDefault(at => at.TagId == id);
                        asset.AssetTags.Remove(tagToRemove);
                    }
                });
                _elastisearchServiceAgent.UpdateAssetDocuments(assets);
            }
        }
    }
}
