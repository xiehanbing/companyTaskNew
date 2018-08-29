using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnumExcly
{
    /// <summary>
    /// 异步编程模式
    /// </summary>
    public class AsyncProgramModelService
    {
        public delegate string RunOnThreadPool(out int threadId);

        public static void Callback(IAsyncResult ar)
        {
            Console.WriteLine("Callback starting a callback...");

            Console.WriteLine("Callback state passed to a callback{0}", ar.AsyncState);

            Console.WriteLine("Callback is thread pool thread:{0}", Thread.CurrentThread.IsThreadPoolThread);

            Console.WriteLine("Callback thread pool worker thread id :{0}", Thread.CurrentThread.ManagedThreadId);
            //int threadId = 0;
            //string result =(ar.AsyncState as RunOnThreadPool) .EndInvoke(out threadId, ar);

            //Console.WriteLine("call result:{0}",result);
        }

        public static string Test(out int threadId)
        {
            Console.WriteLine(" Test starting ...");
            Console.WriteLine(" Test is thread pool thread:{0}", Thread.CurrentThread.IsThreadPoolThread);

            Thread.Sleep(TimeSpan.FromSeconds(2));

            threadId = Thread.CurrentThread.ManagedThreadId;
            return string.Format("Test thread poo; worker thread id was:{0}", threadId);
        }

        /// <summary>
        /// 异步操作
        /// </summary>
        /// <param name="state"></param>
        public static void AsyncOperation(object state)
        {
            Console.WriteLine("operation state:{0}", state ?? "(null)");

            Console.Write("worker thread id:{0} ,", Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        /// <summary>
        /// 使用线程
        /// </summary>
        /// <param name="numberOfOperations"></param>
        public static void UseThreads(int numberOfOperations)
        {
            using (var countDown = new CountdownEvent(numberOfOperations))
            {
                Console.WriteLine("scheduling work by creating threads");

                for (int i = 0; i < numberOfOperations; i++)
                {
                    var thread = new Thread(() =>
                      {
                          Console.Write("{0},", Thread.CurrentThread.ManagedThreadId);

                          Thread.Sleep(TimeSpan.FromSeconds(0.1));

                          countDown.Signal();
                      });
                    thread.Start();

                }

                countDown.Wait();
                Console.WriteLine();
            }
        }

        public static void UseThreadPool(int numberOfOperations)
        {
            using (var countDown = new CountdownEvent(numberOfOperations))
            {
                Console.WriteLine("starting work on a threadpool");

                for (int i = 0; i < numberOfOperations; i++)
                {
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        Console.Write("{0},", Thread.CurrentThread.ManagedThreadId);

                        Thread.Sleep(TimeSpan.FromSeconds(0.1));

                        countDown.Signal();

                    });
                }

                countDown.Wait();
                Console.WriteLine();
            }
        }



        public static void AsyncOperation1(CancellationToken token)
        {
            Console.WriteLine("starting the first task");

            for (int i = 0; i < 5; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("the first task has been canceled");
                    return;
                }


            }
            Console.WriteLine("the first task has completed successfully");
        }


        public static void AsyncOperation2(CancellationToken token)
        {
            try
            {
                Console.WriteLine("starting the second task");

                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                Console.WriteLine("the second task has completed successfully");
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("the second task has been canceld ex");
            }
        }

        public static void AsyncOperation3(CancellationToken token)
        {
            // 是否取消
            bool cancellationFlag = false;

            token.Register(() => cancellationFlag = true);

            Console.WriteLine("starting the third task");

            for (int i = 0; i < 5; i++)
            {
                if (cancellationFlag)
                {
                    Console.WriteLine("the third task has been canceled");
                    return;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            Console.WriteLine("the third task has completed succesfully");

        }



        public static void RunOperations(TimeSpan workerOperationTimeOut)
        {
            using (var evt = new ManualResetEvent(false))
            using (var cts = new CancellationTokenSource())
            {
                Console.WriteLine("registering timeout operaions...");

                var worker = ThreadPool.RegisterWaitForSingleObject(evt,
                    (state, isTimeOut) => WorkerOperationWait(cts, isTimeOut), null, workerOperationTimeOut, true);

                Console.WriteLine("starting long running operation...");

                ThreadPool.QueueUserWorkItem(_ => WorkerOperation(cts.Token, evt));

                Thread.Sleep(workerOperationTimeOut.Add(TimeSpan.FromSeconds(2)));

                worker.Unregister(evt);
            }
        }

        public static void WorkerOperation(CancellationToken token, ManualResetEvent evt)
        {
            for (int i = 0; i < 6; i++)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            evt.Set();
        }


        public static void WorkerOperationWait(CancellationTokenSource cts, bool isTimeOut)
        {
            if (isTimeOut)
            {
                cts.Cancel();

                Console.WriteLine("worker operation timed out and was canceled.");
            }
            else
            {

                Console.WriteLine("worker operation succeded.");
            }
        }



        public static void TimerOperation(DateTime startTime)
        {
            TimeSpan elaSpan = DateTime.Now - startTime;

            Console.WriteLine("{0} seconds ftom {1} . timer thread pool thread id :{2}", elaSpan.Seconds, startTime, Thread.CurrentThread.ManagedThreadId);
        }


        public static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("dowork thread pool thread id:{0}", Thread.CurrentThread.ManagedThreadId);

            var bw = (BackgroundWorker)sender;

            for (int i = 0; i <= 100; i++)
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                if (i % 10 == 0)
                {
                    bw.ReportProgress(i);
                }
                Thread.Sleep(TimeSpan.FromSeconds(0.1));
            }

            e.Result = 42;
        }


        public static void Worker_ProgressChanger(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("{0} % completed. progress thread pool thread id :{1}", e.ProgressPercentage, Thread.CurrentThread.ManagedThreadId);
        }

        public static void Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("completed thread pool thread id:{0}", Thread.CurrentThread.ManagedThreadId);

            if (e.Error != null)
            {
                Console.WriteLine("exception {0} has occured.", e.Error.Message);
            }
            else if (e.Cancelled)
            {
                Console.WriteLine("operation has been canceled.");
            }
            else
            {
                Console.WriteLine("the answer is:{0}", e.Result);
            }
        }
    }
}
