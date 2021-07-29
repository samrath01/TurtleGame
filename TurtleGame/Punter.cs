using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TurtleGame
{
    public abstract class Punter
    {
        public int Amount { set; get; }

        public bool Busted { set; get; }

        public Bet Bet { set; get; }

        public Label Label { set; get; }

        public RadioButton Radio { set; get; }

        public string Name { set; get; }

        public bool Winner { set; get; }

        public TextBox TextBox { set; get; }
    }
}
