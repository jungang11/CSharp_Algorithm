using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    public class Slime : Monster
    {
        private Random random = new Random();
        private int moveTurn = 0;
        private char icon = '▼';

        public Slime()
        {
            name = "슬라임";
            curHp = 10;
            maxHp = 10;
            ap = 3;
            dp = 0;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("                       .B               ");
            sb.AppendLine("                      :QQ7              ");
            sb.AppendLine("                    .PE: .KJ            ");
            sb.AppendLine("                   IZur:.. dL           ");
            sb.AppendLine("               .rISvr7v7ri. iIr         ");
            sb.AppendLine("          .vrI2sr.:rs177ri:.  r2L.      ");
            sb.AppendLine("      :LU2vii.::i7u5XJvri::ii:  iYSr    ");
            sb.AppendLine("    72r:  .irsj22S5Xuvi:::::::..  .rI   ");
            sb.AppendLine("  r5i.JZgvuUUJUjUusL7:.   . .        IE ");
            sb.AppendLine(" :Q .Br.RB7v7vvLvL7i.v1Jr             QE");
            sb.AppendLine("7I..rBv:BQi7i:riri::QB7:BB.         . iE");
            sb.AppendLine("B: :LYPRKi:gPS.Ei..:BE  qBr        ... E");
            sb.AppendLine("B. iuvY::iiiSbZP:...7BZSB7  ... ..:i:. E");
            sb.AppendLine("B. :JuLririr:::..:..  ..   . . .   .:. E");
            sb.AppendLine("Zv .rYjvrrriri:..................  .: .E");
            sb.AppendLine(" 71 .vu1v7rrrrri::.....:.:.:.::i72si..BE");
            sb.AppendLine("  S7  rY1Jsv77v77ri:i:::::iir7Ysur: iPr ");
            sb.AppendLine("   :1i ..rvuujsuJJ7L7v7v7v7777r:...IJ   ");
            sb.AppendLine("     iI77...::irv7Yvvvv7ri:..:::j2S.    ");
            sb.AppendLine("         ii::ir7.::::::::7rrrii:        ");
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
                Console.WriteLine($"공격은 {name}에게 먹히지 않았다.");

            Thread.Sleep(1000);

            if (curHp <= 0)
            {
                Console.WriteLine($"{name}(은/는) 쓰러졌다!");
                Thread.Sleep(1000);
                Console.WriteLine("몬스터는 포션을 떨어뜨렸다!");
                Data.player.GetItem(new Potion());
                Thread.Sleep(1000);
                Console.WriteLine("플레이어는 30 경험치를 획득했다.");
                Data.player.GetExp(30);
                Thread.Sleep(2000);
            }
        }

        public override void Attack(Player player)
        {
            Console.WriteLine($"{name}(이/가) 점액을 이용해 플레이어를 공격합니다.");
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