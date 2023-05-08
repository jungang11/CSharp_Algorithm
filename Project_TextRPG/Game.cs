using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    internal class Game
    {
        public void Run()
        {
            // 초기화
            Init();

            // 게임 루프
            while(true)
            {
                // 렌더링
                Render();
                // 입력
                // 갱신
                Update();
            }
            // 마무리
            Release();
        }

        private void Init()
        {

        }

        private void Render()
        {

        }

        private void Update()
        {

        }

        private void Release()
        {

        }
    }
}
