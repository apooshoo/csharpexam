using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace csharpexam.Reflection.SpecificTypeMethods
{
  class UsingFieldInfo
  {
		public void Run()
		{
			//type.GetFields
			//> fieldInfo.GetValue, SetValue
			var obj = new ReflectionExample();
			var type = obj.GetType();
			FieldInfo[] fields = type.GetFields(
				BindingFlags.Public
				| BindingFlags.NonPublic
				| BindingFlags.Instance //Important: Instance: all non-static fields
				| BindingFlags.Static
				);
			foreach (FieldInfo fieldInfo in fields)
			{
				Console.WriteLine("Field Name: " + fieldInfo.Name);
				Console.WriteLine("Field Value: " + fieldInfo.GetValue(obj));
			}

			//Private fields can be get/set
			var privateField = type.GetField("_private", BindingFlags.NonPublic | BindingFlags.Instance);
			Console.WriteLine("Private field value before alteration: " + privateField.GetValue(obj));
			privateField.SetValue(obj, "Altered private field");
			Console.WriteLine("Private field value after alteration: " + privateField.GetValue(obj));
		}
	}
}
