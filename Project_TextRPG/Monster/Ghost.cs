using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    public class Ghost : Monster
    {
        private Random random = new Random();
        private int moveTurn = 0;

        public Ghost()
        {
            name = "유령";
            curHp = 1;
            maxHp = 1;
            ap = 3;
            dp = 100;

            StringBuilder sb = new StringBuilder();
            sb.Append(",   ,   ,         ,       \r\n" +
                "            rM:         s \r\n" +
                "           2BiBr     :2Ss \r\n" +
                "          9BBGBB2  :sM:   \r\n" +
                "         GBMMMBMBBGiBB    \r\n" +
                "       rBBMBGGMMGBBBBB,   \r\n" +
                "      iBBGMMGGMGBBMBBB2   \r\n" +
                "      hBBBBBMGGGMB MBB,   \r\n" +
                "  :s::Gh9BsGBGGGMBs BB    \r\n" +
                "  iBBB:sMB, BBGGGB9 r     \r\n" +
                "    BB2 iB  BBBMMMBs      \r\n" +
                "     BBG:r   9BBBBBB,     \r\n" +
                "      iXBMs    rsMBM,     \r\n" +
                "         sMB9i     ii,    \r\n" +
                "            ,:r:          ");
            image = sb.ToString();
        }

        public override void TakeDamage(int damage)
        {
            if (damage > dp)
            {
                Console.WriteLine($"{name}(은/는) {damage - dp} 데미지를 받았다.");
                curHp -= damage - dp;
            }
            else
                Console.WriteLine($"{name}에게 공격은 먹히지 않는다.");

            Thread.Sleep(1000);
        }

        public override void Attack(Player player)
        {
            Console.WriteLine($"{name}(이/가) 플레이어를 공격합니다.");
            Thread.Sleep(1000);
            player.TakeDamage(ap);
        }

        public override void MoveAction()
        {
            if (moveTurn++ < 3)
            {
                return;
            }
            moveTurn = 0;

            switch (random.Next(0, 4))
            {
                case 0:
                    TryMove(Direction.Up);
                    break;
                case 1:
                    TryMove(Direction.Down);
                    break;
                case 2:
                    TryMove(Direction.Left);
                    break;
                case 3:
                    TryMove(Direction.Right);
                    break;
            }
        }
    }
}
