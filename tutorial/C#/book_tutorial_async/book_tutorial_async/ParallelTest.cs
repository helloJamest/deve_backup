using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace book_tutorial_async
{
    class ParallelTest
    {
        /// <summary>
        /// echo number
        /// </summary>
        static void Ex1()
        {
            Parallel.For(0, 15, i => Console.WriteLine("The square of{0} is {1}", i, i * i));
        }

        /// <summary>
        /// fill in list
        /// </summary>
        static void Ex2()
        {
            const int maxValues = 50;
            int[] squares = new int[maxValues];

            Parallel.For(0, maxValues, i => squares[i] = i * i);
        }

        /// <summary>
        /// echo the count of string
        /// </summary>
        static void Ex3()
        {
            const int maxValues = 50;
            string[] squares = { "We", "hold", "these", "truths", "to", "be", "self-evident", "that", "all", "men" };

            Parallel.ForEach(squares,i=>Console.WriteLine(string.Format("{0} has {1} letters",i,i.Length)));                
        }
    }
}
