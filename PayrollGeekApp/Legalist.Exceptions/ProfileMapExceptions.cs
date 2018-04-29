using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Legalist.Exceptions
{
    class OutOfRangeYear : NotImplementedException
    {
        public const string EXCEPTION_TEXT = "Out of rage year";
        public OutOfRangeYear() : base(EXCEPTION_TEXT)
        {
        }
    }

    class NotImplementedYear : NotImplementedException
    {
        public const string EXCEPTION_TEXT = "Not implemented year";
        public NotImplementedYear() : base(EXCEPTION_TEXT)
        {
        }
    }
}
