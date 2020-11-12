using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyAttribute;

namespace MyLibrary
{
    public class WithoutDefaultConstructor
    {
        WithoutDefaultConstructor(int x)
        {
        }

        [ExecuteMe()]
        public static void Static1()
        {
            Console.WriteLine($"{nameof(Static1)}");
        }

        [ExecuteMe()]
        [ExecuteMe()]
        public void NoNo1()
        {
            Console.WriteLine($"{nameof(NoNo1)} should never be seen");
        }

        [ExecuteMe()]
        public static void Static2()
        {
            Console.WriteLine($"{nameof(Static2)}");
        }
    }

    public class Foo
    {
        [ExecuteMe]
        public void M1()
        {
            Console.WriteLine("M1");
        }

        [ExecuteMe(3)]
        public void SubTypeExample(object x)
        {
            Console.WriteLine($"{nameof(SubTypeExample)} on {x}");
        }

        [ExecuteMe(45)]
        [ExecuteMe("tre")]
        [ExecuteMe(0)]
        [ExecuteMe(3,4)]
        [ExecuteMe(3)]
        [ExecuteMe()]
        public void M2(int a)
        {
            Console.WriteLine("M2 a = {0}", a);
        }
        [ExecuteMe("hello", "reflection")]
        public void M3(String s1, String s2)
        {
            Console.WriteLine("M3 s1 = {0} s2 = {1}", s1, s2);
        }
    }
}
