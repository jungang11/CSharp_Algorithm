using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    // 노드기반x 배열기반o => 노드기반을 쓰지 않기위해 LinkedList 어댑터를 사용하지 않았음 
    // c#의 특성상 노드기반은 가비지 컬렉터의 대상이 되니 비효율적
    // 하지만 배열에서 데이터의 삭제는 모든 데이터를 당겨와야하는 것과 달리 Queue에서는 데이터들을 당겨오지 않음
    // 데이터를 꺼낸 후 가장 앞을 가리키는 위치를 뒤로 밀어내 당겨오는 작업을 생략할 수 있음
    // 더 이상 갈 곳이 없을 경우 맨 앞으로 돌아가게 됨 => 배열의 처음과 끝을 가리키는 포인터 2개 관리 => 환형 배열

    public class Queue<T>
    {
        private const int DefaultCapacity = 4;

        private T[] array;
        private int head;   // 전단
        private int tail;   // 후단

        public Queue()
        {
            // 전단과 후단이 겹칠 경우를 대비해 실제의 용량보다 1만큼 더 크게 만들어 전단과 후단 사이를 비움
            array = new T[DefaultCapacity + 1]; 
            head = 0;
            tail = 0;
        }

        public int Count
        {
            get
            {
                if (head <= tail)
                    return tail - head;
                else
                    return tail - head + array.Length;
            }
        }

        // 추가 : tail 자리에 하나를 추가하고 민다
        // tail이 끝에 있었을 경우 맨 앞으로 가야함
        public void Enqueue(T item)
        {
            // 꽉 차있는 경우
            if (IsFull())
                Grow(); // 큐의 길이를 늘림

            array[tail] = item;
            MoveNext(ref tail);
        }

        // 데이터를 꺼냄(맨 앞 전단의 데이터) -> 전단의 위치를 이동함
        public T Dequeue()
        {
            // 비어있는 경우 꺼낼 수 없음
            if(IsEmpty())
                throw new InvalidOperationException();

            T result = array[head];
            MoveNext(ref head);
            return result;
        }

        // 맨 앞 최전방 데이터를 반환하는 함수
        public T Peek()
        {
            // 비어있는 경우 꺼낼 수 없음
            if (IsEmpty())
                throw new InvalidOperationException();

            return array[head];
        }

        // 데이터가 추가된 후 후단을 이동하는 함수. 원본이 바뀌어야 하기때문에 ref 를 받아옴
        private void MoveNext(ref int index)
        {
            // 끝에 있을 경우 0(맨 앞)으로 가고 아닐 경우 index가 1증가
            index = (index == array.Length - 1) ? 0 : index + 1;
        }

        // 전단과 후단이 같은 곳을 가리키면 비어있는 상태
        private bool IsEmpty()
        {
            return head == tail;
        }

        // 꽉 차있는 상태
        private bool IsFull()
        {
            // 후단이 전단보다 한칸 뒤에 있을 경우
            if (head > tail)
                return head == tail + 1;
            // head가 처음, tail이 마지막에 위치했을 경우 queue가 꽉 찼지만 head == tail + 1 이 아니게 됨
            else
                return head == 0 && tail == array.Length - 1;
        }

        public void Grow()
        {
            int newCapacity = DefaultCapacity * 2; // 기존 배열보다 용량을 2배 늘린 배열
            T[] newArray = new T[newCapacity];
            if (head < tail) // head가 앞에 있을 경우는 그대로 복사해줘도 됨
            {
                Array.Copy(array, newArray, Count);
            }
            else // head보다 tail이 앞에 있을 경우 head부터 끝까지 복사, 0부터 tail까지 복사하고 head를 0, tail을 끝으로
            {
                Array.Copy(array, head, newArray, 0, array.Length - head); // head부터 끝까지 데이터 복사
                Array.Copy(array, 0, newArray, array.Length - head, tail); // 0에서부터 tail까지 데이터 복사
                head = 0;       // head는 맨 앞에 위치
                tail = Count;   // tail은 데이터들 뒤에 위치
            }
            array = newArray;
        }
    }
}
