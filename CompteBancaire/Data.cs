using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace CompteBancaire
{
    internal class Data
    {
        private List<Compte> allAccount;
        private string DataId;

        public Data(string Id) {
            allAccount = new List<Compte>();
            Id += ".json";
            DataId = Id;
            try
            {
                if (File.Exists(Id))
                {
                    string jsonString = File.ReadAllText(Id);
                    allAccount = JsonSerializer.Deserialize<List<Compte>>(jsonString);
                }
                else
                {
                    allAccount = new List<Compte>();
                }
            }
            catch
            {
                allAccount = new List<Compte>();
            }
        }

        public void Save()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(allAccount);
                File.WriteAllText(DataId, jsonString);
            }
            catch (Exception expt)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Le programme na pas réussi a sauveguardé les élève");
                Console.WriteLine();
                Console.WriteLine(expt);
            }
        }

        public void AddAccount(Compte compte) {
            this.allAccount.Add(compte);
        }

        public int Count {
            get { return allAccount.Count;}
        }

        public List<Compte> GetAllAccount() {
            return allAccount;
        }
    }
}
