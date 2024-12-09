namespace Food.App.Core.Enums;

public enum ErrorCode
{
    None = 00,
    ValidationError = 01,
    DataBaseError = 02,

    // AUTH
    ChangePasswordError = 100,

    // USER
    UserNotFound = 200,
    UserNameExist =201,
    UserEmailExist=202,
    UserPhoneExist=203,

    // RECIPE
    RecipeNotFound = 300,
    RecipeAlreadyExist =301,
    // TAG
    TagNotFound = 400,
    TagAlreadyExist = 401,

    // CATEGORY
    CategoryNotFound = 500,
    CategoryWithSameNameFound = 600,

}
