using Core.Services;
using Moq;

namespace Core.Tests.NUnit
{
    public class CalculatorTests
    {
        private CalculatorService _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CalculatorService();
        }

        [TestCase(2, 3, ExpectedResult = 5)]
        [TestCase(3, 4, ExpectedResult = 7)] 
        [TestCase(-4,-5, ExpectedResult = -9)]
        [TestCase(0, 0, ExpectedResult = 0)]
        public int Add_ShouldReturnCorrectSum(int a, int b)
        {
            return _sut.Add(a, b);
        }

        [TestCase(2, 3, ExpectedResult = -1)]
        [TestCase(4, 3, ExpectedResult = 1)]
        [TestCase(-4, -5, ExpectedResult = 1)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(-5, 5, ExpectedResult = -10)]
        public int Subtract_ShouldReturnCorrectDifference(int a, int b)
        {
            return _sut.Subtract(a, b);
        }

        [TestCase(2, 3, ExpectedResult = 6)]
        [TestCase(4, 3, ExpectedResult = 12)]
        [TestCase(-4, -5, ExpectedResult = 20)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(-5, 5, ExpectedResult = -25)]
        public async Task<int> MultiplyAsync_ShouldReturnCorrectProduct(int a, int b)
        {
            var result = await _sut.MultiplyAsync(a, b);

            return result;
        }

        [Test]
        public void MockingInterface_ShouldReturnConfiguredSumValue()
        {
            var mock = new Mock<ICalculatorService>();

            mock.Setup(x => x.Add(10, 20)).Returns(30);

            var result = mock.Object.Add(10, 20);

            Assert.That(result, Is.EqualTo(30));

            mock.Verify(x => x.Add(10, 20), Times.Once);
        }

        [Test]
        public void MockingInterface_ShouldReturnConfiguredDifferenceValue()
        {
            var mock = new Mock<ICalculatorService>();

            mock.Setup(x => x.Subtract(10, 20)).Returns(-10);

            var result = mock.Object.Subtract(10, 20);

            Assert.That(result, Is.EqualTo(-10));

            mock.Verify(x => x.Subtract(10, 20), Times.Once);
        }

        [Test]
        public async Task MockingInterface_ShouldReturnConfiguredProductValue()
        {
            var mock = new Mock<ICalculatorService>();

            mock.Setup(x => x.MultiplyAsync(10, 20).Result).Returns(200);

            var result = await mock.Object.MultiplyAsync(10, 20);

            Assert.That(result, Is.EqualTo(200));

            mock.Verify(x => x.MultiplyAsync(10, 20), Times.Once);
        }

        [Test]
        public async Task MultiplyAsync_ShouldSetADelay()
        {
            var mock = new Mock<ICalculatorService>();

            var task = _sut.MultiplyAsync(10, 20);

            Assert.That(task.IsCompleted, Is.EqualTo(false));
        }
    }
}
