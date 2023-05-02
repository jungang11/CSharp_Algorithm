using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._ShortestPath
{
    internal class DijkPractice
    {
        const int INF = 99999;

        // out은 출력용 매개변수 한정자, in은 매개변수가 참조로 전달되고(복사비용 절약) 수정불가능
        public static void Short(in int[,] graph, in int start, out int[] distance, out int[] path)
        {
            // 그래프의 size부터
            int size = graph.GetLength(0);
            // 방문했는지 확인하는 배열
            bool[] visited = new bool[size];

            distance = new int[size];
            path = new int[size];
            // size만큼의 크기로 초기값 세팅
            for(int i = 0; i < size; i++)
            {
                // 초기 거리는 직통 거리
                distance[i] = graph[start, i];
                // INF보다 클 경우 단절되었으니 갈 수 없음 -> -1로 표현
                path[i] = graph[start, i] < INF ? start : -1;
            }

            // size만큼 반복
            for(int i = 0; i < size; i++)
            {
                // 방문하지않고 거리가 최솟값보다 작을 경우 next는 j가 될 예정
                int next = -1;
                int minCost = INF; // 최솟값 갱신을 위한 변수
                for(int j=0; j < size; j++)
                {
                    if (!visited[j] && distance[j] < minCost)
                    {
                        next = j;
                        minCost = distance[j];
                    }
                }
                // next가 -1이면 갈 수 없으니 갱신하지않고 반복문 탈출 
                if (next < 0)
                {
                    break;
                }

                for(int j =0; j < size; j++)
                {
                    if (distance[j] > distance[next] + graph[next, j])
                    {
                        distance[j] = distance[next] + graph[next, j];
                        path[j] = next;
                    }
                }
                visited[next] = true;
            }
        }
    }
}
