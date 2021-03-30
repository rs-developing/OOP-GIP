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
        protected Random objRandom = new Random();
        protected List<Plant> _ingeplant = new List<Plant>();
        protected DispatcherTimer objTimer1;
        protected DispatcherTimer objTimer2;

        bool _inBezit;
        Image groei = new Image();        
        Image _oogst = new Image();        

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

        public Image Oogst
        {
            get
            {
                return _oogst;
            }
        }
        //methodes

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

        public void GroeiFase2(object sender, EventArgs e)
        {
            _objCanvas.Children.Remove(groei);
            switch (objRandom.Next(1, 1))
            {
                case 1:
                    Potatoe objPatatje = new Potatoe(_objCanvas, _x_pos, _y_pos);
                    _ingeplant.Add(objPatatje);
                    break;
            }

            objTimer2.Stop();
        }

        public void Oogsten(int pSpelerX, int pSpelerY)
        {
            for(int i = 0; i < _ingeplant.Count; i++)
            {
                if(_ingeplant[i].Xpos == pSpelerX && _ingeplant[i].Ypos == pSpelerY)
                {
                    _ingeplant[i]._inBezit = true;
                }
            }
        }

        public void PlantInMandje(int pXSpeler, int pYSpeler)
        {
            for (int i = 0; i < _ingeplant.Count; i++) //COUNT WORD 0
            {
                if (_ingeplant[i]._inBezit)
                {
                    _objCanvas.Children.Remove(_ingeplant[i].Oogst);
                    _x_pos = pXSpeler + (64 - (_grootte / 3));
                    _y_pos = pYSpeler + (64 - (_grootte / 3));
                    _ingeplant[i].Oogst.Margin = new Thickness(_x_pos, _y_pos, 0, 0);
                    _ingeplant[i].Oogst.Width = _grootte / 3;
                    _ingeplant[i].Oogst.Height = _grootte / 3;
                    _objCanvas.Children.Add(_ingeplant[i].Oogst);
                }
            }
        }
    }
}
