using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StarWars
{
    class TPole
    {
        public TScreen Screen;

        public TStars Stars;

        public TTargets Targets, Targets2;

        public Canvas g, g2;

        public TextBox tbScore;

        public double W, H, W2, H2;

        public double Ax = 50, Ay = 50;

        public TPole(Canvas g, Canvas g2, TextBox tbScore, int CountTargets)
        {
            this.g = g;
            this.g2 = g2;
            this.tbScore = tbScore;

            W = g.Width;
            H = g.Height;

            W2 = W / 2;
            H2 = H / 2;

            Stars = new TStars(1000);

            Targets = new TTargets(CountTargets);

            Targets2 = new TTargets(CountTargets);

            Draw();

            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(onTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            Timer.Start();

        }

        DispatcherTimer Timer;

        void onTick(object sender, EventArgs e)
        {
            Control();

            for (int n = 0; n < Targets.Count; n++)
            {
                Targets[n].Move();
            }

            DrawTarget();
        }

        public bool Firing = false;

        public double dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;

        public void Control()
        {
            if (Firing)
            {
                Fire();
            }

            Screen.Shift(dx1 + dx2, dy1 + dy2);
        }


        public void DrawStar()
        {
            for (int n = 0; n < Stars.Count; n++)
            {
                if (IsVisir(Stars[n].x, Stars[n].y))
                {
                    double x = Stars[n].x - Screen.X;

                    if (x > Ax)
                    {
                        x = x - 360;
                    }
                    if (x < -Ax)
                    {
                        x = x + 360;
                    }

                    double y = Stars[n].y - Screen.Y;

                    if (y > Ay)
                    {
                        y = y - 360;
                    }
                    if (y < -Ay)
                    {
                        y = y + 360;
                    }

                    Stars[n].O.Margin = new Thickness((W2 / Ax) * x + W2, H / 2 - (H2 / Ay) * y , 0, 0);
                    Stars[n].O.Visibility = Visibility.Visible;
                }
                else
                {
                    Stars[n].O.Visibility = Visibility.Hidden;
                }
            }
        }

        public void DrawTarget()
        {
            for (int n = 0; n < Targets.Count; n++)
            {
                if (!Targets[n].Live)
                {
                    continue;
                }

                if (IsVisir(Targets[n].x, Targets[n].y))
                {
                    double x = Targets[n].x - Screen.X;

                    if (x > Ax)
                    {
                        x = x - 360;
                    }
                    if (x < -Ax)
                    {
                        x = x + 360;
                    }

                    double y = Targets[n].y - Screen.Y;

                    if (y > Ay)
                    {
                        y = y - 360;
                    }
                    if (y < -Ay)
                    {
                        y = y + 360;
                    }

                    Targets[n].xs = (W2 / Ax) * x + W2;
                    Targets[n].ys = H / 2 - (H2 / Ay) * y;

                    Targets[n].ReDraw();

                    Targets[n].Visibility = Visibility.Visible;
                }
                else
                {
                    Targets[n].Visibility = Visibility.Hidden;
                }
            }
        }


        public bool IsVisir(double x, double y)
        {
            if (!((Math.Abs(x - Screen.X) < Ax) || (Math.Abs(360 - x + Screen.X) < Ax) || (Math.Abs(360 - Screen.X + x) < Ax)))
            {
                return false;
            }

            if (!((Math.Abs(y - Screen.Y) < Ay) || (Math.Abs(360 - y + Screen.Y) < Ay) || (Math.Abs(360 - Screen.Y + y) < Ay)))
            {
                return false;
            }

            return true;
        }

        public void ChangeXY()
        {
            TextXY.Text = "X = " + Screen.X.ToString() + " Y = " + Screen.Y.ToString();

            DrawStar();
            DrawTarget();
        }

        public void Draw()
        {
            Line l = new Line();

            l.X1 = W2;
            l.Y1 = H2 - H / 10;
            l.X2 = W2;
            l.Y2 = H2 + H / 10;
            l.StrokeThickness = 1;
            l.Stroke = Brushes.White;
            g.Children.Add(l);

            l = new Line();

            l.X1 = W2 - W / 10;
            l.Y1 = H2;
            l.X2 = W2 + W / 10;
            l.Y2 = H2;
            l.StrokeThickness = 1;
            l.Stroke = Brushes.White;
            g.Children.Add(l);

            g.Children.Add(TCircle.O(W2, H2, W / 10, Brushes.White));

            TextXY = new TextBlock();

            TextXY.Text = "X = 0 Y = 0";

            TextXY.Foreground = Brushes.White;

            Canvas.SetLeft(TextXY, W * (3.0 / 4.0));

            Canvas.SetTop(TextXY, H - 20);

            g.Children.Add(TextXY);

            for (int i = 0; i < Stars.Count; i++)
            {
                g.Children.Add(Stars[i].O);
            }

            for (int i = 0; i < Targets.Count; i++)
            {
                Targets[i].Add(g);
            }

            //

            int Name = -1;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Name++;

                    TTarget T = Targets2[Name];
                    T.H /= 2.0;
                    T.W /= 2.0;
                    T.R /= 2.0;

                    T.xs = T.W + T.W * 4 * j;
                    T.ys = T.H + T.H * 4 * i;

                    T.Draw(Brushes.Red);
                    T.Visibility = Visibility.Visible;

                    T.Add(g2);
                }
            }

        }

        bool IsFire = false;
        int FireCur = 0;

        Line L1, L2;

        TextBlock TextXY;

        public TextBlock TextWin;

        DispatcherTimer TimerFire;

        void onTickFire(object sender, EventArgs e)
        {
            FireCur++;

            if(FireCur > 2)
            {
                TimerFire.Stop();

                IsFire = false;

                g.Children.Remove(L1);
                g.Children.Remove(L2);

                Screen.DoFire();
            }

            L1.X1 = W * (3.0 / 8.0);
            L1.Y1 = H * (3.0 / 4.0);
            L1.X2 = W2;
            L1.Y2 = H2;

            L2.X1 = W * (5.0 / 8.0);
            L2.Y1 = H * (3.0 / 4.0);
            L2.X2 = W2;
            L2.Y2 = H2;
        }

        public void Fire()
        {
            if (IsFire)
            {
                return;
            }

            IsFire = true;
            FireCur = 0;

            L1 = new Line();
            L2 = new Line();

            L1.X1 = W / 4;
            L1.Y1 = H;
            L1.X2 = W * (3.0 / 8.0);
            L1.Y2 = H * (3.0 / 4.0);
            L1.Stroke = Brushes.Red;
            L1.StrokeThickness = 1;

            L2.X1 = W * (3.0 / 4.0);
            L2.Y1 = H;
            L2.X2 = W * (5.0 / 8.0);
            L2.Y2 = H * (3.0 / 4.0);
            L2.Stroke = Brushes.Red;
            L2.StrokeThickness = 1;


            g.Children.Add(L1);
            g.Children.Add(L2);

            TimerFire = new DispatcherTimer();
            TimerFire.Tick += new EventHandler(onTickFire);
            TimerFire.Interval = new TimeSpan(0, 0, 0, 0, 10);
            TimerFire.Start();
        }

        public void CheckTarget(int Name)
        {
            Targets2[Name].ReColor(Brushes.Black);

            if (Screen.Score == Targets.Count)
            {
                TextWin = new TextBlock();

                TextWin.Text = "Game over!";

                TextWin.Foreground = Brushes.Red;

                TextWin.FontSize = 46;

                Canvas.SetLeft(TextWin, W * (1.0 / 4.0));

                Canvas.SetTop(TextWin, H / 4);

                g.Children.Add(TextWin);

            }
        }
    }

    static class TCircle
    {
        public static Ellipse O(double x, double y, double R, Brush br)
        {
            Ellipse O = new Ellipse();

            O.Width = R;
            O.Height = R;

            O.Margin = new Thickness(x - R / 2, y - R / 2, 0, 0);
            O.Stroke = br;

            return O;
        }
    }
}
