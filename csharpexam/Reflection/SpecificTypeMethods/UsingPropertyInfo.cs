using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace csharpexam.Reflection.SpecificTypeMethods
{
  class UsingPropertyInfo
  {
		public void Run()
		{
			//Works the same as FieldInfo, but has some of its own methods, like GetGetMethod (get the get accessor method)
			var obj = new ReflectionExample();
			var type = obj.GetType();
			PropertyInfo[] properties = type.GetProperties(
				BindingFlags.Public
				| BindingFlags.NonPublic
				| BindingFlags.Instance //Important: Instance: all non-static props
				| BindingFlags.Static
			);
			foreach (PropertyInfo propertyInfo in properties)
			{
				Console.WriteLine("Property Name: " + propertyInfo.Name);
				Console.WriteLine("Property Value: " + propertyInfo.GetValue(obj));
			}
		}
	}
}
