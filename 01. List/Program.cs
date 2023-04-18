namespace _01._List
{
    internal class Program
    {
        /******************************************************
		 * 배열 (Array)
		 * 
		 * 연속적인 메모리상에 동일한 타입의 요소를 일렬로 저장하는 자료구조
		 * 초기화때 정한 크기가 소멸까지 유지됨
		 * 배열의 요소는 인덱스를 사용하여 직접적으로 엑세스 가능
		 * 
		 * 배열의 변수를 100개 만들 때 100개의 주소를 가리키거나 스택에 저장하는건 비효율적
		 * 그래서 여러개의 크기만큼 연속적으로 일렬로 저장
		 * 동일한 타입을 연속적으로 일렬로 저장했기 때문에 특정 위치를 찾는것이 가능하다 => 인덱스
		 ******************************************************/

        // <배열의 사용>
        void Array()
        {
            int[] intArray = new int[100];

            // 인덱스를 통한 접근
            intArray[0] = 10;
            int value = intArray[0];
        }

        // <배열의 시간복잡도>
        // 접근		탐색
        // O(1)		O(N)

        // int 배열 n번째 자료 접근 : 20번째 자료의 주소 = 배열의 주소 + int 자료형 크기 * (n-1)
        // => 접근은 자료의 위치 상관없이 연산 2번으로 접근이 가능함 -> O(1)

        // 데이터가 n개 있을 때 탐색
        // 최악의 경우 for문 n번 실행, 평균은 그 절반, 최선은 1회
        public int FindIndex(int[] intArray, int data)
        {
            for(int i = 0; i < intArray.Length; i++)
            {
                if (intArray[i] == data)
                    return i;
            }
            return -1;
        }

        /******************************************************
		 * 선형리스트 (동적배열) (Dynamic Array)
		 * 
		 * 런타임 중 크기를 확장할 수 있는 배열기반의 자료구조
		 * 배열요소의 갯수를 특정할 수 없는 경우 사용
		 ******************************************************/

        // <List의 사용>
        void List()
        {
            // 크기를 정하지 않음
            List<string> list = new List<string>();

            // list.Count    : 갖고있는 갯수
            // list.Capacity : 허용량

            // 배열 요소 삽입 => 크기가 결정되어있지 않아서 인덱스에 바로 삽입하는 방식이 아님
            list.Add("0번 데이터");
            list.Add("1번 데이터");
            list.Add("2번 데이터");

            // 배열 요소 삭제
            list.Remove("1번 데이터");

            // 크기를 특정하지 않았기 때문에 Add, Remove 가능 => 배열과의 가장 큰 차이점

            // 배열 요소 접근
            list[0] = "데이터0";
            string value = list[0];

            // 배열 요소 탐색
            string? findValue = list.Find(x => x.Contains('2'));
            int findIndex = list.FindIndex(x => x.Contains('0'));
        }

        // <List의 시간복잡도>
        // 접근		탐색		삽입		삭제
        // O(1)		O(n)	    O(n)	    O(n)

        static void Main(string[] args)
        {
            
        }
    }
}