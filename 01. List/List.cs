using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    internal class List<T>  // Generic
    {
        private const int DefaultCapacity = 10;

        private T[] items;
        private int size;

        public List()
        {
            this.items = new T[DefaultCapacity];
            this.size = 0;
        }

        public int Capacity { get { return items.Length; } }
        public int Count { get { return size; } }

        // indexer
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= size)
                {
                    throw new IndexOutOfRangeException();
                }
                return items[index];
            }
            set
            {
                if (index < 0 || index >= size)
                {
                    throw new IndexOutOfRangeException();
                }
                items[index] = value;
            }
        }

        // 아이템을 집어넣는 함수
        public void Add(T item)
        {
            if(size < items.Length)
            {
                items[size++] = item;
            }
            else
            {
                Grow();
                items[size++] = item;
            }
        }

        // 아이템을 지우는 함수
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            else
                return false;
        }

        // index 위치를 지우는 함수
        public void RemoveAt(int index)
        {
            if( index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException();
            }

            size--; // 지웠으니 size 하나 감소
            Array.Copy(items, index + 1, items, index, size - index);
        }

        // item의 인덱스 찾기 함수
        public int IndexOf(T item)
        {
            // items에 item이 있는지 0에서 size만큼 찾기
            return Array.IndexOf(items, item, 0, size);
        }

        // 못찾으면 Null
        public T? Find(Predicate<T> match)
        {
            if(match == null)
                throw new ArgumentNullException("match"); // match가 Null
        
            for(int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    return items[i];
            }

            return default(T);  // 기본값을 반환해줌
        }

        public int FindIndex(Predicate<T> match)
        {
            for(int i = 0; i < size; i++)
            {
                if (match(items[i])) return i;
            }
            return -1;
        }

        // 리스트의 길이를 넘었을 경우 길이를 늘리는 함수
        public void Grow()
        {
            int newCapacity = items.Length * 2;
            T[] newItems = new T[newCapacity];
            Array.Copy(items, 0, newItems, 0, size); // item의 내용을 newItems로 복사
            items = newItems;
        }
    }
}
