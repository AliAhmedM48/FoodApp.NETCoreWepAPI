namespace Food.App.Core.Enums;

public enum SuccessCode
{
    None = 00,
    OperationCompleted = 01,

    // AUTH
    ChangePasswordUpdated = 100,

    // USER
    UserDeleted = 202,
    UsersRetrieved = 203,
    UserDetailsRetrieved = 204,

    // RECIPE
    RecipeCreated = 300,
    RecipeUpdated = 301,
    RecipeDeleted = 302,
    RecipesRetrieved = 303,
    RecipeDetailsRetrieved = 304,

    // TAG
    TagCreated = 400,
    TagUpdated = 401,
    TagDeleted = 402,
    TagsRetrieved = 403,
    TagDetailsRetrieved = 404,

    // CATEGORY
    CategoryCreated = 500,
    CategoryUpdated = 501,
    CategoryDeleted = 502,
    CategoriesRetrieved = 503,
    CategoryDetailsRetrieved = 504,
}