using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgSpecVerifMethods_Lab1;

//Інтерфейс для введення даних
public interface IInputProvider
{
    double GetEpsilonFromUser(double defaultValue);
}

public class ConsoleInputProvider : IInputProvider
{
    public double GetEpsilonFromUser(double defaultValue)
    {
        Console.Write("Enter the desired precision (epsilon, for example, 0.001): ");
        string input = Console.ReadLine();
        double epsilon;

        if (!double.TryParse(input, System.Globalization.NumberStyles.Float,
            System.Globalization.CultureInfo.InvariantCulture, out epsilon))
        {
            epsilon = defaultValue;
            Console.WriteLine($"The default accuracy is used: {epsilon}");
        }
        else
        {
            Console.WriteLine($"The entered accuracy is used: {epsilon}");
        }

        return epsilon;
    }
}
