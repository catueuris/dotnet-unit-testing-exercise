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
    }
}