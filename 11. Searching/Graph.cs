using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11._Searching
{
	internal class Graph
	{
        /******************************************************
		 * 그래프 (Graph)
		 * 
		 * 정점의 모음과 이 정점을 잇는 간선의 모음의 결합
		 * 한 노드에서 출발하여 다시 자기 자신의 노드로 돌아오는 순환구조를 가짐
		 * 간선의 방향성에 따라 단방향 그래프, 양방향 그래프가 있음
		 * 간선의 가중치에 따라   연결 그래프, 가중치 그래프가 있음
		 ******************************************************/

        // <인접행렬 그래프>
        // 그래프 내의 각 정점의 인접 관계를 나타내는 행렬
        // 2차원 배열을 [출발정점, 도착정점] 으로 표현
        // 장점 : 인접여부 접근이 빠름 O(1)
        // 단점 : 메모리 사용량이 많음 O(N^2)

        // 인접 행렬의 장점은 구현이 쉽다는 점. 노드 i와 j가 연결된지 확인하고 싶을 때 ad[i,j]가 1인지
        // 0인지만 확인하면 되기 때문에 O(1)이라는 시간 복잡도에 확인할 수 있음.
        // 하지만 노드 i에 연결된 모든 노드들에 방문하고 싶을 경우 ad[i,1]부터 ad[i,v]를 모두 확인해야 함
        // 이럴 경우 간선은 적지만 노드가 많을 경우 매우 비효율적임

        // 예시 - 양방향 연결 그래프
        bool[,] matrixGraph1 = new bool[5, 5]
        {
            { false, false, false, false,  true },
            { false, false,  true, false, false },
            { false,  true, false,  true, false },
            { false, false,  true, false,  true },
            {  true, false, false,  true, false },
        };

        // const를 이용해 자료수정 금지
        const int INF = int.MaxValue;

        // 예시 - 단방향 가중치 그래프 (단절은 최대값으로 표현)
        // + (-1) 을 주어 단절을 표현할 수도 있음. 의미상으로는 -1이 좋겠지만
        // -1을 최단거리로 판단하는 오류가 있을 수도 있음 
        int[,] matrixGraph2 = new int[5, 5]
        {
            {   0, 132, INF, INF,  16 },
            {  12,   0, INF, INF, INF },
            { INF,  38,   0, INF, INF },
            { INF,  12, INF,   0,  54 },
            { INF, INF, INF, INF,   0 },
        };

        // <인접리스트 그래프>
        // 그래프 내의 각 정점의 인접 관계를 표현하는 리스트
        // 인접한 간선만을 리스트에 추가하여 관리
        // 장점 : 실제로 연결된 노드들에 대한 정보만 저장하기 때문에 간선의 메모리에
        // 비례하는 메모리만 사용 -> 메모리 사용량이 적음 O(N)
        // 단점 : 인접여부를 확인하기 위해 리스트 탐색이 필요 O(N)


        // 예시
        List<List<int>> listGraph;			// 연결 그래프
		List<List<(int, int)>> listGraph2;	// 가중치 그래프 -> 가중치와 정점을 같이 넣어줘야함

		public void CreateGraph()
		{
			listGraph = new List<List<int>>();
			for(int i = 0; i < 5; i++)
			{
				listGraph.Add(new List<int>());
			}
			listGraph[0].Add(1);
			listGraph[1].Add(0);
			listGraph[2].Add(2);
		}
    }
}
