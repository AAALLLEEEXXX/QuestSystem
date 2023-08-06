using System;

namespace Utils
{
    public static class DisposeUtils
    {
        public static void DisposeAndSetNull<T>(ref T disposable)
            where T : IDisposable
        {
            disposable?.Dispose();
            disposable = default;
        }
    }
}