using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class LinkedListNode<T> // LinkedList는 Node 기반이기 때문에 LinkedListNode 구현
    {
        internal LinkedList<T> list;         // 소속 리스트
        internal LinkedListNode<T> prev;     // 이전 위치
        internal LinkedListNode<T> next;     // 다음 위치
        private T item;                      // 실제 데이터

        // 생성자 3종류
        public LinkedListNode(T value)
        {
            this.list = null;
            this.prev = null;
            this.next = null;
            this.item = value;
        }

        public LinkedListNode(LinkedList<T> list, T value)
        {
            this.list = list;
            this.prev = null;
            this.next = null;
            this.item = value;
        }

        public LinkedListNode(LinkedList<T> list, LinkedListNode<T> prev, LinkedListNode<T> next, T value)
        {
            this.list = list;
            this.prev = prev;
            this.next = next;
            this.item = value;
        }

        // property
        public LinkedList<T> List { get { return list; } }
        public LinkedListNode<T> Prev { get { return prev; } }
        public LinkedListNode<T> Next { get { return next; } }
        // value 는 읽기/쓰기 가능
        public T Value { get { return item; } set { item = value; } }
    }
    
    public class LinkedList<T>
    {
        // head : 가장 앞에 있는 노드 / tail : 가장 뒤에 있는 노드
        private LinkedListNode<T> head;
        private LinkedListNode<T> tail;
        private int count;  // 몇 개 가지고 있는지

        // 생성자
        public LinkedList()
        {
            this.head = null;
            this.tail = null;
            this.count = 0;
        }

        // property
        public LinkedListNode<T> First { get { return head; } }
        public LinkedListNode<T> Last { get { return tail; } }
        public int Count { get { return count; } }

        // AddFirst 가장 앞에 붙이기
        public LinkedListNode<T> AddFirst(T value)
        {
            // 1. 새로운 노드 만들기
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
            // 2. 연결 구조 바꾸기 -> 클래스이므로 주소의 참조
            if(head != null)        // 2-1. head 노드가 있을 때
            {
                newNode.next = head;
                head.prev = newNode;
                head = newNode;
            }
            else                    // 2-2. head 노드가 없을 때 -> newNode밖에 없으니 head, tail이 newNode
            {
                head = newNode;
                tail = newNode;
            }
            // 3. 갯수 늘리기
            count++;
            return newNode;
        }

        public LinkedListNode<T> AddLast(T value)
        {
            // 1. 새로운 노드 만들기
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
            // 2. 연결 구조 바꾸기 -> 클래스이므로 주소의 참조
            if(tail != null)
            {
                newNode.prev = tail;
                tail.next = newNode;
                tail = newNode;
            }
            else
            {
                head = newNode;
                tail = newNode;
            }
            // 3. 갯수 늘리기
            count++;
            return newNode;
        }

        // 지정한 노드 앞에 붙이기
        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            // 예외
            if (node.list != this)   // 예외1 : node가 연결리스트에 포함된 노드가 아닌 경우
                throw new InvalidOperationException();
            if (node == null)       // 예외2 : node가 null인 경우
                throw new ArgumentNullException(nameof(node));
            // 1. 새로운 노드
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);

            // 2. 연결 구조 바꾸기
            newNode.next = node;
            newNode.prev = node.prev;
            node.prev = newNode;
            if (node.prev != null)
                node.prev.next = newNode;
            else
                head = newNode;

            // 3. 갯수 증가
            count++;
            return newNode;
        }
        // 지정한 노드 뒤에 붙이기
        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            // 예외
            if (node.list != this)   // 예외1 : node가 연결리스트에 포함된 노드가 아닌 경우
                throw new InvalidOperationException();
            if (node == null)       // 예외2 : node가 null인 경우
                throw new ArgumentNullException(nameof(node));
            // 1. 새로운 노드
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);

            // 2. 연결 구조 바꾸기
            newNode.prev = node;
            newNode.next = node.next;
            node.next = newNode;
            if (node.next != null)
                node.next.prev = newNode;
            else
                tail = newNode;

            // 3. 갯수 증가
            count++;
            return newNode;
        }

        // 노드 지우기
        public void Remove(LinkedListNode<T> node)
        {
            // 예외
            if(node.list != this)   // 예외1 : node가 연결리스트에 포함된 노드가 아닌 경우
                throw new InvalidOperationException();
            if (node == null)       // 예외2 : node가 null인 경우
                throw new ArgumentNullException(nameof(node));

            // 0. 지웠을 때 head나 tail이 변경되는 경우 적용
            // 0, 1 -> 양방향이기 때문에 else if 사용하지 않음
            if (head == node)
                head = node.next;
            if (tail == node)
                tail = node.prev;

            // 1. 연결구조 바꾸기
            if(node.prev!=null)
                node.prev.next = node.next;
            if(node.next!=null)
                node.next.prev = node.prev;
            
            // 2. 갯수 줄이기
            count--;
        }

        public bool Remove(T value)
        {
            LinkedListNode<T> findNode = Find(value);
            if(findNode != null)
            {
                Remove(findNode);
                return true;
            }
            else
            {
                return false;
            }
        }

        public LinkedListNode<T> Find(T value)
        {
            LinkedListNode<T> target = head;
            // 일반화에 대해서 비교할 수 없어서 EqualityComparer 사용
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            while (target != null)
            {
                // value와 target.Value가 같다면 target 반환
                if (comparer.Equals(value, target.Value))
                    return target;
                // 아니라면 다음 노드 확인
                else
                    target = target.next;
            }
            return null;
        }
    }
}
