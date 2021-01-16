using csharpexam.Threads;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace csharpexam.Reflection
{
	/// <summary>
	///	Assembly: compiled piece of code, which is usually.dll or.exe.
	///		Lets you load and read metadata of assembly, and also create instances of types contained in the assembly.
	///	Module: A file, usually single, and either .dll/.exe, that composes the assembly
	///	An assembly can technically hold more than one module, but this almost never happens.Also contains manifest.
	///	Example: This assembly: csharpexam, module: csharpexam.dll
	///	Remember:
	///		Classes without access modifiers are internal by default
	/// </summary>
	class UsingAssembly
  {
		public void Run()
		{
			UseAssemblyMethods();
		}

		private void CreateInstanceUsingAssembly()
		{
			var assembly = Assembly.Load("System.Data");
			var dt = assembly.CreateInstance("System.Data.DataTable") as DataTable; //Returns null (does not throw) if type not found. so check in production code!
			Console.WriteLine(dt.Rows.Count);
		}

		private void UseAssemblyProperties()
		{
			var assembly = Assembly.Load("System.Data");
			Console.WriteLine("Code Base (Path): " + assembly.CodeBase);
			Console.WriteLine("Defined Types: " + assembly.DefinedTypes);//arr
			Console.WriteLine("Exported Types: " + assembly.ExportedTypes);//arr
			Console.WriteLine("Full Name: " + assembly.FullName);
			Console.WriteLine("From GAC: " + assembly.GlobalAssemblyCache);
			Console.WriteLine("Image Runtime Version: " + assembly.ImageRuntimeVersion);
			Console.WriteLine("Location: " + assembly.Location);
			Console.WriteLine("Modules: " + assembly.Modules);//arr
			Console.WriteLine("Security Rule Set: " + assembly.SecurityRuleSet);
		}

		private void UseAssemblyMethods()
		{
			var assembly = Assembly.GetExecutingAssembly(); //assembly of currently executing code

			var person = assembly.CreateInstance("csharpexam.Person"); //Namespace.[class]
																																 //Console.WriteLine(person.GetType());

			var attributes = assembly.GetCustomAttributes(); //not much difference
			var attributes2 = assembly.CustomAttributes;

			var exportedTypes = assembly.GetExportedTypes(); //public only. 
																											 //foreach (var pType in exportedTypes)
																											 //{
																											 //	Console.WriteLine("Exported Type: " + pType.FullName); 
																											 //}

			var types = assembly.GetTypes(); //all
																			 //foreach (var type in types)
																			 //{
																			 //	Console.WriteLine("Type: " + type.FullName);
																			 //}

			var module = assembly.GetModule("csharpexam.dll");
			//Console.WriteLine(module);

			var modules = assembly.GetModules();
			//foreach (var mod in modules)
			//{
			//	Console.WriteLine("Module Name: " + mod);
			//}

			var name = assembly.GetName(); //Seems to be the same as .FullName
																		 //Console.WriteLine("Assembly Name: " + name);

			var refAssemblies = assembly.GetReferencedAssemblies();
			//Gets all the assemblies your assembly "imports". Can be good for troubleshooting in deployment 
			foreach (var ass in refAssemblies)
			{
				Console.WriteLine("Referenced Assembly Name: " + ass.Name + ", Version: " + ass.Version);
			}

			UseLoads();
		}

		//LOAD CONTEXTS:
		// 1.Load context (.Load): Contains assemblies found by probing, aka. looks in the GAC, host assembly store,
		//folder of executing assembly and its private bin folder.
		// 2.Load-From context (.LoadFrom/.LoadFile): Contains assemblies located in the path provided
		// 3.Reflection-only context (.ReflectionOnlyLoad(From)): Contains assemblies loaded with either of the two methods.

		// Note: LoadFile is more specific than LoadFrom (eg. LoadFrom can throw exceptions if there are two assemblies
		// with the same identity in different folders).
		private static void UseLoads()
		{
			var assembly = Assembly.GetExecutingAssembly();

			var shortLoadedAssembly = Assembly.Load("System.Data"); //Load using short name: seems like it can work
			var loadedAssembly = Assembly.Load(assembly.FullName); //Load using full name,
																														 //format: [assemblyName,Version=,Culture=,PublicKeyToken=]
			//Console.WriteLine(loadedAssembly);

			var loadFileAssembly = Assembly.LoadFile(assembly.Location); //Load using path/location
			//Console.WriteLine(loadFileAssembly);

			var loadFromAssembly = Assembly.LoadFrom(assembly.Location); //Seems to also load using path/location
			//Console.WriteLine(loadFromAssembly);

			//var refOnlyLoad = Assembly.ReflectionOnlyLoad(assembly.FullName);//doesnt work on net core
			//Also can use ReflectionOnlyLoadFrom

			//NOTE: Roughly speaking, this is kind of a limited load.
			//You are not loading the assembly into the AppDomain, so there are some things you cant do with it
			//But you can do reflection stuff.

			var unsafeLoadFrom = Assembly.UnsafeLoadFrom(assembly.Location); //Treat as LoadFrom + bypass for some security checks.
																																			 //Console.WriteLine(unsafeLoadFrom);
		}
	}
}
