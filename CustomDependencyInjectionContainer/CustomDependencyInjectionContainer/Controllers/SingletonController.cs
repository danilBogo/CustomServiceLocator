﻿using Microsoft.AspNetCore.Mvc;

namespace CustomDependencyInjectionContainer.Controllers;

[ApiController]
[Route("[controller]")]
public class SingletonController
{
    private readonly CustomDIContainer _customDiContainer;

    public SingletonController(CustomDIContainer customDiContainer)
    {
        _customDiContainer = customDiContainer;
    }
    
    [HttpGet]
    public IEnumerable<string?> Get()
    {
        for (var i = 0; i < 3; i++)
        {
            var obj = _customDiContainer.Get(typeof(CounterSingleton));
            var counter = (CounterSingleton) obj;
            yield return counter?.Guid.ToString();
        }
    }
}