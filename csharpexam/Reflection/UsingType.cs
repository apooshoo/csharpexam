using csharpexam.Reflection.SpecificTypeMethods;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace csharpexam.Reflection
{
  class UsingType
	{
		/// <summary>
		/// You can create an instance of a type class for a type by:
		///		1: typeof([name of type, eg.Person])
		///		2: instance.GetType() (eg.person.GetType();)
		/// Important:
		/// Fields vs Properties:
		///		Fields are members without get/set accessors. Properties are members with one or both accessors.
		///		If you GetProperties on a type with only fields, you will get nothing.
		///		If you GetFields on a type with only properties, you will get stuff, but it will look a bit funny.
		/// Reflection can get/set values of private members, so do not store sensitive information inside.
		/// Reflection can get inherited members, eg. Depending on your binding flags,
		///		GetMethods can get ToString, GetHashCode, Finalize etc
		///		GetProperties/Fields can get parent stuff.
		///		However, you can block this with BindingFlags.DeclaredOnly
		///	Remember:
		///		You can invoke methods with either
		///			MethodInfo.Invoke(object/instance, object[])
		///			or type.InvokeMember("methodName", BindingFlags, null, obj/instance, object[])
		///			but if you InvokeMember, you must include BindingFlags.InvokeMember
		///		type.GetEnumNames and GetEnumValues are the SAME. Both return the "key", that is, the name and not the int/string "value".
		/// </summary>
		public void Run()
		{
			//UseTypeProperties();

			//UseGenericTypeMethods();

			//Some type methods more commonly used
			new UsingFieldInfo().Run();
			new UsingPropertyInfo().Run();
			new UsingMethodInfo().Run();
		}

		private void UseGenericTypeMethods()
		{
			var type = new Person().GetType();

			var arr = new int[4, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
			//Console.WriteLine(arr[3, 1]);
			Console.WriteLine("Array Rank: " + arr.GetType().GetArrayRank()); //Returns 2 (number of dimensions). 3D will return 3.

			foreach(var constructor in type.GetConstructors())
			{
				Console.WriteLine("Constructor's number of parameters: " + constructor.GetParameters().Length);
				foreach (var param in constructor.GetParameters())
				{
					Console.WriteLine("Parameter Name: " + param.Name);
					Console.WriteLine("Parameter Type:" + param.ParameterType);
					Console.WriteLine("Parameter Default Value: " + param.DefaultValue);
					Console.WriteLine("Parameter Position: " + param.Position);
				}
			}

			//For enums, .GetEnumNames and GetEnumValues are the same (return the enum names, i.e the "key" in the key-value pair)
			//.GetEnumName(value) will get the name (key) for the specific value (eg. 1)
		}

		private void UseTypeProperties()
		{
			var type = new Person().GetType();
			//or: var type = typeof(Person);

			Console.WriteLine("Assembly: " + type.Assembly); //assembly info with version=, etc
			Console.WriteLine("Assembly Qualified Name: " + type.AssemblyQualifiedName); //namespaceOfType.typName + assembly info
			Console.WriteLine("Full Name: " + type.FullName); //namespaceOfType.typeName
			Console.WriteLine("Name: " + type.Name); //eg. typeName eg. Person
			Console.WriteLine("Namespace: " + type.Namespace); //eg. csharpexam (for csharpexam.Person) or csharpexam.Reflection (for csharpexam.Reflection.NestedClass) 
			Console.WriteLine("Base Type: " + type.BaseType); //Returns system.object if no parent. Returns csharpexam.Person if you basetype Employee.
			//ALSO!
			//IsAbstract
			//IsArray (bool for array)
			//IsClass (is class rather than value type or interface)
			//IsEnum
			//IsInterface
			//IsNotPublic
			//IsPublic
			//IsSerializable
			//IsValueType
		}
  }
	class ReflectionExample
	{
		public string _public = "Public field";
		private string _private = "Private field";
		internal string _internal = "Internal field";
		protected string _protected = "Protected field";
		static string _static = "Static field";

		//this would have been a prop
		// public string _public {get;set;}

		public void PublicMethod() { Console.WriteLine("Invoked PublicMethod"); }
		public void PublicMethodWithIntParams(int param) { Console.WriteLine("Invoked PublicMethodWithIntParams with param: " + param); }
		private void PrivateMethod() { Console.WriteLine("Invoked PrivateMethod"); }
	}
	class NestedClass{}
}
