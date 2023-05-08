using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    public abstract class Monster
    {
        private Random random = new Random();

        public string name;
        public Position pos;
        public char icon = '▼';

        public string image;
        public int curHp;
        public int maxHp;
        public int ap;
        public int dp;

        public abstract void MoveAction();

        protected void TryMove(Direction dir)
        {
            Position prevPos = pos;
            // 이동
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
            else if (Data.IsObjectInPos(pos))
            {
                pos = prevPos;
            }
        }

        public virtual void TakeDamage(int damage)
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
                Console.WriteLine("플레이어는 경험치를 획득했다.");
                Data.player.GetExp(120);
                Thread.Sleep(2000);
            }
        }

        public virtual void Attack(Player player)
        {
            Console.WriteLine($"{name}(이/가) 플레이어를 공격합니다.");
            Thread.Sleep(1000);
            player.TakeDamage(ap);
        }
    }
}