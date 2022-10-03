namespace CustomDependencyInjectionContainer;

public class CustomDIContainer
{
    private readonly Dictionary<Type, ResolveClass> _classes = new();
    
    private object? ResolveByLifetime(Type type)
    {
        if (_classes[type].Lifetime == Lifetime.Transient ||
            _classes[type].Lifetime is Lifetime.Scoped or Lifetime.Singleton && _classes[type].Object is null)
            _classes[type].Object = Activator.CreateInstance(type);
        return _classes[type].Object;
    }

    private void Add(Type type, ResolveClass resolveClass)
    {
        if (_classes.ContainsKey(type))
            _classes.Remove(type);
        _classes.Add(type, resolveClass);
    }

    public void AddTransient(Type type) =>
        Add(type, new ResolveClass(Lifetime.Transient));

    public void AddScoped(Type type) =>
        Add(type, new ResolveClass(Lifetime.Scoped));

    public void AddSingleton(Type type) =>
        Add(type, new ResolveClass(Lifetime.Singleton));

    public object? Get(Type type)
    {
        if (!_classes.ContainsKey(type))
            throw new Exception("The type was not added to the container");
        var obj = ResolveByLifetime(type);
        _classes[type].Object = obj;
        return obj;
    }

    public void ResetScoped()
    {
        foreach (var el in _classes.Where(el => el.Value.Lifetime == Lifetime.Scoped))
            el.Value.Object = null;
    }
}