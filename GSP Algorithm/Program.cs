using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSP_Algorithm
{
    public class Program
    {
        public static bool keepOnLVL3 = true;
        public static bool FirstOfLVL3 = true;
        public static List<Sequance> transactions = new List<Sequance>();
        public static List<Item> itemsCounter = new List<Item>();
        public static List<Item> frequant = new List<Item>();
        public static List<Sequance> sequances = new List<Sequance>();
        public static List<Sequance> freqSeq = new List<Sequance>();
        public static List<Sequance> candidates = new List<Sequance>();
        public static List<Sequance> freqCandidates = new List<Sequance>();
        public static List<Sequance> tempCandidates = new List<Sequance>();
        public static float minSupport = 0.4f;

        static void Main(string[] args)
        {
            List<string> transData = new List<string>();

            transData.Add("{b,d},c,b");
            transData.Add("{b,f},{c,e},b");
            transData.Add("{a,g},b");
            transData.Add("{b,e},{c,e}");
            transData.Add("a,{b,d},b,c,b");
            
            foreach(string dt in transData)
            {
                Sequance sq = new Sequance(DataSpliter(dt));
                transactions.Add(sq);
                Console.WriteLine(sq);
            }


            minSupport = (int)(minSupport * transData.Count);

            // ------------ Initialize Start Data ----------
            /*string[][] data = new string[5][];
            data[0] = new string[5];
            data[1] = new string[1];
            data[2] = new string[4];
            data[3] = new string[4];
            data[4] = new string[5];
            // -----------------------------------
            data[0] = new string[] {"a","b","fg","c","d"}; // {bd}{c}
            data[1] = new string[] {"bgd"};
            data[2] = new string[] {"b","f","g","ab"};
            data[3] = new string[] {"f" ,"ab", "c","d"};
            data[4] = new string[] {"a","bc","g","f","de"};
            // --------------------------
            foreach(string[] d in data)
            {
                Sequance t = new Sequance();
                t.initializeData(d);
                transactions.Add(t);
            }*/
            //--------------------------------------------------------------------------

            LVL1.Run_LVL1();
            //Console.WriteLine("-----------LVL1-----------");
            //foreach (Item it in frequant)
            //{
            //    Console.WriteLine(it.name + " | Support = " + it.counter);
            //}


            LVL2.Run_LVL2();
            Console.WriteLine("-----------LVL2-----------");
            //Console.WriteLine(freqSeq.Count);
            foreach (Sequance sq in freqSeq)
            {
                Console.WriteLine(sq + " | Support = " + sq.support);
            }

            int counter = 2;
            while (keepOnLVL3)
            {
                counter++;
                LVL3.Run_LVL3();
                Console.WriteLine("-----------LVL" + counter + "-----------");
                foreach (Sequance sq in freqCandidates)
                {
                    Console.WriteLine(sq + " | Support = " + sq.support);
                }
            }

#if DEBUG
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
#endif
        }

        static string[] DataSpliter(string data) // a,b,{f,g},c,{d,e}
        {
            int counter = 0;
            Dictionary<string, string> comps = new Dictionary<string, string>();

            // Get Compinations
            while (true)
            {
                int startComp = indexOf(data, '{');
                int endComp = indexOf(data, '}');
                if (startComp != -1)
                {
                    string cypher = "GSP:" + counter.ToString();
                    string tmp = data.Substring(startComp, endComp - startComp + 1);
                    string tmp2 = data.Substring(startComp+1, endComp - startComp-1);
                    data = data.Replace(tmp, cypher);
                    comps.Add(cypher, tmp2);
                    counter++;
                }
                else
                {
                    break;
                }
            }

            // Concatinate to get 2D data
            string[] spliter = data.Split(',');
            for(int i=0;i<spliter.Length;i++)
            {
                if (comps.ContainsKey(spliter[i]))
                {
                    spliter[i] = comps[spliter[i]];
                }
            }

            return spliter;
        }

        static int indexOf(string data,char c)
        {
            int index = -1;
            for (int i=0;i< data.Length; i++)
            {
                if(data[i] == c)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }
}
