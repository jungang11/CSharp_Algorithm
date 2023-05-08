using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    public enum ClassType { Knight = 0, Archor = 1 }

    public class ClassScene : Scene
    {
        ClassType type = ClassType.Knight;

        public ClassScene(Game game) : base(game)
        {
        }

        public override void Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("1. 기사");
            sb.AppendLine("2. 아처");
            sb.Append("직업을 선택하세요 : ");

            Console.Write(sb.ToString());
        }

        public override void Update()
        {
            string input = Console.ReadLine();

            int index;
            if (!int.TryParse(input, out index))
            {
                Console.WriteLine("잘못 입력 하셨습니다.");
                Thread.Sleep(1000);
                return;
            }

            switch (index)
            {
                case 1:
                    type = ClassType.Knight;
                    game.GameStart();
                    break;
                case 2:
                    type = ClassType.Archor;
                    game.GameStart();
                    break;
                default:
                    Console.WriteLine("잘못 입력 하셨습니다.");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
}
