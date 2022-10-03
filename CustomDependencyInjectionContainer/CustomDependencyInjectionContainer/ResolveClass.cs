namespace CustomDependencyInjectionContainer;

public class ResolveClass
{
    public Lifetime Lifetime { get; }

    public object? Object { get; set; }

    public ResolveClass(Lifetime lifetime)
    {
        Lifetime = lifetime;
    }
}