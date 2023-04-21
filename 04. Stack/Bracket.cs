using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04._Stack
{
    // 괄호 검사기
    // 1. 왼쪽 괄호의 개수와 오른쪽 괄호의 개수가 같아야 함
    // 2. 동일 타입의 괄호에서 왼쪽 괄호는 오른쪽 괄호보다 먼저 나와야 함
    // 3. 서로 다른 타입의 괄호 쌍이 서로를 교차하면 안됨

    // 문자열에 있는 괄호를 차례대로 조사하며 왼쪽 괄호를 만나면 스택에 push
    // 오른쪽 괄호를 만나면 pop해서 스택의 Top에 위치한 괄호를 꺼냄
    // 마지막 괄호를 조사한 후에도 스택에 괄호가 남아있으면 안됨

    public class Bracket
    {
        public static void BracketCal()
        {
            DataStructure.AdapterStack<char> bracket = new DataStructure.AdapterStack<char>();

            Console.Write("괄호 입력 : ");
            string _bracket = Console.ReadLine();
            char top;

            // 입력받은 문자열 _bracket의 길이만큼 반복
            for(int i = 0; i< _bracket.Length; i++)
            {
                // 왼쪽 괄호가 나올 경우 bracket 스택에 Push
                if (_bracket[i] == '(' || _bracket[i] == '{' || _bracket[i] == '[')
                {
                    bracket.Push(_bracket[i]);
                }
                // 오른쪽 괄호가 나올 경우 bracket 스택의 최상위 노드(마지막으로 Push한 왼쪽 괄호)를 Pop
                else if (_bracket[i] == ')' || _bracket[i] == '}' || _bracket[i] == ']')
                {
                    // 오른쪽 괄호가 나왔는데 스택이 비어있을 경우 유효하지 않음
                    if (bracket.IsEmpty())
                    {
                        break;
                    }
                    else
                    {
                        // Pop한 최상위 노드(왼쪽 괄호) char prev
                        top = bracket.Pop();
                        // Pop한 최상위 노드가 현재 괄호와 짝이 맞지 않는다면 유효하지 않음
                        if( (_bracket[i] == ')' && top != '(') ||
                            (_bracket[i] == '}' && top != '{') ||
                            (_bracket[i] == ']' && top != '[') )
                        {
                            break;
                        }
                    }
                }
            }

            // 반복문을 모두 진행했는데 bracket 스택에 데이터가 남아있다면 유효하지 않음
            if(!bracket.IsEmpty())
                Console.WriteLine("유효하지 않음");
            else
                Console.WriteLine("유효함");
        }
    }
}
