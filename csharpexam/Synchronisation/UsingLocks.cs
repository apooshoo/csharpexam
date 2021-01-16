using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace csharpexam.Synchronisation
{
  class UsingLocks
  {
		//Locks only work on reference types. Avoid locking "this".
		//Locking may also include use of a semaphore or a mutex.
		//Semaphore: signaling mechanism (uses a binary to wait and signal. 1 to start, -1 when waiting, +1 back when signaled.
		//Mutex: Lock
		public void Run()
		{
			//UseMonitor();

			UseLock();
		}

		private void UseLock()
		{
			var obj = new object();
			lock (obj)
			{
				//etc
				Console.WriteLine("IsEntered: " + Monitor.IsEntered(obj));
			}
		}

		private void UseMonitor()
		{
			var obj = new object();
			Monitor.Enter(obj);
			//etc
			Console.WriteLine("IsEntered: " + Monitor.IsEntered(obj));
			Monitor.Exit(obj);
			//or

			Monitor.TryEnter(obj);
			try
			{
				//etc
				Console.WriteLine("IsEntered: " + Monitor.IsEntered(obj));
			}
			finally
			{
				Monitor.Exit(obj);
			}
		}
  }
}
