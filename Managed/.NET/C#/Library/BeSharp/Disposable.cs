using System;

namespace BeSharp
{
    public abstract class Disposable : IDisposable
    {
        public static void Dispose(IDisposable disposable)
        {
            if (null != disposable)
                disposable.Dispose();
        }

        public bool Disposed { get; private set; }

        // Public so you can call it on the class instance as well as through IDisposable.
        // This sounds impure when viewed from an interface based programming perspective, but is very pragmatic.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }

        protected virtual void Dispose(bool disposingManagedResources)
        {
            if (!Disposed)
            {
                InternalDispose(disposingManagedResources);
                Disposed = true;
            }
        }

        protected abstract void InternalDispose(bool disposingManagedResources);

        // Finalizer
        ~Disposable()
        {
            Dispose(false);
        }
    }
}