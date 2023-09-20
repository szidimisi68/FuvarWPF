using System;
using System.IO;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    // <<balra -
    // >>jobbra Muskovszky Tamás

    public partial class MainWindow : Window
    {
        List<Fuvar> fuvarok = new List<Fuvar>();
        public MainWindow()
        {
            InitializeComponent();
            File.ReadAllLines("fuvar.csv").ToList().Skip(1).ToList().ForEach(line => { fuvarok.Add(new Fuvar(Convert.ToInt32(line.Split(";")[0]), line.Split(";")[1], Convert.ToInt32(line.Split(";")[2]), Convert.ToDouble(line.Split(";")[3]), Convert.ToDouble(line.Split(";")[4]), Convert.ToDouble(line.Split(";")[5]), line.Split(";")[6])); });
            lbFuvarokSzama.Content += $"{fuvarok.Count()}";
            List<int> azonositok = new List<int>();
            fuvarok.GroupBy(x => x.Azon).ToList().ForEach(x => azonositok.Add(x.Key));
            cbAzonositok.ItemsSource = azonositok;

            List<string> fizetesiModok = new List<string>();
            fuvarok.GroupBy(x => x.FizetesiMod).ToList().ForEach(x => fizetesiModok.Add($"{x.Key}: {x.Count()} fuvar"));
            lbFizetesiModok.ItemsSource = fizetesiModok;

            lbMegtettKm.Content += $" {Math.Round( fuvarok.Sum(x => x.MegtettTav)*1.6, 2)}km";

            List<string> leghosszabbMegtettUtAdatai = new List<string>();
            fuvarok.Where(x => x.MegtettTav == fuvarok.Max(x => x.MegtettTav)).ToList().ForEach(x=> {
                leghosszabbMegtettUtAdatai.Add($"fuvar hossza: {x.UtazasIdotartam} másodperc");
                leghosszabbMegtettUtAdatai.Add($"Taxi azonosító: {x.Azon}");
                leghosszabbMegtettUtAdatai.Add($"Megtett távolság: {x.MegtettTav} km");
                leghosszabbMegtettUtAdatai.Add($"Viteldíj: {x.Viteldij}$");
            });

            lbLeghosszabbMegtettUt.ItemsSource = leghosszabbMegtettUtAdatai;

            List<string> hibasFuvarok = new List<string>();
            hibasFuvarok.Add("taxi_id;indulas;idotartam;tavolsag;viteldij;borravalo;fizetes_modja");
            fuvarok.Where(x => x.UtazasIdotartam > 0 && x.Viteldij > 0 && x.MegtettTav == 0).ToList().OrderBy(x => x.IndulasIdo).ToList().ForEach(x => hibasFuvarok.Add($"{x.Azon};{x.IndulasIdo};{x.UtazasIdotartam};{x.MegtettTav};{x.Viteldij};{x.Borravalo};{x.FizetesiMod}"));
            File.WriteAllLines("hibak.txt", hibasFuvarok);
        }

        private void btnLekerdez_Click(object sender, RoutedEventArgs e)
        {
            int azon = Convert.ToInt32(cbAzonositok.SelectedValue);
            double kereset = 0;
            fuvarok.Where(x => x.Azon == azon).ToList().ForEach(x => { kereset += x.Viteldij; kereset += x.Borravalo; });

            lbLekertFuvarBevetel.Content = $"Bevétele: {kereset}$";
            lbLekertFuvarSzama.Content = $"fuvarok száma: {fuvarok.Where(x => x.Azon == azon).ToList().Count()}";
        }
    }
}
