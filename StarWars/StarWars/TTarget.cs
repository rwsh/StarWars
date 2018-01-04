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
    class TTargets
    {
        TTarget[] Targets;

        public TTargets(int N)
        {
            Targets = new TTarget[N];

            for (int n = 0; n < N; n++)
            {
                Targets[n] = new TTarget(n);
            }
        }

        public int Count
        {
            get
            {
                return Targets.Length;
            }
        }

        public TTarget this[int Ind]
        {
            get
            {
                return Targets[Ind];
            }

            set
            {
                Targets[Ind] = value;
            }
        }
    }


    class TTarget
    {
        public int Name;

        public bool Live = true;

        public double x, y;

        public double xs, ys;

        public double W, H, R;

        public double vx, vy;
        

        public TTarget(int Name)
        {
            this.Name = Name;

            x = Calc.U(0, 360);
            y = Calc.U(0, 360);

            double f = Calc.U(0, Math.PI * 2);

            vx = Math.Cos(f);
            vy = Math.Sin(f);

            double r = Calc.U(0, 1);

            vx *= r;
            vy *= r;

            W = 30;
            H = 20;
            R = 20;

            Draw();
        }

        Ellipse O;
        Line L1, L2, L3;

        Canvas g;

        public void Move()
        {
            if (!Live)
            {
                return;
            }

            x += vx;
            y += vy;

            if (x < 0)
            {
                x += 360;
            }
            if (x > 360)
            {
                x -= 360;
            }
            if (y < 0)
            {
                y += 360;
            }
            if (y > 360)
            {
                y -= 360;
            }
        }

        public void ReColor(Brush Color)
        {
            O.Fill = Color;
            L1.Stroke = Color;
            L2.Stroke = Color;
            L3.Stroke = Color;
        }

        public void Add(Canvas g)
        {
            this.g = g;

            g.Children.Add(O);
            g.Children.Add(L1);
            g.Children.Add(L2);
            g.Children.Add(L3);
        }

        public void Remove(Canvas g)
        {
            g.Children.Remove(O);
            g.Children.Remove(L1);
            g.Children.Remove(L2);
            g.Children.Remove(L3);
        }

        public Visibility Visibility
        {
            get
            {
                return O.Visibility;
            }
            set
            {
                O.Visibility = value;
                L1.Visibility = value;
                L2.Visibility = value;
                L3.Visibility = value;
            }

        }

        public void ReDraw()
        {
            O.Margin = new Thickness(xs - R / 2, ys - R / 2, 0, 0);

            L1.X1 = xs - W;
            L1.Y1 = ys;
            L1.X2 = xs + W;
            L1.Y2 = ys;

            L2.X1 = xs - W;
            L2.Y1 = ys - H;
            L2.X2 = xs - W;
            L2.Y2 = ys + H;

            L3.X1 = xs + W;
            L3.Y1 = ys - H;
            L3.X2 = xs + W;
            L3.Y2 = ys + H;
        }

        public void Draw(Brush Color = null)
        {
            if (Color == null)
            {
                Color = Brushes.Gray;
            }

            O = TCircle.O(xs, ys, R, Brushes.White);
            O.Fill = Color;
            O.Visibility = Visibility.Hidden;

            L1 = new Line();
            L1.X1 = xs - W;
            L1.Y1 = ys;
            L1.X2 = xs + W;
            L1.Y2 = ys;
            L1.Stroke = Color;
            L1.StrokeThickness = 3;
            L1.Visibility = Visibility.Hidden;

            L2 = new Line();
            L2.X1 = xs - W;
            L2.Y1 = ys - H;
            L2.X2 = xs - W;
            L2.Y2 = ys + H;
            L2.Stroke = Color;
            L2.StrokeThickness = 3;
            L2.Visibility = Visibility.Hidden;

            L3 = new Line();
            L3.X1 = xs + W;
            L3.Y1 = ys - H;
            L3.X2 = xs + W;
            L3.Y2 = ys + H;
            L3.Stroke = Color;
            L3.StrokeThickness = 3;
            L3.Visibility = Visibility.Hidden;

            //Image f = new Image();

        }

        DispatcherTimer Timer;

        int Iter = 0;

        public void Crush()
        {
            Live = false;
            Visibility = Visibility.Hidden;

            Exp = new Ellipse[ExpN];

            for (int n = 0; n < ExpN; n++)
            {
                Exp[n] = TCircle.O(xs, ys, 10, Brushes.Red);
                Exp[n].Fill = Brushes.Yellow;

                g.Children.Add(Exp[n]);
            }

            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(onTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            Timer.Start();
        }

        int ExpN = 10;

        Ellipse[] Exp;

        void onTick(object sender, EventArgs e)
        {
            Iter++;

            if (Iter > 25)
            {
                Timer.Stop();

                for (int n = 0; n < ExpN; n++)
                {
                    g.Children.Remove(Exp[n]);
                }

                return;
            }

            for (int n = 0; n < ExpN; n++)
            {
                Exp[n].Margin = new Thickness(xs + Iter * 2.0 * Math.Cos(n * 2 * Math.PI / ExpN), ys + Iter * 2.0 * Math.Sin(n * 2 * Math.PI / ExpN), 0, 0);
            }
        }
    }
}
