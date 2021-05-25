using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork<TEntity> : IDisposable
        where TEntity : class
    {
        IAssetIssueRepository AssetIssues { get; }
        IAssetMetadataRepository AssetMetadatas { get; }
        IAssetRepository Assets { get; }
        IAssetSuggestionRepository AssetSuggestions { get; }
        IAssetTagRepository AssetTags { get; }
        IAssetTypeRepository AssetTypes { get; }
        IAssetTypeSupportedMetadataRepository AssetTypeSupportedMetadatas { get; }
        IMetadataGroupRepository MetadataGroups { get; }
        IMetadataRepository Metadatas { get; }
        IMimeTypeRepository MimeTypes { get; }
        ITagRepository Tags { get; }
        IUserRepository Users { get; }
        IRepository<TEntity> Entities { get; }
        int Complete();
    }
}
