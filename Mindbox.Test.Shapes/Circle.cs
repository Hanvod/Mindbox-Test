using System;

namespace Mindbox.Test.Shapes
{
    public class Circle: Shape
    {
        public Circle(double radius)
        {
            if (radius < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(radius), radius, "Radius can't be less than zero.");
            }
            
            Radius = radius;
        }
        
        public double Radius { get; }

        public override double Area => Math.PI * Radius * Radius;
    }
}