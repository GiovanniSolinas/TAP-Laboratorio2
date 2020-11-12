using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyAttribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ExecuteMeAttribute : Attribute
    {
        public object[] Arguments { get; }

        public ExecuteMeAttribute(params object[] arguments)
        {
            Arguments = arguments;
        }
    }
}
