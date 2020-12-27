using csharpexam.Events;
using System;

namespace csharpexam
{
    class Program
    {
        static void Main(string[] args)
        {
					var testClass = new Subscribing();
					testClass.Run();
        }
    }
}
