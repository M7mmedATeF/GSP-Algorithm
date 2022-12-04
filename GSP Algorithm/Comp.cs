using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSP_Algorithm
{
    public class Comp
    {
        public List<Item> item = new List<Item>(); // {"asdasdasd"}
        public int itemLength = 0;
        public bool found = false;
        public bool printed = true;
        public Comp()
        {

        }
        public Comp(string data)
        {
            string[] tmp = data.Split(',');
            foreach (string d in tmp)
            {
                itemLength++;
                Item newItem = new Item(d);
                item.Add(newItem);
            }
        }
        public Comp(List<Item> items)
        {
            this.item = items;
        }
        public Comp(Comp cp)
        {
            foreach(Item it in cp.item)
            {
                Item nit = new Item(it.name);
                this.item.Add(nit);
            }
        }
        public List<Item> withoutFirstItem()
        {
            List<Item> lst = new List<Item>();
            foreach(Item it in item)
            {
                lst.Add(it);
            }
            lst.RemoveAt(0);
            return lst;
        }
        public List<Item> withoutLastItem()
        {
            List<Item> lst = new List<Item>();
            foreach (Item it in item)
            {
                lst.Add(it);
            }
            lst.RemoveAt(lst.Count-1);
            return lst;
        }
        /**
         * <{bd}{c}{b}>
         * <{a}{bd}{b}{c}{b}>
         */
        public bool findAllItems(Comp sk) // {bd}
        {
            bool allItemsFound = true;
            foreach (Item it in sk.item)
            {
                int index = -1;
                for(int i = 0; i < item.Count; i++)
                {
                    if (this.item.ElementAt(i).name == it.name)
                    {
                        index = i;
                        break;
                    }
                }
                if (index == -1)
                {
                    allItemsFound = false;
                    break;
                }
            }
            return allItemsFound;
        }

        public bool findAtLeastItem(Comp sk)
        {
            foreach(Item it in sk.item)
            {
                foreach (Item itm in this.item)
                {
                    if (it.name == itm.name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void addLostItem(Comp compare)
        {
            foreach(Item it in compare.item)
            {
                bool notFound = true;

                foreach(Item itm in this.item)
                {
                    if(it.name == itm.name)
                    {
                        notFound = false;
                        break;
                    }
                }

                if (notFound)
                {
                    this.item.Add(it);
                }
            }
        }
        public bool compareTo(Comp comp)
        {
            if (this.ToString() == comp.ToString())
            {
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            string cmp = "{";
            foreach(Item it in item)
            {
                if(it == item.ElementAt(item.Count - 1))
                {
                    cmp += it.name;
                }
                else
                {
                    cmp += it.name + ",";
                }
            }
            cmp += "}";
            return cmp;
        }
    }
}
