using Squirrel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HuntProfit
{
    public class MetodesGenerals
    {
        // Mira si hi ha updates i les fa automaticament quan es tanca el programa
        public async Task CheckForUpdates(string pathUpdates)
        {
            using (var manager = new UpdateManager(pathUpdates))
            {
                await manager.UpdateApp();
            }
        }

        // Posa la imatge del tb corresponent en blanc/blau
        public void CanviarImg(string pathImg, Image nomImg) // A GENERALS
        {
            Image imgTemp = new Image();
            BitmapImage bi3 = new BitmapImage(); 
            bi3.BeginInit();
            bi3.UriSource = new Uri(pathImg, UriKind.Relative);
            bi3.EndInit();
            imgTemp.Stretch = Stretch.Fill;
            nomImg.Source = bi3;
        }


    }
}
