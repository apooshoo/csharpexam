using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharpexam.Synchronisation
{
  class UsingAlternativesToLocks
  {
		//Except ConcurrentDictionary, all concurrentcollections implement IProducerConsumerCollection (eg. TryAdd, TryTake)
		public void Run()
		{
			//UseInterlocked();

			//UseConcurrentCollections();

			UseCancellationToken();
		}

		private void UseInterlocked()
		{
			var a = 0;
			Parallel.For(0, 10, i =>
			{
				//Interlocked.Increment(ref a);
				//Interlocked.Exchange(ref a, i);
				Interlocked.CompareExchange(ref a, i + 1, i);
			});
			Console.WriteLine(a);
		}

		private void UseConcurrentCollections()
		{
			var blocking = new BlockingCollection<int>();
			blocking.CompleteAdding();
			Console.WriteLine("Capacity: " + blocking.BoundedCapacity);
			Console.WriteLine("AddingCompleted: " + blocking.IsAddingCompleted);
			Console.WriteLine("Completed: " + blocking.IsCompleted);

			var bag = new ConcurrentBag<int>();

			var dic = new ConcurrentDictionary<int, int>();
			dic.TryAdd(1, 1);
			dic.GetOrAdd(1, 1);

			var queue = new ConcurrentQueue<int>();
			queue.Enqueue(1);
			var success = queue.TryDequeue(out var result1);

			var stack = new ConcurrentStack<int>();
			stack.Push(1);
			var peekSuccess2 = stack.TryPeek(out int peek2);
			var success2 = stack.TryPop(out int result2);
		}

		private void UseCancellationToken()
		{
			var tokenSource = new CancellationTokenSource();
			var task1 = Task.Run(() => { Thread.Sleep(1000); if(!tokenSource.IsCancellationRequested)Console.WriteLine("Task 1 complete"); }, tokenSource.Token);
			var task2 = Task.Run(() => { Thread.Sleep(2000); if(!tokenSource.IsCancellationRequested) Console.WriteLine("Task 2 complete"); }, tokenSource.Token);
			var task3 = Task.Run(() => { Thread.Sleep(3000); if(!tokenSource.IsCancellationRequested) Console.WriteLine("Task 3 complete"); }, tokenSource.Token);

			var stopwatch = new Stopwatch();
			stopwatch.Start();
			while (stopwatch.IsRunning)
			{
				if (stopwatch.ElapsedMilliseconds > 1000)
				{
					tokenSource.Cancel();
					Console.WriteLine("Canceled!");
					stopwatch.Stop();
				}

				Thread.Sleep(50);
			}

			Task.WaitAll(new Task[] { task1, task2, task3 });
		}
  }
}
