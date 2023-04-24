namespace _06._Heap
{
    internal class Program
    {
        /******************************************************
		 * 힙 (Heap)
		 * 
		 * 힙 속성을 만족하는 트리 기반의 비선형 자료구조
		 * 부모 노드가 항상 자식노드보다 우선순위가 높은 속성을 만족하는 트리기반의 자료구조
		 * 많은 자료 중 우선순위가 가장 높은 요소를 빠르게 가져오기 위해 사용
		 * 
		 * Heap은 부모 노드가 항상 자식 노드보다 크거나 같은 경우(max heap)와
		 * 부모 노드가 항상 자식 노드보다 작거나 같은 경우(min heap)으로 나누어짐
		 * max heap은 루트 노드의 데이터를 꺼내면 최댓값을 반환하고
		 * min heap은 루트 노드의 데이터를 꺼내면 최솟값을 반환함
		 * Heap은 한번에 하나씩 최대 혹은 최소의 데이타를 가져오는 기능이 가장 핵심적인 기능
		 ******************************************************/

        // 우선순위 큐
        static void PriorityQueue()
        {
            // 우선순위 -> int가 아니어도 비교 가능한 자료면 됨. int는 오름차순이 기본
            DataStructure.PriorityQueue<string, int> ascendingPQ = new DataStructure.PriorityQueue<string, int>();

            ascendingPQ.Enqueue("감자", 3);    // 데이터와 우선순위 추가
            ascendingPQ.Enqueue("양파", 5);
            ascendingPQ.Enqueue("당근", 1);
            ascendingPQ.Enqueue("토마토", 4);
            ascendingPQ.Enqueue("마늘", 2);

            while(ascendingPQ.Count > 0)
            {
                Console.WriteLine(ascendingPQ.Dequeue());    // 우선순위가 높은 순서대로 데이터 출력
            }

            // 내림차순
            PriorityQueue<string, int> descendingPQ 
                = new PriorityQueue<string, int>(Comparer<int>.Create((a,b) => b-a));

            descendingPQ.Enqueue("감자", 3);    // 데이터와 우선순위 추가
            descendingPQ.Enqueue("양파", 5);
            descendingPQ.Enqueue("당근", 1);
            descendingPQ.Enqueue("토마토", 4);
            descendingPQ.Enqueue("마늘", 2);

            while (descendingPQ.Count > 0)
            {
                Console.WriteLine(descendingPQ.Dequeue());    // 우선순위가 높은 순서대로 데이터 출력
            }
        }

        // 시간복잡도
        // 탐색(가장우선순위높은)    추가       삭제
        // O(1)                     O(logN)    O(logN)
        // 가장 우선순위가 높거나 낮은 자료를 찾을 때 매우 효율적

        static void Main(string[] args)
        {
            Midnum.midNum();
        }
    }
}