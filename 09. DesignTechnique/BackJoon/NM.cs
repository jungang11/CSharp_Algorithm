using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _09._DesignTechnique.BackJoon
{
    internal class NM
    {
        // 출력위해 sb에 저장
        static StringBuilder sb = new StringBuilder();

        // 재귀함수
        public static void Back(int[] arr, int index, int N, int M)
        {
            // index가 M이 되면 배열 출력
            if (index == M)
            {
                for(int i = 0; i < M; i++)
                {
                    sb.AppendFormat("{0} ", arr[i]);
                }
                sb.Append("\n");
                return;
            }
            for (int i = 1; i <= N; i++)
            {
                // 재귀
                arr[index] = i;
                Back(arr, index + 1, N, M);
            }
        }

        // M개까지 N줄 출력
        public static void Main(string[] args)
        {
            // 한 줄에 두개 입력받기 위해 s 문자열을 공백기준으로 나눔
            string[] s = Console.ReadLine().Split();
            // s 문자열 나눈대로 N과 M
            int N = int.Parse(s[0]);
            int M = int.Parse(s[1]);
            int[] arr = new int[N+1];

            Back(arr, 0, N, M);
            Console.WriteLine(sb);
        }
    }
}
