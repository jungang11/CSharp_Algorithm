using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _06._Heap
{
    public class Emergency
    {
        public static void Test()
        {
            DataStructure.PriorityQueue<string, int> patient = 
                new DataStructure.PriorityQueue<string, int>();

            patient.Enqueue("환자1", 50);
            patient.Enqueue("환자2", 40);
            patient.Enqueue("환자3", 80);
            patient.Enqueue("환자4", 120);
            patient.Enqueue("환자5", 110);
            patient.Enqueue("환자6", 70);

            while(patient.Count>0)
            {
                Console.WriteLine(patient.Dequeue());
                Thread.Sleep(1000);
            }
        }
    }
}
