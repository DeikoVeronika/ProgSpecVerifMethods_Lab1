using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgSpecVerifMethods_Lab1;

// Інтерфейс для розрахунку кількості ітерацій
public interface IIterationCalculator
{
    int CalculateIterations(double z0, double epsilon, double q);
}

public class IterationCalculator : IIterationCalculator
{
    public int CalculateIterations(double z0, double epsilon, double q)
    {
        if (epsilon <= 0)
        {
            throw new ArgumentException("Invalid epsilon");
        }
        if (q >= 1)
        {
            throw new ArgumentException("Invalid q value.");
        }
        double numerator = Math.Log(z0 / epsilon);
        double denominator = Math.Log(1 / q);
        double result = Math.Log(numerator / denominator + 1) / Math.Log(2);
        return (int)Math.Ceiling(result) + 1;
    }
}
