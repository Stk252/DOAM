﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DOAM.Infrastructure.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DOAMDbContext : DbContext
    {
        public DOAMDbContext()
            : base("name=DOAMDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetIssue> AssetIssues { get; set; }
        public virtual DbSet<AssetMetadata> AssetMetadatas { get; set; }
        public virtual DbSet<AssetSuggestion> AssetSuggestions { get; set; }
        public virtual DbSet<AssetTag> AssetTags { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<AssetTypeSupportedMetadata> AssetTypeSupportedMetadatas { get; set; }
        public virtual DbSet<Metadata> Metadatas { get; set; }
        public virtual DbSet<MetadataGroup> MetadataGroups { get; set; }
        public virtual DbSet<MimeType> MimeTypes { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
    }
}
