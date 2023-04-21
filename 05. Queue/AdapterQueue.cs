using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    // 스택은 AdapterStack을 사용해도 되지만 Queue같은 경우는 직접 구현함.
    // => Queue는 추가/삭제가 많고 중요한데 c#에서 LinkedList로 추가/삭제 기능을 자주 쓰면 비효율적이기 때문

    public class AdapterQueue<T>
    {
        private LinkedList<T> container;

        public AdapterQueue()
        {
            container = new LinkedList<T>();
        }

        public void EnQueue(T item)
        {
            container.AddFirst(item);
        }

        public T DeQueue()
        {
            T item = container.First();
            container.RemoveFirst();
            return item;
        }

        public T Peek()
        {
            return container.First();
        }
    }
}
