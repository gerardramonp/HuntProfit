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
using HuntProfit;
using System.ComponentModel;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para finestraDataGrid.xaml
    /// </summary>
    public partial class finestraDataGrid : Window
    {
        // ########## VARIABLES GLOBALS ##########
        string[] historialfull;
        string respawnTXT = "", pagatTXT = "", diaTXT = "", pathHistorial = "", pathUpdates = "";
        int huntIDTXT = 0, personesTXT = 0, posX = 0, posY = 0;
        float wasteEKTXT = 0, wasteEDTXT = 0, wasteRPTXT = 0, wasteMSTXT = 0, totalWasteTXT = 0, lootTXT = 0, balanceTXT = 0, profitEachTXT = 0, transferEKTXT = 0,
            transferEDTXT = 0, transferRPTXT = 0, transferMSTXT = 0;
        MetodesPath metodes = new MetodesPath();
        // #######################################
        public finestraDataGrid()
        {
            InitializeComponent();
            try
            {
                metodes.GenerarPaths(out pathHistorial, out pathUpdates);
                llegirTXT();
                SortDataGrid(HistorialHunts);
            }
            catch
            {
                System.Windows.MessageBox.Show("El path està mal introduit, selecciona la carpeta on es troba l'arxiu <historial.txt>. Després tanca itorna a obrir la finestra de l'historial.");
                metodes.PathAConfig();
            }
        }

        // Guarda la info de la fila que cliques per modificar el pagat si/no de l'arxiu historial.txt
        private void buttonPagar_Click(object sender, RoutedEventArgs e)
        {
            LlegirFilaDG();
            if (pagatTXT == "si") { pagatTXT = "no"; }
            else { pagatTXT = "si"; }
            string newtext = string.Format(">>HuntID: {0}|Respawn: {1}|Dia: {2}|Persones: {3}|WasteEK: {4}|WasteED: {5}|WasteRP: {6}|WasteMS: {7}|" +
                    "WasteTOTAL: {8}|Loot: {9}|Balance: {10}|Profit/Each: {11:F2}|TransferEK: {12:F2}|TransferED: {13:F2}|TransferRP: {14:F2}|TransferMS: {15:F2}|" +
                    "Pagat: {16}", huntIDTXT, respawnTXT, DateTime.Now.ToString("dd/MM"), personesTXT, wasteEKTXT, wasteEDTXT, wasteRPTXT, wasteMSTXT, totalWasteTXT, lootTXT, balanceTXT,
                    profitEachTXT, transferEKTXT, transferEDTXT, transferRPTXT, transferMSTXT, pagatTXT);
            canviarPagat(newtext);
        }

        // METODES
        #region METODES HISTORIAL

        // Llegeix el historial i posa els valors a la taula
        private void llegirTXT()
        {
            StreamReader sr = new StreamReader(pathHistorial);
            string[] blocs;
            int lines = File.ReadLines(pathHistorial).Count();
            for (int i = 0; i < lines; i++)
            {
                string huntactu = sr.ReadLine();
                if (huntactu != "")
                {
                    blocs = huntactu.Split('|');
                    GenerarValors(blocs);
                    GenerarHuntiFila();
                }
            }
            sr.Close();
        }
     
        private void GenerarValors(string[] valors)
        {
            int comptador = 0;
            bool trobat = false;
            foreach (var item in valors)
            {
                StringBuilder sbTemp = new StringBuilder();
                for (int j = 0; j < item.Length; j++)
                {
                    char lletra = item[j];
                    if (item[j] == ':')
                    {
                        trobat = true;
                        j += 2;
                    }
                    if (trobat)
                    {
                        sbTemp.Append(item[j]);
                    }
                }
                string temp = "";
                temp = sbTemp.ToString();
                switch (comptador)
                {
                    case 0:
                        huntIDTXT = int.Parse(temp);
                        break;
                    case 1:
                        respawnTXT = temp;
                        break;
                    case 2:
                        diaTXT = temp;
                        break;
                    case 3:
                        personesTXT = int.Parse(temp);
                        break;
                    case 4:
                        wasteEKTXT = float.Parse(temp);
                        break;
                    case 5:
                        wasteEDTXT = float.Parse(temp);
                        break;
                    case 6:
                        wasteRPTXT = float.Parse(temp);
                        break;
                    case 7:
                        wasteMSTXT = float.Parse(temp);
                        break;
                    case 8:
                        totalWasteTXT = float.Parse(temp);
                        break;
                    case 9:
                        lootTXT = float.Parse(temp);
                        break;
                    case 10:
                        balanceTXT = float.Parse(temp);
                        break;
                    case 11:
                        profitEachTXT = float.Parse(temp);
                        break;
                    case 12:
                        transferEKTXT = float.Parse(temp);
                        break;
                    case 13:
                        transferEDTXT = float.Parse(temp);
                        break;
                    case 14:
                        transferRPTXT = float.Parse(temp);
                        break;
                    case 15:
                        transferMSTXT = float.Parse(temp);
                        break;
                    case 16:
                        pagatTXT = temp;
                        break;
                }
                trobat = false;
                comptador++;
            }
        }

        private void GenerarHuntiFila()
        {
            Hunt huntTemp = new Hunt(huntIDTXT, respawnTXT, diaTXT, personesTXT, wasteEKTXT, wasteEDTXT, wasteRPTXT, wasteMSTXT, totalWasteTXT, lootTXT, balanceTXT, profitEachTXT, transferEKTXT, transferEDTXT, transferRPTXT, transferMSTXT, pagatTXT);
            HistorialHunts.Items.Add(huntTemp); // Afegeix la info de huntTemp a la taula
        }

        private void canviarPagat(string text) // Canvia el si per no i no per si (Datagrid PAID)
        {
            int linia = huntIDTXT;
            if (huntIDTXT != 0) { linia = huntIDTXT * 2; } // Perque hi ha espais en blanc pel mig.
            historialfull = File.ReadAllLines(pathHistorial);
            historialfull[linia] = text; // Canvia la linia antiga per la nova.
            File.WriteAllLines(pathHistorial, historialfull);
            HistorialHunts.Items.Clear(); // Neteja datagrid
            llegirTXT(); // Torna a omplir la datagrid
            SortDataGrid(HistorialHunts);
        }

        private void LlegirFilaDG()
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
            pagatTXT = DGRow.Pagat;
        }

        void SortDataGrid(DataGrid dataGrid, int columnIndex = 0, ListSortDirection sortDirection = ListSortDirection.Descending)
        {
            var column = dataGrid.Columns[columnIndex];
            dataGrid.Items.SortDescriptions.Clear();
            dataGrid.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, sortDirection));
            foreach (var col in dataGrid.Columns)
            {
                col.SortDirection = null;
            }
            column.SortDirection = sortDirection;
            dataGrid.Items.Refresh();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            posX = (int)e.GetPosition(gridHistorial).X;
            posY = (int)e.GetPosition(gridHistorial).Y;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (posX >= 1206 && posX <= 1226 && posY >= 6 && posY <= 25)
            {
                this.Close();
            }
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
    #endregion  
}

