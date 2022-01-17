using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque
{
    /*
        Generic Programming

        One of the problems with OOP is a feature called "code bloat". One type of code bloat occurs when you have to override a method, or a set of methods, to take into
        account all of the possible data types of the method's parameters. One solution to code bloat is the ability of one value to make on multiple data types, while only
        providing one definition of that value. This technique is called generic programming.

        A generic program provides a data type "placeholder" that is filled in by a specified data type at compile-time. This placeholder is represented by a pair of angle
        brackets(<>), with a identifier placed between the brackets. Let's look at an example.

        A canonical first example for generic programming is the Swap function. Here is the definition of a generic Swap function in C#
         
         */
    class TipoGenerico
    {
        static void Swap<T>(ref T val1, ref T val2)
        {
            T temp;
            temp = val1;
            val1 = val2;
            val2 = temp;
        }

        /*The  placeholder for the data type is placed immediately after the function name. The identifier placed inside the angle brackets is now used whenever a generic
         data type is needed. Each of the parameter is assigned a generic data type, as is the temp variable used to make the swap. Here's a program that tests this code:*/

        static void main()
        {
            int num1 = 100;
            int num2 = 200;
            Console.WriteLine("num: " + num1);
            Console.WriteLine("num: " + num2);
            Swap<int>(ref num1, ref num2);
            Console.WriteLine("num: " + num1);
            Console.WriteLine("num: " + num2);
            string str1 = "Sam";
            string str2 = "Tom";
            Console.WriteLine("String 1: " + str1);
            Console.WriteLine("String 2: " + str2);
            Swap<string>(ref str1, ref str2);
            Console.WriteLine("String 1: " + str1);
            Console.WriteLine("String 2: " + str2);

        }

        /* Generics are not limited to function definitions, you can also create generic classes. A generic class definition will contain a generic type placeholder after
         the class name. Anytime the class name is referenced in the definition, the type placeholder must be provided. The following class definition demonstrates
         how to create a generic class:
         */
         public class Pessoa<T>
        {
            T data;
            Pessoa<T> link;

            public Pessoa(T data, Pessoa<T> link)
            {
                this.data = data;
                this.link = link;
            }
        }

        //This class can be used as follows
        Pessoa<string> pessoa = new Pessoa<string>("Mike", null);
        Pessoa<string> pessoa02 = new Pessoa<string>("Michael", null);
    }
}
