using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using domain.Services;
using Moq;

namespace test;

[TestClass]
public class MatrixServiceTests
{
    private MatrixService _matrixService;

    [TestMethod]
    public void Echo_ValidInput_Successful()
    {
        // arrange
        var fileStream = new FileStream(@"matrix.csv", FileMode.Open);

        IFormFile file = new FormFile(fileStream, 0, fileStream.Length, "file", @"matrix.csv");

        // act
        var actual = _matrixService.Echo(file);

        // assert
        const string expected = "1,2,3\n4,5,6\n7,8,9";

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Invert_ValidInput_Successful()
    {
        // arrange
        var fileStream = new FileStream(@"matrix.csv", FileMode.Open);

        IFormFile file = new FormFile(fileStream, 0, fileStream.Length, "file", @"matrix.csv");

        // act
        var actual = _matrixService.Invert(file);

        // assert
        const string expected = "1,4,7\n2,5,8\n3,6,9";

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Flatten_ValidInput_Successful()
    {
        // arrange
        var fileStream = new FileStream(@"matrix.csv", FileMode.Open);

        IFormFile file = new FormFile(fileStream, 0, fileStream.Length, "file", @"matrix.csv");

        // act
        var actual = _matrixService.Flatten(file);

        // assert
        const string expected = "1,2,3,4,5,6,7,8,9";

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Sum_ValidInput_Successful()
    {
        // arrange
        var fileStream = new FileStream(@"matrix.csv", FileMode.Open);

        IFormFile file = new FormFile(fileStream, 0, fileStream.Length, "file", @"matrix.csv");

        // act
        var actual = _matrixService.Sum(file);

        // assert
        const double expected = 45d;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Multiply_ValidInput_Successful()
    {
        // arrange
        var fileStream = new FileStream(@"matrix.csv", FileMode.Open);

        IFormFile file = new FormFile(fileStream, 0, fileStream.Length, "file", @"matrix.csv");

        // act
        var actual = _matrixService.Multiply(file);

        // assert
        const double expected = 362880d;

        Assert.AreEqual(expected, actual);
    }

    [TestInitialize]
    public void Setup()
    {
        var mockLogger = new Mock<ILogger<MatrixService>>();
        _matrixService = new MatrixService(mockLogger.Object);
    }

    [TestCleanup]
    public void TearDown()
    {

    }
}