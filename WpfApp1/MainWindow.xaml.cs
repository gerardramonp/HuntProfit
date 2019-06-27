using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Windows.Interop;
using System.Reflection;

namespace HuntProfit
{
    public partial class MainWindow : Window
    {
        // ########## VARIABLES GLOBALS ##########       
        int persones = 0, ID = 0;
        // float lootED = 0, lootRP = 0, lootMS = 0; Per quan balance positiu
        float wasteEK = 0, wasteED = 0, wasteRP = 0, wasteMS = 0, totalWaste = 0, loot = 0;
        float transferEK = 0, transferED = 0, transferRP = 0, transferMS = 0;
        float balance, profitEach;
        int hours = 0, min = 0, slidermin = 0;
        string respawn = "", pathHistorial = "", pathUpdates = "", time = "0:00";
        MetodesPath metodesPath = new MetodesPath();
        MetodesGenerals metodesGenerals = new MetodesGenerals();

        // #######################################

        public MainWindow()
        {
            InitializeComponent();
        }

        // S'executa quan es carrega la finestra principal
        private void HuntProfit_Loaded(object sender, RoutedEventArgs e)
        {
            lbTitol.Content += metodesGenerals.AfegirVersio();
            if (!File.Exists("config.txt"))
            {
                metodesPath.CrearConfig();
                metodesPath.EscriurePathHistorialAConfig();
            }
            metodesPath.GenerarPaths(out pathHistorial, out pathUpdates);
            if (!File.Exists(pathHistorial)) { metodesPath.CrearHistorial(pathHistorial); }
            metodesGenerals.CheckForUpdates(pathUpdates);
            metodesGenerals.ComprovarEspaisFinal(pathHistorial);
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

        // Eventos en general
        #region Generals

        // Per moure la finestra fent click on sigui
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void LbClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void BtCalcular_Click(object sender, RoutedEventArgs e)
        {
            bool saveHunt = true;
            if (time == "0:00")
            {
                saveHunt = CheckNoTime();
            }
            if (saveHunt)
            {
                CalcularPersones();
                CalcularValors();

                Hunt huntTemp = new Hunt(ID, respawn, DateTime.Now.ToString("dd/MM"), persones, wasteEK, wasteED, wasteRP, wasteMS, totalWaste, loot, balance, profitEach,
                    transferEK, transferED, transferRP, transferMS, "no", time);

                metodesGenerals.EscriureAHistorial(huntTemp, pathHistorial);
                ReiniciarValors();
            }
        }

        private void btHistorial_Click(object sender, RoutedEventArgs e)
        {
            finestraDataGrid formDG = new finestraDataGrid();

            DoubleAnimation animWidth = new DoubleAnimation(0, 1340, TimeSpan.FromSeconds(0.35));
            formDG.BeginAnimation(Window.WidthProperty, animWidth);

            formDG.Show();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            slidermin = (int)sliderTime.Value;
            hours = slidermin / 60;
            min = slidermin - (hours * 60);
            if (min > 9)
            {
                time = $"{hours}:{min}";
            }
            else
            {
                time = $"{hours}:0{min}";
            }
            lbTimeValue.Content = time;
        }

        private void Lb1h_MouseDown(object sender, MouseButtonEventArgs e)
        {
            sliderTime.Value = 60;
        }

        private void Lb2h_MouseDown(object sender, MouseButtonEventArgs e)
        {
            sliderTime.Value = 120;
        }

        private void BtReiniciar_Click(object sender, RoutedEventArgs e)
        {
            ReiniciarValorsFormulari();
        }

        private void LbFolderSettings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to modify the Historial.txt path?", "Confirm path change", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                metodesPath.EscriurePathHistorialAConfig();
            }
            e.Handled = true;
        }

        // Checks if the user want to save the hunt when the time is set to 0. Returns true if yes.
        private bool CheckNoTime()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("You have not introduced any hunt time.\n\nAre you sure you want to save it as 0:00?", "Confirm no Hunt Time", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }

        #endregion
        //###################

        // Metodes
        #region METODES

        private void ReiniciarValors()
        {
            persones = 0;
            loot = 0;
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
            tbTEK.Text = "";
            tbTED.Text = "";
            tbTRP.Text = "";
            tbTMS.Text = "";
            lbBalanceValue.Content = "0";
            lbProfitValue.Content = "0";
            sliderTime.Value = 0;
        }

        private void ActualitzarTotalWaste()
        {
            totalWaste = wasteEK + wasteED + wasteRP + wasteMS;
            tbWTotal.Text = totalWaste.ToString();
        }

        // Mira quantes persones hi ha a la hunt i les demonic.
        private void CalcularPersones()
        {
            if (tbLoot.Text == "") { loot = 0; }
            else { loot = float.Parse(tbLoot.Text); }
            if (wasteEK != 0) { persones++; }
            if (wasteED != 0) { persones++; }
            if (wasteRP != 0) { persones++; }
            if (wasteMS != 0) { persones++; }
        }

        // Calcula el waste, profit i transfers i els mostra per pantalla.
        private void CalcularValors()
        {
            respawn = cbRespawn.Text;
            if (respawn == "" || respawn == "Select a respawn...") { respawn = "NULL"; }
            balance = loot - totalWaste;
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

            lbBalanceValue.Content = balance.ToString();
            lbProfitValue.Content = profitEach.ToString();
            tbTEK.Text = transferEK.ToString();
            tbTED.Text = transferED.ToString();
            tbTRP.Text = transferRP.ToString();
            tbTMS.Text = transferMS.ToString();
        }

        // Perk al fer click al textbox es seleccioni el text
        private void SeleccionarContingutTb(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
            // SelectAddress
        }

        // Perk al fer click al textbox es seleccioni el text (complement)
        private void ForcarSeleccionarContingutTb(object sender, MouseButtonEventArgs e)
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

        // S'assegura que entres un numero al textbox, sinó no deixa escriure.
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Tanca la finestra que rebi
        private void CloseWindow(Window window)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            foreach (Window w in Application.Current.Windows)
            {
                if (w.GetType().Assembly == currentAssembly && w == window)
                {
                    w.Close();
                }
            }
        }

        #endregion
        //###################
    }
}
