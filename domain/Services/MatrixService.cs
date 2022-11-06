using domain.Exceptions;
using domain.Services.Contracts;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace domain.Services;

public class MatrixService : IMatrixService
{
    private readonly ILogger<MatrixService> _logger;

    public MatrixService(ILogger<MatrixService> logger)
    {
        _logger = logger;
    }

    public string Echo(List<int[]> matrix)
    {
        return string.Join('\n', matrix.Select(row => string.Join(',', row)));
    }

    public string Echo2(List<int[]> matrix)
    {
        var sb = new StringBuilder();

        foreach (var row in matrix)
        {
            foreach (var num in row)
            {
                sb.Append(num);
                sb.Append(',');
            }
            // remove the last appended comma
            sb.Remove(sb.Length - 1, 1);

            sb.Append('\n');
        }
        // remove the last appended new line
        sb.Remove(sb.Length - 1, 1);

        return sb.ToString();
    }

    /// Return the matrix as a string in matrix format.
    public string Echo(IFormFile file)
    {
        using (var stream = file.OpenReadStream())
        {
            var matrix = ConvertCsvFileIntoMatrix(file);
            return Echo(matrix);
        }
    }

    public string Invert(IFormFile file)
    {
        var matrix = ConvertCsvFileIntoMatrix(file);
        return Invert(matrix);
    }

    /// Return the matrix as a string in matrix format where the columns and rows are inverted
    public string Invert(List<int[]> matrix)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < matrix[0].Length; i++)
        {
            for (int j = 0; j < matrix.Count; j++)
            {
                sb.Append(matrix[j][i]);
                sb.Append(',');
            }
            // remove the last appended comma
            sb.Remove(sb.Length - 1, 1);

            sb.Append('\n');
        }
        // remove the last appended new line
        sb.Remove(sb.Length - 1, 1);

        return sb.ToString();
    }

    public string Flatten(IFormFile file)
    {
        var matrix = ConvertCsvFileIntoMatrix(file);
        return Flatten(matrix);
    }

    /// Return the matrix as a 1 line string, with values separated by commas.
    public string Flatten(List<int[]> matrix)
    {
        return String.Join(',', matrix.SelectMany(row => row));
    }

    /// Return the sum of the integers in the matrix.
    public double Sum(IFormFile file)
    {
        var matrix = ConvertCsvFileIntoMatrix(file);
        return Sum(matrix);
    }

    public double Sum(List<int[]> matrix)
    {
        return matrix.SelectMany(matrixRow => matrixRow).Sum(num => (double)num);
    }
    /// Return the product of the integers in the matrix.
    public double Multiply(IFormFile file)
    {
        var matrix = ConvertCsvFileIntoMatrix(file);
        return Multiply(matrix);
    }

    public double Multiply(List<int[]> matrix)
    {
        return matrix.Aggregate(1d, (product, row) =>
                product * row.Aggregate(1d, (rowProduct, num) => rowProduct * num));
    }

    private List<int[]> ConvertCsvFileIntoMatrix(IFormFile file)
    {
        using (var stream = file.OpenReadStream())
        using (var reader = new StreamReader(stream))
        {
            var matrix = new List<int[]>();

            int[] parseCsvLine(string csvLine)
            {
                return csvLine.Split(',').Select(str =>
                {
                    if (!int.TryParse(str, out var num))
                    {
                        throw new InvalidMatrixException("csv matrix file contains non-numeric values");
                    }

                    return num;
                }).ToArray();
            }

            var line = reader.ReadLine();
            if (line == null)
            {
                throw new InvalidMatrixException("csv file is empty");
            }
            var row = parseCsvLine(line);
            var matrixWidth = row.Length;
            matrix.Add(row);

            line = reader.ReadLine();
            while (line != null)
            {
                row = parseCsvLine(line);
                if (row.Length != matrixWidth)
                {
                    throw new InvalidMatrixException("csv matrix file has inconsistent row lengths");
                }

                matrix.Add(row);

                line = reader.ReadLine();
            }

            if (matrix.Count != matrixWidth)
            {
                throw new InvalidMatrixException("csv matrix file rows and columns lengths do not match");
            }

            return matrix;
        }
    }
}