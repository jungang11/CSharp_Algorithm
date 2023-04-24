using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    // 이진 트리
    // 모든 자식들이 존재할 경우 => 완전 이진트리
    // C# 노드기반으로 작성할 경우 비효율적 => 배열 이용
    public class PriorityQueue<TElement, TPriority>
    {
        private struct Node
        {
            public TElement element;    // 실제 데이터
            public TPriority priority;  // 우선순위 
        }

        private List<Node> nodes;                // 리스트를 이용한 어댑터로 구현
        private IComparer<TPriority> comparer;   // 우선순위 비교

        public PriorityQueue()
        {
            this.nodes = new List<Node>();
            this.comparer = Comparer<TPriority>.Default;    // 비교자에 Default를 넣을 경우 자료형에 맞춰 비교
        }

        public PriorityQueue(IComparer<TPriority> comparer) // 비교지정자를 직접 정해줄때
        {
            this.nodes = new List<Node>();
            this.comparer = comparer;
        }

        public int Count { get { return this.nodes.Count; } }
        public IComparer<TPriority> Comparer { get { return this.comparer; } }

        // 데이터를 추가하고 힙의 속성을 유지 => 가장 마지막에 데이터를 추가하고
        // 부모랑 비교하여 우선순위가 더 높다면 위치를 바꿈 => 반복
        public void Enqueue(TElement element, TPriority priority)
        {
            Node newNode = new Node() { element = element, priority = priority };

            // 1. 가장 뒤에 데이터 추가
            nodes.Add(newNode);
            int newNodeIndex = nodes.Count - 1;
            
            // 2. 새로운 노드를 힙 상태가 유지되도록 승격 작업 반복
            while(newNodeIndex > 0 ) // 최상위 노드가 아닌 경우 반복
            {
                int parentIndex = GetParentIndex(newNodeIndex);
                Node parentNode = nodes[parentIndex];

                // newNode와 parentNode 우선순위 비교
                if(comparer.Compare(newNode.priority, parentNode.priority) < 0)
                {
                    // newNode와 parentNode를 서로 바꿈
                    nodes[newNodeIndex] = parentNode;
                    nodes[parentIndex] = newNode;
                    newNodeIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        // 가장 위에 있는 노드를 반환하는 함수
        // 최상위 데이터를 꺼냈을 때 => 가장 마지막 데이터를 최상위 데이터의 자리로 옮김
        // => 그 데이터를 자식 노드들과 비교해 점점 내려가 우선순위를 맞추어 힙의 속성을 유지
        public TElement Dequeue()
        {
            // 0번 데이터가 우선순위가 가장 높음
            Node rootNode = nodes[0];

            // 1. 가장 마지막 노드를 최상단으로 위치
            Node lastNode = nodes[nodes.Count - 1];
            nodes[0] = lastNode;
            nodes.RemoveAt(nodes.Count - 1);

            int index = 0;
            // 2. 자식 노드들과 비교해 더 작은 자식과 교체 반복
            while( index < nodes.Count )
            {
                int leftChildIndex = GetLeftChildIndex(index);
                int rightChildIndex = GetRightChildIndex(index);

                // 2-1. 자식이 둘 다 있는 경우
                if (rightChildIndex < nodes.Count)
                {
                    // 왼쪽 자식과 오른쪽 자식을 비교
                    int lessChildIndex = comparer.Compare(nodes[leftChildIndex].priority,
                                                          nodes[rightChildIndex].priority) < 0 ? 
                                                            leftChildIndex : rightChildIndex;
                    // 더 작은 자식과 부모 노드를 비교
                    if (comparer.Compare(nodes[lessChildIndex].priority, lastNode.priority) < 0)
                    {
                        nodes[index] = nodes[lessChildIndex];
                        nodes[lessChildIndex] = lastNode;
                        index = lessChildIndex;
                    }
                    else
                    {
                        break;
                    }
                }

                // 2-2. 자식이 하나만 있는 경우 => 왼쪽 자식만 있는 경우
                else if(leftChildIndex < nodes.Count)
                {
                    if (comparer.Compare(nodes[leftChildIndex].priority, lastNode.priority) < 0)
                    {
                        nodes[index] = nodes[leftChildIndex];
                        nodes[leftChildIndex] = lastNode;
                        index = leftChildIndex;
                    }
                    else
                    {
                        break;
                    }
                }

                // 2-3. 자식이 없는 경우
                else
                {
                    break;
                }

            }

            return rootNode.element;
        }

        // 가장 위에 있는 데이터(0번 데이터) 반환하는 함수
        public TElement Peek()
        {
            return nodes[0].element;
        }

        // 왼쪽 아래 노드, 오른쪽 아래 노드, 부모의 위치 찾기
        // leftChild = index * 2 + 1, rightChild = index * 2 + 2. Parent = (index-1) / 2 (나머지는 버림)
        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }
        private int GetLeftChildIndex(int parentIndex)
        {
            return parentIndex * 2 + 1;
        }

        private int GetRightChildIndex(int parentIndex)
        {
            return parentIndex * 2 + 2;
        }
    }
}
