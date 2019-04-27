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
        public void CrearConfig()
        {
            if (!File.Exists("config.txt")) // Si no existeix config, el crea i demana path del historial
            {
                FileStream config = File.Create("config.txt");
                config.Close();
            }
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

        public void CrearHistorial(string _pathHistorial)
        {
            if (!File.Exists(_pathHistorial))
            {
                FileStream historial = File.Create(_pathHistorial);
            }
        }
        

        public void CrearPathHistorial(string _path, out string _pathHistorial) // Crea el path del historial.txt
        {
            _pathHistorial = $"{ _path }\\historial.txt";
        }

        public void CrearPathDoble(string _path, out string _pathHistorial, out string _pathUpdates) // Crea path historial.txt i /updates
        {
            _pathHistorial = $"{ _path }\\historial.txt";
            _pathUpdates = $"{ _path }\\AutoUpdate";
        }

        




    }
}
