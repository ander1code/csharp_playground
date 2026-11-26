using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading; // Para trabalhar com Thread em C#.

using System.Collections.Concurrent; // ConcurrentBag<>

namespace ConsoleApp
{
    public class ThreadingInCSharp
    {
        #region DoThis
        public static void DoThis(string name, int milliseconds)
        {
            Console.WriteLine("Starting {0} ...", name);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0}: {1}", name, i);
                Thread.Sleep(milliseconds);
            }
            Console.WriteLine("{0} finished!", name);
        }
        private static void DoThisOneObject(object state)
        {
            string name = (string)state; // converto o unico objeto passado: name

            Console.WriteLine("Starting {0} ...", name);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0}: {1}", name, i);
                Thread.Sleep(500);
            }
            Console.WriteLine("{0} finished!", name);
        }

        private static void DoThisOneClassObject(object state)
        {
            ThreadData threaddata = (ThreadData)state; // conversão

            Console.WriteLine("Starting {0} ...", threaddata.name);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0}: {1}", threaddata.name, i);
                Thread.Sleep(threaddata.milliseconds);
            }
            Console.WriteLine("{0} finished!", threaddata.name);
        }

        // _lock: (variavel que vai no lock(){}: simplesmente um objeto usado como "chave" de sincronização.
        private static readonly object _lock = new object();

        private static void DoThisWithLock(string name, int milliseconds)
        {
            lock (_lock)
            {
                Console.WriteLine("Starting {0} ...", name);
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("{0}: {1}", name, i);
                    Thread.Sleep(milliseconds);
                }
                Console.WriteLine("{0} finished!", name);
            }
        }

        // Semaphore:
        //     Initializes a new instance of the System.Threading.Semaphore class, specifying
        //     the initial number of entries and the maximum number of concurrent entries.
        private static readonly Semaphore semaphore = new Semaphore(3, 3);
        
        public static void DoThisWithSemaphore(string name, int milliseconds)
        {
            // WaitOne:
            //     Blocks the current thread until the current System.Threading.WaitHandle receives
            //     a signal.
            semaphore.WaitOne();
            
            try
            {
                Console.WriteLine("Starting {0} ...", name);
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("{0}: {1}", name, i);
                    Thread.Sleep(milliseconds);
                }
                Console.WriteLine("{0} finished!", name);
            }
            finally
            {
                // Summary:
                //     Exits the semaphore and returns the previous count.
                semaphore.Release();
            }
        }

        private static int counter = 0;

        public static void Increment()
        {
            counter++;
        }

        /*
         * lock controla o acesso a um recurso/bloco de código através de um objeto 
         * de sincronização compartilhado. 
         */
        private static readonly object _lock_safety = new object();
        public static void IncrementSafety()
        {
            lock (_lock_safety)
            {
                counter++;
            }
        }

        // Summary:
        //     Represents a thread-safe, unordered collection of objects.
        //     ConcurrentQueue<T> do .NET é conceitualmente bem parecido com queue.Queue do Python.
        public static readonly ConcurrentBag<string> results = new ConcurrentBag<string>();

        public static void DoThisConcurrentBag(string name, int milliseconds)
        {
            Console.WriteLine("Starting {0} ...", name);
            for (int i = 0; i < 10; i++)
            {
                // Console.WriteLine("{0}: {1}", name, i);
                // Thread.Sleep(milliseconds);

                // passando mensagem para ConcurrentBag...
                results.Add(String.Format("{0}: {1}", name, i));
            }
            // Console.WriteLine(String.Format("{0} finished!", name);
            results.Add(String.Format("{0} finished!", name));
        }

        #endregion

        #region Methods
        // MainThread
        public static void mainthread_expection()
        {
            Console.WriteLine(Thread.CurrentThread);
            Console.WriteLine(Thread.CurrentThread.Name);
            Console.WriteLine(Thread.CurrentThread.Priority);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }

        // Unique Thread;
        public static void unique_thread_execution()
        {
            Thread thread = new Thread(() => DoThis("custom_thread", 500));
            thread.Name = "custom_thread";
            Console.WriteLine(thread.IsAlive);

            // Start:
            //     Causes the operating system to change the state of the current instance to
            //     System.Threading.ThreadState.Running.
            thread.Start();
            Console.WriteLine(thread.IsAlive);

            // Join:
            //     Blocks the calling thread until the thread represented by this instance terminates,
            //     while continuing to perform standard COM and SendMessage pumping.
            thread.Join();
            Console.WriteLine(thread.IsAlive);
        }

        //Foreground and Background:
        public static void ForegroundAndBackgroundThreads()
        {
            /*
                thread.IsBackground X Daemon do Python:
                Bastante parecido com o conceito de daemon do Python, mas há algumas diferenças importantes.
                O processo termina quando não existem mais foreground threads executando.
            
                Porém, seu Join() muda tudo: "Main, espere essa thread terminar."
            */

            Thread thread1 = new Thread(() => DoThis("thread_01", 300));
            Thread thread2 = new Thread(() => DoThis("thread_02", 1000));

            thread1.IsBackground = true;
            thread2.IsBackground = false;

            thread1.Start();
            thread2.Start();

            Console.WriteLine(thread1.IsAlive);
            Console.WriteLine(thread2.IsAlive);

            thread1.Join();
            thread2.Join();

            Console.ReadKey();
        }

        public static void ThreadPriorityDemonstration()
        {
            Thread thread1 = new Thread(() => DoThis("thread_01", 500));
            Thread thread2 = new Thread(() => DoThis("thread_02", 200));
            Thread thread3 = new Thread(() => DoThis("thread_03", 300));

            // 200:
            //     Gets or sets a value indicating the scheduling priority of a thread.
            // Possivel ordem de termino: thread2 → thread3 → thread1
            thread1.Priority = ThreadPriority.Highest;
            thread2.Priority = ThreadPriority.Lowest;
            thread3.Priority = ThreadPriority.Normal;

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
        }

        public static void ThreadPoolOneObject()
        {
            // QueueUserWorkItem:
            //     Queues a method for execution. The method executes when a thread pool thread
            //     becomes available.
            ThreadPool.QueueUserWorkItem(DoThisOneObject, "thread_01");
            ThreadPool.QueueUserWorkItem(DoThisOneObject, "thread_02");
            ThreadPool.QueueUserWorkItem(DoThisOneObject, "thread_03");
            Console.ReadKey();
        }

        class ThreadData
        {
            public string name;
            public int milliseconds;

            public ThreadData(string name, int milliseconds)
            {
                this.name = name;
                this.milliseconds = milliseconds;
            }
        }

        public static void ThreadPoolClassObject()
        {
            ThreadData threaddata1 = new ThreadData("thread_01", 500);
            ThreadData threaddata2 = new ThreadData("thread_02", 200);
            ThreadData threaddata3 = new ThreadData("thread_03", 300);
            
            // QueueUserWorkItem:
            //     Queues a method for execution. The method executes when a thread pool thread
            //     becomes available.
            ThreadPool.QueueUserWorkItem(DoThisOneClassObject, threaddata1);
            ThreadPool.QueueUserWorkItem(DoThisOneClassObject, threaddata2);
            ThreadPool.QueueUserWorkItem(DoThisOneClassObject, threaddata3);

            // Console.WriteLine("Count: {0}", ThreadPool.ThreadCount); // Não existe!

            /*
            // solução...
            int maxWorkerThreads;
            int maxCompletionPortThreads;

            // GetMaxThreads:
            //     Retrieves the difference between the maximum number of thread pool threads
            //     returned by the System.Threading.ThreadPool.GetMaxThreads(System.Int32@,System.Int32@)
            //     method, and the number currently active.
            ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads);
            Console.WriteLine("busyWorkerThreads: {0}", (maxWorkerThreads - maxCompletionPortThreads));
            */
        }

        public static void ThreadLockedDemonstration()
        {
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 3; i++)
            {
                // Variaveis: necessario cuidado com a captura dos valores feito pela lambda. Não utilizar direto!
                int index = i;
                string name = String.Format("thread_{0}", index);
                int milliseconds = i % 2 == 0 ? 300 : 500;
                Thread thread = new Thread(() => DoThisWithLock(name, milliseconds));
                thread.Name = name;
                threads.Add(thread);
            }

            foreach (Thread t in threads)
            {
                t.Start();
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }
        }

        public static void ThreadSemaphoreDemonstration()
        {
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 10; i++)
            {
                // variables:
                int index = i;
                int milliseconds = index % 2 == 0 ? 300 : 500;
                string name = String.Format("thread_{0}", index);
                Thread t = null;
                // creating...
                t = new Thread(() => DoThisWithSemaphore(name, milliseconds));
                t.Name = name;
                threads.Add(t);
            }

            foreach (Thread t in threads)
            {
                t.Start();
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }

            Console.ReadKey();
        }

        public static void IncrementWithoutSafety()
        {
            /*
                Thread Safety (segurança em relação a threads) é a propriedade de um código que permite 
                que várias threads executem o mesmo código ou acessem os mesmos dados simultaneamente 
                sem causar resultados incorretos, inconsistentes ou corrupção de estado.

                A ideia central é:

                    Um código thread-safe continua funcionando corretamente mesmo quando várias 
                    threads executam ao mesmo tempo.
            */

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 100; i++)
            {
                // creating...
                Thread t = null;
                t = new Thread(() => Increment());
                threads.Add(t);
            }

            foreach (Thread t in threads)
            {
                t.Start();
            }

            /*
            foreach (Thread t in threads)
            {
                t.Join();
            }
            */

            Console.WriteLine("Cont: {0}", ThreadingInCSharp.counter);
        }

        public static void IncrementWithSafety()
        {
            /*
                Thread Safety (segurança em relação a threads) é a propriedade de um código que permite 
                que várias threads executem o mesmo código ou acessem os mesmos dados simultaneamente 
                sem causar resultados incorretos, inconsistentes ou corrupção de estado.

                A ideia central é:

                    Um código thread-safe continua funcionando corretamente mesmo quando várias 
                    threads executam ao mesmo tempo.
            */

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 100; i++)
            {
                // creating...
                Thread t = null;
                t = new Thread(() => IncrementSafety());
                threads.Add(t);
            }

            foreach (Thread t in threads)
            {
                t.Start();
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }

            Console.WriteLine("Cont: {0}", ThreadingInCSharp.counter);
        }

        public static void ParallelDemonstration()
        {
            // Parallel:
            // Provides support for parallel loops and regions.
            // Parallel.For não garante a ordem de execução das iterações.

            // A. Hello, World!
            Parallel.For(0, 10, i => {
                Console.WriteLine("Looping {0}!", i); // i, nao é impresso de maneira sequencia, mas de maneira aleatoria.
            });

            // 1. Sem Thread Safety
            counter = 0;

            Parallel.For(0, 100, i =>
            {
                Increment();
            });

            Console.WriteLine("Sem lock: {0}", counter);


            // 2. Com lock
            counter = 0;

            Parallel.For(0, 100, i =>
            {
                IncrementSafety();
            });

            Console.WriteLine("Com lock: {0}", counter);


            // 3. Com Interlocked
            int counterInterlocked = 0;

            Parallel.For(0, 100, i =>
            {
                Interlocked.Increment(ref counterInterlocked);
            });

            Console.WriteLine("Com Interlocked: {0}", counterInterlocked);
        }

        public static void ThreadsWithConcurrentBag()
        {
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 3; i++)
            {
                int index = i;
                string name = String.Format("thread_{0}", index);
                int milliseconds = i % 2 == 0 ? 300 : 500;
                Thread thread = new Thread(() => DoThisConcurrentBag(name, milliseconds));
                thread.Name = name;
                threads.Add(thread);
            }

            foreach (Thread t in threads)
            {
                t.Start();
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }


            // porem..., retona um resultado;
            foreach (string result in results)
            {
                Console.WriteLine(result);
            }
        }

        #endregion

        public static void Principal()
        {
            Console.ReadKey();
        }
    }
}
