using System;
using System.Reflection;

namespace ElementsLib.Module.Common
{
    public static class GeneralClazzFactory<T>
    {
        public static T InstanceFor(Assembly assembly, string namespacePattern, string className, string clazzName)
        {
            string instanceClass = ClassNameFor(namespacePattern, className);

            Type instanceType = assembly.GetType(instanceClass);

            if (instanceType == null && clazzName != "")
            {
                instanceClass = ClassNameFor(namespacePattern, clazzName);

                instanceType = assembly.GetType(instanceClass);
            }

            if (instanceType == null)
            {
                throw new InvalidOperationException("Class does't exist: " + instanceClass);
            }

            T instance = (T)Activator.CreateInstance(instanceType);
            if (instance == null)
            {
                throw new InvalidOperationException("Instance wasn't created: " + instanceClass);
            }
            return instance;
        }


        public static string ClassNameFor(string namespacePrefix, string className)
        {
            string fullClassName = string.Join(".", namespacePrefix, className);

            return fullClassName;
        }
    }
}
