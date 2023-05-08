using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    public enum Direction { Up, Down, Left, Right }

    public class MapScene : Scene
    {
        public MapScene(Game game) : base(game)
        {
        }

        public override void Render()
        {
            PrintMap();
            PrintMenu();
            PrintInfo();

            Console.SetCursorPosition(0, Data.map.GetLength(0) + 1);
        }

        public override void Update()
        {
            ConsoleKeyInfo input;

            while (true)
            {
                input = Console.ReadKey();

                if (input.Key == ConsoleKey.Q ||
                    input.Key == ConsoleKey.I ||
                    input.Key == ConsoleKey.UpArrow ||
                    input.Key == ConsoleKey.DownArrow ||
                    input.Key == ConsoleKey.LeftArrow ||
                    input.Key == ConsoleKey.RightArrow)
                {
                    break;
                }
            }

            // 시스템 키 입력시 씬 전환
            if (input.Key == ConsoleKey.Q)
            {
                game.MainMenu();
                return;
            }
            else if (input.Key == ConsoleKey.I)
            {
                game.Inventory();
                return;
            }

            // 플레이어 이동
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    Data.player.TryMove(Direction.Up);
                    break;
                case ConsoleKey.DownArrow:
                    Data.player.TryMove(Direction.Down);
                    break;
                case ConsoleKey.LeftArrow:
                    Data.player.TryMove(Direction.Left);
                    break;
                case ConsoleKey.RightArrow:
                    Data.player.TryMove(Direction.Right);
                    break;
            }

            // 아이템 습득
            Item item = Data.ItemInPos(Data.player.pos);
            if (item != null)
            {
                Data.player.GetItem(item);
                Data.items.Remove(item);
            }

            // 몬스터 전투
            Monster monster = Data.MonsterInPos(Data.player.pos);
            if (monster != null)
            {
                game.Battle(monster);
                return;
            }

            // 몬스터 이동
            foreach (Monster m in Data.monsters)
            {
                m.MoveAction();
                if (m.pos.x == Data.player.pos.x &&
                    m.pos.y == Data.player.pos.y)
                {
                    game.Battle(m);
                    return;
                }
            }
        }

        public void GenerateMap()
        {
            Random rand = new Random();
            int index = rand.Next(0,2);
            if(index == 0) Data.LoadLevel1();
            else Data.LoadLevel2();
        }

        private void PrintMap()
        {
            StringBuilder sb = new StringBuilder();
            Console.ForegroundColor = ConsoleColor.White;
            for (int y = 0; y < Data.map.GetLength(0); y++)
            {
                for (int x = 0; x < Data.map.GetLength(1); x++)
                {
                    if (Data.map[y, x])
                        sb.Append(' ');
                    else
                        sb.Append('▒');
                }
                sb.AppendLine();
            }
            Console.WriteLine(sb.ToString());

            foreach (Monster monster in Data.monsters)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(monster.pos.x, monster.pos.y);
                Console.Write(monster.icon);
            }

            foreach (Item item in Data.items)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(item.pos.x, item.pos.y);
                Console.Write(item.icon);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Data.player.pos.x, Data.player.pos.y);
            Console.Write(Data.player.icon);
        }

        // 메뉴 출력
        private void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            (int left, int top) pos = Console.GetCursorPosition();
            Console.SetCursorPosition(Data.map.GetLength(1) + 3, 1);
            Console.Write("메뉴 : Q");
            Console.SetCursorPosition(Data.map.GetLength(1) + 3, 3);
            Console.Write("이동 : 방향키");
            Console.SetCursorPosition(Data.map.GetLength(1) + 3, 4);
            Console.Write("인벤토리 : I");
        }

        // 정보 출력
        private void PrintInfo()
        {
            Console.SetCursorPosition(0, Data.map.GetLength(0) + 1);
            Console.Write($"HP : {Data.player.CurHp,3}/{Data.player.MaxHp,3}\t");
            Console.Write($"EXP : {Data.player.CurExp,3}/{Data.player.MaxExp,3}\t");
            Console.Write($"Level : {Data.player.Level,3}");
        }
    }
}