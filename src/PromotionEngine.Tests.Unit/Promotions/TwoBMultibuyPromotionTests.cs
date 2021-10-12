using NUnit.Framework;
using PromotionEngine.Promotions;
using System.Collections.Generic;

namespace PromotionEngine.Tests.Unit.Promotions
{
    public class TwoBMultibuyPromotionTests
    {
        internal TwoBMultibuyPromotion _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _sut = new TwoBMultibuyPromotion();
        }

        public class Label : TwoBMultibuyPromotionTests
        {
            [Test]
            public void ReturnsClassName()
            {
                // Arrange

                // Act
                var result = _sut.Label;

                // Assert
                Assert.That(result, Is.EqualTo(nameof(TwoBMultibuyPromotion)));
            }
        }

        public class CalculateSaving : TwoBMultibuyPromotionTests
        {
            [Test]
            public void WhenCartEmpty_ReturnsZero()
            {
                // Arrange
                var cart = new Dictionary<Item, int>();

                // Act
                var result = _sut.CalculateSaving(cart);

                // Assert
                Assert.That(result, Is.Zero);
            }

            [Test]
            public void WhenNotApplicable_ReturnsZero()
            {
                // Arrange
                var cart = new Dictionary<Item, int>
                {
                    { new Item('B', 30), 1 }
                };

                // Act
                var result = _sut.CalculateSaving(cart);

                // Assert
                Assert.That(result, Is.Zero);
            }

            [Test]
            public void WhenApplicable_ReturnsSaving()
            {
                // Arrange
                var cart = new Dictionary<Item, int>
                {
                    { new Item('B', 30), 3 }
                };

                // Act
                var result = _sut.CalculateSaving(cart);

                // Assert
                Assert.That(result, Is.EqualTo(15));
            }

            [Test]
            public void WhenApplicableMultipleTimes_ReturnsTotalSaving()
            {
                // Arrange
                var cart = new Dictionary<Item, int>
                {
                    { new Item('B', 30), 4 }
                };

                // Act
                var result = _sut.CalculateSaving(cart);

                // Assert
                Assert.That(result, Is.EqualTo(30));
            }

            [Test]
            public void WhenApplicableMultipleTimesWithRemainder_ReturnsTotalSavingIgnoringRemainder()
            {
                // Arrange
                var cart = new Dictionary<Item, int>
                {
                    { new Item('B', 30), 5 }
                };

                // Act
                var result = _sut.CalculateSaving(cart);

                // Assert
                Assert.That(result, Is.EqualTo(30));
            }
        }

        public class IsApplicable : TwoBMultibuyPromotionTests
        {
            [Test]
            public void WhenCartEmpty_ReturnsFalse()
            {
                // Arrange
                var cart = new Dictionary<Item, int>();

                // Act
                var result = _sut.IsApplicable(cart);

                // Assert
                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenLimitNotMet_ReturnsFalse()
            {
                // Arrange
                var cart = new Dictionary<Item, int>
                {
                    { new Item('B', 30), 1 }
                };

                // Act
                var result = _sut.IsApplicable(cart);

                // Assert
                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenLimitMet_ReturnsTrue()
            {
                // Arrange
                var cart = new Dictionary<Item, int>
                {
                    { new Item('B', 30), 2 }
                };

                // Act
                var result = _sut.IsApplicable(cart);

                // Assert
                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenLimitExceeded_ReturnsTrue()
            {
                // Arrange
                var cart = new Dictionary<Item, int>
                {
                    { new Item('B', 30), 3 }
                };

                // Act
                var result = _sut.IsApplicable(cart);

                // Assert
                Assert.That(result, Is.True);
            }
        }
    }
}

