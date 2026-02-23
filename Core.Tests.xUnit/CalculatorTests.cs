using Core.Services;
using Moq;
using System.Linq.Expressions;

namespace Core.Tests.xUnit
{
    public class CalculatorTests
    {
        private readonly CalculatorService _sut = new();
        private Mock<ICalculatorService> _mock = new();

        [Theory]
        [InlineData(2, 3, 5)]
        [InlineData(3, 4, 7)]
        [InlineData(-4, -5, -9)]
        [InlineData(0, 0, 0)]
        public void Add_ShouldReturnCorrectSum(int a, int b, int expected)
        {
            var result = _sut.Add(a, b);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2, 3, -1)]
        [InlineData(4, 3, 1)]
        [InlineData(-4, -5, 1)]
        [InlineData(0, 0, 0)]
        [InlineData(-5, 5, -10)]
        public void Subtract_ShouldReturnCorrectDifference(int a, int b, int expected)
        {
            var result = _sut.Subtract(a, b);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2, 3, 6)]
        [InlineData(4, 3, 12)]
        [InlineData(-4, -5, 20)]
        [InlineData(0, 0, 0)]
        [InlineData(-5, 5, -25)]
        public async Task MultiplyAsync_ShouldReturnCorrectProduct(int a, int b, int expected)
        {
            var result = await _sut.MultiplyAsync(a, b);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void MockingInterface_ShouldReturnConfiguredSumValue()
        {
            var expected = 30;
            var result = MockAndReturnValue(x => x.Add(10, 20), 30);

            Assert.Equal(result, expected);

            _mock.Verify(x => x.Add(10, 20), Times.Once);
        }

        [Fact]
        public void MockingInterface_ShouldReturnConfiguredDifferenceValue()
        {
            var expected = -10;
            var result = MockAndReturnValue(x => x.Subtract(10, 20), -10);

            Assert.Equal(result,expected);

            _mock.Verify(x => x.Subtract(10, 20), Times.Once);
        }

        [Fact]
        public async Task MockingInterface_ShouldReturnConfiguredProductValue()
        {
            var expected = 200;
            var result = MockAndReturnValue(x => x.MultiplyAsync(10, 20).Result, 200);

            Assert.Equal(result, expected);

            _mock.Verify(x => x.MultiplyAsync(10, 20), Times.Once);
        }

        [Fact]
        public async Task MultiplyAsync_ShouldSetADelay()
        {
            var task = _sut.MultiplyAsync(10, 20);

            Assert.False(task.IsCompleted);
        }

        private int MockAndReturnValue(Expression<Func<ICalculatorService, int>> expression, int returnValue)
        {
            _mock.Setup(expression).Returns(returnValue);

            return expression.Compile()(_mock.Object);
        }
    }
}
