using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace csharpexam.Events
{
	//PUBLISHING EVENTS: Refers to:
	//EITHER
			//1: Defining delegate type 2: Defining event with delegate signature(1 and 2 can be done in one line with Action/Func)
	//OR
			//Defining event with EventHandler/EventHandler<YourCustomArgs>
	//SUBSCRIBING TO EVENTS: Refers to [event] +=/-= method
	//RAISING EVENTS: Refers to: (if [event] != null){ [event](this, args) }
	//IMPORTANT:
	//Derived classes cannot raise parent events, so they need to use a parent-defined method to raise it from inside.

  class Subscribing
  {
		public void Run()
		{
			var subscribed = new SubscribedClass();
			subscribed.RaiseEvent();
			subscribed.Event += () => { Console.WriteLine("Subscribed to Event."); };
			subscribed.RaiseEvent();
			subscribed.RaiseEventWithDefinedArgs();
			subscribed.EventWithDefinedArgs += Subscribed_EventWithDefinedArgs;
			subscribed.RaiseEventWithDefinedArgs();
		}

		public void Subscribed_EventWithDefinedArgs(object sender, CustomEventArgs args)
		{
			Console.WriteLine("Subscribed to EventWithDefinedArgs");
		}

	}

	public class SubscribedClass
	{
		//Define event:
		//With delegate: [access] event [delegateType] [eventName]
		public delegate void Delegate();
		public event Delegate Event;
		//OR with predefined delegate:
		public event Action ShortcutEvent;
		//OR with custom event args
		public event EventHandler<CustomEventArgs> EventWithDefinedArgs;

		public void RaiseEvent()
		{
			if (Event != null)
			{
				Event();
			}
			else
			{
				Console.WriteLine("Nothign subscribed to Event yet");
			}
		}

		public void RaiseEventWithDefinedArgs()
		{
			if (EventWithDefinedArgs != null)
			{
				EventWithDefinedArgs(this, new CustomEventArgs(1));
			}
			else
			{
				Console.WriteLine("Nothign subscribed to EventWithDefinedArgs yet");
			}
		}
	}

	public class CustomEventArgs : EventArgs
	{
		public int Number { get; set; }
		public CustomEventArgs(int x)
		{
			Number = x;
		}
	}
}
