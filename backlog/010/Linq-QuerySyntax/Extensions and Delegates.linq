<Query Kind="Program" />

void Main()
{
	//DemoExtensions();
	//DemoLinkedList();
	DemoDelegates();
}

// You can define other methods, fields, classes and namespaces here
#region Extension Method Intro
void DemoExtensions()
{
	// A class is a "blueprint" for creating objects. Objects are "instances" of a class.
	// A class is a programmer-defined data type.
	// Your class definition includes fields and properties ("looks like")
	// as well as constructors and methods (how it "behaves").
	string text = "Some random text that I am typing.";
	//    \object/
	text.Dump(); // .Dump() is an extension method that is part of LinqPad
	text.Quack();
	text.Quack(5);
	text = null;
	if(text.IsBlank())
	{
		"It's blank!".Dump();
	}
	text = "To be or not to be";
	text.Dump("Regular dump");
	text.AirQuote().Dump("Airquoted");
	
}
// Extension methods allow a programmer to create methods that aren't part of a class definition,
// but that can "appear" to be part of the class definition.
// Extension methods must be enclosed in a static class
public static class StringExtensions
{
	public static bool IsBlank(this string self) // a shorter name for my IF statements
	{
		return string.IsNullOrWhiteSpace(self); // functionality already exists
	}
	public static string AirQuote(this string self) // enclose the text in double-quotes
	{
		if(self.IsBlank()) return "\"\"";
		else return "\"" + self + "\"";
	}
	public static void Quack(this string self) // the first parameter associates this method with that type
	{
		self.Dump("Quack");
	}
	// When called/used, only the 2nd param onwards is used
	public static void Quack(this string self, int count) 
	{ //               apply to a string type: text.Quack(5); <-- count parameter
		string message = "Quack";
		while (count > 1)
		{
			message += ", quack";
			count--;
		}
		self.Dump(message);
	}
}
#endregion

#region Simple Linked List with Extension Method
void DemoLinkedList()
{
	var codingRaceWinners = new ForwardOnlyLinkedList<string>();
	codingRaceWinners.Add("TB");
	codingRaceWinners.Add("CLR");
	codingRaceWinners.Add("FA");
	codingRaceWinners.Add("ABD");
	codingRaceWinners.Add("WLW");
	int numberOfWinners = codingRaceWinners.ItemCount<string>();
	codingRaceWinners.Dump("There are " + numberOfWinners + " winners");
}
public static class ExtensionMethods
{
	public static int ItemCount<T>(this ForwardOnlyLinkedList<T> self)
	{
		int count = 0;
		var links = self?.First;
		while (links != null)
		{
			count++;
			links = links.Next;
		}
		return count;
	}
}
public class LinkedItem<T>
{
	public T Item {get;set;}
	public LinkedItem<T> Next {get;set;}
	public LinkedItem(T contents)
	{
		Item = contents;
	}
}
public class ForwardOnlyLinkedList<T>
{
	public LinkedItem<T> First { get;set; }
	public void Add(T item)
	{
		var link = new LinkedItem<T>(item);
		if(First == null)
		    First = link;
		else
		{
			var lastItem = First;
			while(lastItem.Next != null)
			    lastItem = lastItem.Next; // travelling along the "chain"
			lastItem.Next = link;
		}
	}
}
/*
var first = new LinkedItem<int>(5);
first.Next = new LinkedItem<int>(7);

first
-----------------           -----------------
| .Item | .Next |   --->    | .Item | .Next |
|-------|-------|           |-------|-------|
|   5   | --->> |           |   7   |  null |
-----------------           -----------------
*/
#endregion

#region Delegates
// A Delegate is a data type that defines the "signature" for a method: return type & parameters
// With delegates, you have the ability to assign a method to a delegate variable
void DemoDelegates()
{
	//// Create my delegate variable:
	//Computer myCalculator;
	//myCalculator = Adder; // Use the Adder method to do my computing
	//// Use my delegate as a method
	//double result = myCalculator(1, 2, 3, 4);
	////              call a method
	//result.Dump("Using the Adder computer");

	DisplayComputation(Adder, "Using the Adder computer");
	DisplayComputation(Averager, "Using the Averager computer");
	// Demo using an "anonymous" method (will use the Lambda syntax  =>)
	Computer myDelegate;
	myDelegate = input =>
	{
		double result = 1;
		foreach(var item in input)
			result *= item;
		return result;
	};
	DisplayComputation(myDelegate, "Using an Anonymous Method");
	// Demo using the "anonymous" method in-line with my call to DisplayComputation()
	DisplayComputation(number => number.Max(), "Lambda Expression");
}
// Here is a method that uses the delegate as a parameter
void DisplayComputation(Computer myComputer, string message)
{
	double result;
	result = myComputer(1, 2, 3, 4);
	result.Dump(message);
}

// Declare my delegate type
delegate double Computer(params int[] values); // Notice that there is no implementation
//       return   name      parameterList

// The following methods are candidates for the Computer() delegate because they match the "signature"
private double Adder(params int[] values)
{
	double result = 0;
	foreach (var number in values)
	 	result += number;
	return result;
}
private double Averager(params int[] values)
{
	double total = 0;
	int count = 0;
	foreach(var number in values)
	{
		total += number;
		count++;
	}
	return total / count;
}
#endregion
