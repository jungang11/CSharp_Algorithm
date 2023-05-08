using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Project_TextRPG
{
    public class Game
    {
        private bool running = true;

        private Scene           curScene;
        private MainMenuScene   mainMenu;
        private MapScene        mapScene;
        private InventoryScene  inventoryScene;
        private BattleScene     battleScene;

        public void Run()
        {
            // 초기화
            Init();

            // 게임 루프
            while(running)
            {
                // 렌더링
                Render();
                // 갱신
                Update();
            }
            // 마무리
            Release();
        }

        private void Init()
        {
            Console.CursorVisible = false;
            Data.Init();

            mainMenu = new MainMenuScene(this);
            mapScene = new MapScene(this);
            inventoryScene = new InventoryScene(this);
            battleScene = new BattleScene(this);

            curScene = mainMenu;
        }

        public void GameStart()
        {
            Data.LoadLevel1();
            curScene = mapScene;
        }

        public void GameOver()
        {
            Console.Clear();
            running = false;
        }

        private void Render()
        {
            Console.Clear();
            curScene.Render();
        }

        private void Update()
        {
            curScene.Update();
        }

        private void Release()
        {
            Data.Release();
        }
    }
}
