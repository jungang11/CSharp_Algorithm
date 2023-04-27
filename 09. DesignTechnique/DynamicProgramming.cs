using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09._DesignTechnique
{
    internal class DynamicProgramming
    {
        /******************************************************
		 * 동적계획법 (Dynamic Programming)
		 * 
		 * 하나의 큰 문제를 여러 개의 작은 문제로 나누어서 그 결과를 저장하여 다시 큰 문제를 해결할 때 사용
		 * 작은문제의 해답을 큰문제의 해답의 부분으로 이용하는 상향식 접근 방식
		 * 주어진 문제를 해결하기 위해 부분 문제에 대한 답을 계속적으로 활용해 나가는 기법
		 ******************************************************/

        // 예시 - 피보나치 수열
        int Fibonachi(int x)
        {
            int[] fibonachi = new int[x + 1];
            fibonachi[1] = 1;
            fibonachi[2] = 1;

            for (int i = 3; i <= x; i++)
            {
                fibonachi[i] = fibonachi[i - 1] + fibonachi[i - 2];
            }

            return fibonachi[x];
        }



        /*public static void Test()
        {
            int[] arr = { 10, -4, 3, 1, 5, 6, -35, 12, 21, -1 };
            int[,] result = new int[arr.Length, arr.Length];
            // { 10 -4 3 1 5 6 -35 12 21 -1 } 1 ~ 10
            //     6 -1 
            for (int i = 0; i < arr.Length; i++)
            {
                result[i, 0] = arr[i];
            }

            for (int i = 1; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    result[j, i] = arr[j] + arr[j + 1];
                }
            }
        }*/
    }
}
