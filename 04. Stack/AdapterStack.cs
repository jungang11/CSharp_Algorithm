using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    /******************************************************
	 * 어댑터 패턴 (Adapter)
	 * 
	 * 한 클래스의 인터페이스를 사용하고자 하는 다른 인터페이스로 변환
	 ******************************************************/

    internal class AdapterStack<T>
    {
        // container에 List를 받아옴
        private List<T> container;

        public AdapterStack()
        {
            // List와 Stack의 기능이 유사하기 때문에 받아와 사용가능
            container = new List<T>();
        }

        // 데이터를 삽입하는 함수
        public void Push(T item)
        {
            // 배열 기반의 스택에서 삽입 연산은 List에서 Add함수와 같이 최상위 노드에 요소를 하나 더 해주면 됨
            container.Add(item);    // List의 Add 함수
        }

        // 데이터를 제거하는 함수
        public T Pop()
        {
            // 스택에서 데이터 제거는 최상위 노드의 값을 지워주기만 하면 끝임.
            T item = container[container.Count - 1]; // 가장 끝에 있는 데이터 꺼내기
            container.RemoveAt(container.Count - 1); // 최상위가 갖고있는 값은 실제 인덱스보다 1 크기 때문에 Count-1
            return item;
        }

        public T Peek()
        {
            return container[container.Count - 1];  // 가장 끝에 있는 데이터 반환
        }

        public void Clear()
        {
            container.Clear();
        }

        public bool IsEmpty()
        {
            if (container.Count == 0) return true;
            else return false;
        }
    }
}
