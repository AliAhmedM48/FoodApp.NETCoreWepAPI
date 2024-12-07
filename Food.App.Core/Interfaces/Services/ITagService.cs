using Food.App.Core.ViewModels.Response;
using Food.App.Core.ViewModels.Tags;

namespace Food.App.Core.Interfaces.Services
{
    public interface ITagService
    {
        ResponseViewModel<IEnumerable<TagViewModel>> GetAllTags();
        Task<ResponseViewModel<int>> Create(TagCreateViewModel viewModel);
        Task<ResponseViewModel<bool>> DeleteTag(int tagID);

    }
}
