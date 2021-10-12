using NUnit.Framework;
using System;

namespace PromotionEngine.Tests.Unit
{
    public class EngineResultTests
    {
        private const decimal TotalCost = 100;
        private EngineResult _sut;

        [SetUp]
        public virtual void Setup()
        {
            _sut = new EngineResult(TotalCost);
        }

        public class HasApplicablePromotion : EngineResultTests
        {
            [Test]
            public void NoPromotion_ReturnsFalse()
            {
                // Arrange

                // Act
                var result = _sut.HasApplicablePromotion;

                // Assert
                Assert.That(result, Is.False);
            }

            [Test]
            public void HasPromotion_ReturnsTrue()
            {
                // Arrange
                _sut.AddPromotion("Test", 50);

                // Act
                var result = _sut.HasApplicablePromotion;

                // Assert
                Assert.That(result, Is.True);
            }
        }

        public class PromotionApplied : EngineResultTests
        {
            [Test]
            public void NoPromotion_ReturnsNull()
            {
                // Arrange

                // Act
                var result = _sut.PromotionApplied;

                // Assert
                Assert.That(result, Is.Null);
            }

            [Test]
            public void HasPromotion_ReturnsPromotionLabel()
            {
                // Arrange
                const string name = "Test";
                _sut.AddPromotion(name, 50);

                // Act
                var result = _sut.PromotionApplied;

                // Assert
                Assert.That(result, Is.EqualTo(name));
            }
        }

        public class PromotionSaving : EngineResultTests
        {
            [Test]
            public void NoPromotion_ReturnsZero()
            {
                // Arrange

                // Act
                var result = _sut.PromotionSaving;

                // Assert
                Assert.That(result, Is.Zero);
            }

            [Test]
            public void HasPromotion_ReturnsPromotionSaving()
            {
                // Arrange
                const decimal saving = 50;
                _sut.AddPromotion("Test", saving);

                // Act
                var result = _sut.PromotionSaving;

                // Assert
                Assert.That(result, Is.EqualTo(saving));
            }
        }

        public class Total : EngineResultTests
        {
            [Test]
            public void NoPromotion_ReturnsInitalCartCost()
            {
                // Arrange

                // Act
                var result = _sut.Total;

                // Assert
                Assert.That(result, Is.EqualTo(TotalCost));
            }

            [Test]
            public void HasPromotion_ReturnsTotalMinusPromotionSaving()
            {
                // Arrange
                const decimal saving = 50;
                _sut.AddPromotion("Test", saving);

                // Act
                var result = _sut.PromotionSaving;

                // Assert
                Assert.That(result, Is.EqualTo(TotalCost - saving));
            }
        }

        public class AddPromotion : EngineResultTests
        {
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            public void InvalidPromotionAppliedArgument_ThrowsArgumentNullException(string promotionApplied)
            {
                // Arrange
                const decimal promotionSaving = 50;

                // Act
                TestDelegate act = () => _sut.AddPromotion(promotionApplied, promotionSaving);

                // Assert
                Assert.Throws<ArgumentNullException>(act);
            }

            [TestCase(-5)]
            [TestCase(0)]
            [TestCase(10000)]
            public void InvalidPromotionSavingArgument_ThrowsArgumentOutOfRangeException(decimal promotionSaving)
            {
                // Arrange
                const string promotionApplied = "Test";

                // Act
                TestDelegate act = () => _sut.AddPromotion(promotionApplied, promotionSaving);

                // Assert
                Assert.Throws<ArgumentOutOfRangeException>(act);
            }

            [Test]
            public void PromotionAlreadyApplied_ThrowsInvalidOperationException()
            {
                // Arrange
                _sut.AddPromotion("Test", 20);

                // Act
                TestDelegate act = () => _sut.AddPromotion("Test2", 50);

                // Assert
                Assert.Throws<InvalidOperationException>(act);
            }

            [Test]
            public void ValidArgs_PromotionAppliedIsSet()
            {
                // Arrange
                const string promotionApplied = "Test";
                const decimal promotionSaving = 50;

                // Act
                _sut.AddPromotion(promotionApplied, promotionSaving);

                // Assert
                Assert.That(_sut.PromotionApplied, Is.EqualTo(promotionApplied));
            }

            [Test]
            public void ValidArgs_PromotionSavingIsSet()
            {
                // Arrange
                const string promotionApplied = "Test";
                const decimal promotionSaving = 50;

                // Act
                _sut.AddPromotion(promotionApplied, promotionSaving);

                // Assert
                Assert.That(_sut.PromotionSaving, Is.EqualTo(promotionSaving));
            }
        }
    }
}
