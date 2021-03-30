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
    class Zeis : Gereedschap
    {
        //klassevariablen
        Image _zeisje = new Image();
        bool _inplanten = new bool();

        //constructor
        public Zeis(Canvas pCanvas) : base(pCanvas) //Aangezien LevelElementen parameters heeft moeten we Edelsteen opnieuw vertellen om deze te gebruiken. Dit doen we door base te gebruiken. Zo niet, krijgen we error CS7036
        {
            _objCanvas = pCanvas;

            _x_tegel = 7;
            _y_tegel = 0;

            _x_pos = _x_tegel * 64;
            _y_pos = _y_tegel * 64;

            BitmapImage objImage1 = new BitmapImage();
            objImage1.BeginInit();
            objImage1.UriSource = new Uri(@"Images/Zeis.png",
            UriKind.Relative);
            objImage1.EndInit();

            _zeisje.Source = objImage1;
            _zeisje.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
            _zeisje.Width = _grootte;
            _zeisje.Height = _grootte;

            _objCanvas.Children.Add(_zeisje);
        }

        //eigenschappen
        public Image Gietertje
        {
            get
            {
                return _zeisje;
            }
        }

        public bool Inplanten
        {
            get
            {
                return _inplanten;
            }

            set
            {
                _inplanten = value;
            }
        }

        //methodes
        public void SpelerVolgen(int pXSpeler, int pYSpeler)
        {
            _objCanvas.Children.Remove(_zeisje);
            _x_pos = pXSpeler;
            _y_pos = pYSpeler;
            _zeisje.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
            _zeisje.Width = _grootte / 3;
            _zeisje.Height = _grootte / 3;
            _objCanvas.Children.Add(_zeisje);
        }

        public void reset()
        {
            _x_tegel = 7;
            _y_tegel = 0;

            _x_pos = _x_tegel * 64;
            _y_pos = _y_tegel * 64;

            _objCanvas.Children.Remove(_zeisje);
            _zeisje.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
            _zeisje.Width = _grootte;
            _zeisje.Height = _grootte;
            _objCanvas.Children.Add(_zeisje);
        }
    }
}
