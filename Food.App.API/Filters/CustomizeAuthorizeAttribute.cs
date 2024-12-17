using AutoMapper.Features;
using Food.App.Core.Enums;
using Food.App.Service.RoleFeatureService;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Food.App.API.Filters;
public class CustomizeAuthorizeAttribute :ActionFilterAttribute
{
    private readonly IRoleFeatureService _roleFeatureService;
    Feature _feature;
    public CustomizeAuthorizeAttribute(Feature feature,IRoleFeatureService roleFeatureService)
    {
        _roleFeatureService = roleFeatureService;
        _feature = feature;
    }
    public override async void OnActionExecuted(ActionExecutedContext context)
    {
        var claims = context.HttpContext.User;
        var roleId = claims.FindFirst(ClaimTypes.Role);
        if (roleId == null || string.IsNullOrEmpty(roleId.Value))
        {
            throw new UnauthorizedAccessException();
        }
        var role = (Role)int.Parse(roleId.Value);
        if (!await _roleFeatureService.HasAcess(role, _feature))
        {
            throw new UnauthorizedAccessException();

        }
    }

}
