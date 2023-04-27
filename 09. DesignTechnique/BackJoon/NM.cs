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
        public static void Back(int[] arr, int index, int N, int M)
        {
            // 횟수가 M이 되면 배열 출력
            if (index == M)
            {
                for(int i = 0; i < M; i++)
                {
                    Console.Write($"{arr[i]} ");
                }
                Console.WriteLine();
            }
            for (int i = 1; i < N+1; i++)
            {
                arr[index] = i;
                Back(arr, index + 1, N, M);
            }
        }

        // M개까지 N줄 출력
        public static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            int M = int.Parse(Console.ReadLine());
            int[] arr = new int[N];
            for(int i = 0; i < N; i++)
            {
                arr[i] = i+1;
            }

            Back(arr, 0, N, M);
        }
    }
}
