using AutoMapper;
using System;
using System.Collections.Generic;
using DOAM.Infrastructure.DB;
using DOAM.Application.Dtos;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Services
{
    public class MappingProfile
    {
        public static IMapper CreateConfig()
        {
            var config = new MapperConfiguration( cfg => 
            {
                // Domain to Dto
                cfg.CreateMap<Asset, AssetDto>()
                    .ForMember(a => a.AssetMetadatas, opt => opt.Ignore())
                    .ReverseMap();
                cfg.CreateMap<AssetIssue, AssetIssueDto>().ReverseMap();
                cfg.CreateMap<AssetMetadata, AssetMetadataDto>().ReverseMap();
                cfg.CreateMap<AssetSuggestion, AssetSuggestionDto>().ReverseMap();
                cfg.CreateMap<AssetTag, AssetTagDto>().ReverseMap();
                cfg.CreateMap<AssetType, AssetTypeDto>()
                    .ForMember(at => at.AssetTypeSupportedMetadatas, opt => opt.Ignore())
                    .ReverseMap();
                cfg.CreateMap<AssetTypeSupportedMetadata, AssetTypeSupportedMetadataDto>().ReverseMap();
                cfg.CreateMap<MimeType, MimeTypeDto>().ReverseMap();
                cfg.CreateMap<Metadata, MetadataDto>()
                    .ForMember(md => md.MetadataGroupName, opt => opt.Equals(opt.DestinationMember.Name))
                    .ReverseMap();
                cfg.CreateMap<MetadataGroup, MetadataGroupDto>()
                    .ReverseMap();
                cfg.CreateMap<Tag, TagDto>().ReverseMap();
            });

            return config.CreateMapper();
        }
    }
}
