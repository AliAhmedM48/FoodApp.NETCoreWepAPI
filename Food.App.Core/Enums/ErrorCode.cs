namespace Food.App.Core.Enums;

public enum ErrorCode
{
    None = 00,
    ValidationError = 01,

    // AUTH
    ChangePasswordError = 100,

    // USER
    UserNotFound = 200,

    // RECIPE
    RecipeNotFound = 300,

    // TAG
    TagNotFound = 400,

    // CATEGORY
    CategoryNotFound = 500,

}
