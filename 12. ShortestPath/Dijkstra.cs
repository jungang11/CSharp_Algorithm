using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._ShortestPath
{
    public class Dijkstra
    {
		/******************************************************
		 * 다익스트라 알고리즘 (Dijkstra Algorithm)
		 * 
		 * 특정한 노드에서 출발하여 다른 노드로 가는 각각의 최단 경로를	 구함
		 * 방문하지 않은 노드 중에서 최단 거리가 가장 짧은 노드를 선택 후,
		 * 해당 노드를 거쳐 다른 노드로 가는 비용 계산
		 * 
		 * a에서 b로 직접적으로 가는 거리가 거쳐가는 거리보다 더 긴 경우 직통 거리를
		 * 거쳐가는 거리로 바꿔버림 -> 전체적으로 최단거리만 갖게 된다.
		 * 
		 * 최단 거리는 여러 개의 최단 거리로 이루어져있다. 작은 문제가 큰 문제의 부분 집합에 속해있다
		 * -> 하나의 최단 거리를 구할 때 그 이전까지 구했던 최단 거리 정보를 그대로 사용 -> Dynamic Programming
		 ******************************************************/

		// 단절되있음을 나타내는 INF. overflow 방지
		// => MaxValue 사용할 경우 INF+2 는 overflow
		const int INF = 99999;

		public static void ShortestPath(in int[,] graph, in int start, out int[] distance, out int[] path)
		{
			int size = graph.GetLength(0);
			bool[] visited = new bool[size];

			// 최단거리에 대한 결과(갯수만큼)
			distance = new int[size];
			path = new int[size];
			// 갖고 있는 size만큼 초기값 세팅
			for(int i = 0; i < size; i++)
			{
				distance[i] = graph[start, i];
                path[i] = graph[start, i] < INF ? start : -1;
            }
			
			// 갖고 있는 정점 갯수만큼
			for(int i = 0; i < size; i++)
			{
				// 1. 방문하지 않은 정점 중 가장 가까운 정점부터 탐색
				// 최솟값을 갱신해나감
				int next = -1;
				int minCost = INF;	// 비교를 위한 최솟값
				for(int j=0; j < size; j++)
				{
					// 방문하지 않았던 곳인데 distance가 현재 minCost보다 작았을 경우
					if (!visited[j] &&
						distance[j] < minCost)
					{
						next = j;
						minCost = distance[j];
					}
				}
				if (next < 0)
				{
					break;
				}

				// 2. 직접 연결된 거리보다 경유했을때의 거리가 더 짧다면 갱신
				for(int j =0; j < size; j++)
				{
                    // distance[j] : 목적지까지 직접 연결된 거리
					// distance[next] : 탐색중인 정점까지의 거리
					// graph[next, j] : 탐색중인 정점부터 목적지까지의 거리
                    if (distance[j] > distance[next] + graph[next, j])	// next를 거쳐서 더 짧아진 경우
                    {
						distance[j] = distance[next] + graph[next, j];  // 거리를 갱신해줌
						path[j] = next;
                    }
				}
				visited[next] = true;	// 방문 끝
			}
		}
    }
}
