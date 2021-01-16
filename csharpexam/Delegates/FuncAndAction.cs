using System;
using System.Collections.Generic;
using System.Text;

namespace csharpexam.Delegates
{
  class FuncAndAction
  {
		//Replacing delegate with Func
		public delegate Person ReturnPersonDelegate();  //Define delegate type
		public ReturnPersonDelegate ReturnPersonMethod; //Define delegate variable
		//>>
		public Func<Person> ReturnPersonFunc;

		//Note that Expression lambdas dont have braces but statements do.
		//Async lambdas just stick an async in front like task = async (float x){return x}

		public void Run()
		{

		}
  }
}
