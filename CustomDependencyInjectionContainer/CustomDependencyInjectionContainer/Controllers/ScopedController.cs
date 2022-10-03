using Microsoft.AspNetCore.Mvc;

namespace CustomDependencyInjectionContainer.Controllers;

[ApiController]
[Route("[controller]")]
public class ScopedController
{
    private readonly CustomDIContainer _customDiContainer;

    public ScopedController(CustomDIContainer customDiContainer)
    {
        _customDiContainer = customDiContainer;
    }
    
    [HttpGet]
    public IEnumerable<string?> Get()
    {
        for (var i = 0; i < 3; i++)
        {
            var obj = _customDiContainer.Get(typeof(CounterScoped));
            var counter = (CounterScoped) obj;
            yield return counter?.Guid.ToString();
        }
    }
}