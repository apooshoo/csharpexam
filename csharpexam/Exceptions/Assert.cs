using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace csharpexam.Exceptions
{
	//Use Asserts if data is unusual (indicating there may be a bug) but app can still continue
	//Debug.Assert will go away in release mode
	//Debug.Assert stops execution and shows stack trace, but IT DOES NOT THROW AN EXCEPTION THAT CAN BE CAUGHT.
  class DebugAssert
  {
		public void Run()
		{
			var testData = 9999;
			Debug.Assert(testData < 100, "ASSERT FAILED");
		}
  }
}
