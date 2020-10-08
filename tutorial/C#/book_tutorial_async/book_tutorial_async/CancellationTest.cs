using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace book_tutorial_async
{
    class CancellationTest
    {
        static void cancell() {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var mc = new MyClass2();
            Task t = mc.RunAsync(token);

            //Thread.Sleep(3000)
            //cts.Cancel(); //取消操作

            t.Wait();//等待Task对象完成
            Console.WriteLine("Was Cancelled:{o}", token.IsCancellationRequested);
        }
    }

    class MyClass2
    {
        public async Task RunAsync(CancellationToken ct)
        {
            if (ct.IsCancellationRequested) return;
            await Task.Run(() => CycleMethod(ct), ct);
        }

        void CycleMethod(CancellationToken ct)
        {
            Console.WriteLine("Starting CycleMethod");
            const int max = 5;
            for (int i = 0; i < max; i++) 
            {
                if (ct.IsCancellationRequested) return;
                Thread.Sleep(1000);
                Console.WriteLine("{0} of {1} iterations completed", i + 1, max);
            }
        }
    }
}


