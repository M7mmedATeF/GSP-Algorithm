using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSP_Algorithm
{
    public static class LVL3
    {

        public static void Run_LVL3()
        {

            //Console.WriteLine("Run Time");
            if (Program.FirstOfLVL3)
            {
                Program.FirstOfLVL3 = false;
            }

            // Generate candidate sequences
            foreach (Sequance sq in Program.freqSeq)
            {
                string first = sq.withoutFirstItem();
                foreach (Sequance ssq in Program.freqSeq)
                {
                    if(sq.ToString() != ssq.ToString())
                    {
                        if (first == ssq.withoutLastItem())
                        {
                            Program.candidates.Add(mergeSequance(sq, ssq));
                        }
                    }
                }
            }

            // Get Support Of Candidates
            LVL2.getSupportOfSequances(Program.candidates);

            // Support Pruning
            Program.freqCandidates.Clear();
            foreach (Sequance sq in Program.candidates)
            {
                if(sq.support >= Program.minSupport)
                {
                    Program.freqCandidates.Add(sq);
                }
            }

            if(Program.freqCandidates.Count > 1)
            {
                Program.candidates.Clear();
                Program.freqSeq.Clear();
                Program.tempCandidates.Clear();
                foreach(Sequance sq in Program.freqCandidates)
                {
                    Program.freqSeq.Add(sq);
                    Program.tempCandidates.Add(sq);
                }
            }
            else
            {
                Program.keepOnLVL3 = false;
                if (Program.freqCandidates.Count != 1)
                {
                    Program.freqCandidates = Program.tempCandidates;
                }
            }
        }

        private static Sequance mergeSequance(Sequance s1, Sequance s2)
        {
            s1 = new Sequance(s1);
            s2 = new Sequance(s2);
            int index = 0;
            foreach (Comp cp in s1.seq)
            {
                if (index < s2.seq.Count)
                {
                    Comp cp2 = s2.seq.ElementAt(index); 
                    if (cp == s1.seq.ElementAt(0) && cp.item.Count == 1)
                    {
                        continue;
                    }else if (cp.item.Count >= cp2.item.Count && cp.findAtLeastItem(cp2))
                    {
                        cp2.printed = false;
                        cp.addLostItem(cp2);
                        index++;
                    }else if(cp2.findAtLeastItem(cp))
                    {
                        cp2.printed = false;
                        cp.addLostItem(cp2);
                    }
                }
            }

            List<Comp> cmps = new List<Comp>();
            foreach (Comp cp in s1.seq)
            {
                if (cp.printed)
                    cmps.Add(cp);
            }
            foreach (Comp cp in s2.seq)
            {
                if (cp.printed)
                    cmps.Add(cp);
            }

            // Console.WriteLine("Merge: " + new Sequance(cmps));
            return new Sequance(cmps);


            /*int s1L = s1.getLast().item.Count;
            int s2F = s2.getFirst().item.Count;
            if (s1L <= s2F)
            {
                s1 = s1.seqWithoutLast();
            }
            else if (s1L > s2F)
            {
                s2 = s2.seqWithoutFirst();
            }
            List<Comp> cmps = new List<Comp>();
            foreach (Comp cp in s1.seq)
            {
                cmps.Add(cp);
            }
            foreach (Comp cp in s2.seq)
            {
                cmps.Add(cp);
            }

            return new Sequance(cmps);*/
        }
    }
}
