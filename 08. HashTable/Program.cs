﻿namespace _08._HashTable
{
    public class Program
    {
        /******************************************************
		 * 해시테이블 (HashTable)
		 * 
		 * 키 값을 해시함수로 해싱하여 해시테이블의 특정 위치로 직접 엑세스하도록 만든 방식
		 * 
		 * <해시 함수> : 임의의 길이를 가진 데이터를 고정된 길이를 가진 데이터로 매핑하는 단방향 함수
		 * => 단방향 함수이기 때문에 결과값을 역으로 입력값으로 변환할 수 없음
		 * 해싱 : 해시 함수에 문자열 입력값을 넣어서 특정한 값으로 추출하는 것
		 * 
		 * 공간을 포기하고 속도를 우선한 자료구조
		 ******************************************************/

        /******************************************************
		 * <해시함수의 조건>
		 * 입력에 대한 해시함수의 결과가 항상 동일한 값이어야 함
		 *  => 고유한 인덱스 값을 설정하는 것이 중요함
		 *  
		 * <해시함수의 인덱스 값 설정 방법>
		 * 1. Division Method : 나눗셈을 이용하는 방법. index = input % Capacity. 
		 *    테이블의 크기를 소수로 정하고 2의 제곱수와 먼 곳을 사용해야 효과가 좋음
		 * 2. Digit Folding : 각 Key의 문자열을 ASCII 코드로 바꾸고 값을 합한 데이터를 테이블 내의 주소로 사용
		 * 3. Multiplication Method : 숫자로 된 Key값 K와 0과 1사이의 실수 A, 보통 2의 제곱수인 m을 이용해
		 *  => h(K) = (KAmod1) * m
		 * 4. Universal Hashing : 다수의 해시함수를 만들어 집합 H에 넣어두고, 
		 * 무작위로 해시함수를 선택해 해시값을 만드는 기법
		 * 
		 * <해시함수의 효율>
		 * 1. 해시함수의 처리 속도가 빠를수록 효율이 좋음
		 * 2. 해시함수 결과의 밀집도가 낮아야 함
		 * 3. 해시테이블의 크기가 클수록 효율적
		 ******************************************************/

        /******************************************************
		 * 해시함수의 용도
		 * 
		 * 1. 자료구조
		 * 해시 테이블 또는 해시 맵이라는 형태로 O(1)의 시간 복잡도로 접근하는데 사용됨
		 * 특정 Key를 해싱해서 나오는 문자열에 Value들을 저장해놓음으로써 Key 값에 따라 바로 원하는 값을 찾을 수 있음
		 * 
		 * 2. 프로그래밍 언어에서 제공되는 해시 함수
		 * Java : Hash Map
		 * JavaScript : 객체 or Map
		 * Python : 사전(Dictionary)
		 * 
		 * 3. 암호화
		 * 입력값을 해싱했을 때 출력값은 일정하다는 것을 근거로, 사용자의 비밀번호나 중요 정보의
		 * 내용을 해싱하여 복호화 할 수 없게 만드는 것
		 ******************************************************/

        // <해시테이블의 시간복잡도>
        // 접근			탐색			삽입			삭제
        // X			O(1)		O(1)		O(1)

        // <해시테이블 주의점 - 충돌>
        // 해시함수가 서로 다른 입력 값에 대해 동일한 해시테이블 주소를 반환하는 것
        // 모든 입력 값에 대해 고유한 해시 값을 만드는 것은 불가능하며 충돌은 피할 수 없음
        // 대표적인 충돌 해결방안으로 체이닝과 개방주소법이 있음
        // 암호와 관련된 보안에서는 보안이 뚫리는 심각한 문제가 발생할 수 있음

        // <충돌해결방안 - 체이닝>
        // 해시 충돌이 발생하면 연결리스트로 데이터들을 연결하는 방식
        // 장점 : 해시테이블에 자료가 많아지더라도 성능저하가 적음
        // 단점 : 해시테이블 외 추가적인 저장공간이 필요 => C#에서는 선호하지 않음

        // <충돌해결방안 - 개방주소법> => C#에서 주로 사용
        // 해시 충돌이 발생하면 다른 빈 공간에 데이터를 삽입하는 방식
        // 해시 충돌시 선형탐색, 제곱탐색, 이중해시 등을 통해 다른 빈 공간을 선정
        // 장점 : 추가적인 저장공간이 필요하지 않음, 삽입삭제시 오버헤드가 적음
        // 단점 : 해시테이블에 자료가 많아질수록 성능저하가 많음
        // 해시테이블의 공간 사용률이 높을 경우 성능저하가 발생하므로 재해싱 과정을 진행함
        // 재해싱 : 해시테이블의 크기를 늘리고 테이블 내의 모든 데이터를 다시 해싱 

        static void Dictionary()
        {
			DataStructure.Dictionary<string, Item> dictionary 
				= new DataStructure.Dictionary<string, Item>();

			// 추가            Key 값
			dictionary.Add("초기 아이템", new Item("초보자용 검", 10));
			dictionary.Add("초기 방어구", new Item("초보자용 가죽 갑옷", 30));
			dictionary.Add("전직 아이템", new Item("푸른 결정", 1));

			// 탐색
			Console.WriteLine(dictionary["초기 아이템"].name);

            // 접근
            dictionary.Remove("전직 아이템");

			// 확인
			if(dictionary.ContainsKey("초기 아이템"))
			{
                Console.WriteLine($"Dictionary에 초기 아이템({dictionary["초기 아이템"].name})이 있음");
            }
        }

        static void Main(string[] args)
        {
			Dictionary();
        }
		
		public class Item
		{
			public string name;
			public int weight;

			public Item(string name, int weight)
			{
				this.name = name;
				this.weight = weight;
			}
		} // string name, int weight
    }
}