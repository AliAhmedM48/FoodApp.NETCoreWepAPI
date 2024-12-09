using Food.App.Core.Enums;

namespace Food.App.Core.ViewModels.Response;
public static class ResponseMessage
{
    private static readonly Dictionary<SuccessCode, string> SuccessMessages = new()
    {
        // AUTH
        { SuccessCode.ChangePasswordUpdated, "Password changed successfully." },

        // USER
        { SuccessCode.UserCreated, "User created successfully." },
        { SuccessCode.UsersRetrieved, "Users retrieved successfully." },
        { SuccessCode.UserDetailsRetrieved, "User details retrieved successfully." },
        { SuccessCode.UserDeleted, "Course deleted successfully."  },

        // RECIPE
        { SuccessCode.RecipesRetrieved, "Recipes retrieved successfully." },
        { SuccessCode.RecipeDetailsRetrieved, "Recipe details retrieved successfully." },
        { SuccessCode.RecipeCreated, "Recipe created successfully."  },
        { SuccessCode.RecipeUpdated, "Recipe updated successfully."  },
        { SuccessCode.RecipeDeleted, "Recipe deleted successfully."  },

        // TAG
        { SuccessCode.TagsRetrieved, "Tags retrieved successfully." },
        { SuccessCode.TagDetailsRetrieved, "Tag details retrieved successfully." },
        { SuccessCode.TagCreated, "Tag created successfully."  },
        { SuccessCode.TagUpdated, "Tag updated successfully."  },
        { SuccessCode.TagDeleted, "Tag deleted successfully."  },

        // CATEGORY
        { SuccessCode.CategoriesRetrieved, "Categories retrieved successfully." },
        { SuccessCode.CategoryDetailsRetrieved, "Category details retrieved successfully." },
        { SuccessCode.CategoryCreated, "Category created successfully."  },
        { SuccessCode.CategoryUpdated, "Category updated successfully."  },
        { SuccessCode.CategoryDeleted, "Category deleted successfully."  },

    };

    private static readonly Dictionary<ErrorCode, string> ErrorMessages = new()
    {
         { ErrorCode.DataBaseError, "A database error occurred while processing the request. Please try again later or contact support if the issue persists." },

        { ErrorCode.ValidationError, "Validation failed: One or more fields contain invalid values. Please review the errors and try again." },

        // USER
        { ErrorCode.UserNotFound, "The specified user could not be found." },
        { ErrorCode.UserNameExist, "The specified username is already in use." },
        { ErrorCode.UserEmailExist, "The specified Email is already in use." },
        { ErrorCode.UserPhoneExist, "The specified Phone is already in use." },
        

        // RECIPE
        { ErrorCode.RecipeNotFound, "The specified recipe could not be found." },
        { ErrorCode.RecipeAlreadyExist, "Recipe Already Exist with the same name" },


        // TAG
        { ErrorCode.TagNotFound, "The specified tag could not be found." },
        { ErrorCode.TagAlreadyExist, "The specified tag Already exist." },

        // CATEGORY
        { ErrorCode.CategoryNotFound, "The specified category could not be found." },

    };


    public static string Success(SuccessCode code)
    {
        return SuccessMessages.TryGetValue(code, out string? message) ?
            message : "Operation completed successfully.";
    }

    public static string Failure(ErrorCode code)
    {
        return ErrorMessages.TryGetValue(code, out string? message) ?
            message : "An unexpected error occurred. Please contact support.";
    }
}
