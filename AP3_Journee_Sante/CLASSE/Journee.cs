using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP3_Journee_Sante.CLASSE
{
    public class Journee
    {
        public int Id { get; }
        public DateTime Date { get; set; }
        public string Lieu { get; set; }

        public Journee(int id, DateTime date, string lieu)
        {
            Id = id;
            Date = date;
            Lieu = lieu;
        }

        public Journee(DateTime date, string lieu)
        {
            Date = date;
            Lieu = lieu;
        }
        public string ArchiverEdition()
        {
            return $"Journée du {Date.ToShortDateString()} à {Lieu} archivée.";
        }
    }
}
