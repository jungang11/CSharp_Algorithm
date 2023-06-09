﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    // 1. 선형리스트 구현
    // 인덱서[], Add, Remove, Find, FindIndex, Count
    internal class ListEx<T>
    {
        const int DefaultCapacity = 10;

        private T[] items;
        private int size;

        public ListEx()
        {
            items = new T[DefaultCapacity];
            size = 0;
        }

        // 인덱서
        public T this[int index]
        {
            get { return items[index]; }
            set { items[index] = value; }
        }

        // item을 더하는 함수
        public void Add(T item)
        {
            // items의 다음 인덱스에 item을 넣음
            if(size < items.Length)
                items[size++] = item;
            // Length 늘리기
            else
            {
                int newCapacity = items.Length * 2;
                T[] newItems = new T[newCapacity];
                Array.Copy(items, 0, newItems, 0, size);
                items = newItems;
                items[size++] = item;
            }
        }

        // 리스트에서 맨 처음 발견되는 특정 개체 제거
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                size--; // 삭제했으니 사이즈를 줄임
                // 삭제된 index의 1칸 뒤의 요소들을 한칸씩 앞으로 당김
                Array.Copy(items, index + 1, items, index, size - index);
                return true;
            }
            return false;
        }

        // item 의 인덱스 찾기
        public int IndexOf(T item)
        {
            // items 에서 item을 0부터 size만큼 찾기
            return Array.IndexOf(items, item, 0, size);
        }

        // 못찾으면 Null. items에서 match 조건에 맞는 아이템을 찾아 반환
        public T? Find(Predicate<T> match)
        {
            for (int i = 0; i < size; i++)
            {
                if (match(items[i]))
                    return items[i];
            }
            return default(T);  // 기본값을 반환해줌
        }
        // 조건과 일치하는 요소를 검색하고 전체 리스트에서 마지막으로 검색한 요소를 반환
        // Find의 반대
        public T? FindLast(Predicate<T> match)
        {
            for (int i = size; i >= 0; i--)
            {
                if (match(items[i]))
                    return items[i];
            }
            return default(T);  // 기본값을 반환해줌
        }

        // match : 검색할 요소의 조건
        // match 조건에 맞는 Index 찾기
        public int FindIndex(Predicate<T> match)
        {
            for(int i = 0; i < size; i++)
            {
                // 조건에 맞으면 i 반환
                if (match(items[i])) 
                    return i;
            }
            // 조건에 맞지 않으면 -1 반환
            return -1;
        }
        public int FindLastIndex(Predicate<T> match)
        {
            for (int i = size; i >= 0; i--)
            {
                // 조건에 맞으면 i 반환
                if (match(items[i]))
                    return i;
            }
            // 조건에 맞지 않으면 -1 반환
            return -1;
        }

        // Count 구현. 요소의 갯수 반환
        public int Count { get { return size; } }
        // Capacity. 크기 반환
        public int Capacity { get { return items.Length; } }

        // List<T>에 요소가 있는지 여부를 확인
        public bool Contains(T item)
        {
            for (int i = 0; i < size; i++)
            {
                // 같은 요소가 있으면 true 반환
                if (items[i].Equals(item))
                    return true;
            }
            return false;  // 없으면 false 반환
        }

        // 지우기
        public void Clear()
        {
            items = new T[DefaultCapacity];
            size = 0;
        }



    }
}
