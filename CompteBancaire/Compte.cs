using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompteBancaire
{
    [Serializable]
    internal class Compte
    {
        private int id;
        private string prenom;
        private string nom;
        private double sold;

        public Compte(int Id, string Prenom, string Nom, double Sold)
        {
            this.id = Id;
            this.prenom = Prenom;
            this.nom = Nom;
            this.sold = Sold;
        }

        public void Debiter(double Amount)
        {
            this.sold -= Amount;
        }

        public void Crediter(double Amount)
        {
            this.sold += Amount;
        }

        public int Id { 
            get { return this.id;}
        }

        public string Prenom
        {
            get { return this.prenom; }
        }

        public string Nom
        {
            get { return this.nom; }
        }

        public double Sold
        {
            get { return this.sold; }
        }
    }
}