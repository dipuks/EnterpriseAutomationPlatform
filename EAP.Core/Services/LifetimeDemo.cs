using System;
using System.Collections.Generic;
using System.Text;

namespace EAP.Core.Services
{
    public interface ITransientService
    {
        Guid id { get; }
    }

    public interface IScopedService
    {
        Guid id { get; }
    }

    public interface ISingletonService
    {
        Guid id { get; }
    }

    public class TransientService : ITransientService
    {
        public Guid id { get; } = Guid.NewGuid();
    }

    public class ScopedService : IScopedService
    {
        public Guid id { get; } = Guid.NewGuid();
    }

    public class SingletonService : ISingletonService
    {
        public Guid id { get; } = Guid.NewGuid();
    }
}
