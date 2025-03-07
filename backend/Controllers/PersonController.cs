using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace takehome.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonContract _service;
    private readonly ILogger<PersonController> _logger;

    public PersonController(IPersonContract service, ILogger<PersonController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // <summary>
    /// Retrieves all people in the system.
    /// </summary>
    /// <returns>A list of people.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Person>), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAllPeople()
    {
        _logger.LogInformation(nameof(GetAllPeople));

        try
        {
            var people = await _service.GetPeopleAsync();
            _logger.LogInformation("Successfully retrieved all people.");
            return Ok(people);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
}
