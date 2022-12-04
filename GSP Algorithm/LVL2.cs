using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSP_Algorithm
{
    public static class LVL2
    {
        public static void Run_LVL2()
        {
            for (int i = 0; i < Program.frequant.Count - 1; i++)
            {
                Program.sequances.Add(getSeqOfSelf(Program.frequant.ElementAt(i).name, Program.frequant.ElementAt(i).name)); // {a}{a}
                for (int j = i + 1; j < Program.frequant.Count; j++)
                {
                    Program.sequances.Add(getSeqOfSelf(Program.frequant.ElementAt(i).name, Program.frequant.ElementAt(j).name));  // {a}{e}
                    Program.sequances.Add(getSeqOfSelf(Program.frequant.ElementAt(j).name, Program.frequant.ElementAt(i).name));  // {e}{a}
                    Program.sequances.Add(getSeqOfOther(Program.frequant.ElementAt(i).name, Program.frequant.ElementAt(j).name)); // {ae}
                }
            }

            Program.sequances.Add(getSeqOfSelf(Program.frequant.ElementAt(Program.frequant.Count - 1).name, Program.frequant.ElementAt(Program.frequant.Count - 1).name)); // {e} {e}

            // Get Support of Program.sequances
            getSupportOfSequances(Program.sequances);
            //foreach (Sequance sq in Program.sequances)
            //{
            //    Console.WriteLine(sq + " | Support = " + sq.support);
            //}
            //Console.WriteLine("-------------------------");
            // Get Program.frequant sequances
            foreach (Sequance sq in Program.sequances)
            {
                if (sq.support >= Program.minSupport)
                {
                    int idx = -1;
                    for (int i = 0; i < Program.freqSeq.Count; i++)
                    {
                        if (Program.freqSeq.ElementAt(i).compareTo(sq))
                        {
                            Program.freqSeq.ElementAt(i).support++;
                            idx = i;
                            break;
                        }
                    }
                    if (idx == -1)
                    {
                        Program.freqSeq.Add(sq);
                    }
                }
            }
        }

        // Helper Functions
        private static Sequance getSeqOfSelf(string c1, string c2)
        {
            Sequance sq = new Sequance(); // <{c1}{c2}>
            sq.createComp(c1);
            sq.createComp(c2);
            return sq;
        }
        private static Sequance getSeqOfOther(string c1, string c2)
        {
            Sequance sq = new Sequance(); // <{c1c2}>
            sq.createComp(c1, c2);
            //Console.WriteLine("test: " + sq);
            return sq;
        }
        public static void getSupportOfSequances(List<Sequance> searchFor) // <{bd}{c}{b}>
        {
            foreach (Sequance sq in searchFor)
            {
                foreach (Sequance tsq in Program.transactions) // <{a}{bd}{b}{c}{b}>
                {
                    int start = 0;
                    bool gotAll = true;
                    foreach (Comp cp in sq.seq)
                    {
                        if (!tsq.findInSeq(cp, ref start)) // {bd}
                        {
                            gotAll = false;
                            break;
                        }
                    }
                    if (gotAll)
                    {
                        sq.support++;
                    }
                }
            }
        }
    }

    
}
