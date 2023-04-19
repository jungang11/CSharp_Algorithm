using System.Runtime.InteropServices;

namespace _02._LinkedList
{
    internal class Program
    {
        /******************************************************
		 * 연결리스트 (Linked List)
		 * 
		 * 데이터를 포함하는 노드들을 연결식으로 만든 자료구조
		 * 노드는 데이터와 이전/다음 노드 객체를 참조하고 있음
		 * 노드가 메모리에 연속적으로 배치되지 않고 이전/다음노드의 위치를 확인
		 * 
		 * 힙 영역의 데이터들이 연속적으로 있지 않지만 이전/다음 데이터의 위치를 알고있음
		 * 배열처럼 데이터들이 연속적이지 않기 때문에 배열의 경우보다 추가/삭제가 효율적임
		 * 하나씩 당겨오거나 할 필요없이 가리키는 참조 위치만 바꾸어주면 되기 때문
		 ******************************************************/

        // <링크드리스트 사용>
        // 추가/삭제가 용이해서 AddFirst, AddLast의 함수가 제공됨
        void LinkedList()
        {
            LinkedList<string> linkedList = new LinkedList<string>();

            // 링크드리스트 요소 삽입
            linkedList.AddFirst("0번 앞데이터");
            linkedList.AddFirst("1번 앞데이터");
            linkedList.AddLast("0번 뒤데이터");
            linkedList.AddLast("1번 뒤데이터");

            // 링크드리스트 요소 삭제 => O(n) 
            linkedList.Remove("1번 앞데이터");

            // 링크드리스트 요소 탐색
            LinkedListNode<string> findNode = linkedList.Find("0번 뒤데이터");

            // 링크드리스트 노드를 통한 노드 참조
            // 링크드리스트는 노드 기반의 연결 구조
            LinkedListNode<string> prevNode = findNode.Previous;
            LinkedListNode<string> nextNode = findNode.Next;

            // 링크드리스트 노드를 통한 노드 삽입
            linkedList.AddBefore(findNode, "찾은노드 앞데이터");
            linkedList.AddAfter(findNode, "찾은노드 뒤데이터");

            // 링크드리스트 노드를 통한 삭제 => O(1)
            linkedList.Remove(findNode);
        }


        // <List의 시간복잡도>     => LinkedList와 접근의 시간복잡도가 다른 이유는 데이터의 연속성
        // 접근		탐색		삽입		삭제
        // O(1)		O(n)	    O(n)	    O(n)

        // <LinkedList의 시간복잡도> => 대신 연속적이지 않기 때문에 삽입/삭제가 효율적임
        // 접근		탐색		삽입		삭제
        // O(n)		O(n)	    O(1)	    O(1)

        // 삽입/삭제 효율이 좋고 상대적으로 접근/탐색의 효율이 좋지않음 => 연속적이지 않기 때문
        // 특정 노드를 찾기 위해선 하나씩 찾아봐야함
        // LinkedList는 배열의 구조가 아니기 때문에 index를 사용할 수 없음
        // 연결형리스트는 삽입/삭제의 속도가 중요한 자료에 쓸 수 있음
        // 배열처럼 데이터가 꽉 차있을 경우가 없기 때문에 언제든 추가할 수 있음

        // C# 은 가비지컬렉터의 유무 때문에 LinkedList를 많이 사용하진 않음 (c++에선 유용)
        // => 이진탐색같은 노드기반의 구조 또한 마찬가지

        // 단방향 LinkedList : 이전의 주소가 필요없음 -> 참조용 데이터가 하나 적음
        // 양방향 LinkedList : 이전, 다음의 참조용 데이터가 필요
        // 환 형 LinkedList : 양방향과 비슷하지만 마지막 노드는 처음의 노드를 가리킴 -> c#

        public static void Main(string[] args)
        {
            DataStructure.LinkedList<string> linkedList = new DataStructure.LinkedList<string>();

            linkedList.AddFirst("첫 데이터 삽입");
            linkedList.AddLast("두번째 데이터 삽입");
            linkedList.AddFirst("세번째 데이터 삽입");
            linkedList.AddLast("네번째 데이터 삽입");
            // 현재 3 1 2 4

            // 첫 데이터의 노드 value1node
            DataStructure.LinkedListNode<string>? value1node = linkedList.Find("첫 데이터 삽입");
            linkedList.AddBefore(value1node, "첫 데이터 앞에 삽입");
            linkedList.AddAfter(value1node, "첫 데이터 뒤에 삽입");

            // node Remove
            linkedList.Remove(value1node);
            // RemoveLast
            linkedList.RemoveLast();
            // 현재 3 2

            Console.WriteLine("linkedList.Contains(\"세번째 데이터 삽입\") : {0}" , linkedList.Contains("세번째 데이터 삽입")); // true




        }
    }
}