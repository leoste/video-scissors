using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors
{
    public static class Exceptions
    {
        public static ArgumentException EndTooBigException { get { return new ArgumentException("End position must be smaller than timeline length."); } }
        public static ArgumentException EndTooSmallException { get { return new ArgumentException("End position must be larger than start position."); } }
        public static ArgumentException LengthTooSmallException { get { return new ArgumentException("Length must be at least 1."); } }
        public static ArgumentException StartTooSmallException { get { return new ArgumentException("Start position can't be below 0."); } }
    }
}
