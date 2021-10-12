using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace PromotionEngine.Tests.Unit
{
    public class EngineTests
    {
        protected Mock<IPromotion> PromotionAMock { get; set; }
        protected Mock<IPromotion> PromotionBMock { get; set; }

        protected Engine _sut { get; set; }

        [SetUp]
        public virtual void Setup()
        {
            PromotionAMock = new Mock<IPromotion>();
            PromotionBMock = new Mock<IPromotion>();

            // Default path is no applicable promotions
            PromotionAMock.SetupGet(x => x.Label).Returns(nameof(PromotionAMock));
            PromotionAMock.Setup(x => x.IsApplicable(It.IsAny<Dictionary<Item, int>>())).Returns(false);
            PromotionAMock.Setup(x => x.CalculateSaving(It.IsAny<Dictionary<Item, int>>())).Returns(0);
            PromotionBMock.SetupGet(x => x.Label).Returns(nameof(PromotionAMock));
            PromotionBMock.Setup(x => x.IsApplicable(It.IsAny<Dictionary<Item, int>>())).Returns(false);
            PromotionBMock.Setup(x => x.CalculateSaving(It.IsAny<Dictionary<Item, int>>())).Returns(0);

            _sut = new Engine(new List<IPromotion> { PromotionAMock.Object, PromotionBMock.Object });
        }

        public class Run : EngineTests
        {
            [Test]
            public void EmptyCart_ReturnsNoPromotion()
            {
                // Arrange
                var cart = new Dictionary<Item, int>();

                // Act
                var result = _sut.Run(cart);

                // Assert
                Assert.That(result.HasApplicablePromotion, Is.False);
            }

            [Test]
            public void NoMatchingPromotions_ReturnsNoPromotion()
            {
                // Arrange
                var cart = new Dictionary<Item, int>
                {
                    { new Item('A', 50), 1 }
                };

                // Act
                var result = _sut.Run(cart);

                // Assert
                Assert.That(result.HasApplicablePromotion, Is.False);
            }

            [Test]
            public void MatchingPromotion_IsApplied()
            {
                // Arrange
                PromotionBMock.Setup(x => x.IsApplicable(It.IsAny<Dictionary<Item, int>>())).Returns(true);
                PromotionBMock.Setup(x => x.CalculateSaving(It.IsAny<Dictionary<Item, int>>())).Returns(10);
                var cart = new Dictionary<Item, int>
                {
                    { new Item('A', 50), 1 }
                };

                // Act
                var result = _sut.Run(cart);

                // Assert
                Assert.That(result.HasApplicablePromotion, Is.True);
            }

            /// <remarks>
            /// Assumption is that first applicable promotion will be applied.
            /// In future different promotion application strategies could be implemented and 
            /// passed into the engine. i.e. ApplyAll, ApplyFirstOnly, ApplyLastOnly, 
            /// ApplyPromotionWithGreatestSaving, ApplyPromotionWithSmallestSaving etc.
            /// </remarks>
            [Test]
            public void MultipleApplicablePromotions_OnlyFirstIsApplied()
            {
                // Arrange
                PromotionAMock.Setup(x => x.IsApplicable(It.IsAny<Dictionary<Item, int>>())).Returns(true);
                PromotionAMock.Setup(x => x.CalculateSaving(It.IsAny<Dictionary<Item, int>>())).Returns(10);
                PromotionBMock.Setup(x => x.IsApplicable(It.IsAny<Dictionary<Item, int>>())).Returns(true);
                PromotionBMock.Setup(x => x.CalculateSaving(It.IsAny<Dictionary<Item, int>>())).Returns(10);
                var cart = new Dictionary<Item, int>
                {
                    { new Item('A', 50), 2 }
                };

                // Act
                var result = _sut.Run(cart);

                // Assert
                Assert.That(result.PromotionApplied, Is.EqualTo(nameof(PromotionAMock)));
            }
        }
    }
}