using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi
{
    public class Manager
    {
        public readonly SingletonService Service1;
        public readonly SingletonService Service2;
        public readonly ScopedService Service3;
        public readonly ScopedService Service4;
        public readonly TransientService Service5;
        public readonly TransientService Service6;
        public readonly BestService Service7;
        public readonly IBehavior Behavior;

        public Manager(
            SingletonService service1, SingletonService service2,
            ScopedService service3, ScopedService service4,
            TransientService service5, TransientService service6,
            BestService service7, IBehavior behavior)
        {
            Service1 = service1;
            Service2 = service2;
            Service3 = service3;
            Service4 = service4;
            Service5 = service5;
            Service6 = service6;
            Service7 = service7;
            Behavior = behavior;
        }
    }
    public interface IBehavior
    {
        string Id { get; }
    }
    public class SameMultiAsClass : IBehavior
    {
        public string Id { get; }
        public SameMultiAsClass(string id)
            => Id = id;
        public override string ToString() 
            => Id;
    }
    public record Multi(string Id) : IBehavior
    {
        public Multi() : this(Guid.NewGuid().ToString()) { }
        public override string ToString() => $"{nameof(Multi)} {Id}";
    }
    public record Multi2(string Id) : IBehavior
    {
        public Multi2() : this(Guid.NewGuid().ToString()) { }
        public override string ToString() => $"{nameof(Multi2)} {Id}";
    }

    public class BestService
    {
        public string Id { get; } = Guid.NewGuid().ToString();
    }
    public abstract class ToStringone
    {
        public abstract string Id { get; }
        public override string ToString()
            => Id.ToString();
    }
    public class SingletonService : ToStringone
    {
        public override string Id { get; } = Guid.NewGuid().ToString();
    }
    public class ScopedService : ToStringone
    {
        public override string Id { get; } = Guid.NewGuid().ToString();
    }
    public class TransientService : ToStringone
    {
        public override string Id { get; } = Guid.NewGuid().ToString();
    }
}
