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
        DispatcherTimer _zaadjesTmr;
        DispatcherTimer _groeiTmr;
        int _xzaadje;
        int _yzaadje;

        //constructor
        public Gereedschap(Canvas pCanvas) : base(pCanvas)
        {
            _objCanvas = pCanvas;

            _zaadjes = new List<Zaad>();
            _planten = new Plant(_objCanvas);

            _zaadjesTmr = new DispatcherTimer();
            _zaadjesTmr.Interval = TimeSpan.FromSeconds(5);
            _zaadjesTmr.Tick += zaadspawn;

            _groeiTmr = new DispatcherTimer();
            _groeiTmr.Interval = TimeSpan.FromMilliseconds(500);
            _groeiTmr.Tick += UpdateGroei;
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
        //Iedere 10 seconden nieuw zaadje inspawnen.
        public void zaadspawn(object sender, EventArgs e)
        {
            objZaadje = new Zaad(_objCanvas);
            _zaadjes.Add(objZaadje);
        }

        //Iedere miliseconden testen of er een plant is gegroeid -- TIJDELIJKE OPLOSSING
        public void UpdateGroei(object sender, EventArgs e)
        {
            _xzaadje = _planten.Xpos;
            _yzaadje = _planten.Ypos;

            //Test of er een groei is
            if(_planten.AantalGegroeid >= 1)
            {
                //Zo ja, tel aantalgegroeid -1 -- WERKT NIET, 'AantalGegroeid' telt niet af!
                _planten.AantalGegroeid -= 1;
            }
        }

        //Opgeroepen bij druk op spatie
        public void Pakken(int pXSpeler, int pYSpeler)
        {
            //Als speler positie gelijk is aan postie van zeis/gieter of zaadje --> waarde in inventory
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

        //Opgeroepen na iedere beweging
        public void WatDragen(int pXSpeler, int pYSpeler)
        {
            //Wat in inventory zit, word visueel op de speler geplakt
            switch(_inventory)
            {
                case "zeis":
                    objZeisje.SpelerVolgen(pXSpeler, pYSpeler);
                    break;
                case "gieter":
                    objGieter.SpelerVolgen(pXSpeler, pYSpeler);
                    break;
                case "zaad":
                    //Testen welk zaadje moet volgen
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

        //Opgeroepen bij klik op 'x' 
        public void Gebruiken(int pXSpeler, int pYSpeler)
        {
            //Functies oproepen naarmate wat er in de inventory zit
            switch (_inventory)
            {
                case "zeis":
                    objZeisje.Oogsten(pXSpeler, pYSpeler);
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
                                _planten.inPlanten(pXSpeler, pYSpeler);
                                _inventory = "";
                                ZaadjesVerwijderen();
                            }
                        }
                    }
                    break;
            }
        }

        //Opgeroepen na gebruik zaadje
        public void ZaadjesVerwijderen()
        {
            //Verwijderd ieder zaadje in bezit
            for (var index = _zaadjes.Count - 1; index >= 0; index--)
            {
                if (_zaadjes[index].InBezit == true)
                {
                    _objCanvas.Children.Remove(_zaadjes[index].Zaadje);
                    _zaadjes.RemoveAt(index);
                }
            }
        }

        //Opgeroepen bij start van spel --> timers starten, objecten initiëren
        public void ToolsStart()
        {
            _zaadjesTmr.Start();

            objGieter = new Gieter(_objCanvas);
            objZeisje = new Zeis(_objCanvas);
        }

        //Opgeroepen bij oprapen zaadje, gieter/zeis worden op start positie gezet
        public void ResetPositie()
        {
            objGieter.reset();
            objZeisje.reset();
        }

        //Test functie om na te gaan of speler in een plantenbak staat
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
