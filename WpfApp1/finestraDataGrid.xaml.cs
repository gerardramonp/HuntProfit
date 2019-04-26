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
using System.Windows.Shapes;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para finestraDataGrid.xaml
    /// </summary>
    public partial class finestraDataGrid : Window
    {
        // ########## VARIABLES GLOBALS ##########
        string[] historialfull;
        int huntIDTXT = 0, personesTXT = 0, idDG = 0;
        string respawnTXT = "", pagatTXT = "", diaTXT = "", pathHistorial = "", path = "", newtext="";
        float wasteEKTXT = 0, wasteEDTXT = 0, wasteRPTXT = 0, wasteMSTXT = 0, totalWasteTXT = 0, lootTXT = 0, balanceTXT = 0, profitEachTXT = 0, transferEKTXT = 0,
            transferEDTXT = 0, transferRPTXT = 0, transferMSTXT = 0;
        // #######################################
        public finestraDataGrid()
        {
            InitializeComponent();
            try
            {
                StreamReader sr = new StreamReader("config.txt");
                path = sr.ReadLine();
                pathHistorial = path + "\\historial.txt";
                sr.Close();
                llegirTXT(pathHistorial);
            }
            catch
            {
                MessageBox.Show("La ruta de l'arxiu <config.txt> no és correcte. Consulte con uno de nuestros técnicos.\n O modifica-ho i posa la ruta de la carpeta on esta historial.txt," +
                    "serà algo aixi:\nC:\\user\\<nomusuari>\\OneDrive\\<carpeta del historial.txt>");
                return;
            }
        }

        public void buttonPagar_Click(object sender, RoutedEventArgs e)
        {
            Hunt DGRow = HistorialHunts.SelectedItem as Hunt;
            huntIDTXT = DGRow.HuntID;
            respawnTXT = DGRow.Respawn;
            personesTXT = DGRow.Persones;
            wasteEKTXT = DGRow.WasteEK;
            wasteEDTXT = DGRow.WasteED;
            wasteRPTXT = DGRow.WasteRP;
            wasteMSTXT = DGRow.WasteMS;
            totalWasteTXT = DGRow.TotalWaste;
            lootTXT = DGRow.Loot;
            balanceTXT = DGRow.Balance;
            profitEachTXT = DGRow.ProfitEach;
            transferEKTXT = DGRow.TransferEK;
            transferEDTXT = DGRow.TransferED;
            transferRPTXT = DGRow.TransferRP;
            transferMSTXT = DGRow.TransferMS;

            newtext = string.Format(">>HuntID: {0}|Respawn: {1}|Dia: {2}|Persones: {3}|WasteEK: {4}|WasteED: {5}|WasteRP: {6}|WasteMS: {7}|" +
                    "WasteTOTAL: {8}|Loot: {9}|Balance: {10}|Profit/Each: {11:F2}|TransferEK: {12:F2}|TransferED: {13:F2}|TransferRP: {14:F2}|TransferMS: {15:F2}|" +
                    "Pagat: si", huntIDTXT, respawnTXT, DateTime.Now.ToString("dd/MM"), personesTXT, wasteEKTXT, wasteEDTXT, wasteRPTXT, wasteMSTXT, totalWasteTXT, lootTXT, balanceTXT,
                    profitEachTXT, transferEKTXT, transferEDTXT, transferRPTXT, transferMSTXT);

            canviarPagat();
        }

        private void canviarPagat()
        {
            int linia = huntIDTXT;
            if (huntIDTXT != 0) { linia = huntIDTXT * 2; }
            historialfull = File.ReadAllLines(pathHistorial);
            historialfull[linia] = newtext;
            File.WriteAllLines(pathHistorial, historialfull);
            HistorialHunts.Items.Clear();
            llegirTXT(pathHistorial);
        }
        // METODES
        #region METODES HISTORIAL
        // Legeix "historial.txt" guarda els valors en un objecte d clase Hunt i posa info a la taula.
        public void llegirTXT(string _path)
        {
            StreamReader sr = new StreamReader(_path);
            string fr = "";
            int comptador = 0;
            string[] blocs;
            string temp;
            bool sino = false;
            int lines = File.ReadLines(_path).Count();
            try
            {
                for (int i = 0; i < lines; i++)
                {
                    fr = sr.ReadLine();
                    if (fr != "")
                    {
                        comptador = 0;
                        blocs = fr.Split('|');
                        foreach (var item in blocs)
                        {
                            temp = "";
                            for (int j = 0; j < item.Length; j++)
                            {
                                char lletra = item[j];
                                if (item[j] == ':')
                                {
                                    sino = true;
                                    j += 2;
                                }
                                if (sino)
                                {
                                    temp += item[j];
                                }
                            }
                            comptador++;
                            sino = false;
                            switch (comptador)
                            {
                                case 1:
                                    huntIDTXT = int.Parse(temp);
                                    break;
                                case 2:
                                    respawnTXT = temp;
                                    break;
                                case 3:
                                    diaTXT = temp;
                                    break;
                                case 4:
                                    personesTXT = int.Parse(temp);
                                    break;
                                case 5:
                                    wasteEKTXT = float.Parse(temp);
                                    break;
                                case 6:
                                    wasteEDTXT = float.Parse(temp);
                                    break;
                                case 7:
                                    wasteRPTXT = float.Parse(temp);
                                    break;
                                case 8:
                                    wasteMSTXT = float.Parse(temp);
                                    break;
                                case 9:
                                    totalWasteTXT = float.Parse(temp);
                                    break;
                                case 10:
                                    lootTXT = float.Parse(temp);
                                    break;
                                case 11:
                                    balanceTXT = float.Parse(temp);
                                    break;
                                case 12:
                                    profitEachTXT = float.Parse(temp);
                                    break;
                                case 13:
                                    transferEKTXT = float.Parse(temp);
                                    break;
                                case 14:
                                    transferEDTXT = float.Parse(temp);
                                    break;
                                case 15:
                                    transferRPTXT = float.Parse(temp);
                                    break;
                                case 16:
                                    transferMSTXT = float.Parse(temp);
                                    break;
                                case 17:
                                    pagatTXT = temp;
                                    break;
                            }
                        }
                        Hunt huntTemp = new Hunt(huntIDTXT, respawnTXT, diaTXT, personesTXT, wasteEKTXT, wasteEDTXT, wasteRPTXT, wasteMSTXT, totalWasteTXT, lootTXT, balanceTXT, profitEachTXT,
                            transferEKTXT, transferEDTXT, transferRPTXT, transferMSTXT, pagatTXT);
                        HistorialHunts.Items.Add(huntTemp); // Afegeix la info de huntTemp a la taula
                    }
                }
                sr.Close();
            }
            catch
            {
                MessageBox.Show("L'arxiu <historial.txt> està corromput i no es poden llegir les dades. Contacte con uno de nuestros técnicos.");
                return;
            }
        }
        #endregion
    }
}
