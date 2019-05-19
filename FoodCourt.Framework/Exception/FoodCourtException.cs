using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework
{
    public class FoodCourtException : Exception
    {
        public FoodCourtException()
        {
        }

        public FoodCourtException(string message) : base(message)
        {
        }

        public FoodCourtException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
