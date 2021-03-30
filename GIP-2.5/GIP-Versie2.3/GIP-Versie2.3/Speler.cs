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
using System.Drawing;

namespace GIP_Versie2._3
{
    class Speler : LevelElementen
    {
        //ClassVariables
        Image player1 = new Image();
        BitmapImage objImage1 = new BitmapImage();
        string orientatie;

        bool _afgeven = false;

        //Constructor
        public Speler(Canvas pCanvas) : base(pCanvas)
        {
            _objCanvas = pCanvas;

            _x_tegel = 2;
            _y_tegel = 2;

            _x_pos = 64 * _x_tegel;
            _y_pos = 64 * _y_tegel;
            _grootte = 64;

            objImage1.BeginInit();
            objImage1.UriSource = new Uri(@"Images/Speler.png",
            UriKind.Relative);
            objImage1.EndInit();

            player1.Source = objImage1;
            player1.Width = _grootte;
            player1.Height = _grootte;
            player1.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
        }

        //Properties
        public Image Player
        {
            get
            {
                return player1;
            }
        }

        public bool DiamantAfgeven
        {
            get
            {
                return _afgeven;
            }

            set
            {
                value = _afgeven;
            }
        }

        public int X_pos
        {
            get
            {
                return _x_pos;
            }
        }
        public int Y_pos
        {
            get
            {
                return _y_pos;
            }
        }

        public string Orientatie
        {
            get
            {
                return orientatie;
            }
        }

        //Methods

        public void LeftArrowPressed()
        {
            if (_x_pos == 0)
            {
                _x_pos = 0;
            }

            else
            {
                _x_pos -= 64;
                _x_tegel--;
            }
            //objImage1.RotateFlip(RotateFlipType.Rotate270FlipNone);
            //objImage1.ro
            orientatie = "links";
            player1.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
        }

        public void RightArrowPressed()
        {
            if (_x_pos == 576)
            {
                _x_pos = 576;
            }

            else
            {
                _x_pos += 64;
                _x_tegel++;
            }

            objImage1.Rotation = Rotation.Rotate90;
            player1.Source = objImage1;
            orientatie = "rechts";
            player1.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
        }

        public void UpArrowPressed()
        {
            if (_y_pos == 0)
            {
                _y_pos = 0;
            }

            else
            {
                _y_pos -= 64;
                _y_tegel--;
            }

            objImage1.Rotation = Rotation.Rotate0;
            player1.Source = objImage1;
            orientatie = "omhoog";
            player1.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
        }

        public void DownArrowPressed()
        {
            if (_y_pos == 576)
            {
                _y_pos = 576;
            }

            else
            {
                _y_pos += 64;
                _y_tegel++;
            }

            objImage1.Rotation = Rotation.Rotate180;
            player1.Source = objImage1;
            orientatie = "onder";
            player1.Margin = new Thickness(_x_pos, _y_pos, 0, 0);            
        }

        public void Planten(bool pZaadVast, int pDoosX, int pDoosY)
        {
            if(pZaadVast == true)
            {
                _afgeven = true; //checken of de positie van de doos binnen 1 tegel van de speler ligt (speler positie update niet)
            }

            else
            {
                _afgeven = false;
            }
        }
    }
}
