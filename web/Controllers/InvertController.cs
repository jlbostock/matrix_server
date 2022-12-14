using Microsoft.AspNetCore.Mvc;
using domain.Services.Contracts;
using domain.Exceptions;

namespace league.Controllers;

[ApiController]
[Route("[controller]")]
public class InvertController : ControllerBase
{
    private readonly ILogger<InvertController> _logger;
    private readonly IMatrixService _matrixService;

    public InvertController(ILogger<InvertController> logger, IMatrixService matrixService)
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
            var result = _matrixService.Invert(file);

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