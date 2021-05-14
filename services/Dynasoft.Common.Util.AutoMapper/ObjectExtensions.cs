using AutoMapper;

namespace Dynasoft.Common.Util.AutoMapper
{
    public static class ObjectExtensions
    {
        internal static IMapper Mapper { private get; set; }

        public static T MapTo<T>(this object source) => Mapper.Map<T>(source);
    }
}
