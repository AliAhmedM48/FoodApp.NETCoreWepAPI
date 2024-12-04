using Food.App.Core.Enums;

namespace Food.App.Core.ViewModels.Response;

public class SuccessResponseViewModel<T> : BaseResponseViewModel<T>
{
    public SuccessResponseViewModel(SuccessCode successCode, T data)
    {
        IsSuccess = true;
        SuccessCode = successCode;
        Data = data;
        Message = ResponseMessage.Success(successCode);
    }
}
