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
    class Potatoe : Plant
    {
        //klassevariablen
        bool _wordGedragen = false, _inDoos = false;
        Image _patatje = new Image();

        //constructor
        public Potatoe(Canvas pCanvas, int pXSpeler, int pYSpeler) : base(pCanvas) //Aangezien LevelElementen parameters heeft moeten we Edelsteen opnieuw vertellen om deze te gebruiken. Dit doen we door base te gebruiken. Zo niet, krijgen we error CS7036
        {
            _objCanvas = pCanvas;

            _x_pos = pXSpeler;
            _y_pos = pYSpeler;

            BitmapImage objImage1 = new BitmapImage();
            objImage1.BeginInit();
            objImage1.UriSource = new Uri(@"Images/Potatoes.png",
            UriKind.Relative);
            objImage1.EndInit();

            _patatje.Source = objImage1;
            _patatje.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
            _patatje.Width = _grootte;
            _patatje.Height = _grootte;
            _objCanvas.Children.Add(_patatje);
        }

        //eigenschappen
        public bool WordGedragen
        {
            get
            {
                return _wordGedragen;
            }

            set
            {
                _wordGedragen = value;
            }
        }

        public bool InDoos
        {
            get
            {
                return _inDoos;
            }

            set
            {
                _inDoos = value;
            }
        }

        //methodes


    }
}
