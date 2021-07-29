using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TurtleGame
{
    public partial class Form1 : Form
    {

        Turtle[] turtles = new Turtle[4];
        Punter[] punters = new Punter[3];
        Turtle winner;

        Timer timer1,timer2,timer3,timer4;
        int RaceTrackLength;


        public Form1()
        {
            InitializeComponent();
            PrepareRaceData();
        }

        private void PrepareRaceData()
        {
            // Turtle Details
            RaceTrackLength = 1030;
            turtles[0] = new Turtle() { Name = "Turtle 1",Picture = picture1 };
            turtles[1] = new Turtle() { Name = "Turtle 2",Picture = picture2 };
            turtles[2] = new Turtle() { Name = "Turtle 3",Picture = picture3 };
            turtles[3] = new Turtle() { Name = "Turtle 4",Picture = picture4 };


            punters[0] = Factory.BuildPunter(1);
            punters[1] = Factory.BuildPunter(2);
            punters[2] = Factory.BuildPunter(3);
            
            punters[0].Label = labelBet;
            punters[0].Radio = punter1Radio;
            punters[0].TextBox = textBoxPunter1;
            punters[0].Radio.Text = punters[0].Name;

            
            punters[1].Label = labelBet;
            punters[1].Radio = punter2Radio;
            punters[1].TextBox = textBoxPunter2;
            punters[1].Radio.Text = punters[1].Name;

            
            punters[2].Label = labelBet;
            punters[2].Radio = punter3Radio;
            punters[2].TextBox = textBoxPunter3;
            punters[2].Radio.Text = punters[2].Name;

            npTurtleNo.Minimum = 1;
            npTurtleNo.Maximum = 4;
            npTurtleNo.Value = 1;
        }


        private void puntersRadio_CheckedChanged(object sender, EventArgs e)
        {
            SetupBetDetails();
        }

        private void SetupBetDetails()
        {
            foreach (Punter punter in punters)
            {
                if (punter.Busted)
                {
                    punter.TextBox.Text = "YOU LOST ALL YOUR AMOUNT. SO BUSTED!!!";
                }
                else
                {
                    if (punter.Bet == null)
                    {
                        punter.TextBox.Text = punter.Name + " hasn't placed a bet";
                    }
                    else
                    {
                        punter.TextBox.Text = punter.Name + " bets $" + punter.Bet.Amount + " on " + punter.Bet.Turtle.Name;
                    }
                    if (punter.Radio.Checked)
                    {
                        labelMax.Text = "Max Bet Amount is $" + punter.Amount;
                        btnAction.Text = "Place Bet for " + punter.Name;
                        punter.Label.Text = punter.Name + " Bets Amount $";
                        npBetAmount.Minimum = 1;
                        npBetAmount.Maximum = punter.Amount;
                        npBetAmount.Value = 1;
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void labelTurtleNo_Click(object sender, EventArgs e)
        {

        }

        private void numericCycleNo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            if (btnAction.Text.Contains("Place"))
            {
                int count = 0;
                int total_active = 0;
                foreach (Punter punter in punters)
                {
                    if (punter.Busted)
                    {
                        //MessageBox.Show("Bet is Not Placed Because " + punter.Name + " is BUSTED");
                    }
                    else
                    { 
                        total_active++;
                        if (punter.Radio.Checked)
                        {
                            if (punter.Bet == null)
                            {
                                int number = (int)npTurtleNo.Value;
                                int amount = (int)npBetAmount.Value;
                                bool alreadyPlaced = false;
                                foreach (Punter pun in punters)
                                {
                                    if (pun.Bet != null && pun.Bet.Turtle == turtles[number - 1])
                                    {
                                        alreadyPlaced = true;
                                        break;
                                    }
                                }
                                if (alreadyPlaced)
                                {
                                    MessageBox.Show("This Turtle is Picked by Another Punter, Try Different Turtle");
                                }
                                else
                                {
                                    punter.Bet = new Bet() { Amount = amount, Turtle = turtles[number - 1] };
                                }

                            }
                            else
                            {
                                MessageBox.Show("You Already Place Bet for " + punter.Name);
                            }
                        }
                        if (punter.Bet != null)
                        {
                            count++;
                        }
                    }                    
                }
                SetupBetDetails();
                if (count == total_active)
                {
                    btnAction.Text = "Begin The Race";
                    panelBet.Enabled = false;
                }
            }
            else if (btnAction.Text.Contains("Begin"))
            {
                timer1 = new Timer();
                timer1.Interval = 15;
                timer1.Tick += Cycling_Tick;

                timer2 = new Timer();
                timer2.Interval = 15;
                timer2.Tick += Cycling_Tick;

                timer3 = new Timer();
                timer3.Interval = 15;
                timer3.Tick += Cycling_Tick;

                timer4 = new Timer();
                timer4.Interval = 15;
                timer4.Tick += Cycling_Tick;

                timer1.Start();
                timer2.Start();
                timer3.Start();
                timer4.Start();
                
            }
            else if (btnAction.Text.Contains("Game"))
            {
                MessageBox.Show("Game Over!!!");
                Application.Exit();
            }
        }


        private void Cycling_Tick(object sender, EventArgs e)
        {
            if(sender is Timer)
            {
                int index = -1;
                Timer timer = sender as Timer;
                if( timer == timer1)
                {
                    index = 0;
                }
                else if (timer == timer2)
                {
                    index = 1;
                }
                else if (timer == timer3)
                {
                    index = 2;
                }
                else if (timer == timer4)
                {
                    index = 3;
                }

                if( index != -1 )
                {
                    PictureBox pbox = turtles[index].Picture;
                    if (pbox.Location.X + pbox.Width > RaceTrackLength)
                    {  
                        if (winner == null)
                        {
                            winner = turtles[index];
                        }
                        timer1.Stop();
                        timer2.Stop();
                        timer3.Stop();
                        timer4.Stop();
                    }
                    else
                    {
                        int jump = new Random().Next(1, 15);
                        pbox.Location = new Point(pbox.Location.X + jump, pbox.Location.Y);
                    }
                }
            }
            if( winner != null)
            {
                MessageBox.Show("Hurray!!! " + winner.Name + " is Won As Well Cash...");
                SetupBetDetails();
                foreach (Punter punter in punters)
                {
                    if (punter.Bet != null)
                    {
                        if (punter.Bet.Turtle == winner)
                        {
                            punter.Amount += punter.Bet.Amount;
                            punter.TextBox.Text = punter.Name + " Won and now has $" + punter.Amount;
                            punter.Winner = true;
                        }
                        else
                        {
                            punter.Amount -= punter.Bet.Amount;
                            if (punter.Amount == 0)
                            {
                                punter.TextBox.Text = "YOU LOST ALL YOUR AMOUNT. SO BUSTED!!!";
                                punter.Busted = true;
                                punter.Radio.Enabled = false;
                                CheckBustedPunter();
                            }
                            else
                            {
                                punter.TextBox.Text = punter.Name + " Lost and now has $" + punter.Amount;
                            }
                        }                        
                    }
                }
                winner = null;
                timer1 = timer2 = timer3 = timer4 = null;
                int count = 0;
                foreach (Punter punter in punters)
                {
                    if (punter.Busted)
                    {
                        count++;
                    }
                    if (punter.Radio.Enabled && punter.Radio.Checked)
                    {
                        labelMax.Text = "Max Bet is $" + punter.Amount;
                        npBetAmount.Maximum = punter.Amount;
                        npBetAmount.Minimum = 1;
                    }
                    punter.Bet = null;
                    punter.Winner = false;
                }
                if (count == punters.Length)
                {
                    btnAction.Text = "Game Over";

                }
                else
                {
                    panelBet.Enabled = true;
                }
                foreach (Turtle turtle in turtles)
                {
                    turtle.Picture.Location = new Point(12, turtle.Picture.Location.Y);
                }
            }
        }

        private void CheckBustedPunter()
        {
            foreach (Punter punter in punters)
            {
                if (punter.Busted)
                {
                    punter.Radio.Checked = false;
                }
                else
                {
                    punter.Radio.Checked = true;
                }
            }
        }
    }
}
