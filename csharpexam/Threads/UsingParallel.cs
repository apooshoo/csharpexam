using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharpexam.Threads
{
  class UsingParallel
  {
		//Has For, ForEach and Invoke
		//For/Foreach accepts ParallelLoopState which has Stop and Break methods
		//Stop: stop ASAP, Break: stop further iterations
		public void Run()
		{
			//Parallel.For(0, 10, i => {
			//	ThreeSecondMethod();
			//});


			//Parallel.Invoke(() => ThreeSecondMethod(), () => ThreeSecondMethod(), () => ThreeSecondMethod());

			//Note: Due to race conditions, if you wish to return values and avoid overwriting, use ParallelLoopState
			//ParallelWithParallelLoopState();

			ForEachWithParallelLoopState();
		}

		private void ForEachWithParallelLoopState()
		{
			var list = new List<int>() { 1, 2, 3, 4, 5 };
			Parallel.ForEach(list, (item, state) =>
			{
				Console.WriteLine(item);
				if (item <= 3)
				{
					//Console.WriteLine("BREAKING");
					//state.Break();
					//Console.WriteLine("BROKEN");
					Console.WriteLine("Stopping");
					Console.WriteLine("IsStopped:" + state.IsStopped);
					state.Stop();
					Console.WriteLine("Stopped");
				}
			});
		}

		private void ParallelWithParallelLoopState()
		{
			double result = 0d;
			Parallel.For(0, 10,
				//Init value for interim result
				() => 0d,
				//Do whatever
				(i, state, interimResult) => interimResult += ThreeSecondCalculation(),
				//Resolve with result
				(lastInterimResult) => result += lastInterimResult);
			Console.WriteLine(result);
		}

		private double ThreeSecondCalculation()
		{
			ThreeSecondMethod();
			return 10;
		}

		private void ThreeSecondMethod()
		{
			Console.WriteLine("Three seconds method started!");
			Thread.Sleep(3000);
			Console.WriteLine("Three seconds method done!");
		}

		private void OneSecondMethod()
		{
			Console.WriteLine("One second method started!");
			Thread.Sleep(1000);
			Console.WriteLine("One second method done!");
		}
	}
}
