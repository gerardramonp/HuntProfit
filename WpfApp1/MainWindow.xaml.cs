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
using HuntProfit;
using System.Windows.Threading;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        // ########## VARIABLES GLOBALS ##########
        int persones = 0, demonic = 0, ID = 0, posX, posY;
        float wasteEK = 0, wasteED = 0, wasteRP = 0, wasteMS = 0, totalWaste = 0, loot = 0;
        float transferEK = 0, transferED = 0, transferRP = 0, transferMS = 0;
        float lootFinal, balance, profitEach;
        string respawn = "", pathHistorial = "", pathUpdates = "";
        MetodesPath metodesPath = new MetodesPath();
        MetodesGenerals metodesGenerals = new MetodesGenerals();

        DispatcherTimer timer1 = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        // #######################################

        public MainWindow()
        {
            InitializeComponent();
            AfegirVersio();
        }

        // S'executa quan es carrega la finestra principal
        private void HuntProfit_Loaded(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("config.txt"))
            {
                metodesPath.CrearConfig();
                metodesPath.PathAConfig();
            }
            metodesPath.GenerarPaths(out pathHistorial, out pathUpdates);
            if (!File.Exists(pathHistorial)) { metodesPath.CrearHistorial(pathHistorial); }
            metodesGenerals.CheckForUpdates(pathUpdates);
        }

        // Eventos
        // Pels wastes, actualitzen waste total al canviar text dels textbox waste per poder calcular TWASTE en temps real
        #region Events TBWastes -> WasteTotal
        private void TbWEK_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbWEK.Text == "") { wasteEK = 0; }
            else { wasteEK = float.Parse(tbWEK.Text); }
            ActualitzarTotalWaste();
        }

        private void TbWED_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbWED.Text == "") { wasteED = 0; }
            else { wasteED = float.Parse(tbWED.Text); }
            ActualitzarTotalWaste();
        }

        private void TbWRP_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbWRP.Text == "") { wasteRP = 0; }
            else { wasteRP = float.Parse(tbWRP.Text); }
            ActualitzarTotalWaste();
        }

        private void TbWMS_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbWMS.Text == "") { wasteMS = 0; }
            else { wasteMS = float.Parse(tbWMS.Text); }
            ActualitzarTotalWaste();
        }

        private void TbDemonic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbDemonic.Text == "") { demonic = 0; }
            else { demonic = int.Parse(tbDemonic.Text); }
        }
        #endregion

        // Pel canvi dels iconos de Vocations
        #region CanviImg
        private void TbWEK_GotFocus(object sender, RoutedEventArgs e)
        {
            metodesGenerals.CanviarImg("\\Resources\\sb64.png", imgEK);
        }
        private void TbWEK_LostFocus(object sender, RoutedEventArgs e)
        {
            metodesGenerals.CanviarImg("\\Resources\\sw128.png", imgEK);
        }
        #endregion

        private void BtCalcular_Click(object sender, RoutedEventArgs e)
        {
            if (tbLoot.Text == "") { loot = 0; }
            else { loot = float.Parse(tbLoot.Text); }
            if (wasteEK != 0) { persones++; }
            if (wasteED != 0) { persones++; }
            if (wasteRP != 0) { persones++; }
            if (wasteMS != 0) { persones++; }

            CalcularValors(wasteEK, wasteED, wasteRP, wasteMS, totalWaste, loot, demonic, persones);
            escriureAHistorial();
            ReiniciarValors();
        }

        private void btHistorial_Click(object sender, RoutedEventArgs e)
        {
            finestraDataGrid formDG = new finestraDataGrid();
            formDG.ShowDialog();
        }

        private void BtReiniciar_Click(object sender, RoutedEventArgs e)
        {
            ReiniciarValorsFormulari();
        }

        private void HuntProfit_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            posX = (int)e.GetPosition(HuntProfit).X;
            posY = (int)e.GetPosition(HuntProfit).Y;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (posX >= 358 && posX <= 385.5 && posY >= 0 && posY <= 27)
            {
                this.Close();
            }
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        // Metodes
        #region METODES
        private void ReiniciarValors()
        {
            persones = 0;
            lootFinal = 0;
            balance = 0;
            transferEK = 0;
            transferED = 0;
            transferRP = 0;
            transferMS = 0;
        }

        private void ReiniciarValorsFormulari()
        {
            ReiniciarValors();
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
            lbLootFinalValue.Content = "0";
            lbBalanceValue.Content = "0";
            lbProfitValue.Content = "0";
        }

        // Calcula els valors i transfers i els mostra per pantalla.
        private void CalcularValors(float wEK, float wED, float wRP, float wMS, float tWaste, float loot, int demonic, int persones)
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

        private void ActualitzarTotalWaste()
        {
            totalWaste = wasteEK + wasteED + wasteRP + wasteMS;
            tbWTotal.Text = totalWaste.ToString();
        }

        private void escriureAHistorial()
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
                System.Windows.MessageBox.Show("El path està mal introduit, selecciona la carpeta on es troba l'arxiu <historial.txt>");
                metodesPath.PathAConfig();
                metodesPath.GenerarPaths(out pathHistorial, out pathUpdates);
            }
        }

        // Comprova la versio del AssemblyInfo i la afegeix al titol de la finestra.
        private void AfegirVersio() // A GENERALS
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            lbTitol.Content += $"  v.{ versionInfo.FileVersion }";
        }


        private void SelectAddress(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox tb = (sender as System.Windows.Controls.TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.TextBox tb = (sender as System.Windows.Controls.TextBox);
            if (tb != null)
            {
                if (!tb.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    tb.Focus();
                }
            }
        }
        #endregion
    }
}
