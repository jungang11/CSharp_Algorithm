using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    public class Player
    {
        public string image;
        public ClassType type;
        public char icon = '♥';
        public Position pos;
        public List<Skill> skills;

        public Player()
        {
            CurHp = 60;
            MaxHp = 100;
            Level = 1;
            CurExp = 0;
            MaxExp = 100;
            AP = 5;
            DP = 1;

            skills = new List<Skill>();
            skills.Add(new Skill("공격하기", Attack));
            skills.Add(new Skill("회복하기", Recovery));
            skills.Add(new Skill("스킬사용", Fireball));
            skills.Add(new Skill("도망가기", Escape));

            StringBuilder sb = new StringBuilder();
            sb.Append("      BB   \r\n" +
                "     :70   \r\n" +
                "    iB2    \r\n" +
                "  ..r027i:.\r\n" +
                "  :22i:777:\r\n" +
                " 2i72i.iri \r\n" +
                ".7X BS ir..\r\n" +
                " :. BB.::7.\r\n" +
                "   i07iiri \r\n" +
                "  iBM  B2  \r\n" +
                "  7rr  2.  \r\n" +
                "  7B.  7X  ");
            image = sb.ToString();
        }

        public int CurHp { get; private set; }
        public int MaxHp { get; private set; }
        public int Level { get; private set; }
        public int CurExp { get; private set; }
        public int MaxExp { get; private set; }
        public int AP { get; private set; }
        public int DP { get; private set; }

        public void TryMove(Direction dir)
        {
            Position prevPos = pos;
            // 플레이어 이동
            switch (dir)
            {
                case Direction.Up:
                    pos.y--;
                    break;
                case Direction.Down:
                    pos.y++;
                    break;
                case Direction.Left:
                    pos.x--;
                    break;
                case Direction.Right:
                    pos.x++;
                    break;
            }

            // 이동한 자리가 벽일 경우
            if (!Data.map[pos.y, pos.x])
            {
                // 원위치 시키기
                pos = prevPos;
            }
        }

        public void GetItem(Item item)
        {
            Data.inventory.Add(item);
        }

        public void UseItem(Item item)
        {
            Data.inventory.Remove(item);
            item.Use();
        }

        public void GetExp(int exp)
        {
            CurExp += exp;
            if(CurExp > MaxExp)
            {
                CurExp -= MaxExp;
                LevelUp();
            }
        }

        public void LevelUp()
        {
            Console.WriteLine("플레이어 레벨 업!");
            Level++;
            CurHp = MaxHp;
            AP += 1;
            DP += 1;
            MaxExp += 10;
        }

        public void Heal(int heal)
        {
            CurHp += heal;
            if (CurHp > MaxHp)
                CurHp = MaxHp;
        }

        public void Buff(int buff)
        {
            AP += buff;
        }

        public void Attack(Monster monster)
        {
            Console.WriteLine($"플레이어가 {monster.name}(을/를) 공격한다.");
            Thread.Sleep(1000);
            monster.TakeDamage(AP);
        }

        public void Recovery(Monster monster)
        {
            Console.WriteLine("플레이어가 회복을 시도합니다.");
            Thread.Sleep(1000);
            Heal(5);
            Console.WriteLine($"플레이어의 체력이 {CurHp}가 되었습니다.");
            Thread.Sleep(1000);
        }

        public void Fireball(Monster monster)
        {
            Console.WriteLine("(AP * 5) 데미지 파이어볼 사용.");
            Thread.Sleep(1000);
            monster.TakeDamage(AP * 5);
        }

        public void Escape(Monster monster)
        {
            Console.WriteLine($"{monster.name}에서 도망가기");
            Thread.Sleep(1000);
        }

        public void TakeDamage(int damage)
        {
            if (damage > DP)
            {
                Console.WriteLine($"플레이어는 {damage - DP} 데미지를 받았다.");
                CurHp -= damage - DP;
            }
            else
                Console.WriteLine($"공격은 플레이어에게 먹히지 않았다.");

            Thread.Sleep(1000);

            if (CurHp <= 0)
            {
                Console.WriteLine($"플레이어는 쓰려졌다!");
                Thread.Sleep(1000);
            }
        }
    }
}