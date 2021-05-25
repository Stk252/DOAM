using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DOAM.Infrastructure.DB;
using System.Threading.Tasks;
using DOAM.Infrastructure.Repositories;
using System.IO;
using Microsoft.Owin.Security.OAuth;
using DOAM.Infrastructure.Elasticsearch.Services;

namespace DOAM.Domain.Services
{
    public class AssetTypeService : EntityService<AssetType>
    {
        private readonly ElasticsearchServiceAgent _elasticsearchServiceAgent;

        public AssetTypeService()
        {
            _elasticsearchServiceAgent = new ElasticsearchServiceAgent();
        }

        public void AddAssetType(AssetType assetType, List<int> supportedMetadatas)
        {
            if (supportedMetadatas != null)
                supportedMetadatas.ForEach(supportedMetadataId =>
                {
                    assetType.AssetTypeSupportedMetadatas.Add(new AssetTypeSupportedMetadata()
                    {
                        AssetTypeId = assetType.AssetTypeId,
                        MetadataId = supportedMetadataId
                    });
                });

            using (UnitOfWork<AssetType> unitOfWork = new UnitOfWork<AssetType>())
            {
                unitOfWork.AssetTypes.Add(assetType);
                unitOfWork.Complete();
            }
        }

        public void UpdateAssetType(int id, AssetType assetType, List<int> supportedMetadatas)
        {
            List<Asset> assets;

            var assetTypeSupportedMetadatas = new List<AssetTypeSupportedMetadata>();
            if (supportedMetadatas != null)
                supportedMetadatas.ForEach(supportedMetadataId =>
                {
                    assetTypeSupportedMetadatas.Add(new AssetTypeSupportedMetadata()
                    {
                        AssetTypeId = assetType.AssetTypeId,
                        MetadataId = supportedMetadataId
                    });
                });

            using (UnitOfWork<AssetType> unitOfWork = new UnitOfWork<AssetType>())
            {
                var assetTypeInDb = unitOfWork.AssetTypes.Get(id);
                assetTypeInDb.TypeLabel = assetType.TypeLabel;
                assetTypeInDb.TypeDescription = assetType.TypeDescription;
                assetTypeInDb.ImageUrl = assetType.ImageUrl;

                var supportedMetadatasToAdd = assetTypeSupportedMetadatas.Where(sm => !assetTypeInDb.AssetTypeSupportedMetadatas.Any(smdb => smdb.MetadataId == sm.MetadataId)).ToArray();
                var supportedMetadatasToRemove = assetTypeInDb.AssetTypeSupportedMetadatas.Where(smdb => !assetTypeSupportedMetadatas.Any(sm => sm.MetadataId == smdb.MetadataId)).ToArray();

                unitOfWork.AssetTypeSupportedMetadatas.RemoveRange(supportedMetadatasToRemove);
                unitOfWork.AssetTypeSupportedMetadatas.AddRange(supportedMetadatasToAdd);

                unitOfWork.Complete();

                assets = unitOfWork.Assets.GetFullAssetsForGivenAssetType(assetTypeInDb.AssetTypeId).ToList();
            }

            if (assets.Count > 0)
                _elasticsearchServiceAgent.UpdateAssetDocuments(assets);
        }

        public void DeleteAssetType(int id)
        {
            List<int> assetIds;
            using (UnitOfWork<AssetType> unitOfWork = new UnitOfWork<AssetType>())
            {
                var assetType = unitOfWork.AssetTypes.Get(id);
                assetIds = unitOfWork.Assets.GetAssetIdsForAssetType(id).ToList();
                if (assetType != null)
                {
                    unitOfWork.AssetTypes.Remove(assetType);
                    unitOfWork.Complete();
                }
            }

            if (assetIds.Count > 0)
                _elasticsearchServiceAgent.DeleteAssetDocuments(assetIds);
        }

        public IEnumerable<AssetType> GetFullAssetTypes()
        {
            using (UnitOfWork<AssetType> unitOfWork = new UnitOfWork<AssetType>())
            {
                return unitOfWork.AssetTypes.GetFullAssetTypes();
            }
        }
    }
}
