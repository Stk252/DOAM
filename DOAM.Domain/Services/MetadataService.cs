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
    public class MetadataService : EntityService<Metadata>
    {
        private readonly ElasticsearchServiceAgent _elastisearchServiceAgent;

        public MetadataService()
        {
            _elastisearchServiceAgent = new ElasticsearchServiceAgent();
        }

        public IEnumerable<Metadata> GetFullMetadatas()
        {
            using (UnitOfWork<Metadata> unitOfWork = new UnitOfWork<Metadata>())
            {
                return unitOfWork.Metadatas.GetFullMetadatas();
            }

        }


        public void UpdateMetadata(int id, Metadata metadata)
        {
            List<Asset> assets;

            using (UnitOfWork<Metadata> unitOfWork = new UnitOfWork<Metadata>())
            {
                var metadataInDb = unitOfWork.Metadatas.Get(id);
                metadataInDb.Name = metadata.Name;
                metadataInDb.Description = metadata.Description;
                metadataInDb.MetadataGroupId = metadata.MetadataGroupId;

                unitOfWork.Complete();

                assets = unitOfWork.Assets.GetFullAssetsForGivenMetadata(id).ToList();
            }

            if (assets.Count > 0)
                _elastisearchServiceAgent.UpdateAssetDocuments(assets);
        }

        public void DeleteMetadata(int id)
        {
            List<Asset> assets;
            using (UnitOfWork<Metadata> unitOfWork = new UnitOfWork<Metadata>())
            {
                var metadata = unitOfWork.Metadatas.Get(id);
                assets = unitOfWork.Assets.GetFullAssetsForGivenMetadata(id).ToList();
                if (metadata != null)
                {
                    unitOfWork.Metadatas.Remove(metadata);
                    unitOfWork.Complete();
                }
            }

            if (assets.Count > 0)
            {
                assets.ForEach(asset =>
                {
                    if (asset.AssetMetadatas.Any(am => am.MetadataId == id))
                    {
                        var metadataToRemove = asset.AssetMetadatas.SingleOrDefault(am => am.MetadataId == id);
                        asset.AssetMetadatas.Remove(metadataToRemove);
                    }
                });
                _elastisearchServiceAgent.UpdateAssetDocuments(assets);
            }
        }
    }
}
