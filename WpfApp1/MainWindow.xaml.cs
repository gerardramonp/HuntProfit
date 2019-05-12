using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows.Media.Animation;

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


        private void TbDemonic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbDemonic.Text == "") { demonic = 0; }
            else { demonic = int.Parse(tbDemonic.Text); }
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

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            int posX = (int)e.GetPosition(windowHuntProfit).X;
            int posY = (int)e.GetPosition(windowHuntProfit).Y;
            if (posX >= 358 && posX <= 381 && posY >= 0 && posY <= 27)
            {
                this.Close();
            }
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void BtCalcular_Click(object sender, RoutedEventArgs e)
        {
            if (tbLoot.Text == "") { loot = 0; }
            else { loot = float.Parse(tbLoot.Text); }
            if (wasteEK != 0) { persones++; }
            if (wasteED != 0) { persones++; }
            if (wasteRP != 0) { persones++; }
            if (wasteMS != 0) { persones++; }

            CalcularValors(wasteEK, wasteED, wasteRP, wasteMS, totalWaste, loot, demonic, persones);

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

        // Comprova la versio del AssemblyInfo i la afegeix al titol de la finestra.
        private void AfegirVersio()
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
