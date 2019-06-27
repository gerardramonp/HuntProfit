using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.ComponentModel;

namespace HuntProfit
{
    /// <summary>
    /// Lógica de interacción para finestraDataGrid.xaml
    /// </summary>
    public partial class finestraDataGrid : Window
    {
        // ########## VARIABLES GLOBALS ##########
        string[] historialfull;
        string respawnTXT = "", pagatTXT = "", diaTXT = "", timeTXT="", pathHistorial = "", pathUpdates = "";
        int huntIDTXT = 0, personesTXT = 0;
        float wasteEKTXT = 0, wasteEDTXT = 0, wasteRPTXT = 0, wasteMSTXT = 0, totalWasteTXT = 0, lootTXT = 0, balanceTXT = 0, profitEachTXT = 0, transferEKTXT = 0,
            transferEDTXT = 0, transferRPTXT = 0, transferMSTXT = 0, faltaPagarED = 0;

        MetodesGenerals metodesGenerals = new MetodesGenerals();
        MetodesPath metodesPath = new MetodesPath();
        // #######################################
        public finestraDataGrid()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                metodesPath.GenerarPaths(out pathHistorial, out pathUpdates);
                LlegirTXT();
                SortDataGrid(HistorialHunts);
                CalcularQuantAPagarED();
            }
            catch
            {
                this.Close();
                MessageBox.Show("El path del Historial.txt està mal introduit. Introdueix-lo i torna-ho a provar.");
                metodesPath.EscriurePathHistorialAConfig();
            }
        }

        private void LbClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        // Guarda la info de la fila que cliques per modificar el pagat si/no de l'arxiu historial.txt
        private void buttonPagar_Click(object sender, RoutedEventArgs e)
        {
            LlegirFilaDG();
            if (pagatTXT == "si") { pagatTXT = "no"; }
            else { pagatTXT = "si"; }
            string newtext = string.Format(">>HuntID: {0}|Respawn: {1}|Dia: {2}|Persones: {3}|WasteEK: {4}|WasteED: {5}|WasteRP: {6}|WasteMS: {7}|" +
                    "WasteTOTAL: {8}|Loot: {9}|Balance: {10}|Profit/Each: {11:F2}|TransferEK: {12:F2}|TransferED: {13:F2}|TransferRP: {14:F2}|TransferMS: {15:F2}|" +
                    "Pagat: {16}|Time: {17}", huntIDTXT, respawnTXT, diaTXT, personesTXT, wasteEKTXT, wasteEDTXT, wasteRPTXT, wasteMSTXT, totalWasteTXT, lootTXT, balanceTXT,
                    profitEachTXT, transferEKTXT, transferEDTXT, transferRPTXT, transferMSTXT, pagatTXT, timeTXT);
            CanviarPagat(newtext);
            CalcularQuantAPagarED();
        }

        // METODES
        #region METODES HISTORIAL

        // Llegeix el historial i posa els valors a la taula
        private void LlegirTXT() //  Pot posar a generals return blocs
        {
            string[] blocs;
            string huntActual;
            StreamReader sr = new StreamReader(pathHistorial);
            while ((huntActual = sr.ReadLine()) != null)
            {
                if (huntActual != "")
                {
                    blocs = huntActual.Split('|');
                    GenerarValorsHunt(blocs);
                    AfegirHuntATaula();
                }
            }
            sr.Close();
        }

        // Avalua la hunt que rep per paràmetre, i li posa els valors a les variables globals.
        private void GenerarValorsHunt(string[] valors)
        {
            int columnaActual = 0;
            bool trobat = false;
            foreach (var bloc in valors)
            {
                StringBuilder stringTemp = new StringBuilder();
                for (int j = 0; j < bloc.Length; j++)
                {
                    char lletra = bloc[j];
                    if (bloc[j] == ':' && !trobat)
                    {
                        trobat = true;
                        j += 2;
                    }
                    if (trobat)
                    {
                        stringTemp.Append(bloc[j]);
                    }
                }
                string valorColumna = "";
                valorColumna = stringTemp.ToString();

                DonarValorAVariables(columnaActual, valorColumna);

                trobat = false;
                columnaActual++;
            }
        }

        // Dona valor del string que rep a la variable que toca (segons la columna del historial.txt que estigui mirant)
        private void DonarValorAVariables(int comptador, string valorColumna)
        {
            switch (comptador)
            {
                case 0:
                    huntIDTXT = int.Parse(valorColumna);
                    break;
                case 1:
                    respawnTXT = valorColumna;
                    break;
                case 2:
                    diaTXT = valorColumna;
                    break;
                case 3:
                    personesTXT = int.Parse(valorColumna);
                    break;
                case 4:
                    wasteEKTXT = float.Parse(valorColumna);
                    break;
                case 5:
                    wasteEDTXT = float.Parse(valorColumna);
                    break;
                case 6:
                    wasteRPTXT = float.Parse(valorColumna);
                    break;
                case 7:
                    wasteMSTXT = float.Parse(valorColumna);
                    break;
                case 8:
                    totalWasteTXT = float.Parse(valorColumna);
                    break;
                case 9:
                    lootTXT = float.Parse(valorColumna);
                    break;
                case 10:
                    balanceTXT = float.Parse(valorColumna);
                    break;
                case 11:
                    profitEachTXT = float.Parse(valorColumna);
                    break;
                case 12:
                    transferEKTXT = float.Parse(valorColumna);
                    break;
                case 13:
                    transferEDTXT = float.Parse(valorColumna);
                    break;
                case 14:
                    transferRPTXT = float.Parse(valorColumna);
                    break;
                case 15:
                    transferMSTXT = float.Parse(valorColumna);
                    break;
                case 16:
                    pagatTXT = valorColumna;
                    break;
                case 17:
                    timeTXT = valorColumna;
                    break;                   
            }
        }

        // Crea la clase Hunt i afegeix la hunt a la taula
        private void AfegirHuntATaula()
        {
            Hunt huntTemp = new Hunt(huntIDTXT, respawnTXT, diaTXT, personesTXT, wasteEKTXT, wasteEDTXT, wasteRPTXT, wasteMSTXT, totalWasteTXT, lootTXT, balanceTXT, profitEachTXT, transferEKTXT, transferEDTXT, transferRPTXT, transferMSTXT, pagatTXT, timeTXT);
            HistorialHunts.Items.Add(huntTemp);
        }

        // Agafa dades de la fila seleccionada de la DataGrid
        private void LlegirFilaDG()
        {
            Hunt DGRow = HistorialHunts.SelectedItem as Hunt;
            huntIDTXT = DGRow.HuntID;
            respawnTXT = DGRow.Respawn;
            timeTXT = DGRow.Time;
            diaTXT = DGRow.Dia;
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

        // Canvia el si per no i no per si (Datagrid PAID)
        private void CanviarPagat(string text)
        {
            int linia = huntIDTXT;
            if (huntIDTXT != 0) { linia = huntIDTXT * 2; } // Perque hi ha espais en blanc pel mig.
            historialfull = File.ReadAllLines(pathHistorial);
            historialfull[linia] = text; // Canvia la linia antiga per la nova.
            File.WriteAllLines(pathHistorial, historialfull);
            HistorialHunts.Items.Clear();
            LlegirTXT();
            SortDataGrid(HistorialHunts);
        }

        // Ordena la datagrid per ID de hunt (descendent)
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

        private void CalcularQuantAPagarED()
        {
            faltaPagarED = 0;
            for (int i = 0; i < HistorialHunts.Items.Count; i++)
            {
                Hunt huntTemp = (Hunt)HistorialHunts.Items[i]; // Agafa la fila de la datagrid
                if (huntTemp.Pagat == "no")
                {
                    faltaPagarED += huntTemp.TransferED;
                }
            }
            tbFaltaPagarED.Text = faltaPagarED.ToString();
        }
    }
    #endregion  
}

