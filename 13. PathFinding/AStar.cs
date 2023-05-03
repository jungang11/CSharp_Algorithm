using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _13._PathFinding
{
    internal class AStar
    {
		/******************************************************
		 * A* 알고리즘
		 * 
		 * 다익스트라 알고리즘을 확장하여 만든 최단경로 탐색알고리즘
		 * 경로 탐색의 우선순위를 두고 유망한 해부터 우선적으로 탐색
		 * 
		 * 다익스트라 알고리즘과 다르게 출발지와 목적지가 명확해 두 노드간의 최단 경로를 파악할 수 있음
		 * 
		 * 각각의 정점에서 f가 가장 낮은 값부터 탐색하자 -> A* Algorithm
		 * f : g + h => 총 거리
		 * g : 현재 노드에서 출발 지점까지의 거리
		 * h(휴리스틱) : 예상거리
		 ******************************************************/

		const int CostStraight = 10; // 직선 Cost
		const int CostDiagonal = 14; // 대각선 Cost

		// 상하좌우 따로따로 구현하지 않기 위해
		static Point[] Direction =
		{
			new Point( 0, +1),    // 상
			new Point( 0, -1),    // 하
			new Point( -1, 0),    // 좌
			new Point( +1, 0)     // 우
		};

		// 경로 탐색 성공은 true / 실패는 false 반환 위해 bool 자료형 이용
		public static bool PathFinding(in bool[,] tileMap, in Point start, in Point end, out List<Point> path)
		{
			// 초기화
			int ySize = tileMap.GetLength(0);
			int xSize = tileMap.GetLength(1);

            // 정점의 탐색 여부 확인
            bool[,] visited = new bool[ySize, xSize];   
            ASNode[,] nodes = new ASNode[ySize, xSize];
			// 정점과 f값을 넣어줄 우선순위 큐 -> f값이 가장 낮은 정점을 바로 꺼내기 위해 사용
			PriorityQueue<ASNode, int> nextPointPQ = new PriorityQueue<ASNode, int>();

			// 0. 시작 정점을 생성하여 추가
			ASNode startNode = new ASNode(start, null, 0, Heuristic(start, end));
			nodes[startNode.point.y, startNode.point.x] = startNode;
			nextPointPQ.Enqueue(startNode, startNode.f);	// 우선순위큐에 f 기준으로 삽입

			while(nextPointPQ.Count > 0)
			{
				// 1. 다음으로 탐색할 정점 꺼내기
				ASNode nextNode = nextPointPQ.Dequeue();

				// 2. 방문한 정점은 방문 표시
				visited[nextNode.point.y, nextNode.point.x] = true;

				// 3. 탐색할 정점이 도착지인 경우 도착했다고 판단해서 경로 반환
				if(nextNode.point.x == end.x && nextNode.point.y == end.y)
				{
					Point? pathPoint = end;
					// 경로를 보관하는 리스트
					path = new List<Point>();

					// 지금 정점의 Point가 startNode일 때까지
					while(pathPoint != null)
					{
						Point point = pathPoint.GetValueOrDefault(); // 그냥 pathPoint -> nullable이라 불가
						path.Add(point);
						pathPoint = nodes[point.y, point.x].parent;	 // 다음 위치는 해당 노드의 부모 노드
					}

					path.Reverse();	// 끝에서 부터 삽입했으니 Reverse
					return true;
				}

				// 4. 도착지가 아닌 경우 AStar 탐색을 진행
				for(int i=0;i<Direction.Length;i++)
				{
                    int x = nextNode.point.x + Direction[i].x;
                    int y = nextNode.point.y + Direction[i].y;

					// 4-1. 탐색하면 안되는 경우 제외
					// 맵을 벗어났을 때
					if (x < 0 || x >= xSize || y < 0 || y >= ySize)
						continue;	// break하지말고 continue로 스킵
					// 탐색할 수 없는 정점일 경우(장애물)
					else if (tileMap[y, x] == false)
						continue;	// 스킵
					// 이미 방문한 정점일 경우
					else if (visited[y, x])
						continue;

					// 4-2. 탐색(g, h값 확인하기) => 점수 계산
					int g = nextNode.g + 10;    // 탐색했던 노드에 10 더하기
					int h = Heuristic(new Point(x, y), end);
					ASNode newNode = new ASNode(new Point(x, y), nextNode.point, g, h);

					// 4-3. 정점의 갱신이 필요한 경우 새로운 정점으로 할당
					if (nodes[y, x] == null || 		// 탐색이 안 된(점수가 없는 경우) 정점
						nodes[y, x].f > newNode.f)  // 가중치가 더 높은 정점인 경우
					{
						nodes[y, x] = newNode;
						nextPointPQ.Enqueue(newNode, newNode.f);
					}
                }
			}
			// 우선순위 큐가 빌 때까지 경로를 찾지 못 할 경우
			path = null;
			return false;
		}
		

        // 시작위치와 목적지를 통해 h값(휴리스틱)을 구하기 위한 함수
        // 휴리스틱 (Heuristic) : 최상의 경로를 추정하는 순위값, 휴리스틱에 의해 경로탐색 효율이 결정됨
        public static int Heuristic(Point start, Point end)
		{
            int xSize = Math.Abs(start.x - end.x);  // 가로로 가야하는 횟수
            int ySize = Math.Abs(start.y - end.y);  // 세로로 가야하는 횟수

            // 맨해튼 거리 : 가로 세로를 통해 이동하는 거리 => 계산 횟수가 많아짐
            // return CostStraight * (xSize + ySize);

            // 유클리드 거리 : 대각선을 통해 이동하는 거리
            return CostStraight * (int)Math.Sqrt(xSize * xSize + ySize * ySize);
        }

		// 클래스를 쓴 이유 : 구조체는 빈 데이터를 가질 수 없음
		private class ASNode
		{
			public Point point;		// 현재 정점 위치
			public Point? parent;	// 이 정점을 탐색한 정점(없을 수도 있음 -> nullable)

			public int f;			// f(x) = g(x) + h(x) : 총 거리
			public int g;		    // g(x) : 출발지에서부터 현재 노드까지의 거리
			public int h;			// h(x) : 휴리스틱, 앞으로 예상되는 거리 (목표까지 추정 경로)

			// 생성자
			public ASNode(Point point, Point? parent, int g, int h)
			{
				this.point = point;
				this.parent = parent;
				this.g = g;
				this.h = h;
				this.f = g + h;	// f는 지정해줄 필요가 없음
			}
		}
    }

	public struct Point
	{
		// 객체가 필요한게 아니라 위치 정보가 필요하기 때문에 구조체 이용
		public int x;
		public int y;

		// x없는 y는 없을테니
		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
