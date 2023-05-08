using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    public class Knight : Player
    {
        public Knight()
        {
            CurHp = 110;
            MaxHp = 140;
            Level = 1;
            CurExp = 0;
            MaxExp = 100;
            AP = 7;
            DP = 5;

            skills = new List<Skill>();
            skills.Add(new Skill("공격하기", Attack));
            skills.Add(new Skill("회복하기", Recovery));
            skills.Add(new Skill("스킬사용", Fireball));
            skills.Add(new Skill("도망가기", Escape));

            StringBuilder sb = new StringBuilder();
            sb.Append("  ");
            image = sb.ToString();
        }

        public int CurHp { get; private set; }
        public int MaxHp { get; private set; }
        public int Level { get; private set; }
        public int CurExp { get; private set; }
        public int MaxExp { get; private set; }
        public int AP { get; private set; }
        public int DP { get; private set; }
    }
}
