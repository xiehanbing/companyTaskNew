using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnumExcly
{
    public class AwaitableService
    {

        public static async Task AsynchorousProcessing()
        {
            var sync = new CustomAwaitable(true);
            string result = await sync;
            Console.WriteLine(result);

            var async=new CustomAwaitable(false);
            result = await async;
            Console.WriteLine(result);
        }
    }

    public class CustomAwaitable
    {
        private readonly bool _completeSynchronously;
        public CustomAwaitable(bool completeSynchronously)
        {
            _completeSynchronously = completeSynchronously;
        }

        public CustomAwaiter GetAwaiter()
        {
            return  new CustomAwaiter(_completeSynchronously);
        }
    }

    public class CustomAwaiter : INotifyCompletion
    {
        private string _result = "Completed synchronously";
        private readonly bool _completeSynchronously;
        public bool IsCompleted => _completeSynchronously;

        public CustomAwaiter(bool completeSynchronously)
        {
            _completeSynchronously = completeSynchronously;
        }

        public string GetResult()
        {
            return _result;
        }

        public void OnCompleted(Action continuation)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {

                Thread.Sleep(TimeSpan.FromSeconds(1));
                _result = GetInfo();
                continuation?.Invoke();
            });
        }

        public string GetInfo()
        {
            return string.Format("Task is running on a thread id {0}, is thread pool thread:{1}",
                Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        }
    }
}
