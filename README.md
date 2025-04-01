Використовуючи xUnit, NUnit, jUnit , etc. написати автономні (юніт) тести, які б:
покривали принаймні 75% коду;
- містили setup (fixture) методи; 
- містили принаймні 4 різних Assert вирази;   
- принаймні один тестовий метод, що тестує виключення;    
- використовували принаймні 2 складних assert (приклади: для рядків, колекцій);  
- містили принаймні один параметризований тестовий метод;
- містили файл з конфігурацією набору тестів.
- 
У якості проєкту для тестування було обрано програму для розв’язку нелінійного рівняння модифікованим методом Ньютона.

Реалізовані тести:
1. Параметризовані тестові методи
- Function_ReturnsCorrect. Перевіряє правильність обчислення функції Function(x). Тестує коректність обчислення функції для різних значень x.
- FirstDeriv_ReturnsCorrect. Перевіряє правильність обчислення першої похідної функції FirstDerivative(x).
- SecondDeriv_ReturnsCorrect. Перевіряє правильність обчислення другої похідної функції SecondDerivative(x).
2. Тестовий метод що тестує виключення
- CalcIters_ThrowsArgEx_InvalidEpsilon. Перевіряє чи викидає метод CalculateIterations() виняток, коли йому передається некоректне значення epsilon (-0.001).  
3. Використання складних Assert, принаймні 2
- CalcIters_ThrowsArgEx_InvalidQ. Перевіряє чи викидає метод CalculateIterations() виняток, коли йому передається некоректне значення q (1). 
Перевіряє, чи повідомлення винятку містить рядок "Invalid q value", що підтверджує, що виняток був викинутий саме через некоректне значення q.
- Derivatives_ProducesExpected. Перевіряє очікувані значення з результатами обчисленням першої та другої похідних функції для набору заданих точок.
4. Інші тестові методи
- ModifiedNewton_ReturnsCorrectRoot. Перевіряє, чи знаходиться результат, отриманий від ModifiedNewton(), в очікуваному діапазоні.
- CalcIters_ReturnsPositiveInteger. Перевіряє, чи кількість ітерацій є більшою на нуль і знаходиться у заданому діапазоні.
