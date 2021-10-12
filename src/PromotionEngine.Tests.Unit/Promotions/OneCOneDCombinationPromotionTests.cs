using NUnit.Framework;
using PromotionEngine.Promotions;
using System.Collections.Generic;

namespace PromotionEngine.Tests.Unit.Promotions
{
    public class OneCOneDCombinationPromotionTests
    {
        protected OneCOneDCombinationPromotion _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _sut = new OneCOneDCombinationPromotion();
        }

        public class Label : OneCOneDCombinationPromotionTests
        {
            [Test]
            public void ReturnsClassName()
            {
                // Arrange

                // Act
                var result = _sut.Label;

                // Assert
                Assert.That(result, Is.EqualTo(nameof(OneCOneDCombinationPromotion)));
            }
        }

        public class CalculateSaving : OneCOneDCombinationPromotionTests
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
                    { new Item('C', 20), 1 }
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
                    { new Item('C', 20), 1 },
                    { new Item('D', 15), 1 }
                };

                // Act
                var result = _sut.CalculateSaving(cart);

                // Assert
                Assert.That(result, Is.EqualTo(5));
            }

            [Test]
            public void WhenApplicableMultipleTimes_ReturnsTotalSaving()
            {
                // Arrange
                var cart = new Dictionary<Item, int>
                {
                    { new Item('C', 20), 2 },
                    { new Item('D', 15), 2 }
                };

                // Act
                var result = _sut.CalculateSaving(cart);

                // Assert
                Assert.That(result, Is.EqualTo(10));
            }

            [Test]
            public void WhenApplicableMultipleTimesWithRemainder_ReturnsTotalSavingIgnoringRemainder()
            {
                // Arrange
                var cart = new Dictionary<Item, int>
                {
                    { new Item('C', 20), 2 },
                    { new Item('D', 15), 3 }
                };

                // Act
                var result = _sut.CalculateSaving(cart);

                // Assert
                Assert.That(result, Is.EqualTo(10));
            }
        }

        public class IsApplicable : OneCOneDCombinationPromotionTests
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
                    { new Item('C', 20), 1 }
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
                    { new Item('C', 20), 1 },
                    { new Item('D', 15), 1 }
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
                    { new Item('C', 20), 4 },
                    { new Item('D', 15), 3 }
                };

                // Act
                var result = _sut.IsApplicable(cart);

                // Assert
                Assert.That(result, Is.True);
            }
        }
    }
}

