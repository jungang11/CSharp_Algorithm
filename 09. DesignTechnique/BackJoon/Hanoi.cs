using System.Text;

namespace _09._DesignTechnique.BackJoon
{
    // 시간초과

    public class Hanoi
    {
        static StringBuilder sb = new StringBuilder();

        public static void Move(int count, int start, int end)
        {
            int other = 6 - start - end;

            if(count == 1)
            {
                sb.AppendFormat($"{start} {end}\n");
                return;
            }
            else
            {
                Move(count - 1, start, other);
                sb.AppendFormat($"{start} {end}\n");
                Move(count - 1, other, end);
            }
        }
        
        /*public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            
            Console.WriteLine(Math.Pow(2, n) - 1);
            Move(n, 1, 3);
            Console.WriteLine(sb);
        }*/
    }
}
