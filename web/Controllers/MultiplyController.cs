using Microsoft.AspNetCore.Mvc;
using domain.Services.Contracts;
using domain.Exceptions;

namespace league.Controllers;

[ApiController]
[Route("[controller]")]
public class MultiplyController : ControllerBase
{
    private readonly ILogger<MultiplyController> _logger;
    private readonly IMatrixService _matrixService;

    public MultiplyController(ILogger<MultiplyController> logger, IMatrixService matrixService)
    {
        _logger = logger;
        _matrixService = matrixService;
    }

    public IActionResult Post()
    {
        var file = Request.Form.Files["file"];

        if (file == null)
        {
            return BadRequest("A csv file containing a matrix must be included in the request as form variable 'file'");
        }

        try
        {
            var result = _matrixService.Multiply(file);

            return Ok(result);
        }
        catch (InvalidMatrixException e)
        {
            _logger.LogInformation(e, e.Message);
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Problem(statusCode: 500, title: "An error occured while processing your request");
        }
    }
}