using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace _11._Searching
{
    public class Searching
    {
        // <순차 탐색>
        // 정렬이 안되있어도 탐색이 가능하다.
        public static int SequentialSearch<T>(in IList<T> list, in T item) where T : IEquatable<T>
        {
            // 처음부터 Count끝까지 반복
            for (int i = 0; i < list.Count; i++)
            {
                // 비교를 했는데 동일한 경우
                if (item.Equals(list[i]))
                {
                    // i 반환
                    return i;
                }
            }
            // 데이터 없음
            return -1;
        }

        // <이진 탐색>
        // 정렬이 되어 있다고 무조건은 아님.
        public static int BinarySearch<T>(in IList<T> list, in T item) where T : IComparable
        {
            int low = 0;
            int high = list.Count - 1;

            while(low <= high)
            {
                int middle = (low + high) / 2;
                int compare = list[middle].CompareTo(item);

                if (compare < 0)
                    low = middle + 1;
                else if(compare > 0)
                    high = middle - 1;
                else
                    return middle;
            }
            return -1;
        }

        // <깊이 우선 탐색 (Depth first search)>
        // 그래프의 분기를 만났을 때 최대한 깊이 내려간 뒤,
        // 더이상 깊이 갈 곳이 없을 경우 다음 분기를 탐색 (백트래킹)
        public static void DFS(bool[,] graph, int start, out bool[] visited, out int[] path) // 갈 수 있는지, 경로
        {
            visited = new bool[graph.GetLength(0)];
            path = new int[graph.GetLength(0)];

            // 값들을 초기화
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                // 한번도 방문하지 않은
                visited[i] = false;
                path[i] = -1;
            }
            SearchNode(graph, start, visited, path);
        }

        private static void SearchNode(bool[,] graph, int start, bool[] visited, int[] path)
        {
            visited[start] = true;
            for(int i = 0; i < graph.GetLength(0); i++)
            {
                if (graph[start, i] && // start부터 i까지 갈 수 있는지 확인
                       !visited[i])
                {
                    path[i] = start;
                    SearchNode(graph, i, visited, path);
                }
            }
        }

        // <너비 우선 탐색 (Breath first search)>
        // 그래프의 분기를 만났을 때 모든 분기를 하나씩 저장하고,
        // 저장한 분기를 한번씩 거치면서 탐색
        public static void BFS(in bool[,] graph, int start, out bool[] visited, out int[] parents)
        {
            visited = new bool[graph.GetLength(0)];
            parents = new int[graph.GetLength(0)];

            for (int i = 0; i < graph.GetLength(0); i++)
            {
                visited[i] = false;
                parents[i] = -1;
            }

            Queue<int> bfsQueue = new Queue<int>();

            bfsQueue.Enqueue(start);
            while (bfsQueue.Count > 0)
            {
                int next = bfsQueue.Dequeue();
                visited[next] = true;

                for (int i = 0; i < graph.GetLength(0); i++)
                {
                    if (graph[next, i] &&       // 연결되어 있는 정점이며,
                        !visited[i])            // 방문한적 없는 정점
                    {
                        parents[i] = next;
                        bfsQueue.Enqueue(i);
                    }
                }
            }
        }
    }
}
