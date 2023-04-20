namespace _03._Iterator
{
    internal class Program
    {
        /******************************************************
		 * 반복기 (Enumerator(Iterator)) => 열거자
		 * 
		 * 자료구조에 저장되어 있는 요소들을 순회하는 인터페이스
		 * 복합객체의 내부 구조에 상관 없이 동일한 방식의 요소를 열거하는 디자인 패턴
		 * 
		 * C#에서 모든 컬렉션은 IEnumerable<T> 의 인터페이스를 구현함
		 * 이 인터페이스에는 공통의 메소드가 있는데 이 메소드가 GetEnumerator()
		 * 
		 * 메소드가 반환하는 열거자는 객체
		 * 객체가 요소를 가리키며 다음 요소로 이동하거나 값을 꺼내올 수 있게 됨
		 * 모든 열거자는 IEnumerator<T> 인터페이스를 구현하기 때문에 사용법이 동일
		 * IEnumerable<T> 는 컬렉션의 인터페이스이고 IEnumerator<T>는 열거자의 인터페이스
		 * 따라서 반환값을 받을 때에는 IEnumerator<T>로 받으면 됨
		 * 
		 ******************************************************/

        void Iterator()
        {
            // 대부분의 자료구조가 반복기를 구현함
            // 반복기를 이용한 기능을 구현할 경우, 그 기능은 대부분의 자료구조를 호환할 수 있음
            List<int> list = new List<int>();
            LinkedList<int> linkedList = new LinkedList<int>();
            Stack<int> stack = new Stack<int>();
            Queue<int> queue = new Queue<int>();
            SortedList<int, int> sList = new SortedList<int, int>();
            SortedSet<int> set = new SortedSet<int>();
            SortedDictionary<int, int> map = new SortedDictionary<int, int>();
            Dictionary<int, int> dic = new Dictionary<int, int>();

            // 반복기를 이용한 순회
            // foreach 반복문은 데이터집합의 반복기를 통해서 단계별로 반복
            // 즉, 반복기가 있다면 foreach 반복문으로 순회 가능
            foreach (int i in list) { }
            foreach (int i in linkedList) { }
            foreach (int i in stack) { }
            foreach (int i in queue) { }
            foreach (int i in set) { }
            foreach (KeyValuePair<int, int> i in sList) { }
            foreach (KeyValuePair<int, int> i in map) { }
            foreach (KeyValuePair<int, int> i in dic) { }
            foreach (int i in IterFunc()) { }

            // 반복기 직접 조작
            List<string> strings = new List<string>();
            for (int i = 0; i < 5; i++) strings.Add($"{i}데이터");

            IEnumerator<string> iter = strings.GetEnumerator();
            iter.MoveNext();
            Console.WriteLine(iter.Current);    // output : 0데이터
            iter.MoveNext();
            Console.WriteLine(iter.Current);    // output : 1데이터

            // -------------- 
            iter.Reset();
            while (iter.MoveNext())
            {
                Console.WriteLine(iter.Current); // 1, 2, 3, 4, 5
            }
            iter.Dispose();
            // --------------> foreach문

            // 어떤 자료형이 들어와도 동작시킬 수 있는 광범위한 사용범위
            // 자료구조들은 여러 인터페이스들을 사용중
            Find(list);
            // Find(linkedList);
            Find(stack);
        }

        // IList 자료구조를 받는 Sort함수
        public delegate int Compare<T>(T left, T right);
        public static void Sort<T>(IList<T> array, Compare<T> compare)
        {
            // Bubble Sort
            for (int i = 0; i < array.Count; i++)
            {
                for (int j = i; j < array.Count; j++)
                {
                    if (compare(array[i], array[j]) > 0)
                    {
                        T temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
                Console.Write(array[i] + ", ");
            }
            Console.WriteLine();
        }

        // 오름차순 정렬
        public static int AscendingOrder(int left, int right)
        {
            if (left > right)
                return 1;
            else if (left < right)
                return -1;
            else
                return 0;
        }

        // 내림차순 정렬
        public static int DescendingOrder(int left, int right)
        {
            if (left < right)
                return 1;
            else if (left > right)
                return -1;
            else
                return 0;
        }


        public void Find(IEnumerable<int> container)
        {
            IEnumerator<int> iter = container.GetEnumerator();

            iter.Reset();
            while (iter.MoveNext())
            {
                if(iter.Current == 10)
                    Console.WriteLine("10 찾음");
            }
            iter.Dispose();
            Console.WriteLine("못찾았다.");
        }

        IEnumerable<int> IterFunc()
        {
            yield return 1;
        }

        static void Main(string[] args)
        {
            // 실습 1. foreach에 List, LinkedList 반복 확인
            List<int> list = new List<int>();
            for (int i = 1; i <= 5; i++) list.Add(i);

            LinkedList<int> linkedList = new LinkedList<int>();
            for (int i = 1; i <= 5; i++) linkedList.AddLast(i);

            Console.Write("List foreach : ");
            foreach (int i in list)
                Console.Write($"{i} ");

            Console.WriteLine();

            Console.Write("LinkedList foreach : ");
            foreach (int i in linkedList)
                Console.Write($"{i} ");

            Console.WriteLine();

            // 실습 0. Sort(배열), Sort(리스트) 오버로딩x 둘 모두 정렬 가능한 하나의 함수 Sort 구현 
            int[] array = { 3, -2, 1, -4, 9, -8, 7, -6, 5 };
            Console.WriteLine("int[] array : { 3, -2, 1, -4, 9, -8, 7, -6, 5");
            Console.WriteLine("오름차순 : "); Sort(array, AscendingOrder);
            Console.WriteLine("내림차순 : "); Sort(array, DescendingOrder);
        }
    }
}