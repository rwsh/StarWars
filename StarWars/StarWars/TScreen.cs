using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Threading;

namespace StarWars
{
    class TScreen
    {
        public TPole Pole;

        public double X, Y;

        public int Score = 0;

        SoundPlayer spFire, spCrush;

        public TScreen(TPole Pole)
        {
            this.Pole = Pole;
            Pole.Screen = this;

            //spFire = new SoundPlayer("fire.wav");
            //spCrush = new SoundPlayer("crush.wav");

            spFire = new SoundPlayer();
            spFire.Stream = Properties.Resources.fire;

            spCrush = new SoundPlayer();
            spCrush.Stream = Properties.Resources.crush;

            X = 0;
            Y = 0;

            Pole.DrawStar();
            Pole.DrawTarget();
        }

        public void DoFire()
        {
            double eps = 10;

            for (int n = 0; n < Pole.Targets.Count; n++)
            {
                if (!Pole.Targets[n].Live)
                {
                    continue;
                }

                double rho = Math.Sqrt(Calc.S2(X - Pole.Targets[n].x) + Calc.S2(Y - Pole.Targets[n].y));

                if (rho < eps)
                {
                    Score++;

                    Pole.tbScore.Text = "Сбитых истербителей: " + Score.ToString();

                    Pole.Targets[n].Crush();
                    
                    Pole.CheckTarget(Pole.Targets[n].Name);

//                    if (!Tscc)
                    {
                        spCrush.Play();
                        Sleep(100);
                    }

                    return;
                }
            }

            if (!Tscc)
            {
                spFire.Play();
                Sleep(10);
            }
            
        }

        bool Tscc = false;
        int CountSleep;
        int ms10;

        public void Sleep(int ms10) // 10 ms
        {
            Tscc = true;
            CountSleep = 0;
            this.ms10 = ms10;

            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(onTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            Timer.Start();
        }

        DispatcherTimer Timer;

        void onTick(object sender, EventArgs e)
        {
            CountSleep++;

            if (CountSleep > ms10)
            {
                Timer.Stop();
                Tscc = false;
            }
        }


        public void Shift(double dx, double dy)
        {
            X = X + dx;
            Y = Y + dy;

            if (X < 0)
            {
                X = X + 360;
            }

            if (X > 360)
            {
                X = X - 360;
            }

            if (Y < 0)
            {
                Y = Y + 360;
            }

            if (Y > 360)
            {
                Y = Y - 360;
            }

            Pole.ChangeXY();
        }
    }
}
