using System;

namespace Mindbox.Test.Shapes
{
    public class Triangle: Shape
    {
        public Triangle(double side1, double side2, double side3)
        {
            if (!FormsValidTriangle(side1, side2, side3))
            {
                throw new ArgumentException("Side lengths forms invalid triangle.");
            }

            (Side1, Side2, Side3) = (side1, side2, side3);
        }

        public double Side1 { get; }
        public double Side2 { get; }
        public double Side3 { get; }

        public override double Area
        {
            get
            {
                var halfPerimeter = (Side1 + Side2 + Side3) / 2;
                
                return Math.Sqrt(halfPerimeter * (halfPerimeter - Side1) * (halfPerimeter - Side2) * (halfPerimeter - Side3));
            }
        }
        
        /// <summary>
        /// Вычисляет примерно, является ли треугольник прямоугольным.
        /// Ищет комбинацию длин сторон (a, b, с) такую, что что |(1 - c^2 / (a^2 + b^2))| &lt;= precision
        /// </summary>
        /// <param name="precision">Используемая точность</param>
        /// <exception cref="ArgumentOutOfRangeException">Используемая точность меньше 0</exception>
        public bool IsRightAngled(double precision)
        {
            if (precision < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(precision), precision, "Precision can't be less than zero.");
            }
            
            return IsPythagoreanTripleApproximately(Side2, Side3, Side1, precision) ||
                   IsPythagoreanTripleApproximately(Side1, Side3, Side2, precision) ||
                   IsPythagoreanTripleApproximately(Side1, Side2, Side3, precision);
        }
        
        private bool FormsValidTriangle(double side1, double side2, double side3)
        {
            // Валидировать, что стороны больше 0 не нужно.
            // https://www.wolframalpha.com/input/?i2d=true&i=a+%3C+b+%2B+c%5C%2844%29+b+%3C+a+%2B+c%5C%2844%29+c+%3C+b+%2B+a%5C%2844%29+a+%3C%3D+0
            
            // Экстремальные значения свойственные "почти вырожденным" треугольникам,
            // например 1E20, 1E-20, 1E20, формируют правильный с точки зрения математики треугольник,
            // однако не проходят эту проверку из-за погрешности вычислений с плавающей точкой.
            // я также допускаю, что существует комбинация сторон, которая проходит эту проверку, но дает NaN в результате
            // вычисления площади. В общем, double не годится для точных математических вычислений, и поделать с этим ничего нельзя.
            
            return side1 < side2 + side3 &&
                   side2 < side1 + side3 &&
                   side3 < side1 + side2;
        }
        
        // Используем одну из вариаций теоремы Пифагора, погрешность в таком случае не зависит от порядка длин сторон.
        // В процессе раздумий были отброшены два решения: использующее теорему Пифагора в явном виде, с предварительным
        // приведением к decimal (с^2 == a^2 + b^2) и использующее теорему Пифагора c погрешностью без нормализации (|c^2 - a^2 - b^2| <= precision).
        // Первое решение было отброшено как неудачное из-за очевидных ограничений (работало только на некотором количестве
        // значимых цифр, накладывало серьезные ограничение на длины сторон),
        // второе - из-за того, что значение precision сильно зависело от длин сторон. 
        private bool IsPythagoreanTripleApproximately(double a, double b, double c, double precision)
        {
            return Math.Abs(1 - c * c / (a * a + b * b)) <= precision;
        }
    }
}