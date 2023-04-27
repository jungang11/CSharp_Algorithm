namespace _09._DesignTechnique.BackJoon
{
    // 시간초과

    public class Hanoi
    {
        public static void Move(int count, int start, int end)
        {
            int other = 6 - start - end;

            if(count == 1)
            {
                Console.WriteLine($"{start} {end}");
            }
            else
            {
                Move(count - 1, start, other);
                Console.WriteLine($"{start} {end}");
                Move(count - 1, other, end);
            }
        }
        
        public static void hanoi()
        {
            int n = int.Parse(Console.ReadLine());
            
            Console.WriteLine(Math.Pow(2, n) - 1);
            Move(n, 1, 3);
        }
    }
}
