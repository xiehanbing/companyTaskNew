using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnumExcly
{
    public class TaskService
    {
        //public static Task AsynchronyWithTPL()
        //{
        //    var containerTask = new Task(() =>
        //      {
        //          Task<string> t = GetInfoAsync("TPL 1");
        //          t.ContinueWith(task =>
        //          {
        //              Console.WriteLine(t.Result);
        //              Task<string> t2 = GetInfoAsync("TPL 2");
        //              t2.ContinueWith(innerTask => Console.WriteLine(innerTask.Result), TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.AttachedToParent);
        //              t2.ContinueWith(innerTask => Console.WriteLine(innerTask.Exception.InnerException), TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.AttachedToParent);

        //          }, TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.AttachedToParent);
        //          t.ContinueWith(task => Console.WriteLine(t.Exception.InnerException), TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.AttachedToParent);

        //      });
        //    containerTask.Start();

        //    return containerTask;
        //}

        //public async static Task AsynchronyWithAwait()
        //{
        //    try
        //    {
        //        string result = await GetInfoAsync("Async 1");
        //        Console.WriteLine(result);
        //        result = await GetInfoAsync("Async 2");
        //        Console.WriteLine(result);
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex);
        //    }
        //}

        //public async static Task<string> GetInfoAsync(string name)
        //{
        //    Console.WriteLine("task {0} started!", name);
        //    await Task.Delay(TimeSpan.FromSeconds(2));
        //    if (name == "TPL 2")
        //    {
        //        throw new Exception("bOOM!");
        //    }
        //    return string.Format("task {0} is running on a thread id {1} . is thread pool thread:{2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        //}



        //public async static Task AsynchronousProcessing()
        //{
        //    Task<string> t1 = GetInfoAsyncV2("task 1", 3);
        //    Task<string> t2 = GetInfoAsyncV2("task 2", 5);
        //    string[] results = await Task.WhenAll(t1, t2);


        //    foreach (string result in results)
        //    {
        //        Console.WriteLine(result);
        //    }
        //}

        //public async static Task<string> GetInfoAsyncV2(string name, int seconds)
        //{
        //     await Task.Delay(TimeSpan.FromSeconds(seconds));
        //    //await Task.Run(() => Thread.Sleep(TimeSpan.FromSeconds(seconds)));
        //    return string.Format("task {0} is running on a thread id {1}. is thread pool thread:{2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        //}

        public async static Task AsynchronousProcessing()
        {
            Console.WriteLine("1. Single exception");

            try
            {
                string result = await GetInfoAsyncV3("task 1", 2);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception details:{0}", ex);
                //throw;
            }

            Console.WriteLine();

            Console.WriteLine("2. Multiple exceptions");

            Task<string> t1 = GetInfoAsyncV3("task 1", 3);
            Task<string> t2 = GetInfoAsyncV3("task 2", 2);

            try
            {
             
                string[] results = await Task.WhenAll(t1, t2);
                Console.WriteLine(results.Length);

            }
            catch (Exception ex)
            {

                Console.WriteLine("exception details :{0}", ex);
            }

            Console.WriteLine();

            Console.WriteLine("3. Multiple exceptions with AggregateException");

            t1 = GetInfoAsyncV3("task 1", 3);

            t2 = GetInfoAsyncV3("task 2", 2);
            Task<string[]> t3 = Task.WhenAll(t1, t2);
            try
            {
                string[] results = await t3;
                Console.WriteLine(results.Length);
            }
            catch (Exception ex)
            {
                var ae = t3.Exception.Flatten();
                var exceptioms = ae.InnerExceptions;
                Console.WriteLine("exceptions caught:{0}", exceptioms.Count);
                foreach (var e in exceptioms)
                {
                    Console.WriteLine("exception details:{0}", e);
                    Console.WriteLine();
                }
               // throw;
            }
        }

        public async static Task<string> GetInfoAsyncV3(string name, int seconds)
        {
            await Task.Delay(TimeSpan.FromSeconds(seconds));

            throw new Exception(string.Format("boom from {0}", name));
        }


        public async static Task AsyncTaskWithErrors()
        {
            string result = await GetInfoAsyncV4("AsyncTaskException", 2);

            Console.WriteLine(result);
        }

        public async static Task<string> GetInfoAsyncV4(string name, int seconds)
        {
            await Task.Delay(TimeSpan.FromSeconds(seconds));

            if (name.Contains("Exception"))
            {
                throw new Exception(string.Format("Boom from {0}!", name));
            }
            return string.Format("task {0} is running on a thread id {1}. is thread pool thread:{2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        }

        public async static void AsyncVoidWithErrors()
        {
            string result = await GetInfoAsyncV4("AsyncVoidException", 2);
            Console.WriteLine(result);
        }

        public async static Task AsyncTask()
        {
            string result = await GetInfoAsyncV4("AsyncTask", 2);
            Console.WriteLine(result);
        }

        public async static void AsyncVoid()
        {
            string result = await GetInfoAsyncV4("AsyncVoid", 2);
            Console.WriteLine(result);
        }
    }
}
