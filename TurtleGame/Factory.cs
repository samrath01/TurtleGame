using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleGame
{
    public static class Factory
    {
        public static Punter BuildPunter(int number)
        {
            if (number == 1)
            {
                return new Archie();
            }
            else if (number == 2)
            {
                return new Luca();
            }
            else if (number == 3)
            {
                return new Cooper();
            }
            return null;
        }
    }
}
