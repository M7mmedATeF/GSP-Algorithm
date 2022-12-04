using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSP_Algorithm
{
    /** Level1
    *  sammary:
    *      loop1:
    *          Add Unique ITEMs
    *      Loop2:
    *          Get Frequent with Min-Support
    *      initFunc:
    *          initialize added => false;
    */
    public static class LVL1
    {
        public static void Run_LVL1()
        {
            /**
            *  sammary:
            *      Count every event's Support  
            */
            for (int i = 0; i < Program.transactions.Count; i++)
            {
                initCounterBool(Program.itemsCounter);
                Sequance sq = Program.transactions.ElementAt(i);
                // Comp
                for (int j = 0; j < sq.seq.Count; j++)
                {
                    Comp cmp = sq.seq.ElementAt(j);
                    // Item
                    for (int x = 0; x < cmp.itemLength; x++) // {asdasd}
                    {
                        int index = -1;
                        for (int z = 0; z < Program.itemsCounter.Count; z++)
                        {
                            if (Program.itemsCounter.ElementAt(z).name == cmp.item.ElementAt(x).name)
                            {
                                index = z;
                                break;
                            }
                        }

                        if (index != -1)
                        {
                            if (!Program.itemsCounter.ElementAt(index).added)
                            {
                                Program.itemsCounter.ElementAt(index).counter++;
                                Program.itemsCounter.ElementAt(index).added = true;
                            }
                        }
                        else
                        {
                            Program.itemsCounter.Add(new Item(cmp.item.ElementAt(x).name));
                        }
                    }
                }
            }

            /**
            *  sammary:
            *      Get Frequant events and add it in Frequant List
            */
            for (int i = 0; i < Program.itemsCounter.Count; i++)
            {
                if (Program.itemsCounter.ElementAt(i).counter >= Program.minSupport)
                {
                    Program.frequant.Add(Program.itemsCounter.ElementAt(i));
                }
            }

            /**
            *  sammary:
            *      Helper Function that initialize event's bool in the start of every sequance  
            */
            void initCounterBool(List<Item> list)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    list.ElementAt(j).added = false;
                }
            }
        }
    }
}
