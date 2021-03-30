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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Speler objPlayer;
        LevelElementen Level1;
        Plant objplanten;
        Gereedschap objTools;

        public MainWindow()
        {
            InitializeComponent();

            Level1 = new LevelElementen(objCanvas);
            objPlayer = new Speler(objCanvas);
            objTools = new Gereedschap(objCanvas);
            objplanten = new Plant(objCanvas);

            Level1.Achtergrond();
            objCanvas.Children.Add(objPlayer.Player);
            objTools.ToolsStart();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            objCanvas.Children.Remove(objPlayer.Player);

            switch (e.Key)
            {
                case Key.Left:
                    objPlayer.LeftArrowPressed();
                    break;
                case Key.Right:
                    objPlayer.RightArrowPressed();
                    break;
                case Key.Up:
                    objPlayer.UpArrowPressed();
                    break;
                case Key.Down:
                    objPlayer.DownArrowPressed();
                    break;
                case Key.Space:
                    objTools.Pakken(objPlayer.Xpos, objPlayer.Ypos);
                    break;
                case Key.X:
                    objTools.Gebruiken(objPlayer.Xpos, objPlayer.Ypos);
                    break;
            }

            objCanvas.Children.Add(objPlayer.Player);
            objTools.WatDragen(objPlayer.Xpos, objPlayer.Ypos);
            objplanten.PlantInMandje(objPlayer.Xpos, objPlayer.Ypos);
        }

    }
}
