using DOAM.Application.Services.Interfaces;
using DOAM.Application.Services.Validation;
using DOAM.Application.ViewModels.Tags;
using DOAM.Domain.Services;
using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAM.Application.Services
{
    public class TagsControllerService : ITagsControllerService
    {
        private readonly TagService _tagService;
        private readonly IValidationDictionary _validatonDictionary;
        private readonly Validate _validate;


        public TagsControllerService(IValidationDictionary validationDictionary)
        {
            _tagService = new TagService();
            _validatonDictionary = validationDictionary;
            _validate = new Validate(_validatonDictionary);
        }
        public IndexViewModel GetIndexViewModel()
        {
            List<Tag> tags = _tagService.GetFullTags().ToList();

            if (tags.Count == 0) return null;

            var indexViewModel = new IndexViewModel()
            {
                Tags = tags
            };

            return indexViewModel;
        }

        public FormViewModel GetTagFormViewModel(int? id)
        {
            var vm = new FormViewModel()
            {
                Tag = id.HasValue ? _tagService.Get(id.Value) : new Tag(),
            };

            return vm;
        }

        public bool CreateTag(FormViewModel formViewModel)
        {
            if (!_validate.ValidateTag(formViewModel.Tag, _tagService.GetAll()))
                return false;

            _tagService.Add(formViewModel.Tag);
            return true;
        }

        public bool UpdateTag(FormViewModel formViewModel)
        {
            if (!_validate.ValidateTag(formViewModel.Tag, _tagService.GetAll()))
                return false;

            _tagService.UpdateTag(formViewModel.Tag.TagId, formViewModel.Tag);
            return true;
        }

        public void DeleteTag(int id)
        {
            _tagService.DeleteTag(id);
        }


    }
}
