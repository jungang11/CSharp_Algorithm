using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project_TextRPG
{
    public class BuffPotion : Item
    {
        private int point = 2;

        public BuffPotion()
        {
            name = "힘포션";
            description = $"플레이어의 공격력이 {point} 강해집니다.";
            weight = 2;
        }

        public override void Use()
        {
            Console.WriteLine($"힘포션을 사용하여 플레이어의 공격력이 {point} 강해집니다.");
            Thread.Sleep(1000);
            Data.player.Buff(point);
            Console.WriteLine($"플레이어의 공격력이 {Data.player.AP}이 되었습니다.");
            Thread.Sleep(1000);
        }
    }
}
