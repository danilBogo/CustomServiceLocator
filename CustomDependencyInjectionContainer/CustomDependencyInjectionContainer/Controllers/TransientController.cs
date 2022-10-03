using Microsoft.AspNetCore.Mvc;

namespace CustomDependencyInjectionContainer.Controllers;

[ApiController]
[Route("[controller]")]
public class TransientController
{
    private readonly CustomDIContainer _customDiContainer;

    public TransientController(CustomDIContainer customDiContainer)
    {
        _customDiContainer = customDiContainer;
    }
    
    [HttpGet]
    public IEnumerable<string?> Get()
    {
        for (var i = 0; i < 3; i++)
        {
            var obj = _customDiContainer.Get(typeof(CounterTransient));
            var counter = (CounterTransient) obj;
            yield return counter?.Guid.ToString();
        }
    }
}