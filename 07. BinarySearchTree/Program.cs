using System.Diagnostics;
using System;
using System.Net.Http.Headers;

namespace _07._BinarySearchTree
{
    internal class Program
    {
        /******************************************************
		 * 트리 (Tree)
		 * 
		 * 계층적인 자료를 나타내는데 자주 사용되는 자료구조
		 * 부모노드가 0개 이상의 자식노드들을 가질 수 있음
		 * 한 노드에서 출발하여 다시 자기 자신의 노드로 돌아오는 순환구조를 가질 수 없음
		 ******************************************************/

        /******************************************************
		 * 이진탐색트리 (BinarySearchTree)
		 * 
		 * 이진속성과 탐색속성을 적용한 트리
		 * 이진탐색을 통한 탐색영역을 절반으로 줄여가며 탐색 가능
		 * 이진 : 부모노드는 최대 2개의 자식노드들을 가질 수 있음
		 * 탐색 : 자신의 노드보다 작은 값들은 왼쪽, 큰 값들은 오른쪽에 위치
		 * 
		 * 정렬이 보장되어 있는 자료구조 중 하나 => SortedSet
		 ******************************************************/

        // <이진탐색트리의 시간복잡도>
        // 접근			탐색			삽입			삭제
        // O(log n)		O(log n)	  O(log n)	      O(log n)

        static void BinarySearchTree()
        {
            SortedSet<int> sortedSet = new SortedSet<int>();

            // 추가
            sortedSet.Add(1);
            sortedSet.Add(2);
            sortedSet.Add(3);
            sortedSet.Add(4);
            sortedSet.Add(5);

            // 탐색
            int searchValue1;
            bool find = sortedSet.TryGetValue(3, out searchValue1);     // 탐색 시도

            //삭제
            sortedSet.Remove(3);

            // key, value 이진탐색트리
            SortedDictionary<string, Monster> sortedDic = new SortedDictionary<string, Monster>();

            sortedDic.Add("피카츄", new Monster() { name = "피카츄", hp = 100 });
            sortedDic.Add("파이리", new Monster() { name = "파이리", hp = 120 });
            sortedDic.Add("꼬부기", new Monster() { name = "꼬부기", hp = 180 });
            sortedDic.Add("리아코", new Monster() { name = "리아코", hp = 110 });
            sortedDic.Add("이상해씨", new Monster() { name = "이상해씨", hp = 130 });

            Monster monster;
            // sortedDic.TryGetValue("파이리", out monster);  // 파이리 탐색 시도
            Monster indexerMonster = sortedDic["파이리"];  // 인덱서를 통한 탐색. key값이 없다면 exception

            // 이진탐색 검색효율
            List<int> list = new List<int>();
            SortedSet<int> set = new SortedSet<int>();

            Random random = new Random();
            int rand;
            for (int i = 0; i < 100000; i++)
            {
                rand = random.Next();
                list.Add(rand);
                set.Add(rand);
            }
            list[99999] = -1;
            set.Add(-1);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            list.Find((x) => x == -1);
            stopwatch.Stop();
            Console.WriteLine("리스트 time : {0}", stopwatch.ElapsedTicks);

            stopwatch.Restart();
            int value;
            set.TryGetValue(-1, out value);
            stopwatch.Stop();
            Console.WriteLine("트리 time : {0}", stopwatch.ElapsedTicks);
        }

        // * <이진탐색트리의 주의점> -> 이진탐색트리의 한계점
        // 이진탐색트리의 노드들이 한쪽 자식으로만 추가되는 불균형 발생 가능
        // 이 경우 탐색영역이 절반으로 줄여지지 않기 때문에 시간복잡도 증가
        // 이러한 현상을 막기 위해 자가균형기능을 추가한 트리의 사용이 일반적
        // 대표적인 방식으로 Red-Black Tree, AVL Tree 등이 있음
        // 노드의 균형을 맞추기 위해서 자가 균형을 씀

        // 균형트리 : 모든 하위 트리의 높이 차이가 1 이하인 트리
        // 자가균형이진탐색트리 : 트리에서 노드의 삽입이나 삭제같은 연산이 일어날 때 자동으로 균형 유지
        // - 노드 분할 및 병합 : 노드의 자식은 두 개를 초과하지 못하며, 노드가 많으면 두개의 하위 노드로 나눔
        // - 노드 회전 : 간선을 전환. x가 y의 부모이면 y를 x의 부모로 만들고, x는 y의 자식 중 하나를 거둠


        static void Main(string[] args)
        {
            DataStructure.BinarySearchTree<int> bst = new DataStructure.BinarySearchTree<int>();
            int value = 0;

            bst.Add(3);
            bst.Add(15);
            bst.Add(30);
            bst.Add(10);
            bst.Add(5);

            Console.WriteLine(bst.TryGetValue(15, out value));
            bst.Remove(15);
            Console.WriteLine(bst.TryGetValue(15, out value));

        }

        class Monster
        {
            public string name;
            public int hp;
            public int mp;
            public int ap;
            public int dp;

        }
    }
}