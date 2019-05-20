using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Text.RegularExpressions;

namespace HuntProfit
{
    public partial class MainWindow : Window
    {
        // ########## VARIABLES GLOBALS ##########       
        int persones = 0, demonic = 0, ID = 0;
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
        }

        // S'executa quan es carrega la finestra principal
        private void HuntProfit_Loaded(object sender, RoutedEventArgs e)
        {
            AfegirVersio();
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

        #endregion
        //################################################################################################################

        // Pel canvi dels iconos de Vocations ################
        #region CanviImg
        // EK
        private void TbWEK_GotFocus(object sender, RoutedEventArgs e)
        {
            metodesGenerals.CanviarImg("\\Resources\\EK_b.png", imgEK);
        }
        private void TbWEK_LostFocus(object sender, RoutedEventArgs e)
        {
            metodesGenerals.CanviarImg("\\Resources\\EK_g.png", imgEK);
        }

        // ED
        private void TbWED_GotFocus(object sender, RoutedEventArgs e)
        {
            metodesGenerals.CanviarImg("\\Resources\\ED_b.png", imgED);
        }
        private void TbWED_LostFocus(object sender, RoutedEventArgs e)
        {
            metodesGenerals.CanviarImg("\\Resources\\ED_g.png", imgED);
        }

        // RP
        private void TbWRP_GotFocus(object sender, RoutedEventArgs e)
        {
            metodesGenerals.CanviarImg("\\Resources\\RP_b.png", imgRP);
        }
        private void TbWRP_LostFocus(object sender, RoutedEventArgs e)
        {
            metodesGenerals.CanviarImg("\\Resources\\RP_g.png", imgRP);
        }      

        // MS
        private void TbWMS_GotFocus(object sender, RoutedEventArgs e)
        {
            metodesGenerals.CanviarImg("\\Resources\\MS_b.png", imgMS);
        }
        private void TbWMS_LostFocus(object sender, RoutedEventArgs e)
        {
            metodesGenerals.CanviarImg("\\Resources\\MS_g.png", imgMS);
        }
        #endregion
        //####################################################

        private void LbClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        // Per moure la finestra fent click on sigui
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void BtCalcular_Click(object sender, RoutedEventArgs e)
        {
            CalcularPersonesDemonic();
            CalcularValors();

            Hunt huntTemp = new Hunt(ID, respawn, DateTime.Now.ToString("dd/MM"), persones, wasteEK, wasteED, wasteRP, wasteMS, totalWaste, loot, balance, profitEach,
                transferEK, transferED, transferRP, transferMS, "no");

            metodesGenerals.EscriureAHistorial(huntTemp, pathHistorial);
            ReiniciarValors();
        }

        private void btHistorial_Click(object sender, RoutedEventArgs e)
        {
            finestraDataGrid formDG = new finestraDataGrid();

            DoubleAnimation animWidth = new DoubleAnimation(0, 1340, TimeSpan.FromSeconds(0.55));   
            formDG.BeginAnimation(Window.WidthProperty, animWidth);

            formDG.ShowDialog();
        }

        private void BtReiniciar_Click(object sender, RoutedEventArgs e)
        {
            ReiniciarValorsFormulari();
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

        private void ActualitzarTotalWaste()
        {
            totalWaste = wasteEK + wasteED + wasteRP + wasteMS;
            tbWTotal.Text = totalWaste.ToString();
        }

        // Mira quantes persones hi ha a la hunt i les demonic.
        private void CalcularPersonesDemonic()
        {
            if (tbLoot.Text == "") { loot = 0; }
            else { loot = float.Parse(tbLoot.Text); }
            if (wasteEK != 0) { persones++; }
            if (wasteED != 0) { persones++; }
            if (wasteRP != 0) { persones++; }
            if (wasteMS != 0) { persones++; }

            if (tbDemonic.Text == "") { demonic = 0; }
            else { demonic = int.Parse(tbDemonic.Text); }
        }

        // Calcula el waste, profit i transfers i els mostra per pantalla.
        private void CalcularValors()
        {
            respawn = cbRespawn.Text;
            if (respawn == "" || respawn == "Select a respawn...") { respawn = "NULL"; }

            lootFinal = loot - demonic;
            balance = lootFinal - totalWaste;
            profitEach = balance / persones;

            // Si no participen a la hunt, no se li ha de transferir res
            if (wasteEK == 0) { transferEK = 0; }
            else { transferEK = wasteEK + profitEach; }
            if (wasteED == 0) { transferED = 0; }
            else { transferED = wasteED + profitEach; }
            if (wasteRP == 0) { transferRP = 0; }
            else { transferRP = wasteRP + profitEach; }
            if (wasteMS == 0) { transferMS = 0; }
            else { transferMS = wasteMS + profitEach; }

            lbLootFinalValue.Content = lootFinal.ToString();
            lbBalanceValue.Content = balance.ToString();
            lbProfitValue.Content = profitEach.ToString();
            tbTEK.Text = transferEK.ToString();
            tbTED.Text = transferED.ToString();
            tbTRP.Text = transferRP.ToString();
            tbTMS.Text = transferMS.ToString();
        }       

        // Comprova la versio del AssemblyInfo i la afegeix al titol de la finestra.
        private void AfegirVersio() // Mirar a general retornant un string
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            lbTitol.Content += $"  v.{ versionInfo.FileVersion }";
        }

        // Perk al fer click al textbox es seleccioni el text
        private void SelectAddress(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        // Perk al fer click al textbox es seleccioni el text (complement)
        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                if (!tb.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    tb.Focus();
                }
            }
        }

        // Per evitar que als textbox es posin lletres.
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion
    }
}
