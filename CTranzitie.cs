using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_Compilatoare
{
    class CTranzitie
    {
        string MCatre;
        string MCost;
        string MDinspre;

        public CTranzitie(string catre,string dinspre,string caracter)
        {
            MCatre = catre;
            MCost = caracter;
            MDinspre = dinspre;
        }
        public  string get_cost()
        {
            return MCost;
        }
        public string get_catre()
        {
            return MCatre;
        }
        public string get_dinspre()
        {
            return MDinspre;
        }

         static public bool operator == (CTranzitie a,CTranzitie b)
        {
            bool ok = true;
            if (a.get_catre() != b.get_catre())
                ok = false;
            if (a.get_cost() != b.get_cost())
                ok = false;
            if (a.get_dinspre() != b.get_dinspre())
                ok = false;
            return ok;

        }
        static public bool operator !=(CTranzitie a, CTranzitie b)
        {
            if (a == b)
                return false;
            else return true;
        }
    }
}
