using System;

public class Item
{
	public string name;
	public int counter;
	public bool added;
	
	public Item(string name)
	{
		this.name = name;
		this.counter = 1;
		this.added = true;
	}
}
