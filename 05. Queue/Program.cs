namespace _05._Queue
{
    internal class Program
    {
        /******************************************************
		 * 큐 (Queue)
		 * 
		 * 선입선출(FIFO), 후입후출(LILO) 방식의 자료구조
		 * 입력된 순서대로 처리해야 하는 상황에 이용
		 ******************************************************/
        
        // 사용 용도가 직관적. 먼저 온 데이터가 먼저 처리되어야함.
        // ex) 대기열, 진행순서

        static void Test()
        {
            Queue<int> queue = new Queue<int>();

            for(int i = 0; i < 10; i++) { queue.Enqueue(i); }       // 0 1 2 3 4 5 6 7 8 9

            Console.WriteLine(queue.Peek());    // 가장 앞에 있는 데이터 : 0 => 최전방 데이터

            // stack: Push, Pop / queue: Enqueue, Dequeue
            while (queue.Count > 0) 
                Console.WriteLine(queue.Dequeue());     // 0 1 2 3 4 5 6 7 8 9
        }

        static void Main(string[] args)
        {
            Test();
        }
    }
}