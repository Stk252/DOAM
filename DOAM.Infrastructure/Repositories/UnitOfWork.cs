using DOAM.Infrastructure.Repositories.Interfaces;
using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Infrastructure.Repositories
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
    {
        private readonly DOAMDbContext _context;

        // Using same context accross all repositories
        public UnitOfWork()
        {
            _context = new DOAMDbContext();
            Entities = new Repository<TEntity>(_context);
            AssetIssues = new AssetIssueRepository(_context);
            AssetMetadatas = new AssetMetadataRepository(_context);
            Assets = new AssetRepository(_context);
            AssetSuggestions = new AssetSuggestionRepository(_context);
            AssetTags = new AssetTagRepository(_context);
            AssetTypes = new AssetTypeRepository(_context);
            AssetTypeSupportedMetadatas = new AssetTypeSupportedMetadataRepository(_context);
            MetadataGroups = new MetadataGroupRepository(_context);
            Metadatas = new MetadataRepository(_context);
            MimeTypes = new MimeTypeRepository(_context);
            Tags = new TagRepository(_context);
            Users = new UserRepository(_context);
        }

        // Implementations of the properties declared in the UnitOfWork interface
        public IAssetIssueRepository AssetIssues { get; private set; }
        public IAssetMetadataRepository AssetMetadatas { get; private set; }
        public IAssetRepository Assets { get; private set; }
        public IAssetSuggestionRepository AssetSuggestions { get; private set; }
        public IAssetTagRepository AssetTags { get; private set; }
        public IAssetTypeRepository AssetTypes { get; private set; }
        public IAssetTypeSupportedMetadataRepository AssetTypeSupportedMetadatas { get; private set; }
        public IMetadataGroupRepository MetadataGroups { get; private set; }
        public IMetadataRepository Metadatas { get; private set; }
        public IMimeTypeRepository MimeTypes { get; private set; }
        public ITagRepository Tags { get; private set; }
        public IUserRepository Users { get; private set; }

        public IRepository<TEntity> Entities { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
