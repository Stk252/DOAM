using DOAM.Domain.Models;
using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Domain.Services
{
    public class AssetTypeSupportedMetadataService : EntityService<AssetTypeSupportedMetadata>
    {

        public AssetTypeSupportedMetadataService()
        {
        }


        public IEnumerable<int> GetAssetTypeSupportedMetadasIdsForGivenAssetType(int id)
        {
            using (UnitOfWork<AssetTypeSupportedMetadata> unitOfWork = new UnitOfWork<AssetTypeSupportedMetadata>())
            {
                return unitOfWork.AssetTypeSupportedMetadatas.GetAssetTypeSupportedMetadasIdsForGivenAssetType(id);
            }
        }
        

        public List<MetadataWithValue> GetAssetTypeSupportedMetadatasForGivenAssetType(int? assetId, int assetTypeId)
        {
            List<MetadataWithValue> metadatasWithValue = new List<MetadataWithValue>();

            using (UnitOfWork<AssetTypeSupportedMetadata> unitOfWork = new UnitOfWork<AssetTypeSupportedMetadata>())
            {
                var supportedMetadatas = unitOfWork.AssetTypeSupportedMetadatas.GetFullAssetTypeSupportedMetadasForGivenAssetType(assetTypeId).OrderBy(atsm => atsm.MetadataId).ToList();
                var assetMetadatas = assetId.HasValue ? unitOfWork.AssetMetadatas.GetAssetMetadatasForGivenAsset(assetId.Value).ToList() : new List<AssetMetadata>();
                foreach (var supportedMetadata in supportedMetadatas)
                {
                    if (assetMetadatas.Any(am => am.MetadataId == supportedMetadata.MetadataId))
                    {
                        var assetMetadata = assetMetadatas.Find(am => am.MetadataId == supportedMetadata.MetadataId);

                        metadatasWithValue.Add(new MetadataWithValue()
                        {
                            Id = assetMetadata.AssetMetadataId,
                            MetadataGroupName = assetMetadata.Metadata.MetadataGroup.Name,
                            MetadataName = assetMetadata.Metadata.Name,
                            AssetId = assetMetadata.AssetId,
                            MetadataId = assetMetadata.MetadataId,
                            Value = assetMetadata.Value
                        });
                    }
                    else
                    {
                        metadatasWithValue.Add(new MetadataWithValue()
                        {
                            Id = 0,
                            MetadataGroupName = supportedMetadata.Metadata.MetadataGroup.Name,
                            MetadataName = supportedMetadata.Metadata.Name,
                            AssetId = assetId ?? 0,
                            MetadataId = supportedMetadata.Metadata.MetadataId,
                            Value = ""
                        });
                    }

                }
            }
            return metadatasWithValue;
        }
    }
}
