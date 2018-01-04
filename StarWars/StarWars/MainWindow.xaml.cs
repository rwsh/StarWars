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
using System.Media;

namespace StarWars
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Calc.Init();

            int Count = 16;

            Pole = new TPole(g, g2, tbScore, Count);
            Screen = new TScreen(Pole);
        }

        TScreen Screen;
        TPole Pole;

        double dx = 2;
        double dy = 2;

        private void cmKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Pole.Firing = true;
            }

            if (e.Key == Key.Right)
            {
                Pole.dx1 = dx;
            }

            if (e.Key == Key.Left)
            {
                Pole.dx2 = -dx;
            }

            if (e.Key == Key.Up)
            {
                Pole.dy1 = dy;
            }

            if (e.Key == Key.Down)
            {
                Pole.dy2 = -dy;
            }

            //if (e.Key == Key.Space)
            //{
            //    Pole.Fire();
            //}

            //if (e.Key == Key.Right)
            //{
            //    Screen.Shift(dx, 0);
            //}

            //if (e.Key == Key.Left)
            //{
            //    Screen.Shift(-dx, 0);
            //}

            //if (e.Key == Key.Up)
            //{
            //    Screen.Shift(0, dy);
            //}

            //if (e.Key == Key.Down)
            //{
            //    Screen.Shift(0, -dy);

            //} 
            //SoundPlayer sp = new SoundPlayer();
        }

        private void cmKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Pole.Firing = false;
            }

            if (e.Key == Key.Right)
            {
                Pole.dx1 = 0;
            }

            if (e.Key == Key.Left)
            {
                Pole.dx2 = 0;
            }

            if (e.Key == Key.Up)
            {
                Pole.dy1 = 0;
            }

            if (e.Key == Key.Down)
            {
                Pole.dy2 = -0;
            }

        }


        private void cmRestart(object sender, RoutedEventArgs e)
        {
            for (int n = 0; n < Pole.Targets.Count; n++)
            {
                Pole.Targets[n].Remove(g);
            }

            Pole.Targets = new TTargets(Pole.Targets.Count);

            for (int n = 0; n < Pole.Targets.Count; n++)
            {
                Pole.Targets[n] = new TTarget(n);
                Pole.Targets[n].Add(g);

                Pole.Targets2[n].ReColor(Brushes.Red);
            }

            if(Pole.TextWin != null)
            {
                g.Children.Remove(Pole.TextWin);
            }

            tbScore.Text = "Сбитых истребителей нет";
            Screen.Score = 0;

            g.Focus();

            tbScore.Focusable = false;
        }

    }
}
