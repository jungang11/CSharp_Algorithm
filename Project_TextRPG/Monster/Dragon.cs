using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Project_TextRPG
{
    public class Dragon : Monster
    {
        private Random random = new Random();
        private int moveTurn = 0;

        public Dragon()
        {
            name = "드래곤";
            curHp = 100;
            maxHp = 100;
            ap = 15;
            dp = 10;

            StringBuilder sb = new StringBuilder();
            sb.Append("             ,                   ,     ,   ,   ,   ,         \r\n" +
                "                            sr:                              \r\n" +
                "                           ,BBs                           ,  \r\n" +
                "                           ,2                              , \r\n" +
                ",                         ,s                                 \r\n" +
                "                         rS                                  \r\n" +
                ",                      ,95     ihs:i22ssr                    \r\n" +
                "                      2Gi    :, sBBBBGBB9                    \r\n" +
                "        :shMBMG9Xsssssi     ,is2sS2ss5s                      \r\n" +
                "     :GBBBBBM922shMBB,        rBBMG2s, ,                     \r\n" +
                "   iBBBBBBhSss9BBMBBBM:      9BBGGG2rsGM92s,                 \r\n" +
                "  ,BXssGr,   rGSMBBBBBSr  ,r9BMGGX5GBBBBGsSMM2,              \r\n" +
                ",             M22h2shG22S99B9GG9GGGBBBBBBM9s9BB9,         ,  \r\n" +
                "            i2225hGGG99GBBMB929GM99:,hBBGGG955GBB2           \r\n" +
                "         ,9hh252hGMh99959GGBBG9M2rX,        rhSBBBM          \r\n" +
                "        rBG,     :GMGS55ssss2Ssr:hGr          sBBBBs         \r\n" +
                ",   ,ir2Bs         iG9sMBM92i   9Bi            is9BB,        \r\n" +
                "    ,,,,            sB2       :MBh,    ,          :B:        \r\n" +
                "            ,   ,   r9Gs,     ,       ,                 ,    ");
            image = sb.ToString();
        }

        public override void MoveAction()
        {
            if (moveTurn++ < 3)
            {
                return;
            }
            moveTurn = 0;

            List<Position> path;
            if (!PathFinding(in Data.map, pos, Data.player.pos, out path))
                return;

            if (path[1].x == pos.x)
            {
                if (path[1].y == pos.y - 1)
                    TryMove(Direction.Up);
                else
                    TryMove(Direction.Down);
            }
            else
            {
                if (path[1].x == pos.x - 1)
                    TryMove(Direction.Left);
                else
                    TryMove(Direction.Right);
            }
        }

        public override void TakeDamage(int damage)
        {
            if (damage > dp)
            {
                Console.WriteLine($"{name}(은/는) {damage - dp} 데미지를 받았다.");
                curHp -= damage - dp;
            }
            else
                Console.WriteLine($"공격은 {name}에게 먹히지 않았다.");

            Thread.Sleep(1000);

            if (curHp <= 0)
            {
                Console.WriteLine($"{name}(은/는) 쓰러졌다!");
                Thread.Sleep(1000);
                Console.WriteLine("몬스터는 힘포션을 떨어뜨렸다!");
                Data.player.GetItem(new BuffPotion());
                Thread.Sleep(1000);
                Console.WriteLine("플레이어는 150 경험치를 획득했다.");
                Data.player.GetExp(150);
                Thread.Sleep(2000);
            }
        }

        public override void Attack(Player player)
        {
            Console.WriteLine($"{name}(이/가) 브레스를 발사합니다.");
            Thread.Sleep(1000);
            player.TakeDamage(ap);
        }

        /******************************************************
		 * A* 알고리즘
		 * 
		 * 다익스트라 알고리즘을 확장하여 만든 최단경로 탐색알고리즘
		 * 경로 탐색의 우선순위를 두고 유망한 해부터 우선적으로 탐색
		 ******************************************************/

        const int CostStraight = 10;
        const int CostDiagonal = 14;

        static (int x, int y)[] direction =
        {
            (  0, +1 ),			// 상
			(  0, -1 ),			// 하
			( -1,  0 ),			// 좌
			( +1,  0 ),			// 우
			// ( -1, +1 ),		// 좌상
			// ( -1, -1 ),		// 좌하
			// ( +1, +1 ),		// 우상
			// ( +1, -1 )		// 우하
		};

        public bool PathFinding(in bool[,] tileMap, in Position start, in Position end, out List<Position> path)
        {
            int ySize = tileMap.GetLength(0);
            int xSize = tileMap.GetLength(1);

            ASNode[,] nodes = new ASNode[ySize, xSize];
            bool[,] visited = new bool[ySize, xSize];
            PriorityQueue<ASNode, int> nextPointPQ = new PriorityQueue<ASNode, int>();

            // 0. 시작 정점을 생성하여 추가
            ASNode startNode = new ASNode(start, null, 0, Heuristic(start, end));
            nodes[startNode.point.y, startNode.point.x] = startNode;
            nextPointPQ.Enqueue(startNode, startNode.f);

            while (nextPointPQ.Count > 0)
            {
                // 1. 다음으로 탐색할 정점 꺼내기
                ASNode nextNode = nextPointPQ.Dequeue();

                // 2. 방문한 정점은 방문표시
                visited[nextNode.point.y, nextNode.point.x] = true;

                // 3. 다음으로 탐색할 정점이 도착지인 경우
                // 도착했다고 판단해서 경로 반환
                if (nextNode.point.x == end.x &&
                    nextNode.point.y == end.y)
                {
                    Position? pathPoint = end;
                    path = new List<Position>();

                    while (pathPoint != null)
                    {
                        Position point = pathPoint.GetValueOrDefault();
                        path.Add(point);
                        pathPoint = nodes[point.y, point.x].parent;
                    }

                    path.Reverse();
                    return true;
                }

                // 4. AStar 탐색을 진행
                // 방향 탐색
                for (int i = 0; i < direction.Length; i++)
                {
                    int x = nextNode.point.x + direction[i].x;
                    int y = nextNode.point.y + direction[i].y;

                    // 4-1. 탐색하면 안되는 경우
                    // 맵을 벗어났을 경우
                    if (x < 0 || x >= xSize || y < 0 || y >= ySize)
                        continue;
                    // 탐색할 수 없는 정점일 경우
                    else if (tileMap[y, x] == false)
                        continue;
                    // 이미 방문한 정점일 경우
                    else if (visited[y, x])
                        continue;

                    // 4-2. 탐색한 정점 만들기
                    int g = nextNode.g + ((nextNode.point.x == x || nextNode.point.y == y) ? CostStraight : CostDiagonal);
                    int h = Heuristic(new Position(x, y), end);
                    ASNode newNode = new ASNode(new Position(x, y), nextNode.point, g, h);

                    // 4-3. 정점의 갱신이 필요한 경우 새로운 정점으로 할당
                    if (nodes[y, x] == null ||      // 탐색하지 않은 정점이거나
                        nodes[y, x].f > newNode.f)  // 가중치가 높은 정점인 경우
                    {
                        nodes[y, x] = newNode;
                        nextPointPQ.Enqueue(newNode, newNode.f);
                    }
                }
            }

            path = null;
            return false;
        }

        // 휴리스틱 (Heuristic) : 최상의 경로를 추정하는 순위값, 휴리스틱에 의해 경로탐색 효율이 결정됨
        private static int Heuristic(Position start, Position end)
        {
            int xSize = Math.Abs(start.x - end.x);  // 가로로 가야하는 횟수
            int ySize = Math.Abs(start.y - end.y);  // 세로로 가야하는 횟수

            // 맨해튼 거리 : 가로 세로를 통해 이동하는 거리
            // return CostStraight * (xSize + ySize);

            // 유클리드 거리 : 대각선을 통해 이동하는 거리
            return CostStraight * (int)Math.Sqrt(xSize * xSize + ySize * ySize);
        }

        private class ASNode
        {
            public Position point;    // 현재 정점
            public Position? parent;  // 이 정점을 탐색한 정점

            public int g;           // 현재까지의 값, 즉 지금까지 경로 가중치
            public int h;           // 앞으로 예상되는 값, 목표까지 추정 경로 가중치
            public int f;           // f(x) = g(x) + h(x);

            public ASNode(Position point, Position? parent, int g, int h)
            {
                this.point = point;
                this.parent = parent;
                this.g = g;
                this.h = h;
                this.f = g + h;
            }
        }
    }
}