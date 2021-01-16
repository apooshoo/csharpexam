using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace csharpexam.Threads
{
	class UsingThreadPool
	{
		//Using ThreadPool (automatic) versus manual thread usage:
		//All ThreadPool threads are background, vs foreground by default which can be set to bg.
		//Cannot abort or interrupt.
		//Cannot Join.
		//Reused when finished, versus destroyed.
		//You cannot control its priority.
		public int FinalResult { get; set; }
		public void Run()
		{
			//This is the most common usage of Threadpool.
			ThreadPool.QueueUserWorkItem((x) => { TestVoidMethod(1); });

			//This is commonly used in WPF/Forms apps to manage async results (because Join isnt possible)
			var worker = new BackgroundWorker();
			worker.DoWork += DoWorkMethod;
		}

		private void TestVoidMethod(int x) { Console.WriteLine("Test " + x); }
		public void DoWorkMethod(object sender, DoWorkEventArgs e)
		{
			e.Result = 5;
		}

		public void WorkerCompletedMethod(object sender, RunWorkerCompletedEventArgs e)
		{
			//Note that this may have issues in WPF if the BGWorker is triggered from a non-UI thread.
			//There is a fix related to using this.Dispatcher.Invoke, but probably dont bother. 
			FinalResult = (int)e.Result;
		}

		//Worker threads: threads for CPU work, Completion port threads: TDLR, but is a separate pool used for I/O work
		private void LessImptThreadPoolMethods()
		{
			ThreadPool.GetAvailableThreads(out int workerThreads, out int completionPortThreads);
			Console.WriteLine(workerThreads);
			Console.WriteLine(completionPortThreads);

			ThreadPool.GetMinThreads(out int workerThreads2, out int completionPortThreads2);
			Console.WriteLine(workerThreads2);
			Console.WriteLine(completionPortThreads2);

			ThreadPool.GetMaxThreads(out int workerThreads3, out int completionPortThreads3);
			Console.WriteLine(workerThreads3);
			Console.WriteLine(completionPortThreads3);

			//ThreadPool.SetMaxThreads(1, 1);
			//ThreadPool.SetMinThreads(1, 1); This may be good for performance if you want to prepopulate the threadpool when you
			//know you will use the threads.
		}
	}
}
