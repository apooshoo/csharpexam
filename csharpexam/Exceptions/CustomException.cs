using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace csharpexam.Exceptions
{
	//Init constructors to mirror the base Exception class (not all are mandatory but its useful)
	//Add Serializable attribute to allow Binary or SoapFormatter to serialise the class, which is required for it
	//to cross AppDomain boundaries.
	//Note that this has nothing to do with XML/JSON serialisation.
	//You can derive from ApplicationException, but this is outdated.
	[Serializable]
  class CustomException : Exception
  {
		public CustomException() : base() { }
		public CustomException(string msg) : base(msg) { }
		public CustomException(string msg, Exception innerEx) : base(msg, innerEx) { }
		protected CustomException(SerializationInfo info, StreamingContext ctx) : base(info, ctx) { }
  }
}
