using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace _04._Stack
{
    public class StackCalCulator
    {
        // 스택 사칙연산 계산기

        // 중위 연산자를 후위 연산자로 바꿈
        // 피연산자는 스택에 넣지않고 출력
        // 연산자는 스택이 비어있으면 스택에 push, 스택이 비어있지 않을 경우
        // 스택의 연산자의 우선순위가 현재 연산자보다 크거나 같다면 스택을 Pop하고 출력
        // 그 후 현재 연산자를 스택에 Push
        // 우선순위가 현재 연산자가 크다면 현재 연산자를 스택에 push
        public static void GetPostfix(string str)
        {
            DataStructure.AdapterStack<char> postfix = new DataStructure.AdapterStack<char>();
            List<char> list = new List<char>();

            char prev;

            foreach (char c in str)
            {
                // 연산자를 받을 때
                if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    if (!postfix.IsEmpty())
                    {
                        prev = postfix.Peek();
                        if ((prev == '*' || prev == '/') && (c == '+' || c == '-'))
                        {
                            list.Add(postfix.Pop());
                            postfix.Push(c);
                        }
                        if ((prev == '*' || prev == '/') && (c == '*' || c == '/'))
                        {
                            list.Add(postfix.Pop());
                            postfix.Push(c);
                        }
                        if ((prev == '+' || prev == '-') && (c == '+' || c == '-'))
                        {
                            list.Add(postfix.Pop());
                            postfix.Push(c);
                        }
                        if ((prev == '+' || prev == '-') && (c == '*' || c == '/'))
                        {
                            postfix.Push(c);
                        }
                        if (prev == '(')
                        {
                            postfix.Push(c);
                        }
                    }
                    else
                    {
                        postfix.Push(c);
                    }
                }
                // 왼쪽 괄호 받을 때
                else if (c == '(')
                {
                    postfix.Push(c);
                }
                // 오른쪽 괄호 받을 때
                else if (c == ')')
                {
                    while(postfix.Peek() != '(')
                    {
                        list.Add(c);
                    }
                    postfix.Pop();
                }
                // 숫자를 받을 때
                else
                {
                    list.Add(c);
                }
            }

            while (postfix.IsEmpty())
            {
                list.Add(postfix.Pop());
            }

            foreach(char s in list)
                Console.Write(s);
        }

        // 피연산자면 스택에 push
        // 연산자를 만나면 pop을 두번하고 각각의 값을 연산자에 맞춰 계산
        // 계산후의 값을 스택에 push -> 반복
        // 수식이 끝나면 스택에 남은 값이 결과값
        public static void StackCal()
        {
            DataStructure.AdapterStack<char> cal = new DataStructure.AdapterStack<char> ();
            int result = 0;
            int a, b = 0;
            
            string str = "2+3*4+5";

            foreach(char c in str)
            {
                if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    a = cal.Pop();
                    b = cal.Pop();
                    switch (c)
                    {
                        case '+':
                            result = Add(a, b); break;
                        case '-':
                            result = Minus(a, b); break;
                        case '*':
                            result = Multi(a, b); break;
                        case '/':
                            result = Divide(a, b); break;
                    }
                    cal.Push(result);
                }
            }






        }

        public static int Add(int a, int b) { return a + b; }
        public static int Minus(int a, int b) { return a - b; }
        public static int Multi(int a, int b) { return a * b; }
        public static int Divide(int a, int b) { return a / b; }
    }
}
