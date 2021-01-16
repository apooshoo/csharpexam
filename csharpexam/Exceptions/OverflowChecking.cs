using System;
using System.Collections.Generic;
using System.Text;

namespace csharpexam.Exceptions
{
	//Please note that double is more performative but less precise than decimal

	//If you want overflow exceptions to be thrown, use a 'checked' block

	//Please note that rethrowing (catch e, throw new Exception) is frowned upon as you know, but
	//there are specific instances you might want to, eg: if you are using private methods and you want the call stack
	//to only contain information from public methods.
  class OverflowChecking
  {
		public void Run()
		{
			CheckSpecialValues();
		}

		private void CheckOverflowException()
		{
				checked
				{
					int a = 9999999;
					var b = 9999999;
					int c = a * b; //throws an exception. Without 'checked', would have rounded off and printed.
					Console.WriteLine(c);
				}
		}

		private void CheckSpecialValues()
		{
			// Overflow, underflow or special value NaN may happen to floating point types. Decimal is limited.
			// Possible to compare a variable to Double.NegativeInfinity, but not for NaN. NaN comparisons are always false.
			// Just to be sure, use built in comparers.
			var a = 100;
			var b = Double.IsInfinity(a);
			var c = Single.IsNegativeInfinity(a);
			var d = Double.IsPositiveInfinity(a);
			var e = Single.IsNaN(a);

			try
			{
				checked
				{
					int f = 99999999;
					int g = 99999999;
					var h = f * g;
					Console.WriteLine(h);
				}

			}
			catch (OverflowException ex)
			{
				throw new Exception("Msg", ex);//This newly thrown exception will not be caught by the other catch in the same try statement
			}
			catch (Exception ex)
			{
				throw;
			}
		}
  }
}
