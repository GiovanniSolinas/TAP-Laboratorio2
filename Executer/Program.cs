using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyLibrary;

namespace Executer
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = Assembly.LoadFrom("MyLibrary.dll");
            foreach (var type in a.GetTypes())
            {
                if (type.IsClass)
                {
                    var o = Activator.CreateInstance(type);
                    Foo pippo = (Foo) o;
                    var methods = type.GetMethods();
                    foreach (var method in methods)
                    {
                        Console.WriteLine(method.Name);
                    }
                }
            }
            Console.ReadLine();
        }
            
    }
}
