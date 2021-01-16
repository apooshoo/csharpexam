using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharpexam.Threads
{
	//Task.Run/WhenAll/WhenAny return a Task object
	//One of the task arguments, TaskScheduler.etc, is really only used for Forms/WPF apps, eg:
		//...TaskScheduler.FromCurrentSynchronisationContext will have the task be executed by the UI thread.
  class UsingTasks
  {
		public async Task Run()
		{
			//UsingTaskAndRun();

			//UsingTaskAndStart();

			//UseTaskFactoryAndTaskCreationOptions();

			UseTasksAndTaskFactory();
		}

		private void UseTasksAndTaskFactory()
		{
			var task1 = Task.Run(() => ThreeSecondMethod());
			var task2 = Task.Run(() => OneSecondMethod());
			var task3 = new TaskFactory().ContinueWhenAll(new Task[] { task1, task2 },
				(prevTasks) => { Console.WriteLine("EVERYTHING DONE!"); });
			task3.Wait();
		}

		private void UsingTaskAndRun()
		{
			var task1 = Task.Run(() => ThreeSecondMethod());
			var task2 = Task.Run(() => OneSecondMethod());
			//task1.Wait();
			//Task.WaitAll(task1, task2);
			//Task.WaitAny(task1, task2);
			//var task = Task.WhenAll(task1, task2).ContinueWith((x) => Console.WriteLine("ALL DONE!"));
			var task = Task.WhenAny(task1, task2).ContinueWith((x) => Console.WriteLine("SOMETHING WAS DONE!"));
			task.Wait();
		}

		private void UsingTaskAndStart()
		{
			//Please note that some Task methods like ContinueWith must be defined before running,
			//so it only works with new Task() (before Task.Start()) and not with Task.Run().
			var task3 = new Task(() => ThreeSecondMethod());
			task3.ContinueWith((x) => OneSecondMethod());
			task3.Start();
			Console.WriteLine(task3.Status);
			task3.Wait();
			Console.WriteLine(task3.Status);
		}

		private void UseTaskFactoryAndTaskCreationOptions()
		{
			//var taskFactory = Task.Factory;
			var taskFactory = new TaskFactory();

			//Specifying options is also possible for Tasks.
			//taskFactory.StartNew(() => ThreeSecondMethod(), TaskCreationOptions.None);
			//taskFactory.StartNew(() => ThreeSecondMethod(), TaskCreationOptions.PreferFairness);
			//taskFactory.StartNew(() => ThreeSecondMethod(),
			//	TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach
			//	| TaskCreationOptions.HideScheduler | TaskCreationOptions.LongRunning);
			var tasks = new List<Task>();
			for (var i = 0; i < 10; i++)
			{
				var task = taskFactory.StartNew(() => OneSecondMethod(), TaskCreationOptions.None);
				tasks.Add(task);
			}
			var anyTask = taskFactory.ContinueWhenAny(tasks.ToArray(), (x) => Console.WriteLine("SOMETHING WAS DONE!"));
			var finalTask = taskFactory.ContinueWhenAll(tasks.ToArray(), (x) => Console.WriteLine("EVERYTHING DONE!"));
			finalTask.Wait();

			//var intTask = taskFactory.StartNew(() => { OneSecondMethod(); return 10; });
			//var result = intTask.Result;
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
