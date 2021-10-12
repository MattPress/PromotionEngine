using NUnit.Framework;
using PromotionEngine.Promotions;
using System.Collections.Generic;

namespace PromotionEngine.Tests.Unit.Promotions
{
    public class ThreeAMultibuyPromotionTests
    {
        internal ThreeAMultibuyPromotion _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _sut = new ThreeAMultibuyPromotion();
        }

        public class Label : ThreeAMultibuyPromotionTests
        {
            [Test]
            public void ReturnsClassName()
            {
                // Arrange

                // Act
                var result = _sut.Label;

                // Assert
                Assert.That(result, Is.EqualTo(nameof(ThreeAMultibuyPromotion)));
            }
        }

        public class CalculateSaving : ThreeAMultibuyPromotionTests
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
                    { new Item('A', 50), 2 }
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
                    { new Item('A', 50), 3 }
                };

                // Act
                var result = _sut.CalculateSaving(cart);

                // Assert
                Assert.That(result, Is.EqualTo(20));
            }

            [Test]
            public void WhenApplicableMultipleTimes_ReturnsTotalSaving()
            {
                // Arrange
                var cart = new Dictionary<Item, int>
                {
                    { new Item('A', 50), 6 }
                };

                // Act
                var result = _sut.CalculateSaving(cart);

                // Assert
                Assert.That(result, Is.EqualTo(40));
            }

            [Test]
            public void WhenApplicableMultipleTimesWithRemainder_ReturnsTotalSavingIgnoringRemainder()
            {
                // Arrange
                var cart = new Dictionary<Item, int>
                {
                    { new Item('A', 50), 7 }
                };

                // Act
                var result = _sut.CalculateSaving(cart);

                // Assert
                Assert.That(result, Is.EqualTo(40));
            }
        }

        public class IsApplicable : ThreeAMultibuyPromotionTests
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
                    { new Item('A', 50), 2 }
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
                    { new Item('A', 50), 3 }
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
                    { new Item('A', 50), 4 }
                };

                // Act
                var result = _sut.IsApplicable(cart);

                // Assert
                Assert.That(result, Is.True);
            }
        }
    }
}
