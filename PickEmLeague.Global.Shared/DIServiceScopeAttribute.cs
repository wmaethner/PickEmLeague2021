using System;
namespace PickEmLeague.Global.Shared
{
    public class DIServiceScopeAttribute : Attribute
    {
        public Type InterfaceType { get; set; }
        public Type ImplementationType { get; set; }
        public ServiceScope ServiceScope { get; set; }

        public DIServiceScopeAttribute(Type interfaceType, Type implementedType, ServiceScope serviceScope = ServiceScope.Scoped)
        {
            InterfaceType = interfaceType;
            ImplementationType = implementedType;
            ServiceScope = serviceScope;
        }
    }

    public enum ServiceScope
    {
        Scoped,
        Transient,
        Singleton
    }
}
