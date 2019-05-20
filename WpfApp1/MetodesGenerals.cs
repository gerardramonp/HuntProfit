using Squirrel;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
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

        // Escriu les dades de la hunt al historial
        public void EscriureAHistorial(Hunt huntTemp, string pathHistorial)
        {
            try
            {
                huntTemp.HuntID = File.ReadLines(pathHistorial).Count();
                if (huntTemp.HuntID != 0) { huntTemp.HuntID /= 2; } // /2 per ignorar els espais en blanc. Conta quantes hunts hi ha per saber quina es la ID de la hunt temp.
                StreamWriter wfile = File.AppendText(pathHistorial);
                wfile.WriteLine(">>HuntID: {0}|Respawn: {1}|Dia: {2}|Persones: {3}|WasteEK: {4}|WasteED: {5}|WasteRP: {6}|WasteMS: {7}|" +
                    "WasteTOTAL: {8}|Loot: {9}|Balance: {10}|Profit/Each: {11:F2}|TransferEK: {12:F2}|TransferED: {13:F2}|TransferRP: {14:F2}|TransferMS: {15:F2}|" +
                    "Pagat: no\n", huntTemp.HuntID, huntTemp.Respawn, huntTemp.Dia, huntTemp.Persones, huntTemp.WasteEK, huntTemp.WasteED, huntTemp.WasteRP, huntTemp.WasteMS,
                    huntTemp.TotalWaste, huntTemp.Loot, huntTemp.Balance, huntTemp.ProfitEach, huntTemp.TransferEK, huntTemp.TransferED, huntTemp.TransferRP, huntTemp.TransferMS);
                wfile.Close();
            }
            catch
            {
                System.Windows.MessageBox.Show("No s'ha pogut escriure la hunt al txt. Torna a introduir la carpeta on es troba el historial.txt.");
                return;
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

        public string AfegirVersio()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return $" v.{versionInfo.FileVersion}";
        }

    }
}
