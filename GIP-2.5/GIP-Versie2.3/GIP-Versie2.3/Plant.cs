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

namespace GIP_Versie2._3
{
    class Plant : LevelElementen
    {
        //klassevariablen
        protected DispatcherTimer objTimer1;
        protected DispatcherTimer objTimer2;

        List<Plant> _ingeplant = new List<Plant>();

        bool _inBezit;
        int _aantalGegroeid = 0;
        Image groei = new Image();        
        Image _oogst = new Image();
        Random objRandom = new Random();

        //constructor
        public Plant(Canvas pCanvas) : base(pCanvas)
        {
            _objCanvas = pCanvas;
            _grootte = 64;

            objTimer1 = new DispatcherTimer();
            objTimer1.Interval = TimeSpan.FromSeconds(5);
            objTimer1.Tick += GroeiFase1;

            objTimer2 = new DispatcherTimer();
            objTimer2.Interval = TimeSpan.FromSeconds(5);
            objTimer2.Tick += GroeiFase2;
        }

        //eigenschappen
        public bool InBezit
        {
            get
            {
                return _inBezit;
            }

            set
            {
                _inBezit = value;
            }
        }

        public int AantalGegroeid
        {
            get
            {
                return _aantalGegroeid;
            }

            set
            {
                value = _aantalGegroeid;
            }
        }

        public int Xzaadje
        {
            get
            {
                return _x_pos;
            }
        }

        public int Yzaadje
        {
            get
            {
                return _y_pos;
            }
        }

        public Image Oogst
        {
            get
            {
                return _oogst;
            }
        }

        public List<Plant> Ingeplant
        {
            get
            {
                return _ingeplant;
            }

            set
            {
                _ingeplant = value;
            }
        }

        //methodes

        //Word opgeroepen bij gebruik zaadje
        public void inPlanten(int pSpelerX, int pSpelerY)
        {
            _x_pos = pSpelerX;
            _y_pos = pSpelerY;

            groei.Source = new BitmapImage(new Uri("Images/Groei1.png", UriKind.Relative));
            groei.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
            groei.Width = _grootte;
            groei.Height = _grootte;
            _objCanvas.Children.Add(groei);

            objTimer1.Start();

        }

        //na 15 seconden is de groei al halfweg --> nieuwe afbeelding
        public void GroeiFase1(object sender, EventArgs e)
        {
            _objCanvas.Children.Remove(groei);
            groei.Source = new BitmapImage(new Uri("Images/Groei2.png", UriKind.Relative));
            groei.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
            groei.Width = _grootte;
            groei.Height = _grootte;
            _objCanvas.Children.Add(groei);

            objTimer1.Stop();

            objTimer2.Start();
        }

        //15sec later, groei is compleet
        public void GroeiFase2(object sender, EventArgs e)
        {
            _objCanvas.Children.Remove(groei);
            //aantalGegroeid optellen voor UpdateGroei() in Gereedschap.cs -- TIJDELIJKE OPLOSSING||WERKT NIET
            OogstToevoegen(_x_pos, _y_pos);
            objTimer2.Stop();
        }

        //toevoegen aan lijst --> loopt mis, lijst reset zichzelf (uitleg in zeis.cs.Oogsten() )
        public void OogstToevoegen(int ZaadjeX, int ZaadjeY)
        {
            //Willekeurige plant kiezen en toevoegen
            switch (objRandom.Next(1, 1))
            {
                case 1:
                    Potatoe objPatatje = new Potatoe(_objCanvas, ZaadjeX, ZaadjeY);
                    _ingeplant.Add(objPatatje);
                    break;
            }
        }
    }
}
