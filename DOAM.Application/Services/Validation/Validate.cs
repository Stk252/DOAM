using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DOAM.Application.Services.Validation
{
    public class Validate
    {
        private IValidationDictionary _validatonDictionary;

        public Validate(IValidationDictionary validationDictionary)
        {
            _validatonDictionary = validationDictionary;
        }

        public bool ValidateMetadataGroup(MetadataGroup metadataGroup, IEnumerable<MetadataGroup> metadataGroups)
        {
            if (metadataGroup.Name == null || metadataGroup.Name.Trim().Length == 0)
                _validatonDictionary.AddError("MetadataGroup.Name", "Name is required.");
            if (metadataGroup.Name != null && metadataGroup.Name.Length > 100)
                _validatonDictionary.AddError("MetadataGroup.Name", "Name is too long, maximum 100 characters.");
            if (metadataGroup.Description == null || metadataGroup.Description.Trim().Length == 0)
                _validatonDictionary.AddError("MetadataGroup.Description", "Description is required.");
            if (metadataGroup.Name != null && metadataGroup.Description.Length > 500)
                _validatonDictionary.AddError("MetadataGroup.Description", "Description is too long, maximum 500 characters.");
            if (metadataGroups.Any(mdg => mdg.Name.ToLower() == metadataGroup.Name.ToLower() && mdg.MetadataGroupId != metadataGroup.MetadataGroupId))
                _validatonDictionary.AddError("MetadataGroup.Name", "That metadata group exists already.");

            return _validatonDictionary.IsValid;
        }

        public bool ValidateMetadata(Metadata metadata, IEnumerable<Metadata> metadatas)
        {
            if (metadata.Name == null || metadata.Name.Trim().Length == 0)
                _validatonDictionary.AddError("Metadata.Name", "Name is required.");
            if (metadata.Name != null && metadata.Name.Length > 100)
                _validatonDictionary.AddError("Metadata.Name", "Name is too long, maximum 100 characters.");
            if (metadata.Description == null || metadata.Description.Trim().Length == 0)
                _validatonDictionary.AddError("Metadata.Description", "Description is required.");
            if (metadata.Description != null && metadata.Description.Length > 500)
                _validatonDictionary.AddError("Metadata.Description", "Description is too long, maximum 500 characters.");
            if (metadata.MetadataGroupId == 0)
                _validatonDictionary.AddError("Metadata.MetadataGroupId", "Metadata Group is required.");
            if (metadatas.Any(md => md.Name.ToLower() == metadata.Name.ToLower() && md.MetadataGroupId == metadata.MetadataGroupId && md.MetadataId != metadata.MetadataId))
                _validatonDictionary.AddError("Metadata.Name", "That metadata exists already.");

            return _validatonDictionary.IsValid;
        }

        internal bool ValidateAssetType(AssetType assetType, IEnumerable<AssetType> assetTypes)
        {
            if (assetType.TypeLabel == null || assetType.TypeLabel.Trim().Length == 0)
                _validatonDictionary.AddError("AssetType.TypeLabel", "Label is required.");
            if (assetType.TypeLabel != null && assetType.TypeLabel.Length > 100)
                _validatonDictionary.AddError("AssetType.TypeLabel", "Label is too long, maximum 100 characters.");
            if (assetType.TypeDescription == null || assetType.TypeDescription.Trim().Length == 0)
                _validatonDictionary.AddError("AssetType.TypeDescription", "Description is required.");
            if (assetType.TypeDescription != null && assetType.TypeDescription.Length > 500)
                _validatonDictionary.AddError("AssetType.TypeDescription", "Description is too long, maximum 500 characters.");
            if (assetTypes.Any(at => at.TypeLabel.ToLower() == assetType.TypeLabel.ToLower() && at.AssetTypeId != assetType.AssetTypeId))
                _validatonDictionary.AddError("AssetType.TypeLabel", "That asset type exists already.");
            if (assetType.ImageUrl == null || assetType.ImageUrl.Trim().Length == 0)
                _validatonDictionary.AddError("AssetType.ImageUrl", "Image is required.");
            if (assetType.ImageUrl != null && !assetType.ImageUrl.Trim().StartsWith("data:image/"))
                _validatonDictionary.AddError("AssetType.ImageUrl", "Image has the wrong format.");

            return _validatonDictionary.IsValid;
        }

        public bool ValidateTag(Tag tag, IEnumerable<Tag> tags)
        {
            if (tag.Label == null || tag.Label.Trim().Length == 0)
                _validatonDictionary.AddError("Tag.Label", "Label is required.");
            if (tag.Label != null && tag.Label.Length > 50)
                _validatonDictionary.AddError("Tag.Label", "Label is too long, maximum 50 characters.");
            if (tags.Any(t => t.Label.ToLower() == tag.Label.ToLower() && t.TagId != tag.TagId))
                _validatonDictionary.AddError("Tag.Label", "That tag exists already.");

            return _validatonDictionary.IsValid;
        }

        internal bool ValidateAsset(Asset asset)
        {
            if (asset.Name == null || asset.Name.Trim().Length == 0)
                _validatonDictionary.AddError("Asset.Name", "Name is required.");
            if (asset.Name != null && asset.Name.Length > 200)
                _validatonDictionary.AddError("Asset.Name", "Name is too long, maximum 200 characters.");
            if (asset.Url == null || asset.Url.Trim().Length == 0)
                _validatonDictionary.AddError("Asset.Url", "Url is required.");
            if (asset.Description == null || asset.Description.Trim().Length == 0)
                _validatonDictionary.AddError("Asset.Description", "Description is required.");
            if (asset.MimeTypeId == 0)
                _validatonDictionary.AddError("Asset.MimeTypeId", "Mime Type is required.");


            return _validatonDictionary.IsValid;
        }

        internal bool ValidateMimeType(MimeType mimeType, IEnumerable<MimeType> enumerable)
        {
            var templateRegex = new Regex(@"^(application|chemical|audio|font|example|image|message|model|multipart|text|video|x-(?:[0-9A-Za-z!#$%&'*+.^_`|~-]+))/([0-9A-z!#$%&'*+.,^_`|~-]+)([0-9A-z!#$%&'*+.,^_`|~-]+)$");
            var fileExtensionRegex = new Regex(@"^\.[0-9A-Za-z].+$");

            if (mimeType.Name == null || mimeType.Name.Trim().Length == 0)
                _validatonDictionary.AddError("MimeType.Name", "Name is required.");
            if (mimeType.Name != null && mimeType.Name.Length > 100)
                _validatonDictionary.AddError("MimeType.Name", "Name is too long, maximum 100 characters.");
            if (mimeType.Template == null || mimeType.Template.Trim().Length == 0)
                _validatonDictionary.AddError("MimeType.Template", "Template is required.");
            if (mimeType.Template != null && mimeType.Template.Length > 200)
                _validatonDictionary.AddError("MimeType.Template", "Template is too long, maximum 200 characters.");
            if (! templateRegex.IsMatch(mimeType.Template))
                _validatonDictionary.AddError("MimeType.Template", "Template has the wrong format (.../... expected).");
            if (mimeType.FileExtension != null && mimeType.FileExtension.Length > 50)
                _validatonDictionary.AddError("MimeType.FileExtension", "File Extension is too long, maximum 50 characters.");
            if (! fileExtensionRegex.IsMatch(mimeType.FileExtension.Trim()))
                _validatonDictionary.AddError("MimeType.FileExtension", "File Extension has the wrong format (.xxx expected).");
            
            return _validatonDictionary.IsValid;
        }
    }
}
