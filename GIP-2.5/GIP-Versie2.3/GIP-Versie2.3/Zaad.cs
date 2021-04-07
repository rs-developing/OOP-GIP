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
    class Zaad : Gereedschap
    {
        //klassevariablen
        Image _zaadje = new Image();
        bool _inplanten = new bool();

        //constructor
        public Zaad(Canvas pCanvas) : base(pCanvas) //Aangezien LevelElementen parameters heeft moeten we Edelsteen opnieuw vertellen om deze te gebruiken. Dit doen we door base te gebruiken. Zo niet, krijgen we error CS7036
        {
            _objCanvas = pCanvas;

            
            _x_tegel = objRandom.Next(1, 9);
            _y_tegel = objRandom.Next(1, 9);

            _x_pos = _x_tegel * 64;
            _y_pos = _y_tegel * 64;

            BitmapImage objImage1 = new BitmapImage();
            objImage1.BeginInit();
            objImage1.UriSource = new Uri(@"Images/Seeds.png",
            UriKind.Relative);
            objImage1.EndInit();

            _zaadje.Source = objImage1;
            _zaadje.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
            _zaadje.Width = _grootte;
            _zaadje.Height = _grootte;

            _objCanvas.Children.Add(_zaadje);
        }

        //eigenschappen
        public Image Zaadje
        {
            get
            {
                return _zaadje;
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
        //Verwijderd zaadje, zet deze in hand van speler
        public void SpelerVolgen(int pXSpeler, int pYSpeler)
        {
                _objCanvas.Children.Remove(_zaadje);
                _x_pos = pXSpeler;
                _y_pos = pYSpeler;
                _zaadje.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
                _zaadje.Width = _grootte / 4;
                _zaadje.Height = _grootte / 4;
                _objCanvas.Children.Add(_zaadje);           
        }
    }
}
