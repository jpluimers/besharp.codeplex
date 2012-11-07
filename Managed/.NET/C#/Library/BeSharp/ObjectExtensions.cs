using System;

namespace BeSharp
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// People coming from a Delphi background will be extremely happy with this: 
        /// Call Free on any object instance, and it will automatically call the IDisposable.Dispose() method
        /// when applicable.
        /// Other users will also be happy: when a class starts implementing IDisposable in the future, this works.
        /// If a class stops implementing IDisposable in the future, it also still works.
        /// Magic isn't it? (:
        /// </summary>
        /// <param name="value">Object instance to check for IDisposable interface.</param>
        public static void Free(this object value)
        {
            IDisposable disposable = value as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}