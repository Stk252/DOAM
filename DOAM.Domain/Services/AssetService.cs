using DOAM.Infrastructure.DB;
using DOAM.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOAM.Infrastructure.Elasticsearch.Services;
using DOAM.Domain.Models;

namespace DOAM.Domain.Services
{
    public class AssetService : EntityService<Asset>
    {
        private readonly ElasticsearchServiceAgent elasticsearchServiceAgent;

        public AssetService()
        {
            elasticsearchServiceAgent = new ElasticsearchServiceAgent();
        }


        public IEnumerable<Asset> GetFullAssets()
        {
            using (UnitOfWork<Asset> unitOfWork = new UnitOfWork<Asset>())
            {
                return unitOfWork.Assets.GetFullAssets().ToList();
            }

        }

        public IEnumerable<Asset> GetLatestAssets(int count)
        {
            using (UnitOfWork<Asset> unitOfWork = new UnitOfWork<Asset>())
            {
                return unitOfWork.Assets.GetLatestAssets(count).ToList();
            }

        }

        public Asset GetFullAsset(int id)
        {
            using (UnitOfWork<Asset> unitOfWork = new UnitOfWork<Asset>())
            {
                return unitOfWork.Assets.GetFullAsset(id);
            }

        }

        public IEnumerable<int> GetAssetTagIdsForGivenAsset(int id)
        {
            using (UnitOfWork<AssetTag> unitOfWork = new UnitOfWork<AssetTag>())
            {
                return unitOfWork.AssetTags.GetAssetTagsIdsForGivenAsset(id);
            }
        }


        public void AddAsset(Asset asset, List<int> assetTagIds, List<MetadataWithValue> metadatasWithValue)
        {
            Asset fullAssetToAdd;

            if (assetTagIds != null)
            {
                foreach (var tagId in assetTagIds)
                {
                    asset.AssetTags.Add(new AssetTag() 
                    {
                        AssetId = asset.AssetId,
                        TagId = tagId
                    });
                }
            }

            foreach (var metadata in metadatasWithValue)
            {
                if (metadata.Value != null)
                {
                    asset.AssetMetadatas.Add(new AssetMetadata()
                    {
                        AssetId = asset.AssetId,
                        MetadataId = metadata.MetadataId,
                        Value = metadata.Value,
                    });
                }
            }

            asset.DateAdded = DateTime.Now;

            using (UnitOfWork<Asset> unitOfWork = new UnitOfWork<Asset>())
            {
                unitOfWork.Assets.Add(asset);

                unitOfWork.Complete();
                fullAssetToAdd = unitOfWork.Assets.GetFullAsset(asset.AssetId);
            }

            elasticsearchServiceAgent.CreateOrUpdtateAssetDocument(fullAssetToAdd);
        }

        public void UpdateAsset(int id, Asset asset, List<int> assetTagIds, List<MetadataWithValue> metadatasWithValue)
        {
            Asset assetToUpdate;
            List<AssetTag> assetTags = new List<AssetTag>();
            List<AssetMetadata> assetMetadatas = new List<AssetMetadata>();

            if (assetTagIds != null)
            {
                assetTagIds.ForEach(tagId => 
                { 
                    assetTags.Add(new AssetTag() 
                    {
                        AssetId = id,
                        TagId = tagId
                    });
                
                });
            }

            metadatasWithValue.ForEach(metadata => 
            { 
                if (metadata.Value != null)
                {
                    assetMetadatas.Add(new AssetMetadata()
                    {
                        AssetId = id,
                        MetadataId = metadata.MetadataId,
                        Value = metadata.Value
                    });
                }
            }); 

            using (UnitOfWork<Asset> unitOfWork = new UnitOfWork<Asset>())
            {
                var assetInDb = unitOfWork.Assets.GetFullAsset(id);
                assetInDb.Name = asset.Name;
                assetInDb.Description = asset.Description;
                assetInDb.Url = asset.Url;
                assetInDb.ImageUrl = asset.ImageUrl;
                assetInDb.MimeTypeId = asset.MimeTypeId;

                var assetTagsToAdd = assetTags.ToArray();
                var assetTagsToRemove = assetInDb.AssetTags.ToArray();
                
                var metadatasToAdd = assetMetadatas.ToArray();
                var metadatasToRemove = assetInDb.AssetMetadatas.ToArray();


                unitOfWork.AssetTags.RemoveRange(assetTagsToRemove);
                unitOfWork.AssetTags.AddRange(assetTagsToAdd);

                unitOfWork.AssetMetadatas.RemoveRange(metadatasToRemove);
                unitOfWork.AssetMetadatas.AddRange(metadatasToAdd);

                unitOfWork.Complete();

                assetToUpdate = unitOfWork.Assets.GetFullAsset(id);
            }

            elasticsearchServiceAgent.CreateOrUpdtateAssetDocument(assetToUpdate);
        }
        

        public void DeleteAsset(int id)
        {
            using (UnitOfWork<Asset> unitOfWork = new UnitOfWork<Asset>())
            {
                var asset = unitOfWork.Assets.Get(id);

                if (asset != null)
                {
                    unitOfWork.Assets.Remove(asset);
                    unitOfWork.Complete();

                    elasticsearchServiceAgent.DeleteAssetDocument(id);
                }
            }

            elasticsearchServiceAgent.DeleteAssetDocument(id);
        }
    }
}
