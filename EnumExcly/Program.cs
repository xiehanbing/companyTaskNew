using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EnumExcly
{
    public enum QQStatus
    {
        OnLine,
        OffLine,Leave,
        Busy,
        Qme
    }

    class ThreadSample
    {
        private bool _isStoped = false;

        public void Stop()
        {
            _isStoped = true;
        }

        public void CountNumbers()
        {
            long counter = 0;
            while (!_isStoped)
            {
                counter++;
            }

            Console.WriteLine($"{Thread.CurrentThread.Name} with {Thread.CurrentThread.Priority}  priority {counter:N0}");
        }
    }

    class ThreadSample2
    {
        private readonly int _iterations;

        public ThreadSample2(int iterations)
        {
            _iterations = iterations;
        }

        public void CountNumbers()
        {
            for (int i = 0; i < _iterations; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));

                Console.WriteLine($"{Thread.CurrentThread.Name} prints {i}");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //枚举转换  如果转换不了  则 直接 赋值 

            Console.WriteLine("Starting...");
            //for(int i=1;i<10;i++)
            //{
            //    Console.WriteLine(i);
            //}
            //Thread t = new Thread(PrintNumbersDelay);
            //t.Start();
            //Thread.Sleep(TimeSpan.FromSeconds(6));
            //t.Abort();
            //Console.WriteLine("A thread has been aborted");
            //Thread t = new Thread(PrintNumbers);
            //t.Start()
            //t.Join();
            //PrintNumbers();
            //Console.WriteLine("Thread completed");


            //var sample=new ThreadSample();
            //var threadOne=new Thread(sample.CountNumbers);
            //threadOne.Name = "TheadOne";
            //var threadTwo=new Thread(sample.CountNumbers);
            //threadTwo.Name = "TheadTwo";

            //threadOne.Priority = ThreadPriority.Lowest;
            //threadTwo.Priority = ThreadPriority.Highest;//高优先级 先执行
            //threadOne.Start();
            //threadTwo.Start();
            //Thread.Sleep(TimeSpan.FromSeconds(2));

            //sample.Stop();


            //var sampleForgeground=new ThreadSample2(10);

            //var sampleBackgrount=new ThreadSample2(10);

            //var threadOne=new Thread(sampleForgeground.CountNumbers);

            //var threadTwo=new Thread(sampleBackgrount.CountNumbers);

            //threadOne.Name = "Forgeground";

            //threadTwo.Name = "Backgrount";

            //threadTwo.IsBackground = true;

            //threadOne.Start();

            //threadTwo.Start();

            //Console.WriteLine($"Current thread priority:{Thread.CurrentThread.Priority}");

            //Console.WriteLine("Runing on all cores available");

            //RunThreads();

            //var t1=new Thread(()=>TravelThroughGates("thread1",5));

            //var t2=new Thread(()=>TravelThroughGates("thread2",6));

            //var t3 = new Thread(() => TravelThroughGates("thread3", 12));

            //t1.Start();

            //t2.Start();

            //t3.Start();

            //Thread.Sleep(TimeSpan.FromSeconds(6));

            //Console.WriteLine($"the gates are now open!");

            //_mainSlim.Set();

            //Thread.Sleep(TimeSpan.FromSeconds(2));

            //_mainSlim.Reset();

            //Console.WriteLine("the gates have been closed!");

            //Thread.Sleep(TimeSpan.FromSeconds(10));

            //Console.WriteLine("the gates are now open for the secondtime");

            //_mainSlim.Set();
            //Thread.Sleep(TimeSpan.FromSeconds(2));

            //Console.WriteLine("the gates have been closd!");

            //_mainSlim.Reset();

            //Console.WriteLine("starting two operations");

            //var t1=new Thread(()=>PerformOperation("operatopn 1 is completed",4));


            //var t2=new Thread(()=>PerformOperation("operation 2 is completed",8));

            //t1.Start();

            //t2.Start();

            //_countDown.Wait();

            //Console.WriteLine("both operations have been completed.");
            //_countDown.Dispose();
            //var t1=new Thread(()=>PlayMusic("the guitarist","play an amazing solo",2));

            //var t2 = new Thread(() => PlayMusic("the singer", "sing his song", 2));

            //t1.Start();

            //t2.Start();

            //int threadId = 0;
            //AsyncProgramModelService.RunOnThreadPool poolDelegate = AsyncProgramModelService.Test;

            //var t=new Thread(()=>AsyncProgramModelService.Test(out threadId));

            //t.Start();

            //t.Join();

            //Console.WriteLine("main thread id :{0}",threadId);

            //IAsyncResult ar = poolDelegate.BeginInvoke(out threadId, AsyncProgramModelService.Callback,
            //    poolDelegate);
            //string result = poolDelegate.EndInvoke(out threadId, ar);

            //Console.WriteLine("thread pool worker thread id :{0}",threadId);

            //Console.WriteLine("result:"+result);

            //Thread.Sleep(TimeSpan.FromSeconds(2));


            //int x = 1;
            //int y = 2;
            //string lambdaState = "lambda state 2";

            //ThreadPool.QueueUserWorkItem(AsyncProgramModelService.AsyncOperation);

            //Thread.Sleep(TimeSpan.FromSeconds(1));

            //ThreadPool.QueueUserWorkItem(AsyncProgramModelService.AsyncOperation, "async state");

            //Thread.Sleep(TimeSpan.FromSeconds(1));

            //ThreadPool.QueueUserWorkItem(state =>
            //{
            //    Console.WriteLine("operation state :{0}", state);
            //    Console.WriteLine("worker thread id:{0}", Thread.CurrentThread.ManagedThreadId);

            //    Thread.Sleep(TimeSpan.FromSeconds(2));
            //},"lambda state");

            //ThreadPool.QueueUserWorkItem(_ =>
            //{
            //    Console.WriteLine("operation state :{0},{1}", x+y,lambdaState);

            //    Console.WriteLine("worker thread id:{0}", Thread.CurrentThread.ManagedThreadId);

            //    Thread.Sleep(TimeSpan.FromSeconds(2));
            //}, "lambda state");


            //ThreadPool.QueueUserWorkItem(data =>
            //{
            //    var info = (TestPoolModel) (data);
            //    Console.WriteLine("operation state :{0},{1}", info.X1 + info.Y1+info.Z1, lambdaState);

            //    Console.WriteLine("worker thread id:{0}", Thread.CurrentThread.ManagedThreadId);

            //    Thread.Sleep(TimeSpan.FromSeconds(2));
            //}, new TestPoolModel()
            //{
            //    X1 = 1,
            //    Y1 = 2,
            //    Z1 = 3
            //});

            //Thread.Sleep(TimeSpan.FromSeconds(2));

            ////定义500个线程
            //int numberOfOperations = 500;
            //int workthread = 0;
            //int actiove = 0;
            //ThreadPool.GetMaxThreads(out workthread, out actiove);
            //Console.WriteLine("pool   workthread:{0}  complete:{1}",workthread,actiove);

            //var sw = new Stopwatch();

            //sw.Start();

            //AsyncProgramModelService.UseThreads(numberOfOperations);

            //sw.Stop();

            //Console.WriteLine("execution time using threads :{0}",sw.ElapsedMilliseconds);

            //sw.Reset();
            //sw.Start();

            //AsyncProgramModelService.UseThreadPool(numberOfOperations);

            //sw.Stop();

            //Console.WriteLine("threadpool  execution time using threads :{0}", sw.ElapsedMilliseconds);

            //using (var cts=new CancellationTokenSource())
            //{
            //    CancellationToken token = cts.Token;

            //    ThreadPool.QueueUserWorkItem(_ => AsyncProgramModelService.AsyncOperation1(token));
            //    Thread.Sleep(TimeSpan.FromSeconds(2));

            //    cts.Cancel();
            //}

            //using (var cts = new CancellationTokenSource())
            //{
            //    CancellationToken token = cts.Token;

            //    ThreadPool.QueueUserWorkItem(_ => AsyncProgramModelService.AsyncOperation2(token));
            //    Thread.Sleep(TimeSpan.FromSeconds(2));

            //    cts.Cancel();
            //}

            //using (var cts = new CancellationTokenSource())
            //{
            //    CancellationToken token = cts.Token;

            //    ThreadPool.QueueUserWorkItem(_ => AsyncProgramModelService.AsyncOperation3(token));
            //    Thread.Sleep(TimeSpan.FromSeconds(2));

            //    cts.Cancel();
            //}


            //Thread.Sleep(TimeSpan.FromSeconds(2));

            //AsyncProgramModelService.RunOperations(TimeSpan.FromSeconds(5));

            //AsyncProgramModelService.RunOperations(TimeSpan.FromSeconds(7));

            //Console.WriteLine("press 'enter'  to stop the timer...");

            //DateTime start =DateTime.Now;

            //_timer = new Timer(_ => AsyncProgramModelService.TimerOperation(start), null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));

            //Thread.Sleep(TimeSpan.FromSeconds(6));

            //_timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(4));

            //Console.ReadLine();

            //_timer.Dispose();

            //var bw = new BackgroundWorker();
            //bw.WorkerReportsProgress = true;
            //bw.WorkerSupportsCancellation = true;
            //bw.DoWork += AsyncProgramModelService.Worker_DoWork;
            //bw.ProgressChanged += AsyncProgramModelService.Worker_ProgressChanger;

            //bw.RunWorkerCompleted += AsyncProgramModelService.Worker_Completed;

            //bw.RunWorkerAsync();

            //Console.WriteLine("press c to cancel work");

            //do
            //{
            //    if (Console.ReadKey(true).KeyChar == 'C')
            //    {
            //        bw.CancelAsync();

            //    }
            //} while (bw.IsBusy);

            //var firstTask=new Task<int>(()=>TPLService.TaskMethod("first task",3));

            //var secondTask = new Task<int>(() => TPLService.TaskMethod("second task", 2));

            //firstTask.ContinueWith(t=>)

            //Task t = TaskService.AsynchronyWithTPL();

            //t.Wait();

            //t = TaskService.AsynchronyWithAwait();
            //t.Wait();

            //Task t = TaskService.AsynchronousProcessing();
            //t.Wait();

            //Task t = TaskService.AsyncTask();
            //t.Wait();

            //TaskService.AsyncVoid();

            //Thread.Sleep(TimeSpan.FromSeconds(3));

            //t = TaskService.AsyncTaskWithErrors();

            //while (!t.IsFaulted)
            //{
            //    Thread.Sleep()
            //}

         Task t=   AwaitableService.AsynchorousProcessing();
            t.Wait();
            Console.ReadKey();
        }

        private static Label _label;
        async static void Click(object sender, EventArgs e)
        {
            _label.Content = new TextBlock { Text = "Calculating...." };
            //TimeSpan resultWithContext = await Test();
        }

        public static Timer _timer;
        public class TestPoolModel
        {
            public int  X1 { get; set; }
            public int Y1 { get; set; }
            public int Z1 { get; set; }
        }

        static Barrier _barrier =new Barrier(2,b=>Console.WriteLine($"end of phase {b.CurrentPhaseNumber+1}"));
        static void PlayMusic(string name, string message, int seconds)
        {
            for (int i = 0;  i < 3;  i++)
            {
                Console.WriteLine("----------------------------------");
                Thread.Sleep(TimeSpan.FromSeconds(seconds));

                Console.WriteLine($"{name} starts to {message}");

                Thread.Sleep(TimeSpan.FromSeconds(seconds));

                Console.WriteLine($"{name} finishes to {message}");

                _barrier.SignalAndWait();
            }
        }



        static  CountdownEvent _countDown=new CountdownEvent(2);

        static void PerformOperation(string message, int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));

            Console.WriteLine(message);

            _countDown.Signal();
        }


        static ManualResetEventSlim _mainSlim =new ManualResetEventSlim(false);

        static void TravelThroughGates(string threadName, int seconds)
        {
            Console.WriteLine($"{threadName} falls to sleep");

            Thread.Sleep(TimeSpan.FromSeconds(seconds));

            Console.WriteLine($"{threadName} waits for the gates to open!");

            _mainSlim.Wait();

            Console.WriteLine($"{threadName} enters the gates!");
        }


        static void PrintNumbers()
        {
            Console.WriteLine("Starting...");
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }
        static void PrintNumbersDelay()
        {
            Console.WriteLine("Starting...");
            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(i);
            }
        }
    }
}
