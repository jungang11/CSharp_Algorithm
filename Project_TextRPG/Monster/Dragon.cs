using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    public class Dragon : Monster
    {
        public override void MoveAction()
        {
            List<Point> path;
            bool result = AStar.PathFinding(Data.map, new Point(pos.x, pos.y),
                new Point(Data.player.pos.x, Data.player.pos.y), out path);

            if (!result)
                return;
        }
    }
}

