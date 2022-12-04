using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSP_Algorithm
{
	public class Item
	{
		public string name;
		public int counter;
		public bool added = false;

		public Item(string name)
		{
			this.name = name;
			added = true;
		}
	}
}