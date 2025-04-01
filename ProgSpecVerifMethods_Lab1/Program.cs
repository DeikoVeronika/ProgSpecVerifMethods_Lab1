using System.Globalization;

namespace ProgSpecVerifMethods_Lab1;

public class ModifiedNewtonMethod
{
    public static double Function(double x) => Math.Pow(x, 3) - 4 * Math.Pow(x, 2) + x + 6;
    public static double FirstDerivative(double x) => 3 * Math.Pow(x, 2) - 8 * x + 1;
    public static double SecondDerivative(double x) => 6 * x - 8;

    public static int CalculateIterations(double z0, double epsilon, double q)
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

    public static double ModifiedNewton(double x0, double epsilon)
    {
        double a = -1.5, b = -0.8;

        double x = x0;
        double fx = Function(x);
        double dfx0 = FirstDerivative(x0);

        double m1 = Math.Abs(FirstDerivative(b));
        double M2 = Math.Abs(SecondDerivative(a));

        double z0 = (b - a);
        double q = M2 * Math.Abs(z0) / (2 * m1);

        int n = CalculateIterations(z0, epsilon, q);
        Console.WriteLine($"Max iterations estimated (n): {n}");

        for (int iteration = 0; Math.Abs(fx) > epsilon && iteration < n; iteration++)
        {
            double x_new = x - fx / dfx0;

            if (Math.Abs(x_new - x) <= epsilon)
                break;

            x = x_new;
            fx = Function(x);

            Console.WriteLine($"Iteration {iteration + 1}: x = {x} \t f(x) = {fx}");

            if (iteration == n)
                Console.WriteLine("Maximum number of iterations reached.");
        }

        return x;
    }

    public static void Main(string[] args)
    {
        double x0 = -1.1;
        double epsilon;

        Console.WriteLine("Solution to the equation: x^3 - 4x^2 + x + 6 = 0");
        Console.WriteLine($"Initial approximation: x0 = {x0}");

        Console.Write("Enter the desired precision (epsilon, for example, 0.001): ");
        string input = Console.ReadLine();

        if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out epsilon))
        {
            epsilon = 0.001;
            Console.WriteLine($"The default accuracy is used: {epsilon}");
        }
        else
        {
            Console.WriteLine($"The entered accuracy is used: {epsilon}");
        }

        try
        {
            double root = ModifiedNewton(x0, epsilon);
            Console.WriteLine($"The root has been found: {root}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
