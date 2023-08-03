using System;

namespace Common
{
    public interface ICustomDisposable : IDisposable
    {
        Action OnDispose { get; set; }
    }
}
