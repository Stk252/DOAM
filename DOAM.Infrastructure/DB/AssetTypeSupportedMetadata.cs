//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class AssetTypeSupportedMetadata
    {
        public int AssetTypeSupportedMetadataId { get; set; }
        public int AssetTypeId { get; set; }
        public int MetadataId { get; set; }
    
        public virtual AssetType AssetType { get; set; }
        public virtual Metadata Metadata { get; set; }
    }
}
