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
    
    public partial class MimeType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MimeType()
        {
            this.Assets = new HashSet<Asset>();
        }
    
        public int MimeTypeId { get; set; }
        public string Name { get; set; }
        public string Template { get; set; }
        public string FileExtension { get; set; }
        public int AssetTypeId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual AssetType AssetType { get; set; }
    }
}