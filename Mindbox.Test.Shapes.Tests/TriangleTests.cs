using System;
using FluentAssertions;
using NUnit.Framework;

namespace Mindbox.Test.Shapes.Tests
{
    [TestFixture]
    public class TriangleTests
    {
        private const double DefaultPrecision = 1E-12;
        
        [Test]
        [TestCase(-1, 0, 2, false)]
        [TestCase(1000, 2, 5, false)]
        [TestCase(5, 2, 3, false)]
        [TestCase(-2, -3, -5, false)]
        [TestCase(10, 10, 5, true)]
        [TestCase(5, 5, 5, true)]
        public void TestInvariants(double a, double b, double с, bool isValid)
        {
            if (isValid)
            {
                Assert.DoesNotThrow(() => new Triangle(a, b, с));
            }
            else
            {
                Assert.Throws<ArgumentException>(() => new Triangle(a, b, с));
            }
        }
    
        [Test]
        [TestCase(1, 1.5, 1, 0.49607837082461073571905295380736 /* sqrt(63/256) */)]
        [TestCase(3, 4, 5, 6)]
        [TestCase(2, 2, 2, 1.7320508075688772935274463415059 /* sqrt(3) */)]
        public void TestArea(double a, double b, double c, double expectedArea)
        {
            var triangle = new Triangle(a, b, c);

            triangle.Area.Should().BeApproximately(expectedArea, DefaultPrecision);
        }

        [Test]
        [TestCase(3, 4, 5, true, DefaultPrecision)]
        [TestCase(0.0003, 0.0004, 0.0005, true, DefaultPrecision)]
        [TestCase(5 * 1E-9, 12 * 1E-9, 13 * 1E-9, true, DefaultPrecision)]
        [TestCase(2, 2, 2, false, DefaultPrecision)]
        [TestCase(2, 2, 2, true, double.PositiveInfinity)]
        [TestCase(4.999, 4, 3, false, DefaultPrecision)]
        [TestCase(4.999, 4, 3, true, 0.1)]
        public void TestIsRightAngled(double a, double b, double c, bool expectedRightAngled, double withPrecision)
        {
            var triangle = new Triangle(a, b, c);
            
            triangle.IsRightAngled(withPrecision).Should().Be(expectedRightAngled);
        }
    }
}

