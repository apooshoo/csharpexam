using System;
using System.Collections.Generic;
using System.Text;

namespace csharpexam
{
  public class Person
  {
		public string Name { get; set; }

		public Person() { }
		public Person(string name)
		{
			Name = name;
		}
  }
	public class Employee : Person
	{
	}
}
