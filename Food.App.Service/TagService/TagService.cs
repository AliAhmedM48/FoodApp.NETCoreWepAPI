using AutoMapper.QueryableExtensions;
using Food.App.Core.Entities;
using Food.App.Core.Enums;
using Food.App.Core.Interfaces;
using Food.App.Core.Interfaces.Services;
using Food.App.Core.MappingProfiles;
using Food.App.Core.ViewModels.Response;
using Food.App.Core.ViewModels.Tags;

namespace Food.App.Service
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Tag> _tagRepository;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tagRepository = unitOfWork.GetRepository<Tag>();

        }

        public async Task<ResponseViewModel<int>> Create(TagCreateViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new FailureResponseViewModel<int>(ErrorCode.ValidationError);
            }

            var isExist = await _tagRepository.AnyAsync(e=>e.Name== viewModel.Name);
            if (isExist) 
            { 
                return new FailureResponseViewModel<int>(ErrorCode.TagAlreadyExist);
            }

            var tag = viewModel.Map<Tag>();
            await _tagRepository.AddAsync(tag);
            var isSaved = await _unitOfWork.SaveChangesAsync();
            if (isSaved == 0) {
                return new FailureResponseViewModel<int>(ErrorCode.DataBaseError);
            }
            return new SuccessResponseViewModel<int>(SuccessCode.TagCreated);

        }

        public ResponseViewModel<IEnumerable<TagViewModel>> GetAllTags()
        {
            var tags = _tagRepository.GetAll();
            var tagsViewModel = tags.ProjectTo<TagViewModel>().ToList();
            var response = new SuccessResponseViewModel<IEnumerable<TagViewModel>>(SuccessCode.TagsRetrieved , tagsViewModel);
            return response;
        }

        public async Task<ResponseViewModel<bool>> DeleteTag(int tagID)
        {
            var isExist = await _tagRepository.AnyAsync(e=>e.Id==tagID && !e.IsDeleted);
            if (!isExist) 
            {
                return new FailureResponseViewModel<bool>(ErrorCode.TagNotFound);
            }

            var tag = new Tag { Id = tagID };
            _tagRepository.Delete(tag);
            var isSaved = await _unitOfWork.SaveChangesAsync();
            if (isSaved == 0)
            {
                return new FailureResponseViewModel<bool>(ErrorCode.DataBaseError);
            }
            return new SuccessResponseViewModel<bool>(SuccessCode.TagDeleted);
        }

        public  async Task<ResponseViewModel<bool>> UpdateTag(TagUpdateViewModel viewModel)
        {
            
            var isExist = await _tagRepository.AnyAsync(e => e.Id == viewModel.Id && !e.IsDeleted);
            if (!isExist)
            {
                return new FailureResponseViewModel<bool>(ErrorCode.TagNotFound);
            }
            var tag = viewModel.Map<Tag>();
            _tagRepository.SaveInclude(tag, t=>t.Name);
            var isSaved = await _unitOfWork.SaveChangesAsync();
            if (isSaved == 0)
            {
                return new FailureResponseViewModel<bool>(ErrorCode.DataBaseError);
            }
            return new SuccessResponseViewModel<bool>(SuccessCode.TagUpdated);
        }
    }
}
