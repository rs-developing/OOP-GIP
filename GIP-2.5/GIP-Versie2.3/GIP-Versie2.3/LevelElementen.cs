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

namespace GIP_Versie2._3
{
    class LevelElementen
    {
        //klassevariablen
        protected Canvas _objCanvas;
        protected int _x_pos, _y_pos, _grootte, _x_tegel, _y_tegel;
        protected BitmapImage objImage1 = new BitmapImage();
        protected Image picture = new Image();

        //constructor
        public LevelElementen(Canvas pCanvas)
        {
            _objCanvas = pCanvas;
            _grootte = 64;
        }

        //eigenschappen
        public int Xpos
        {
            get
            {
                return _x_pos;
            }
        }

        public int Ypos
        {
            get
            {
                return _y_pos;
            }
        }


        //methodes
        //Achtergrond van spel inladen
        public void Achtergrond()
        {
            objImage1.BeginInit();
            objImage1.UriSource = new Uri(@"Images/Background.jpg.png",
            UriKind.Relative);
            objImage1.EndInit();

            picture.Source = objImage1;
            picture.Margin = new Thickness(0, 0, 0, 0);
            picture.Width = 640;
            picture.Height = 640;
            _objCanvas.Children.Add(picture);
        }
    }
}
