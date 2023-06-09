﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10._Sort
{
    internal class Sort
    {
        /******************************************************
		 * 선형 정렬
		 * 
		 * 1개의 요소를 재위치시키기 위해 전체를 확인하는 정렬
		 * n개의 요소를 재위치시키기 위해 n개를 확인하는 정렬
		 * 시간복잡도 : O(N^2)
		 ******************************************************/

        // <선택정렬>
        // 데이터 중 가장 작은 값부터 하나씩 선택하여 정렬
        public static void SelectionSort(IList<int> list)
        {
            // 하나의 원소만 남을 때까지 과정을 반복한다.
            for (int i = 0; i < list.Count; i++)
            {
                int minIndex = i;
                // 맨 처음 위치를 뺀 나머지 리스트를 같은 방법으로 교체한다.
                for (int j = i + 1; j < list.Count; j++)
                {
                    // 주어진 배열 중 최솟값을 찾는다.
                    if (list[j] < list[minIndex])
                        // 그 값을 맨 앞에 위치한 값과 교체한다.
                        minIndex = j;
                }
                Swap(list, i, minIndex);
            }
        }

        // <삽입정렬>
        // 레코드 수가 적거나 대부분이 정렬되있을 경우 매우 효율적일 수 있음
        // 비교적 많은 레코드들의 이동을 포함, 레코드 수가 많고 크기가 클 경우 적합하지 않음
        // 데이터를 하나씩 꺼내어 정렬된 자료 중 적합한 위치에 삽입하여 정렬
        public static void InsertionSort(IList<int> list)
        {
            // 인덱스 0은 이미 정렬된 것으로 본다.
            for (int i = 1; i < list.Count; i++)
            {
                // 현재 삽입될 숫자인 list[i]를 key 변수로 복사한다.
                int key = list[i];
                int j;
                // 현재 정렬된 배열은 i-1까지이므로 i-1번째부터 역순으로 조사한다.
                // j 값은 음수가 아니어야 되고
                for (j = i - 1; j >= 0 && key < list[j]; j--)
                {
                    // key 값보다 정렬된 배열에 있는 값이 크면 j번째를 j+1번째로 이동
                    list[j + 1] = list[j];
                }
                list[j + 1] = key;
            }
        }

        // <버블정렬>
        // 구현이 매우 간단함. 선형 중에선 자주 사용
        // 하나의 요소가 가장 왼쪽에서 가장 오른쪽으로 이동할 경우 배열에서 모든 요소들과 교환해야하는 단점
        // 서로 인접한 데이터를 비교하여 정렬
        public static void BubbleSort(IList<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 1; j < list.Count; j++)
                {
                    if (list[j - 1] > list[j])
                        Swap(list, j - 1, j);
                }
            }
        }

        /******************************************************
		 * 분할정복 정렬
		 * 
		 * 1개의 요소를 재위치시키기 위해 전체의 1/2를 확인하는 정렬
		 * n개의 요소를 재위치시키기 위해 n/2개를 확인하는 정렬
		 * 시간복잡도 : O(NlogN)
		 ******************************************************/

        // <힙정렬>
        // 동일한 숫자의 순서까지 지켜주진 않음 => 깨질 수 있음 => 안정성이 떨어짐
        // 힙을 이용하여 우선순위가 가장 높은 요소부터 가져와 정렬
        public static void HeapSort(IList<int> list)
        {
            // 우선순위 큐 사용
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();

            // 우선순위 큐에 list를 Enqueue해 힙 트리를 만든다.
            for (int i = 0; i < list.Count; i++)
            {
                pq.Enqueue(list[i], list[i]);
            }
            // 만든 힙 트리를 하나씩 Dequeue하며 list에 넣는다.
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = pq.Dequeue();
            }
        }

        // <합병정렬>
        // 다른 정렬과 다르게 메모리적 부담이 있음
        // 데이터를 2분할하여 정렬 후 합병
        public static void MergeSort(IList<int> list, int left, int right)
        {
            if (left == right) return;

            int mid = (left + right) / 2;
            MergeSort(list, left, mid);
            MergeSort(list, mid + 1, right);
            Merge(list, left, mid, right);
        }

        public static void Merge(IList<int> list, int left, int mid, int right)
        {
            List<int> sortedList = new List<int>();
            int leftIndex = left;
            int rightIndex = mid + 1;

            // 분할 정렬된 List를 병합
            while (leftIndex <= mid && rightIndex <= right)
            {
                if (list[leftIndex] < list[rightIndex])
                    sortedList.Add(list[leftIndex++]);
                else
                    sortedList.Add(list[rightIndex++]);
            }

            if (leftIndex > mid)    // 왼쪽 List가 먼저 소진 됐을 경우
            {
                for (int i = rightIndex; i <= right; i++)
                    sortedList.Add(list[i]);
            }
            else  // 오른쪽 List가 먼저 소진 됐을 경우
            {
                for (int i = leftIndex; i <= mid; i++)
                    sortedList.Add(list[i]);
            }

            // 정렬된 sortedList를 list로 재복사
            for (int i = left; i <= right; i++)
            {
                list[i] = sortedList[i - left];
            }
        }

        // <퀵정렬>
        // 합병정렬에 비해 메모리적 부담은 적음 / 하지만 최악의 경우 O(n^2)을 가짐
        // 하나의 피벗을 기준으로 작은값과 큰값을 2분할하여 정렬
        public static void QuickSort(IList<int> list, int start, int end)
        {
            if (start >= end) return;

            int pivotIndex = start;
            int leftIndex = pivotIndex + 1;
            int rightIndex = end;

            while (leftIndex <= rightIndex) // 엇갈릴때까지 반복
            {
                // pivot보다 큰 값을 만날때까지
                while (list[leftIndex] <= list[pivotIndex] && leftIndex < end)
                    leftIndex++;
                while (list[rightIndex] >= list[pivotIndex] && rightIndex > start)
                    rightIndex--;

                if (leftIndex < rightIndex)     // 엇갈리지 않았다면
                    Swap(list, leftIndex, rightIndex);
                else    // 엇갈렸다면
                    Swap(list, pivotIndex, rightIndex);
            }

            QuickSort(list, start, rightIndex - 1);
            QuickSort(list, rightIndex + 1, end);
        }

        private static void Swap(IList<int> list, int left, int right)
        {
            int temp = list[left];
            list[left] = list[right];
            list[right] = temp;
        }
    }
}
