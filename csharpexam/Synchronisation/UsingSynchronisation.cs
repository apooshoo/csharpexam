using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharpexam.Synchronisation
{
  class UsingSynchronisation
  {
		//Remember that while CountdownEvent and Barriers use some form of Signal and/or Wait,
		//EventWaitHandle uses Set instead of Signal and WaitOne instead of Wait.
		public void Run()
		{
			//UseEventWaitHandle();

			//Is a smoother version of EventWaitHandle. Where you once would have had to create a waithandle for each
			//method you wanted to signal and WaitAll at the end, CountdownEvent lets you set, trigger and wait for
			//your methods easily.
			//UseCountdownEvent();

			//Different usage. Allows you to structure your async code by stages, where all participating threads
			//(designated by the particpants number) will wait at set intervals for each other, and you can track info like
			//which is ongoing, and set drop-out conditions.
			UseBarriers();
		}

		private void UseBarriers()
		{
			var participants = 5;
			var barrier = new Barrier(5 + 1, // + 1 for current thread
				(b) => {
					Console.WriteLine($"Operation complete!" +
					$" Participant count: {b.ParticipantCount - 1}, Phase: {b.CurrentPhaseNumber}");
				});
			for (var i = 0; i < participants; i++)
			{
				var localCopy = i;
				Task.Run(() => {
					Console.WriteLine($"Participant index {localCopy} has started work!");
					Thread.Sleep(1000);
					if (localCopy % 2 == 0)
					{
						Console.WriteLine($"Participant index {localCopy} has reached the halfway mark!");
						barrier.SignalAndWait();
						Thread.Sleep(1000);
						Console.WriteLine($"Participant index {localCopy} has fully succeeded!");
						barrier.SignalAndWait();
					}
					else
					{
						Console.WriteLine($"Aborting particpant index {localCopy} at the halfway mark.");
						barrier.RemoveParticipant();
					}
				});
			}

			var stopWatch = new Stopwatch();
			stopWatch.Start();
			Console.WriteLine("Main thread waiting!");
			barrier.SignalAndWait();
			Console.WriteLine($"Done waiting at first phase! {stopWatch.ElapsedMilliseconds} ms has passed.");
			barrier.SignalAndWait();
			Console.WriteLine($"Done waiting at second phase! {stopWatch.ElapsedMilliseconds} ms has passed.");
			Console.WriteLine("All done!");
		}

		private void UseCountdownEvent()
		{
			int result = 0;
			var ctDown = new CountdownEvent(5);

			Parallel.For(1, 10, (i) =>
			{
				Thread.Sleep(3000);
				if (!ctDown.IsSet)
				{
					Console.WriteLine("Countdown ongoing, adding.");
					result += 1;
					ctDown.Signal();
					Console.WriteLine("Added and signaled.");
				}
				else
				{
					Console.WriteLine("Countdown over, aborting.");
				}
			});

			ctDown.Wait();
			Console.WriteLine(result);
		}

		private void UseEventWaitHandle()
		{
			int result = 0;
			var allDone = new EventWaitHandle(false, EventResetMode.ManualReset);
			ThreadPool.QueueUserWorkItem((x) =>
			{
				OneSecondMethod();
				result += 1;
				allDone.Set();
			});
			var result2 = Task.Run(() =>
			{
				OneSecondMethod();
				return 1;
			});
			//Blocks until the first method finishes
			allDone.WaitOne();
			result += result2.Result;
			Console.WriteLine(result);
		}

		private void OneSecondMethod()
		{
			Console.WriteLine("One second method started!");
			Thread.Sleep(1000);
			Console.WriteLine("One second method done!");
		}
	}
}
