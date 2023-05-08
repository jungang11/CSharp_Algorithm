﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    public enum Direction { Left, Right, Up, Down }

    public class Player
    {
        public Position pos;
        public char icon = '♥';

        public void Move(Direction dir)
        {
            Position prevPos = pos;
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

            // 이동한 자리가 벽
            if (!Data.map[pos.y, pos.x])
            {
                // 원위치
                pos = prevPos;
            }
        }
    }
}
