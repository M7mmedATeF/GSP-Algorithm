using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSP_Algorithm
{
    public class Sequance
    {
        public List<Comp> seq = new List<Comp>();
        public int support = 0;
        public int transLength = 0;

        public Sequance()
        {

        }

        public Sequance(string[] data)
        {
            // Split Data
            foreach(string dt in data)
            {
                Comp cp = new Comp(dt);
                seq.Add(cp);
            }
        }
        public Sequance(List<Comp> cp)
        {
            this.seq = cp;
        }

        public Sequance(Sequance sq)
        {
            foreach(Comp cp in sq.seq)
            {
                Comp tmp = new Comp(cp);
                this.seq.Add(tmp);
            }
        }

        public bool hasDB() {
            foreach(Comp cp in seq)
            {
                if(cp.item.Count > 1)
                {
                    return true;
                }
            }
            return false;
        }
        public Comp getFirst()
        {
            return this.seq.ElementAt(0);
        }
        public Comp getLast()
        {
            return this.seq.ElementAt(this.seq.Count-1);
        }
        public Sequance seqWithoutFirst()
        {
            Sequance sq = new Sequance();
            bool head = true;
            foreach (Comp cp in seq)
            {
                if (head)
                {
                    head = false;
                    if (cp.item.Count == 1)
                    {
                        continue;
                    }
                    else
                    {
                        Comp ncp = new Comp(cp.withoutFirstItem());
                        sq.seq.Add(ncp);
                    }
                }
                else
                {
                    sq.seq.Add(cp);
                }
            }
            return sq;
        }
        public Sequance seqWithoutLast()
        {
            Sequance sq = new Sequance();
            for (int i = 0; i < seq.Count - 1; i++)
            {
                sq.seq.Add(seq.ElementAt(i));
            }

            // Last Element
            Comp cp = seq.ElementAt(seq.Count - 1);
            if (cp.item.Count != 1)
            {
                Comp ncp = new Comp(cp.withoutLastItem());
                sq.seq.Add(ncp);
            }
            return sq;
        }
        public override string ToString()
        {
            string sq = "<";
            foreach(Comp cp in this.seq)
            {
                sq += cp.ToString();
            }
            sq += ">";
            return sq;
        }
        public bool compareTo(Sequance sequance)
        {
            return this.ToString() == sequance.ToString();
        }
        public string withoutFirstItem()
        {
            string rt = "<";
            bool head = true;
            foreach(Comp cp in seq)
            {
                if (head)
                {
                    head = false;
                    if (cp.item.Count == 1)
                    {
                        continue;
                    }
                    else
                    {
                        Comp ncp = new Comp(cp.withoutFirstItem());
                        rt += ncp.ToString();
                    }
                }
                else
                {
                    rt += cp.ToString();
                }
            }
            rt += ">";
            return rt;
        }
        public string withoutLastItem()
        {
            string rt = "<";
            
            for(int i=0;i< seq.Count - 1; i++)
            {
                rt += seq.ElementAt(i).ToString();
            }

            // Last Element
            if (seq.Count - 1 >= 0)
            {
                Comp cp = seq.ElementAt(seq.Count - 1);
                if (cp.item.Count != 1)
                {
                    Comp ncp = new Comp(cp.withoutLastItem());
                    rt += ncp.ToString();
                }
            }

            rt += ">";
            return rt;
        }
        public void initializeData(string[] data) 
        {
            foreach(string d in data) // {acc}
            {
                transLength++;
                Comp newComp = new Comp(d);
                seq.Add(newComp);
            }
        }
        public bool findInSeq(Comp sk,ref int start) // {bd}
        {
            for (; start < seq.Count; start++)
            {
                if (seq.ElementAt(start).findAllItems(sk))
                {
                    start++;
                    return true;
                }
            }
            return false;
        }
        public void createComp(string c)
        {
            Comp cmp = new Comp(c);
            seq.Add(cmp);
        }
        public void createComp(string c1 , string c2)
        {
            string s = c1 + "," + c2;
            Comp cmp = new Comp(s);
            seq.Add(cmp);
        }
    }
}
