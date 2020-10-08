using System;
using System.Threading.Tasks;

namespace book_tutorial_async
{
    class MyClass
    {
        public int Get10()
        {
            return 10;
        }

        public async Task DoWorkAsync()
        {
            Func<int> ten = new Func<int>(Get10);
            int a = await Task.Run(ten);

            int b = await Task.Run(new Func<int>(Get10));//创建Func<int>委托

            int c = await Task.Run(() => { return 10; }); //Lambda表达式
            Console.WriteLine("{0} {1} {2}", a, b, c);

            //4种不同的委托类型所表示的方法
            await Task.Run(() => Console.WriteLine(5.ToString()));//Action
            Console.WriteLine((await Task.Run(() => 6)).ToString());//TResult Func()
            await Task.Run(() => Task.Run(() => Console.WriteLine(7.ToString())));//Task Func()
            Console.WriteLine((await Task.Run(() => Task.Run(() => 8))).ToString()); //Task<TRsult> Func()
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Task t = (new MyClass()).DoWorkAsync();
            t.Wait();
            Console.WriteLine("Hello World!");
        }
    }
}
