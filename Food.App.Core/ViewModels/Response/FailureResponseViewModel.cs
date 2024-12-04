using Food.App.Core.Enums;
using Food.App.Core.Validation;

namespace Food.App.Core.ViewModels.Response;

public class FailureResponseViewModel<T> : BaseResponseViewModel<T>
{
    public FailureResponseViewModel(ErrorCode errorCode, IEnumerable<ValidationError> validationErrors)
    {
        IsSuccess = false;
        ErrorCode = errorCode;
        Message = ResponseMessage.Failure(errorCode);
        ValidationErrors = validationErrors;
    }
}
