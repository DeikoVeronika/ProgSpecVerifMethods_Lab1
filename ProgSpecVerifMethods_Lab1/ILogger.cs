using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgSpecVerifMethods_Lab1;

// Інтерфейс для виведення інформації
public interface ILogger
{
    void LogInfo(string message);
    void LogError(string message);
    void LogIterationResult(int iteration, double x, double fx);
}

public class ConsoleLogger : ILogger
{
    public void LogInfo(string message)
    {
        Console.WriteLine(message);
    }

    public void LogError(string message)
    {
        Console.WriteLine($"Error: {message}");
    }

    public void LogIterationResult(int iteration, double x, double fx)
    {
        Console.WriteLine($"Iteration {iteration + 1}: x = {x} \t f(x) = {fx}");
    }
}