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
        Plant _planten;

        //constructor
        public Zeis(Canvas pCanvas) : base(pCanvas) //Aangezien LevelElementen parameters heeft moeten we Edelsteen opnieuw vertellen om deze te gebruiken. Dit doen we door base te gebruiken. Zo niet, krijgen we error CS7036
        {
            _objCanvas = pCanvas;

            _planten = new Plant(_objCanvas);

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
        //Verwijderd zeis, zet deze in hand van speler
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

        //Zet zeis terug op begin positie
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

        //Zet waarde van een gegroeide plant op true indien positie gelijk
        public void Oogsten(int pXSpeler, int pYSpeler)
        {
            for (int i = 0; i < _planten.Ingeplant.Count; i++)
            {
                //De eigenschap ingeplant retourneert hier als waarde 0, ookal is er bij stap voor stap debuggen te zien
                //dat in plant.cs, de lijst wel 1 item krijgt.
                if (_planten.Ingeplant[i].Xpos == pXSpeler && _planten.Ingeplant[i].Ypos == pYSpeler)
                {
                    _planten.Ingeplant[i].InBezit = true;
                    PlantInMandje(pXSpeler, pYSpeler);
                }
            }
        }

        //Zet de plant die in bezit in, in de hand van de speler.
        public void PlantInMandje(int pXSpeler, int pYSpeler)
        {
            for (int i = 0; i < _planten.Ingeplant.Count; i++) //COUNT WORD 0
            {
                if (_planten.Ingeplant[i].InBezit)
                {
                    _objCanvas.Children.Remove(_planten.Ingeplant[i].Oogst);
                    _x_pos = pXSpeler + (64 - (_grootte / 3));
                    _y_pos = pYSpeler + (64 - (_grootte / 3));
                    _planten.Ingeplant[i].Oogst.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
                    _planten.Ingeplant[i].Oogst.Width = _grootte / 3;
                    _planten.Ingeplant[i].Oogst.Height = _grootte / 3;
                    _objCanvas.Children.Add(_planten.Ingeplant[i].Oogst);
                }
            }
        }
    }
}
