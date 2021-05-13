using System;

namespace IndeedIQ.Common.Util.AutoMapper
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class MapFromAttribute : Attribute
    {
        public Type Origin { get; private set; }

        public bool Reverse { get; private set; }

        public MapFromAttribute(Type origin) => this.Origin = origin;
    }
}
