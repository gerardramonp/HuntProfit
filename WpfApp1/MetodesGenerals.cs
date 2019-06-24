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

        // Retorna la versio de la aplicació
        public string AfegirVersio()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return $" v.{versionInfo.FileVersion}";
        }

        // Posa la imatge del tb corresponent en blanc/blau
        public void CanviarImg(string pathImg, Image imgACanviar) // A GENERALS
        {
            Image imgTemp = new Image();
            BitmapImage novaImatge = new BitmapImage();
            novaImatge.BeginInit();
            novaImatge.UriSource = new Uri(pathImg, UriKind.Relative);
            novaImatge.EndInit();
            imgTemp.Stretch = Stretch.Fill;
            imgACanviar.Source = novaImatge;
        }

        // Comprova si falten els espais al final del Historial i els posa si fan falta.
        public void ComprovarEspaisFinal(string pathHistorial)
        {
            try
            {             
                string ultimaLinia = File.ReadLines(pathHistorial).Last();
                StreamWriter sw = File.AppendText(pathHistorial);
                if (ultimaLinia != "")
                {
                    sw.WriteLine("\n");
                }
                sw.Close();
            }
            catch
            {
                System.Windows.MessageBox.Show("No s'ha pogut escriure la hunt al txt. Torna a introduir la carpeta on es troba l'arxiu historial.txt.");
                return;
            }
        }

        // Escriu les dades de la hunt al historial
        public void EscriureAHistorial(Hunt huntTemp, string pathHistorial)
        {
            try
            {
                huntTemp.HuntID = File.ReadLines(pathHistorial).Count();
                if (huntTemp.HuntID != 0) { huntTemp.HuntID /= 2; } // /2 per ignorar els espais en blanc. Conta quantes hunts hi ha per saber quina es la ID de la hunt temp.
                StreamWriter sw = File.AppendText(pathHistorial);
                sw.WriteLine(">>HuntID: {0}|Respawn: {1}|Dia: {2}|Persones: {3}|WasteEK: {4}|WasteED: {5}|WasteRP: {6}|WasteMS: {7}|" +
                    "WasteTOTAL: {8}|Loot: {9}|Balance: {10}|Profit/Each: {11:F2}|TransferEK: {12:F2}|TransferED: {13:F2}|TransferRP: {14:F2}|TransferMS: {15:F2}|" +
                    "Pagat: no|Time: {16}\n", huntTemp.HuntID, huntTemp.Respawn, huntTemp.Dia, huntTemp.Persones, huntTemp.WasteEK, huntTemp.WasteED, huntTemp.WasteRP, huntTemp.WasteMS,
                    huntTemp.TotalWaste, huntTemp.Loot, huntTemp.Balance, huntTemp.ProfitEach, huntTemp.TransferEK, huntTemp.TransferED, huntTemp.TransferRP, huntTemp.TransferMS, huntTemp.Time);
                sw.Close();
            }
            catch
            {
                System.Windows.MessageBox.Show("No s'ha pogut escriure la hunt al txt. Torna a introduir la carpeta on es troba el historial.txt.");
                return;
            }
        }
    }
}
