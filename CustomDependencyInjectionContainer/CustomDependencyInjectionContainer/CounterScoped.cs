namespace CustomDependencyInjectionContainer;

public class CounterScoped
{
    public Guid Guid { get; } = Guid.NewGuid();
}