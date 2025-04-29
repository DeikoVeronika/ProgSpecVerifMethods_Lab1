using Moq;
using Xunit;
using System;
using ProgSpecVerifMethods_Lab1;

public class ModifiedNewtonMethodMockTests
{
    private readonly double _epsilon = 0.001;
    private readonly double _x0 = -1.1;
    private readonly double _a = -1.5;
    private readonly double _b = -0.8;

    // Сценарій 1: Нормальна робота з очікуваними значеннями 
    [Fact]
    public void ModifiedNewton_ReturnsCorrectRoot()
    {
        // Arrange
        var mathMock = new Mock<IMathFunctions>();
        mathMock.Setup(m => m.Calculate(It.IsAny<double>())).Returns<double>(x => Math.Pow(x, 3) - 4 * Math.Pow(x, 2) + x + 6);
        mathMock.Setup(m => m.CalculateFirstDerivative(It.IsAny<double>())).Returns<double>(x => 3 * Math.Pow(x, 2) - 8 * x + 1);
        mathMock.Setup(m => m.CalculateSecondDerivative(It.IsAny<double>())).Returns<double>(x => 6 * x - 8);

        var loggerMock = new Mock<ILogger>();
        var iterCalcMock = new Mock<IIterationCalculator>();
        iterCalcMock.Setup(i => i.CalculateIterations(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>())).Returns(10);

        var method = new ModifiedNewtonMethod(mathMock.Object, loggerMock.Object, iterCalcMock.Object);

        // Act
        double root = method.ModifiedNewton(_x0, _epsilon, _a, _b);

        // Assert
        Assert.InRange(root, -1.001, -0.999);
    }

    // Сценарій 2: Обробка виключень - коли функція генерує виключення
    [Fact]
    public void ModifiedNewton_WhenFunctionThrowsException_PropagatesException()
    {
        // Arrange
        var mathMock = new Mock<IMathFunctions>();
        mathMock.Setup(m => m.Calculate(It.IsAny<double>()))
            .Throws(new InvalidOperationException("Function calculation error"));

        // Встановлюємо повернення значень для інших методів, щоб уникнути додаткових помилок
        mathMock.Setup(m => m.CalculateFirstDerivative(It.IsAny<double>())).Returns(1.0);
        mathMock.Setup(m => m.CalculateSecondDerivative(It.IsAny<double>())).Returns(1.0);

        var loggerMock = new Mock<ILogger>();
        var iterCalcMock = new Mock<IIterationCalculator>();
        iterCalcMock.Setup(i => i.CalculateIterations(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>())).Returns(10);

        var method = new ModifiedNewtonMethod(mathMock.Object, loggerMock.Object, iterCalcMock.Object);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => method.ModifiedNewton(_x0, _epsilon, _a, _b));
    }

    // Сценарій 3: Співставлення параметрів для складної поведінки
    [Fact]
    public void ModifiedNewton_WithParameterMatching_ReturnsCorrectResult()
    {
        // Arrange
        var mathMock = new Mock<IMathFunctions>();

        // Задаємо різні повернення значень залежно від параметра
        mathMock.Setup(m => m.Calculate(It.Is<double>(x => x < -1))).Returns(2.0);
        mathMock.Setup(m => m.Calculate(It.Is<double>(x => x >= -1 && x < 0))).Returns(0.0001);
        mathMock.Setup(m => m.Calculate(It.Is<double>(x => x >= 0))).Returns(-2.0);

        mathMock.Setup(m => m.CalculateFirstDerivative(It.IsAny<double>())).Returns(-1.0);
        mathMock.Setup(m => m.CalculateSecondDerivative(It.IsAny<double>())).Returns(0.5);

        var loggerMock = new Mock<ILogger>();
        var iterCalcMock = new Mock<IIterationCalculator>();
        iterCalcMock.Setup(i => i.CalculateIterations(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>())).Returns(10);

        var method = new ModifiedNewtonMethod(mathMock.Object, loggerMock.Object, iterCalcMock.Object);

        // Act
        double result = method.ModifiedNewton(_x0, _epsilon, _a, _b);

        // Assert
        Assert.InRange(result, -1.5, 0.0);
        mathMock.Verify(m => m.Calculate(It.IsAny<double>()), Times.AtLeastOnce());
    }

    // Сценарій 4: Різні відповіді для послідовних викликів
    [Fact]
    public void ModifiedNewton_WithSequentialResponses_ConvergesToRoot()
    {
        // Arrange
        var mathMock = new Mock<IMathFunctions>();

        // Задаємо послідовність відповідей на виклики методу Calculate
        mathMock.SetupSequence(m => m.Calculate(It.IsAny<double>()))
            .Returns(1.0)
            .Returns(0.5)
            .Returns(0.2)
            .Returns(0.05)
            .Returns(0.001);

        mathMock.Setup(m => m.CalculateFirstDerivative(It.IsAny<double>())).Returns(1.0);
        mathMock.Setup(m => m.CalculateSecondDerivative(It.IsAny<double>())).Returns(0.5);

        var loggerMock = new Mock<ILogger>();
        var iterCalcMock = new Mock<IIterationCalculator>();
        iterCalcMock.Setup(i => i.CalculateIterations(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>())).Returns(10);

        var method = new ModifiedNewtonMethod(mathMock.Object, loggerMock.Object, iterCalcMock.Object);

        // Act
        double result = method.ModifiedNewton(_x0, _epsilon, _a, _b);

        // Assert
        mathMock.Verify(m => m.Calculate(It.IsAny<double>()), Times.AtLeast(5));
    }

    // Перевірка кількості викликів та порядку
    [Fact]
    public void ModifiedNewton_VerifyMethodCallCountAndOrder()
    {
        // Arrange
        var mathMock = new Mock<IMathFunctions>();
        var loggerMock = new Mock<ILogger>();
        var iterCalcMock = new Mock<IIterationCalculator>();

        // Встановлюємо очікуваний порядок викликів
        var sequence = new MockSequence();
        mathMock.InSequence(sequence).Setup(m => m.Calculate(_x0)).Returns(1.0);
        mathMock.InSequence(sequence).Setup(m => m.CalculateFirstDerivative(_x0)).Returns(1.0);
        mathMock.InSequence(sequence).Setup(m => m.CalculateSecondDerivative(_a)).Returns(1.0);

        // Додаткові дозволені виклики поза sequence
        mathMock.Setup(m => m.Calculate(It.IsAny<double>())).Returns(0.1);
        mathMock.Setup(m => m.CalculateFirstDerivative(It.IsAny<double>())).Returns(1.0);
        mathMock.Setup(m => m.CalculateSecondDerivative(It.IsAny<double>())).Returns(1.0);

        iterCalcMock.Setup(i => i.CalculateIterations(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>())).Returns(5);

        var method = new ModifiedNewtonMethod(mathMock.Object, loggerMock.Object, iterCalcMock.Object);

        // Act
        method.ModifiedNewton(_x0, _epsilon, _a, _b);

        // Assert
        mathMock.Verify(m => m.Calculate(_x0), Times.Once());
        mathMock.Verify(m => m.Calculate(It.IsAny<double>()), Times.AtLeast(2));
        mathMock.Verify(m => m.CalculateFirstDerivative(_x0), Times.Once());
        mathMock.Verify(m => m.CalculateSecondDerivative(_a), Times.Once());
        iterCalcMock.Verify(i => i.CalculateIterations(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()), Times.Once());
    }
}