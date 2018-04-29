using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsLib.Module.Interfaces.Permadom
{
    using ConfigRole = UInt16;
    using SymbolName = String;
    public class ArticleRoleConfigData
    {
        public ConfigRole Role { get; set; }
        public SymbolName Name { get; set; }
        public ConfigRole[] Path { get; set; }

        public ArticleRoleConfigData(ConfigRole _role, SymbolName _name, params ConfigRole[] _path)
        {
            Role = _role;
            Name = _name;
            Path = _path.ToArray();
        }
        public override string ToString()
        {
            return string.Format("{0}, \"{1}\", {2}", Role.ToString(), 
                Name, string.Join(", ", Path));
        }
    }
}
