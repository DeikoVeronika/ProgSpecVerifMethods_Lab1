using System.Globalization;

namespace ProgSpecVerifMethods_Lab1;

public class ModifiedNewtonMethod
{
    private readonly IMathFunctions _mathFunctions; //об'єкт, який вміє обчислювати значення функції та її похідних
    private readonly ILogger _logger; //об'єкт, який відповідає за логування інформації
    private readonly IIterationCalculator _iterationCalculator; //об'єкт, який вміє розраховувати необхідну кількість ітерацій

    public ModifiedNewtonMethod(
        IMathFunctions mathFunctions,
        ILogger logger,
        IIterationCalculator iterationCalculator)
    {
        _mathFunctions = mathFunctions ?? throw new ArgumentNullException(nameof(mathFunctions));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _iterationCalculator = iterationCalculator ?? throw new ArgumentNullException(nameof(iterationCalculator));
    }

    // Статичні методи для зворотної сумісності
    //public static double Function(double x) => new PolynomialFunctions().Calculate(x);
    //public static double FirstDerivative(double x) => new PolynomialFunctions().CalculateFirstDerivative(x);
    //public static double SecondDerivative(double x) => new PolynomialFunctions().CalculateSecondDerivative(x);
    //public static int CalculateIterations(double z0, double epsilon, double q) => new IterationCalculator().CalculateIterations(z0, epsilon, q);

    
    // Модифікований метод Ньютона з інтерфейсами
    public double ModifiedNewton(double x0, double epsilon, double a = -1.5, double b = -0.8)
    {
        double x = x0;
        double fx = _mathFunctions.Calculate(x);
        double dfx0 = _mathFunctions.CalculateFirstDerivative(x0);

        if (dfx0 == 0)
        {
            throw new DivideByZeroException("First derivative at x0 is zero, which leads to division by zero.");
        }

        double m1 = Math.Abs(_mathFunctions.CalculateFirstDerivative(b));
        double M2 = Math.Abs(_mathFunctions.CalculateSecondDerivative(a));
        double z0 = (b - a);
        double q = M2 * Math.Abs(z0) / (2 * m1);

        int n = _iterationCalculator.CalculateIterations(z0, epsilon, q);
        _logger.LogInfo($"Max iterations estimated (n): {n}");

        for (int iteration = 0; Math.Abs(fx) > epsilon && iteration < n; iteration++)
        {
            double x_new = x - fx / dfx0;
            if (Math.Abs(x_new - x) <= epsilon)
                break;

            x = x_new;
            fx = _mathFunctions.Calculate(x);
            _logger.LogIterationResult(iteration, x, fx);

            if (iteration == n - 1)
                _logger.LogInfo("Maximum number of iterations reached.");
        }

        return x;
    }

    // Статичний метод для сумісності з попередньою версією
    //public static double ModifiedNewton(double x0, double epsilon)
    //{
    //    var mathFunctions = new PolynomialFunctions();
    //    var logger = new ConsoleLogger();
    //    var iterationCalculator = new IterationCalculator();

    //    var newtonMethod = new ModifiedNewtonMethod(mathFunctions, logger, iterationCalculator);
    //    return newtonMethod.ModifiedNewton(x0, epsilon);
    //}

    public static void Main(string[] args)
    {
        double x0 = -1.1;
        double epsilon;

        var logger = new ConsoleLogger();
        var inputProvider = new ConsoleInputProvider();
        var mathFunctions = new PolynomialFunctions(); // Створюємо реалізацію IMathFunctions
        var iterationCalculator = new IterationCalculator(); // Створюємо реалізацію IIterationCalculator

        logger.LogInfo("Solution to the equation: x^3 - 4x^2 + x + 6 = 0");
        logger.LogInfo($"Initial approximation: x0 = {x0}");

        epsilon = inputProvider.GetEpsilonFromUser(0.001);

        // Створюємо екземпляр ModifiedNewtonMethod, передаючи залежності
        var newtonSolver = new ModifiedNewtonMethod(mathFunctions, logger, iterationCalculator);

        try
        {
            // Викликаємо нестатичний метод ModifiedNewton на екземплярі об'єкта
            double root = newtonSolver.ModifiedNewton(x0, epsilon);
            logger.LogInfo($"The root has been found: {root}");
        }
        catch (ArgumentException ex)
        {
            logger.LogError(ex.Message);
        }
        catch (DivideByZeroException ex)
        {
            logger.LogError(ex.Message);
        }
    }
}


