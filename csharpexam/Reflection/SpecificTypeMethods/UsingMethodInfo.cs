using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace csharpexam.Reflection.SpecificTypeMethods
{
  class UsingMethodInfo
  {
		public void Run()
		{
			var obj = new ReflectionExample();
			var type = obj.GetType();

			MethodInfo[] methods = type.GetMethods(
				BindingFlags.Public
				| BindingFlags.NonPublic
				| BindingFlags.Instance //Important: Instance: all non-static methods
				| BindingFlags.Static
			);
			foreach (MethodInfo methodInfo in methods)
			{
				Console.WriteLine("Method Name: " + methodInfo.Name);
				Console.WriteLine("Method Return Type: " + methodInfo.ReturnType);
				//methodInfo.GetParameters will work the same as in type.GetConstructors.GetParameters
			}

			//INVOKING WITH methodInfo.Invoke
			var publicMethod = type.GetMethod("PublicMethod", BindingFlags.Public | BindingFlags.Instance);
			publicMethod.Invoke(obj, null);
			var privateMethod = type.GetMethod("PrivateMethod", BindingFlags.NonPublic | BindingFlags.Instance);
			privateMethod.Invoke(obj, null);
			var publicMethodWithParam = type.GetMethod("PublicMethodWithIntParams", BindingFlags.Public | BindingFlags.Instance);
			publicMethodWithParam.Invoke(obj, new object[] { 1 });

			//INVOKING WITH type.InvokeMember
			type.InvokeMember("PublicMethod",
				BindingFlags.InvokeMethod,
				null,
				obj,
				null);
			type.InvokeMember("PrivateMethod",
				BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance,
				null,
				obj,
				null);
			type.InvokeMember("PublicMethodWithIntParams",
				BindingFlags.InvokeMethod,
				null,
				obj,
				new object[] { 1 });
		}
	}
}
