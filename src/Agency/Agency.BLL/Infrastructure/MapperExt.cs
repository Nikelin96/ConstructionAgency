namespace Agency.BLL.Infrastructure
{
    using System.Collections.Generic;
    using AutoMapper;

    internal static class MapperExt
    {
        public static List<TDestination> MapToList<TSource, TDestination>(this IMapper mapper,
            IList<TSource> source)
        {
            List<TDestination> mappedList = mapper.Map<IList<TSource>, List<TDestination>>(source);

            return mappedList;
        }
    }
}