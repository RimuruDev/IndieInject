using System;

namespace destructive_code.ExtensionSystem
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DisallowMultipleExtensionsAttribute : Attribute
    {
        
    }
}