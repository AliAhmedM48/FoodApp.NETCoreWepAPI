using AutoMapper;

namespace Food.App.Core.MappingProfiles;
public static class MappingExtensions
{
    public static IMapper Mapper;

    public static destination Map<destination>(this object source) => Mapper.Map<destination>(source);

    public static destination? MapToExistingEntity<destination>(this object source, destination existingEntity) => Mapper.Map<object, destination>(source, existingEntity);

    public static IQueryable<destination> ProjectTo<destination>(this IQueryable<object> source) => Mapper.ProjectTo<destination>(source);

    public static destination? ProjectToForFirstOrDefault<destination>(this IQueryable<object> source) => Mapper.ProjectTo<destination>(source).FirstOrDefault();

}
