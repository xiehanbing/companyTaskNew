using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnumExcly
{
   public class TPLService
    {
        public static Task<int> CreateTask(string name)
        {
            return  new Task<int>(()=> 40);
        }


        public static int TaskMethod(string name, int seconds)
        {
            Console.WriteLine("task {0} is running on a thread id {1}  is thread pool thread :{2}",name,Thread.CurrentThread.ManagedThreadId,Thread.CurrentThread.IsThreadPoolThread);

            Thread.Sleep(TimeSpan.FromSeconds(seconds));

            return 42 * seconds;
        }


    }
}
