using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharpexam.Threads
{
  class UsingAsyncAwaitWithTasks
  {
		public void Run()
		{
			//var task = AsyncIntTask();
			//Console.Write(task.Result);

			//var task = Task.Run(IntMethod);
			//Console.WriteLine(task.Result);

			var task = AsyncIntMethod();
			Console.WriteLine(task.Result);
		}

		private async Task<int> AsyncIntMethod()
		{
			return await AsyncIntTask();
		}

		private Task<int> AsyncIntTask()
		{
			return Task.Run(new Func<int>(IntMethod));
		}

		private int IntMethod()
		{
			Thread.Sleep(3000);
			return 10;
		}
  }
}
