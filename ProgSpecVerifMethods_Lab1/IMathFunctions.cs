using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgSpecVerifMethods_Lab1;

// Інтерфейс для математичних операцій
public interface IMathFunctions
{
    double Calculate(double x);
    double CalculateFirstDerivative(double x);
    double CalculateSecondDerivative(double x);
}

// Реалізація математичних функцій для рівняння x^3 - 4x^2 + x + 6 = 0
public class PolynomialFunctions : IMathFunctions
{
    public double Calculate(double x) => Math.Pow(x, 3) - 4 * Math.Pow(x, 2) + x + 6;
    public double CalculateFirstDerivative(double x) => 3 * Math.Pow(x, 2) - 8 * x + 1;
    public double CalculateSecondDerivative(double x) => 6 * x - 8;
}

