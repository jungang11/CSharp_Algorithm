using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06._Heap
{
    public class Midnum
    {
        // 중간값 구하기
        // 우선순위 큐 두개를 이용해 중앙값 구하기
        // 최대힙과 최소힙을 준비. 첫 값을 중앙값으로 설정.
        // 그 뒤부터 입력받은 수를 중앙값과 비교해 최대힙 최소힙에 Enqueue
        // 중앙값은 최대힙의 루트노드
        public static void midNum()
        {
            DataStructure.PriorityQueue<int, int> MinHeap =
                new DataStructure.PriorityQueue<int, int>();

            DataStructure.PriorityQueue<int, int> MaxHeap
                = new DataStructure.PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b - a));

            
            while (true)
            {
                Console.Write("숫자 입력 : ");
                int num = int.Parse(Console.ReadLine());

                if (MaxHeap.Count == 0 && MinHeap.Count == 0)
                {
                    MaxHeap.Enqueue(num, num);
                }
                else if(MaxHeap.Count > MinHeap.Count)
                {
                    if(num < MaxHeap.Peek())
                    {
                        MinHeap.Enqueue(MaxHeap.Peek(), MaxHeap.Peek());
                        MaxHeap.Dequeue();
                        MaxHeap.Enqueue(num, num);
                    }
                    else
                    {
                        MinHeap.Enqueue(num, num);
                    }
                }
                else if(MaxHeap.Count == MinHeap.Count)
                {
                    if(num > MaxHeap.Peek())
                    {
                        MinHeap.Enqueue(num, num);
                        MaxHeap.Enqueue(MinHeap.Peek(), MinHeap.Peek());
                        MinHeap.Dequeue();
                    }
                    else
                    {
                        MaxHeap.Enqueue(num, num);
                    }
                }
                // 숫자의 개수가 홀수일때 중간값 출력
                if((MaxHeap.Count+MinHeap.Count) % 2 == 1)
                    Console.WriteLine("현재 중간값 : {0}",MaxHeap.Peek());
            }
        }
    }
}
