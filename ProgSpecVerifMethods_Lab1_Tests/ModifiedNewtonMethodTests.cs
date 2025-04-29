//using ProgSpecVerifMethods_Lab1;

//public class ModifiedNewtonMethodTests
//{
//    private double _epsilon;
//    private double _x0;

//    public ModifiedNewtonMethodTests()
//    {
//        _epsilon = 0.001;
//        _x0 = -1.1;
//    }

//    // Фукнція
//    [Theory]
//    [InlineData(-1, -1 - 4 + (-1) + 6)] // f(-1) = (-1)^3 - 4(-1)^2 + (-1) + 6
//    [InlineData(0, 6)]
//    [InlineData(2, 2 * 2 * 2 - 4 * 2 * 2 + 2 + 6)] // f(2) = 2^3 - 4(2)^2 + 2 + 6
//    public void Function_ReturnsCorrect(double x, double expected)
//    {
//        double actual = ModifiedNewtonMethod.Function(x);
//        Assert.Equal(expected, actual, precision: 5);
//    }

//    // Перша похідна
//    [Theory]
//    [InlineData(-1, 3 * (-1) * (-1) - 8 * (-1) + 1)] // f'(-1)
//    [InlineData(0, 3 * 0 * 0 - 8 * 0 + 1)] // f'(0)
//    [InlineData(2, 3 * 2 * 2 - 8 * 2 + 1)] // f'(2)
//    public void FirstDeriv_ReturnsCorrect(double x, double expected)
//    {
//        double actual = ModifiedNewtonMethod.FirstDerivative(x);
//        Assert.Equal(expected, actual, precision: 5);
//    }

//    // Друга похідна
//    [Theory]
//    [InlineData(-1, 6 * (-1) - 8)] // f''(-1)
//    [InlineData(0, 6 * 0 - 8)] // f''(0)
//    [InlineData(2, 6 * 2 - 8)] // f''(2)
//    public void SecondDeriv_ReturnsCorrect(double x, double expected)
//    {
//        double actual = ModifiedNewtonMethod.SecondDerivative(x);
//        Assert.Equal(expected, actual, precision: 5);
//    }

//    // Виняток для epsilon <=0 
//    [Fact]
//    public void CalcIters_ThrowsArgEx_InvalidEpsilon()
//    {
//        var exception = Assert.Throws<ArgumentException>(() => ModifiedNewtonMethod.CalculateIterations(1, -0.001, 0.5));
//    }

//    // Виняток для q >= 1
//    [Fact]
//    public void CalcIters_ThrowsArgEx_InvalidQ()
//    {
//        var exception = Assert.Throws<ArgumentException>(() => ModifiedNewtonMethod.CalculateIterations(1, 0.001, 1));
//        Assert.Contains("Invalid q value", exception.Message);
//    }

//    // Колекції похідних
//    [Fact]
//    public void Derivatives_ProducesExpected()
//    {
//        var testPoints = new Dictionary<double, (double, double)>
//        {
//            { -1, (ModifiedNewtonMethod.FirstDerivative(-1), ModifiedNewtonMethod.SecondDerivative(-1)) },
//            { 0, (ModifiedNewtonMethod.FirstDerivative(0), ModifiedNewtonMethod.SecondDerivative(0)) },
//            { 2, (ModifiedNewtonMethod.FirstDerivative(2), ModifiedNewtonMethod.SecondDerivative(2)) }
//        };

//        foreach (var point in testPoints)
//        {
//            double x = point.Key;
//            double expectedFirst = 3 * x * x - 8 * x + 1;
//            double expectedSecond = 6 * x - 8;

//            Assert.Equal(expectedFirst, point.Value.Item1, precision: 5);
//            Assert.Equal(expectedSecond, point.Value.Item2, precision: 5);
//        }
//    }

//    // Перевірка кореня (діапазон)
//    [Fact]
//    public void ModifiedNewton_ReturnsCorrectRoot()
//    {
//        double root = ModifiedNewtonMethod.ModifiedNewton(_x0, _epsilon);
//        Assert.InRange(root, -1.001, -0.999);
//    }

//    // Перевірка кількості ітерацій (діапазон)
//    [Fact]
//    public void CalcIters_ReturnsPositiveInteger()
//    {
//        int iterations = ModifiedNewtonMethod.CalculateIterations(0.7, 0.001, 0.5);
//        Assert.InRange(iterations, 1, 100);
//    }
//}