using csharpexam.Events;
using csharpexam.Exceptions;
using csharpexam.Reflection;
using csharpexam.Synchronisation;
using csharpexam.Threads;
using System;

namespace csharpexam
{
    class Program
    {
        static void Main(string[] args)
        {
					var testClass = new UsingType();
					testClass.Run();

					//try
					//{
					//	var testClass = new DebugAssert();
					//	testClass.Run();
					//}
					//catch (Exception e)
					//{
					//	Console.WriteLine("CAUGHT");
					//}
				}
    }
}
