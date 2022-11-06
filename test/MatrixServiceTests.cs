using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using domain.Services;
using domain.Exceptions;
using Moq;

namespace test;

[TestClass]
public class MatrixServiceTests
{
    private MatrixService _matrixService = new MatrixService();

    [TestMethod]
    public void Echo_Successful()
    {
        // arrange
        var file = CreateFormFile(@"matrix.csv");

        // act
        var actual = _matrixService.Echo(file);

        // assert
        const string expected = "1,2,3\n4,5,6\n7,8,9";

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Invert_Successful()
    {
        // arrange
        var file = CreateFormFile(@"matrix.csv");

        // act
        var actual = _matrixService.Invert(file);

        // assert
        const string expected = "1,4,7\n2,5,8\n3,6,9";

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Flatten_Successful()
    {
        // arrange
        var file = CreateFormFile(@"matrix.csv");

        // act
        var actual = _matrixService.FlattenAndPrint(file);

        // assert
        const string expected = "1,2,3,4,5,6,7,8,9";

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Sum_Successful()
    {
        // arrange
        var file = CreateFormFile(@"matrix.csv");

        // act
        var actual = _matrixService.Sum(file);

        // assert
        const double expected = 45d;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Multiply_Successful()
    {
        // arrange
        var file = CreateFormFile(@"matrix.csv");

        // act
        var actual = _matrixService.Multiply(file);

        // assert
        const double expected = 362880d;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ConvertCsvFileIntoMatrix_Successful()
    {
        // arrange
        var file = CreateFormFile(@"matrix.csv");

        // act
        var actual = _matrixService.ConvertCsvFileIntoMatrix(file);

        // assert
        var expected = new List<int[]>
        {
            new int[] {1,2,3},
            new int[] {4,5,6},
            new int[] {7,8,9}
        };

        for (var i = 0; i < expected.Count; i++)
        {
            CollectionAssert.AreEquivalent(expected[i], actual[i]);
        }
    }

    [TestMethod]
    public void ConvertCsvFileIntoMatrix_EmptyCsv_ThrowsException()
    {
        // arrange
        var file = CreateFormFile(@"empty.csv");

        // act
        // assert
        const string expectedMessage = "csv file is empty";
        Assert.ThrowsException<InvalidMatrixException>(() => _matrixService.ConvertCsvFileIntoMatrix(file), expectedMessage);
    }

    [TestMethod]
    public void ConvertCsvFileIntoMatrix_InconsistentRowSizes_ThrowsException()
    {
        // arrange
        var file = CreateFormFile(@"invalid_row_size.csv");

        // act
        // assert
        const string expectedMessage = "csv matrix file has inconsistent row lengths";
        Assert.ThrowsException<InvalidMatrixException>(() => _matrixService.ConvertCsvFileIntoMatrix(file), expectedMessage);
    }

    [TestMethod]
    public void ConvertCsvFileIntoMatrix_InconsistentColSizes_ThrowsException()
    {
        // arrange
        var file = CreateFormFile(@"invalid_col_size.csv");

        // act
        // assert
        const string expectedMessage = "csv matrix file must have equal row and column lengths";
        Assert.ThrowsException<InvalidMatrixException>(() => _matrixService.ConvertCsvFileIntoMatrix(file), expectedMessage);
    }

    [TestMethod]
    public void ConvertCsvFileIntoMatrix_NonNumericCsv_ThrowsException()
    {
        // arrange
        var file = CreateFormFile(@"non_numeric.csv");

        // act
        // assert
        const string expectedMessage = "csv matrix file contains non-numeric values";
        Assert.ThrowsException<InvalidMatrixException>(() => _matrixService.ConvertCsvFileIntoMatrix(file), expectedMessage);
    }

    [TestMethod]
    public void ConvertCsvFileIntoMatrix_BadFormat_ThrowsException()
    {
        // arrange
        var file = CreateFormFile(@"bad_format.csv");

        // act
        // assert
        const string expectedMessage = "csv matrix file contains non-numeric values";
        Assert.ThrowsException<InvalidMatrixException>(() => _matrixService.ConvertCsvFileIntoMatrix(file), expectedMessage);
    }

    [TestMethod]
    public void Sum_LargeIntegers()
    {
        // arrange
        var file = CreateFormFile(@"large_numbers.csv");

        // act
        var actual = _matrixService.Sum(file);

        // assert
        const double expected = 6442450971d;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Multiply_LargeIntegers()
    {
        // arrange
        var file = CreateFormFile(@"large_numbers.csv");

        // act
        var actual = _matrixService.Multiply(file);

        // assert
        const double expected = 4.278320769793529E+31;

        Assert.AreEqual(expected, actual);
    }

    [TestInitialize]
    public void Setup()
    {
        _matrixService = new MatrixService();
    }

    [TestCleanup]
    public void TearDown()
    {

    }

    private IFormFile CreateFormFile(string fileName)
    {
        var fileStream = new FileStream(fileName, FileMode.Open);
        var file = new FormFile(fileStream, 0, fileStream.Length, "file", fileName);
        return file;
    }
}