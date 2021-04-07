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
    class Gieter : Gereedschap
    {
        //klassevariablen
        Image _gietertje = new Image();
        //constructor
        public Gieter(Canvas pCanvas) : base(pCanvas) //Aangezien LevelElementen parameters heeft moeten we Edelsteen opnieuw vertellen om deze te gebruiken. Dit doen we door base te gebruiken. Zo niet, krijgen we error CS7036
        {
            _objCanvas = pCanvas;

            _x_tegel = 9;
            _y_tegel = 2;

            _x_pos = _x_tegel * 64;
            _y_pos = _y_tegel * 64;

            BitmapImage objImage1 = new BitmapImage();
            objImage1.BeginInit();
            objImage1.UriSource = new Uri(@"Images/Gieter.png",
            UriKind.Relative);
            objImage1.EndInit();

            _gietertje.Source = objImage1;
            _gietertje.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
            _gietertje.Width = _grootte;
            _gietertje.Height = _grootte;

            _objCanvas.Children.Add(_gietertje);
        }

        //eigenschappen
        public Image Gietertje
        {
            get
            {
                return _gietertje;
            }
        }

        //methodes
        //Verwijder gieter, teken daarna opnieuw in de hand v/d speler
        public void SpelerVolgen(int pXSpeler, int pYSpeler)
        {
            _objCanvas.Children.Remove(_gietertje);
            _x_pos = pXSpeler;
            _y_pos = pYSpeler;
            _gietertje.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
            _gietertje.Width = _grootte / 3;
            _gietertje.Height = _grootte / 3;
            _objCanvas.Children.Add(_gietertje);
        }

        //Zet gieter terug op begin positie
        public void reset()
        {
            _x_tegel = 9;
            _y_tegel = 2;

            _x_pos = _x_tegel * 64;
            _y_pos = _y_tegel * 64;

            _objCanvas.Children.Remove(_gietertje);
            _gietertje.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
            _gietertje.Width = _grootte;
            _gietertje.Height = _grootte;
            _objCanvas.Children.Add(_gietertje);
        }
    }
}
