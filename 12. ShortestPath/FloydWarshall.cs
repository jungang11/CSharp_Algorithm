using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._ShortestPath
{
    internal class FloydWarshall
    {
        /******************************************************
		 * 플로이드-워셜 알고리즘 (Floyd-Warshall Algorithm)
		 * 
		 * 모든 노드에서 출발하여 다른 노드로 가는 각각의 최단 경로를 구함
		 * 모든 노드를 거쳐가며 최단 거리가 갱신되는 조합이 있을 경우 갱신
		 * 3중 반복문을 거치기 때문에 속도가 굉장히 느린 알고리즘
		 ******************************************************/

        const int INF = 99999;

        public static void ShortestPath(in int[,] graph, out int[,] costTable, out int[,] pathTable)
        {
            int size = graph.GetLength(0);
            costTable = new int[size, size];
            pathTable = new int[size, size];

            // 전체 2차원 배열들을 모두 확인
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    costTable[y, x] = graph[y, x];  // 갱신 전 그래프를 받음
                    pathTable[y, x] = -1;           // 갱신 전이므로 -1
                }
            }

            // 중간을 거쳐가는 경우 모두 다 진행
            for (int middle = 0; middle < size; middle++)
            {
                // 시작부터
                for (int start = 0; start < size; start++)
                {
                    // 끝까지 진행
                    for (int end = 0; end < size; end++)
                    {
                        // 원래의 거리가 경유 거리보다 클 경우
                        if (costTable[start, end] > costTable[start, middle] + costTable[middle, end])
                        {
                            // 원래의 거리를 더 짧은 경유 거리로 변경
                            costTable[start, end] = costTable[start, middle] + costTable[middle, end];
                            pathTable[start, end] = middle;
                        }
                    }
                }
            }
        }
    }
}
