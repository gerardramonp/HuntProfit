using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HuntProfit
{
    public class MetodesPath
    {
        public void CrearConfig() // Crea l'arxiu config
        {
            FileStream config = File.Create("config.txt");
            config.Close();
        }

        public void PathAConfig() // Buscador de carpetes, escriu el path al config.txt
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            string path = fbd.SelectedPath;
            StreamWriter sw = new StreamWriter("config.txt");
            sw.Write(path);
            sw.Close();
        }

        public void GenerarPaths(out string _pathHistorial, out string _pathUpdates) // Crea path historial.txt i /updates
        {
            StreamReader sr = new StreamReader("config.txt");
            string _path = sr.ReadLine();
            while (_path == null || _path == "")
            {
                MessageBox.Show("El path no està introduit, selecciona la carpeta on es troba l'arxiu <historial.txt>");
                PathAConfig();
            }
            _pathHistorial = $"{ _path }\\historial.txt";
            _pathUpdates = $"{ _path }\\AutoUpdate";
        }

        public void CrearHistorial(string _pathHistorial)
        {

            FileStream historial = File.Create(_pathHistorial);

        }



    }
}
