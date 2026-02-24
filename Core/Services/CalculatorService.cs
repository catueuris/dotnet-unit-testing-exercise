namespace Core.Services
{
    public class CalculatorService : ICalculatorService
    {
        public int Add(int a, int b) => a + b;

        public int Subtract(int a, int b) => a - b;

        public async Task<int> MultiplyAsync(int a, int b)
        {
            await Task.Delay(50);
            return a * b;
        }

        public int Divide(int a, int b)
        {
            if (b == 0) throw new DivideByZeroException("Denominator cannot be zero.");
            return a / b;
        }
    }
}