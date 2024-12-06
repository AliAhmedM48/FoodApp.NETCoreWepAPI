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
        public ResponseViewModel<IEnumerable<TagViewModel>> GetAllTags()
        {
            var tags = _tagRepository.GetAll();
            var tagsViewModel = tags.ProjectTo<TagViewModel>().ToList();
            var response = new SuccessResponseViewModel<IEnumerable<TagViewModel>>(SuccessCode.TagsRetrieved , tagsViewModel);
            return response;
        }
    }
}
