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
    class TStars
    {
        TStar[] Stars;

        public TStars(int N)
        {
            Stars = new TStar[N];

            for (int i = 0; i < N; i++)
            {
                Stars[i] = new TStar();
            }
        }

        public int Count
        {
            get
            {
                return Stars.Length;
            }
        }

        public TStar this[int Ind]
        {
            get
            {
                return Stars[Ind];
            }

            set
            {
                Stars[Ind] = value;
            }
        }
    }

    class TStar
    {
        public double x, y;

        public Ellipse O;

        public TStar()
        {
            x = Calc.U(0, 360);
            y = Calc.U(0, 360);

            O = new Ellipse();

            O.Width = 3;
            O.Height = 3;
            O.Fill = Brushes.White;
            O.Stroke = Brushes.Yellow;

            O.Visibility = Visibility.Hidden;
        }
    }
}
