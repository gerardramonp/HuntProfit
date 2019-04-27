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
using System.IO;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Diagnostics;
using Squirrel;



namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        // ########## VARIABLES GLOBALS ##########
        int persones = 0, demonic = 0, ID = 0;
        float wasteEK = 0, wasteED = 0, wasteRP = 0, wasteMS = 0, totalWaste = 0, loot = 0;
        float transferEK = 0, transferED = 0, transferRP = 0, transferMS = 0;
        float lootFinal, balance, profitEach;
        string respawn = "", path = "", pathHistorial = "", pathUpdates = "";
        
        // #######################################

        public MainWindow()
        {
            InitializeComponent();
            AfegirVersio();
        }

        // S'executa quan es carrega la finestra principal
        private void HuntProfit_Loaded(object sender, RoutedEventArgs e)
        {


            //if (!File.Exists("config.txt")) // Si no existeix config, el crea i demana path del historial
            //{
            //    FileStream config = File.Create("config.txt");
            //    config.Close();
            //    pathAConfig();
            //}
            //StreamReader sr = new StreamReader("config.txt");
            //path = sr.ReadLine();
            //sr.Close();
            //if (path == "" || path == null) { pathAConfig(); } // Canviar per while
            //pathHistorial = $"{ path }\\historial.txt";
            //pathUpdates = $"{ path }\\AutoUpdate";
            //CheckForUpdates();
            //try
            //{
            //    if (!File.Exists(pathHistorial)) { FileStream historial = File.Create(pathHistorial); } // Si no existeix historial, el crea al path k li hem dit
            //}
            //catch
            //{
            //    System.Windows.Forms.MessageBox.Show("La ruta de l'arxiu <config.txt> no és correcte. Consulte con uno de nuestros técnicos.\n O modifica-ho i posa la ruta de la carpeta on esta historial.txt," +
            //        "serà algo aixi:\nC:\\user\\<nomusuari>\\OneDrive\\<carpeta del historial.txt>");
            //    return;
            //}
        }

        // Eventos
        // Pels wastes, actualitzen waste total al canviar text dels textbox waste per poder calcular TWASTE en temps real
        #region Events TBWastes -> WasteTotal
        public void TbWEK_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbWEK.Text == "") { wasteEK = 0; }
            else { wasteEK = float.Parse(tbWEK.Text); }
            actualitzarTotalWaste();
        }

        private void TbWED_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbWED.Text == "") { wasteED = 0; }
            else { wasteED = float.Parse(tbWED.Text); }
            actualitzarTotalWaste();
        }

        private void TbWRP_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbWRP.Text == "") { wasteRP = 0; }
            else { wasteRP = float.Parse(tbWRP.Text); }
            actualitzarTotalWaste();
        }

        private void TbWMS_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbWMS.Text == "") { wasteMS = 0; }
            else { wasteMS = float.Parse(tbWMS.Text); }
            actualitzarTotalWaste();
        }
        #endregion

        private void TbDemonic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbDemonic.Text == "") { demonic = 0; }
            else { demonic = int.Parse(tbDemonic.Text); }
        }

        private void BtCalcular_Click(object sender, RoutedEventArgs e)
        {
            if (tbLoot.Text == "") { loot = 0; }
            else { loot = float.Parse(tbLoot.Text); }
            if (wasteEK != 0) { persones++; }
            if (wasteED != 0) { persones++; }
            if (wasteRP != 0) { persones++; }
            if (wasteMS != 0) { persones++; }

            calcularValors(wasteEK, wasteED, wasteRP, wasteMS, totalWaste, loot, demonic, persones);
            escriureAHistorial();
            reiniciarValors();
        }

        private void BtReiniciar_Click(object sender, RoutedEventArgs e)
        {
            reiniciarValors_Formulari();
        }

        private void btHistorial_Click(object sender, RoutedEventArgs e)
        {
            // Obre la finestra del historial
            finestraDataGrid finestra = new finestraDataGrid();
            finestra.ShowDialog();
        }

        // Metodes
        #region METODES
        public void reiniciarValors()
        {
            persones = 0;
            lootFinal = 0;
            balance = 0;
            transferEK = 0;
            transferED = 0;
            transferRP = 0;
            transferMS = 0;
        }

        public void reiniciarValors_Formulari()
        {
            reiniciarValors();
            tbWEK.Text = "";
            tbWED.Text = "";
            tbWRP.Text = "";
            tbWMS.Text = "";
            tbLoot.Text = "";
            tbDemonic.Text = "";
            tbTEK.Text = "";
            tbTED.Text = "";
            tbTRP.Text = "";
            tbTMS.Text = "";
            lbLootFinalValue.Content = "";
            lbBalanceValue.Content = "";
            lbProfitValue.Content = "";
        }

        // Calcula els valors i transfers i els mostra per pantalla.
        public void calcularValors(float wEK, float wED, float wRP, float wMS, float tWaste, float loot, int demonic, int persones)
        {
            respawn = cbRespawn.Text;
            if (respawn == "" || respawn == "Select a respawn...") { respawn = "NULL"; }

            lootFinal = loot - demonic;
            balance = lootFinal - tWaste;
            profitEach = balance / persones;

            // Si no participen a la hunt, no se li ha de transferir res
            if (wEK == 0) { transferEK = 0; }
            else { transferEK = wEK + profitEach; }
            if (wED == 0) { transferED = 0; }
            else { transferED = wED + profitEach; }
            if (wRP == 0) { transferRP = 0; }
            else { transferRP = wRP + profitEach; }
            if (wMS == 0) { transferMS = 0; }
            else { transferMS = wMS + profitEach; }

            lbLootFinalValue.Content = lootFinal.ToString();
            lbBalanceValue.Content = balance.ToString();
            lbProfitValue.Content = profitEach.ToString();
            tbTEK.Text = transferEK.ToString();
            tbTED.Text = transferED.ToString();
            tbTRP.Text = transferRP.ToString();
            tbTMS.Text = transferMS.ToString();          
        }

        // Suma els wastes per calcular waste total (als eventos de canviar text x calcular en temps real)
        public void actualitzarTotalWaste()
        {
            totalWaste = wasteEK + wasteED + wasteRP + wasteMS;
            tbWTotal.Text = totalWaste.ToString();
        }

        public void pathAConfig()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            path = fbd.SelectedPath;
            pathHistorial = $"{ path }\\historial.txt";
            pathUpdates = $"{ path }\\AutoUpdate";
            StreamWriter sw = new StreamWriter("config.txt");
            sw.Write(path);
            sw.Close();
        }

        public void escriureAHistorial()
        {
            try
            {
                ID = File.ReadLines(pathHistorial).Count();
                if (ID != 0) { ID /= 2; } // /2 per ignorar els espais en blanc.
                StreamWriter wfile = File.AppendText(pathHistorial);
                wfile.WriteLine(">>HuntID: {0}|Respawn: {1}|Dia: {2}|Persones: {3}|WasteEK: {4}|WasteED: {5}|WasteRP: {6}|WasteMS: {7}|" +
                    "WasteTOTAL: {8}|Loot: {9}|Balance: {10}|Profit/Each: {11:F2}|TransferEK: {12:F2}|TransferED: {13:F2}|TransferRP: {14:F2}|TransferMS: {15:F2}|" +
                    "Pagat: no\n", ID, respawn, DateTime.Now.ToString("dd/MM"), persones, wasteEK, wasteED, wasteRP, wasteMS, totalWaste, lootFinal, balance, profitEach, transferEK,
                    transferED, transferRP, transferMS);
                wfile.Close();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("La ruta de l'arxiu <config.txt> no és correcte. Consulte con uno de nuestros técnicos.\n O modifica-ho i posa la ruta de la carpeta on esta historial.txt," +
                    "serà algo aixi:\nC:\\user\\<nomusuari>\\OneDrive\\<carpeta del historial.txt>");
                return;
            }
        }

        // Mira si hi ha updates i les fa automaticament quan es tanca el programa
        private async Task CheckForUpdates()
        {
            using (var manager = new UpdateManager(pathUpdates))
            {
                await manager.UpdateApp();
            }
        }

        // Comprova la versio del AssemblyInfo i la afegeix al titol de la finestra.
        private void AfegirVersio()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            HuntProfit.Title += $"  v.{ versionInfo.FileVersion }";
        }
        #endregion
    }
} 
