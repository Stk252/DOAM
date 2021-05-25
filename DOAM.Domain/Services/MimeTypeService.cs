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
    public class MimeTypeService : EntityService<MimeType>
    {
        private readonly ElasticsearchServiceAgent _elastisearchServiceAgent;

        public MimeTypeService()
        {
            _elastisearchServiceAgent = new ElasticsearchServiceAgent();
        }

        public IEnumerable<MimeType> GetFullMimeTypes()
        {
            using (UnitOfWork<MimeType> unitOfWork = new UnitOfWork<MimeType>())
            {
                return unitOfWork.MimeTypes.GetFullMimeTypes();
            }

        }


        public void UpdateMimeType(int id, MimeType mimeType)
        {
            List<Asset> assets;

            using (UnitOfWork<MimeType> unitOfWork = new UnitOfWork<MimeType>())
            {
                var mimeTypeInDb = unitOfWork.MimeTypes.Get(id);
                mimeTypeInDb.Name = mimeType.Name;
                mimeTypeInDb.Template = mimeType.Template;
                mimeTypeInDb.FileExtension = mimeType.FileExtension;
                mimeTypeInDb.AssetTypeId = mimeType.AssetTypeId;

                unitOfWork.Complete();

                assets = unitOfWork.Assets.GetFullAssetsForGivenMimeType(mimeTypeInDb.MimeTypeId).ToList();
            }

            if (assets.Count > 0)
                _elastisearchServiceAgent.UpdateAssetDocuments(assets);
        }

        public void DeleteMimeType(int id)
        {
            List<int> assetIds;
            using (UnitOfWork<MimeType> unitOfWork = new UnitOfWork<MimeType>())
            {
                var mimeType = unitOfWork.MimeTypes.Get(id);
                assetIds = unitOfWork.Assets.GetAssetIdsForMimeType(id).ToList();
                if (mimeType != null)
                {
                    unitOfWork.MimeTypes.Remove(mimeType);
                    unitOfWork.Complete();
                }
            }

            if (assetIds.Count > 0)
                _elastisearchServiceAgent.DeleteAssetDocuments(assetIds);
        }
    }
}
