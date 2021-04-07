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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Speler objPlayer;
        LevelElementen Level1;
        Plant objplanten;
        Gereedschap objTools;
        Zeis _zeis;
        DispatcherTimer _gameTmr;

        public MainWindow()
        {
            InitializeComponent();

            Level1 = new LevelElementen(objCanvas);
            objPlayer = new Speler(objCanvas);
            objTools = new Gereedschap(objCanvas);
            objplanten = new Plant(objCanvas);
            _zeis = new Zeis(objCanvas);

            Level1.Achtergrond();
            objCanvas.Children.Add(objPlayer.Player);
            objTools.ToolsStart();

            _gameTmr = new DispatcherTimer();
            _gameTmr.Interval = TimeSpan.FromSeconds(5000);
            _gameTmr.Tick += GameStop;
            _gameTmr.Start();
        }

        //Opgeroepen als er een toets word ingedrukt
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            objCanvas.Children.Remove(objPlayer.Player);

            //Test welke toets
            switch (e.Key)
            {
                case Key.Left:
                    objPlayer.LeftArrowPressed();
                    objTools.WatDragen(objPlayer.Xpos, objPlayer.Ypos);
                    _zeis.PlantInMandje(objPlayer.Xpos, objPlayer.Ypos);
                    break;
                case Key.Right:
                    objPlayer.RightArrowPressed();
                    objTools.WatDragen(objPlayer.Xpos, objPlayer.Ypos);
                    _zeis.PlantInMandje(objPlayer.Xpos, objPlayer.Ypos);
                    break;
                case Key.Up:
                    objPlayer.UpArrowPressed();
                    objTools.WatDragen(objPlayer.Xpos, objPlayer.Ypos);
                    _zeis.PlantInMandje(objPlayer.Xpos, objPlayer.Ypos);
                    break;
                case Key.Down:
                    objPlayer.DownArrowPressed();
                    objTools.WatDragen(objPlayer.Xpos, objPlayer.Ypos);
                    _zeis.PlantInMandje(objPlayer.Xpos, objPlayer.Ypos);
                    break;
                case Key.Space:
                    objTools.Pakken(objPlayer.Xpos, objPlayer.Ypos);
                    objTools.WatDragen(objPlayer.Xpos, objPlayer.Ypos);
                    break;
                case Key.X:
                    objTools.Gebruiken(objPlayer.Xpos, objPlayer.Ypos);
                    _zeis.PlantInMandje(objPlayer.Xpos, objPlayer.Ypos);
                    break;
            }

            objCanvas.Children.Add(objPlayer.Player);
        }

        //Na 5min stopt het spel en gaan we naar het eind scherm
        public void GameStop(object sender, EventArgs e)
        {
            Window Eindscherm = new Window();
            Eindscherm.Show();
            this.Close();
        }

    }
}
