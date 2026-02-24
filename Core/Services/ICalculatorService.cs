namespace Core.Services
{
    public interface ICalculatorService
    {
        int Add(int a, int b);
        int Subtract(int a, int b);
        Task<int> MultiplyAsync(int a, int b);
        int Divide(int a, int b);

    }
}