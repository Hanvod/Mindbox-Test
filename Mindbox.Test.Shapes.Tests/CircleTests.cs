using System;
using FluentAssertions;
using NUnit.Framework;

namespace Mindbox.Test.Shapes.Tests
{
    [TestFixture]
    public class CircleTests
    {
        [Test]
        public void TestInvariants()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Circle(-1));
            
            Assert.DoesNotThrow(() => new Circle(0));
            Assert.DoesNotThrow(() => new Circle(5));
        }
        
        [Test]
        public void TestArea()
        {
            var radius = 5;

            var circle = new Circle(radius);
            
            circle.Area.Should().Be(25 * Math.PI);
        }
    }
}