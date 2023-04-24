namespace _04._Stack
{
    internal class Program
    {
        /******************************************************
		 * 스택 (Stack)
		 * 
		 * 선입후출(FILO), 후입선출(LIFO) 방식의 자료구조
		 * 가장 최신 입력된 순서대로 처리해야 하는 상황에 이용
		 ******************************************************/

        // 스택과 큐는 시간복잡도보다 역할에 의해 사용 용도가 갈림
        // ex) 뒤로가기(ctrl + z), 스킬트리(poe, diablo)

        static void Test()
        {
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < 10; i++) { stack.Push(i); }     // 0 1 2 3 4 5 6 7 8 9

            Console.WriteLine(stack.Peek());    // Peek : 꺼내지는 않고 가장 위에 있는 데이터를 확인. out : 9

            while (stack.Count > 0)
            {
                Console.WriteLine(stack.Pop());     // 9 8 7 6 5 4 3 2 1 0
            }
        }


        static void Main(string[] args)
        {
            string pos = "2*4+5+6/3";
            StackCalCulator.GetPostfix(pos);
        }
    }
}