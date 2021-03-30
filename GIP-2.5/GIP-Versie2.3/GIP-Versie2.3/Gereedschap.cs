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
    class Gereedschap : LevelElementen
    {
        //klassevariablen
        protected bool _inBezit;
        protected string _inventory;
        int[,] _plantenbakken = { {1, 0}, {2, 0}, {4, 0}, {5, 0}, {0, 1},
            {0, 2}, {0, 4}, {0, 5}, {0, 7}, {0, 8},
            { 1, 9 }, { 2, 9 }, { 4, 9 }, { 5, 9 }, { 7, 9 },
            { 8, 9 }, { 9, 4 }, { 9, 5 }, { 9, 7 }, { 9, 8 } };

        List<Zaad> _zaadjes;
        Zaad objZaadje;
        Plant _planten;
        Gieter objGieter;
        Zeis objZeisje;

        protected Random objRandom = new Random();
        DispatcherTimer _objTimer;

        //constructor
        public Gereedschap(Canvas pCanvas) : base(pCanvas)
        {
            _objCanvas = pCanvas;

            _zaadjes = new List<Zaad>();

            _objTimer = new DispatcherTimer();
            _objTimer.Interval = TimeSpan.FromSeconds(5);
            _objTimer.Tick += zaadspawn;
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

        public string Inventory
        {
            get
            {
                return _inventory;
            }
        }
        //methodes
        public void zaadspawn(object sender, EventArgs e)
        {
            objZaadje = new Zaad(_objCanvas);
            _zaadjes.Add(objZaadje);
        }

        public void Pakken(int pXSpeler, int pYSpeler)
        {
            if (pXSpeler == 448 && pYSpeler == 0)
            {
                _inventory = "zeis";
            }
            if (pXSpeler == 576 && pYSpeler == 128)
            {
                _inventory = "gieter";
            }

            for (int index = 0; index < _zaadjes.Count; index++)
            {
                if (pXSpeler == _zaadjes[index].Xpos && pYSpeler == _zaadjes[index].Ypos)
                {
                    _inventory = "zaad";
                    _zaadjes[index].InBezit = true;
                }
            }
        }

        public void WatDragen(int pXSpeler, int pYSpeler)
        {
            switch(_inventory)
            {
                case "zeis":
                    objZeisje.SpelerVolgen(pXSpeler, pYSpeler);
                    break;
                case "gieter":
                    objGieter.SpelerVolgen(pXSpeler, pYSpeler);
                    break;
                case "zaad":
                    for (int index = 0; index < _zaadjes.Count; index++)
                    {
                        if (_zaadjes[index].InBezit)
                        {
                            _zaadjes[index].SpelerVolgen(pXSpeler, pYSpeler);
                        }
                    }
                    ResetPositie();
                    break;
                default:
                    break;
            }
        }


        public void Gebruiken(int pXSpeler, int pYSpeler)
        {
            switch (_inventory)
            {
                case "zeis":
                    _planten.Oogsten(pXSpeler, pYSpeler);
                        break;
                case "gieter":
                    break;
                case "zaad":
                    for(int index = 0; index < _zaadjes.Count; index++)
                    {
                        if (BakkenChecken(pXSpeler, pYSpeler))
                        {
                            if (_zaadjes[index].InBezit)
                            {
                                _planten = new Plant(_objCanvas);
                                _planten.inPlanten(pXSpeler, pYSpeler);
                                _inventory = "";
                                ZaadjesVerwijderen();
                            }
                        }
                    }
                    break;
            }
        }

        public void ZaadjesVerwijderen()
        {
            for (var index = _zaadjes.Count - 1; index >= 0; index--)
            {

                if (_zaadjes[index].InBezit == true)
                {
                    _objCanvas.Children.Remove(_zaadjes[index].Zaadje);
                    _zaadjes.RemoveAt(index);
                }
            }
        }

        public void ToolsStart()
        {
            _objTimer.Start();

            objGieter = new Gieter(_objCanvas);
            objZeisje = new Zeis(_objCanvas);
        }

        public void ResetPositie()
        {
            objGieter.reset();
            objZeisje.reset();
        }

        public bool BakkenChecken(int pXSpeler, int pYSpeler)
        {
            bool _bakkenChecken = false;

            for (int x = 0; x < _plantenbakken.GetLength(0); x++)
            {
                    if((_plantenbakken[x, 0] * 64) == pXSpeler && (_plantenbakken[x, 1] * 64) == pYSpeler)
                    {
                      _bakkenChecken = true;
                    }                
            }

            return _bakkenChecken;
        }
    }
}
