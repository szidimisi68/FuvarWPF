using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class Fuvar
    {
        int azon;
        string indulasIdo;
        int utazasIdotartam;
        double megtettTav;
        double viteldij;
        double borravalo;
        string fizetesiMod;

        public Fuvar(int azon, string indulasIdo, int utazasIdotartam, double megtettTav, double viteldij, double borravalo, string fizetesiMod)
        {
            this.azon = azon;
            this.indulasIdo = indulasIdo;
            this.utazasIdotartam = utazasIdotartam;
            this.megtettTav = megtettTav;
            this.viteldij = viteldij;
            this.borravalo = borravalo;
            this.fizetesiMod = fizetesiMod;
        }

        public int Azon { get => azon; }
        public string IndulasIdo { get => indulasIdo; }
        public int UtazasIdotartam { get => utazasIdotartam; }
        public double MegtettTav { get => megtettTav; }
        public double Viteldij { get => viteldij; }
        public double Borravalo { get => borravalo; }
        public string FizetesiMod { get => fizetesiMod; }
    }
}
