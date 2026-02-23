using Core.Services;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace Core.Tests.NUnit
{
    public class CalculatorTests
    {
        private CalculatorService _sut;
        private Mock<ICalculatorService> _mock;

        [SetUp]
        public void Setup()
        {
            _sut = new CalculatorService();
            _mock = new Mock<ICalculatorService>();
        }

        //NUnit tests

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
            var result = MockAndReturnValue(x => x.Add(10, 20), 30);

            Assert.That(result, Is.EqualTo(30));

            _mock.Verify(x => x.Add(10, 20), Times.Once);
        }

        [Test]
        public void MockingInterface_ShouldReturnConfiguredDifferenceValue()
        {
            var result = MockAndReturnValue(x => x.Subtract(10, 20), -10);

            Assert.That(result, Is.EqualTo(-10));

            _mock.Verify(x => x.Subtract(10, 20), Times.Once);
        }

        [Test]
        public async Task MockingInterface_ShouldReturnConfiguredProductValue()
        {
            var result = MockAndReturnValue(x => x.MultiplyAsync(10, 20).Result, 200);

            Assert.That(result, Is.EqualTo(200));

            _mock.Verify(x => x.MultiplyAsync(10, 20), Times.Once);
        }

        [Test]
        public async Task MultiplyAsync_ShouldSetADelay()
        {
            var task = _sut.MultiplyAsync(10, 20);

            Assert.That(task.IsCompleted, Is.EqualTo(false));
        }


        //FluentAssertions tests

        [TestCase(2, 3)]
        [TestCase(3, 4)]
        [TestCase(-4, -5)]
        [TestCase(0, 0)]
        public void Add_ShouldReturnCorrectSum_With_FluentAssertions_components(int a, int b)
        {
            _sut.Add(a, b).Should().Be(a + b);
        }

        [TestCase(2, 3)]
        [TestCase(4, 3)]
        [TestCase(-4, -5)]
        [TestCase(0, 0)]
        [TestCase(-5, 5)]
        public void Subtract_ShouldReturnCorrectDifference_With_FluentAssertions_components(int a, int b)
        {
            _sut.Subtract(a, b).Should().Be(a - b);
        }

        [TestCase(2, 3)]
        [TestCase(4, 3)]
        [TestCase(-4, -5)]
        [TestCase(0, 0)]
        [TestCase(-5, 5)]
        public async Task MultiplyAsync_ShouldReturnCorrectProduct_With_FluentAssertions_components(int a, int b)
        {
            var result = await _sut.MultiplyAsync(a, b);

            result.Should().Be(a * b);
        }

        [Test]
        public void MockingInterface_ShouldReturnConfiguredSumValue_With_FluentAssertions_components()
        {
            var result = MockAndReturnValue(x => x.Add(10, 20), 30);

            result.Should().Be(30);

            _mock.Verify(x => x.Add(10, 20), Times.Once);
        }

        [Test]
        public void MockingInterface_ShouldReturnConfiguredDifferenceValue_With_FluentAssertions_components()
        {
            var result = MockAndReturnValue(x => x.Subtract(10, 20), -10);

            result.Should().Be(-10);

            _mock.Verify(x => x.Subtract(10, 20), Times.Once);
        }

        [Test]
        public async Task MockingInterface_ShouldReturnConfiguredProductValue_With_FluentAssertions_components()
        {
            var result = MockAndReturnValue(x => x.MultiplyAsync(10, 20).Result, 200);

            result.Should().Be(200);

            _mock.Verify(x => x.MultiplyAsync(10, 20), Times.Once);
        }

        [Test]
        public async Task MultiplyAsync_ShouldSetADelay_With_FluentAssertions_components()
        {
            var mock = new Mock<ICalculatorService>();

            var task = _sut.MultiplyAsync(10, 20);

            task.IsCompleted.Should().BeFalse();
        }

        private int MockAndReturnValue(Expression<Func<ICalculatorService, int>> expression, int returnValue)
        {
            _mock.Setup(expression).Returns(returnValue);

            return expression.Compile()(_mock.Object);
        }
    }
}