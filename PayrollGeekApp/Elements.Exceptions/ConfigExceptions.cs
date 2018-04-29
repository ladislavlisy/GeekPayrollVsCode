using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Elements.Exceptions
{
    class NoneExistingConfig : NotImplementedException
    {
        public const string EXCEPTION_TEXT = "Config Collection doesn't exist!";
        public NoneExistingConfig() : base(EXCEPTION_TEXT)
        {
        }
    }
}
