using System;
using System.Collections.Generic;
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
        private List<T> container;

        public AdapterStack()
        {
            container = new List<T>();
        }

        public void Push(T item)
        {
            container.Add(item);    // List의 Add 함수
        }

        public T Pop()
        {
            T item = container[container.Count - 1]; // 가장 끝에 있는 데이터 꺼내기
            container.RemoveAt(container.Count - 1); // 지우기
            return item;
        }

        public T Peek()
        {
            return container[container.Count - 1];  // 가장 끝에 있는 데이터 반환
        }
    }
}
