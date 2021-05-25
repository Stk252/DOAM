using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Repositories;
using DOAM.Infrastructure.Repositories.Interfaces;
using System.Threading.Tasks;
using DOAM.Infrastructure.Elasticsearch.Services;

namespace DOAM.Domain.Services
{
    public class MetadataGroupService :  EntityService<MetadataGroup>
    {
        private readonly ElasticsearchServiceAgent _elastisearchServiceAgent;

        public MetadataGroupService()
        {
            _elastisearchServiceAgent = new ElasticsearchServiceAgent();
        }

        public IEnumerable<MetadataGroup> GetFullMetadataGroups()
        {
            using (UnitOfWork<MetadataGroup> unitOfWork = new UnitOfWork<MetadataGroup>())
            {
                return unitOfWork.MetadataGroups.GetFullMetadataGroups();
            }

        }


        public void UpdateMetadataGroup(int id, MetadataGroup metadataGroup)
        {
            List<Asset> assets;

            using (UnitOfWork<MetadataGroup> unitOfWork = new UnitOfWork<MetadataGroup>())
            {
                var metadataGroupInDb = unitOfWork.MetadataGroups.Get(id);
                metadataGroupInDb.Name = metadataGroup.Name;
                metadataGroupInDb.Description = metadataGroup.Description;

                unitOfWork.Complete();

                assets = unitOfWork.Assets.GetFullAssetsForGivenMetadataGroup(id).ToList();
            }

            if (assets.Count > 0)
                _elastisearchServiceAgent.UpdateAssetDocuments(assets);
        }

        public void DeleteMetadataGroup(int id)
        {
            List<int> assetIds;
            List<Asset> assets;
            using (UnitOfWork<MetadataGroup> unitOfWork = new UnitOfWork<MetadataGroup>())
            {
                var metadataGroup = unitOfWork.MetadataGroups.Get(id);
                assetIds = unitOfWork.Assets.GetAssetIdsForMetadataGroup(id).ToList();
                if (metadataGroup != null)
                {
                    unitOfWork.MetadataGroups.Remove(metadataGroup);
                    unitOfWork.Complete();
                }

                assets = unitOfWork.Assets.GetFullAssetsForGivenIds(assetIds).ToList();
            }

            if (assets.Count > 0)
            {
                _elastisearchServiceAgent.UpdateAssetDocuments(assets);
            }
        }
    }
}
