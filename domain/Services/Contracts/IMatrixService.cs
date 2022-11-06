using Microsoft.AspNetCore.Http;

namespace domain.Services.Contracts;

public interface IMatrixService
{
    public string Echo(IFormFile file);
    public string Invert(IFormFile file);
    public string Flatten(IFormFile file);
    public double Sum(IFormFile file);
    public double Multiply(IFormFile file);
}