using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Tema_Compilatoare
{

    
    class Program
    {
        static void Citeste_automatul(string nume_fis,CAutomat automat)
        {
            StreamReader cititor = File.OpenText(nume_fis);
            string linie;
            int hai = 0;
            while ((linie = cititor.ReadLine()) != null)
            {
                automat.load_TAutomat(linie);
                switch (hai)
                {
                    case 0:
                        {
                            automat.load_Alfabet(linie);
                            hai++;
                            break;
                        }
                    case 1:
                        {
                            automat.load_Stari(linie);
                            hai++;
                            break;
                        }
                    case 2:
                        {
                            automat.load_Stari_Finale(linie);
                            hai++;
                            break;
                        }
                    case 3:
                        {
                            automat.load_Stare_Initiala(linie);
                            hai++;
                            break;
                        }
                    default:
                        {
                            automat.load_Tranzitii(linie,hai);
                            hai++;
                            break;
                        }
                        
                }
            }
        }

        static void Afiseaza_automatul(CAutomat automat)
        {
            File.WriteAllText("out.txt", ""); //sterg continutul anterior al fisierului 
            for (int i = 0; i < automat.get_TAutomat().Count ; i++)
            {
                
                File.AppendAllText("out.txt", automat.get_TAutomat()[i]+Environment.NewLine);
            }
            File.AppendAllText("out.txt", Environment.NewLine);

            File.AppendAllText("out.txt", automat.check_type());
        }






        static void Remove_Epsilon(string state,CAutomat automat,string fake_state,CAutomat NonEpsilon)
        {
            foreach (CTranzitie Aux_tranz in automat.get_Tranzitii())
            {
                if (state==Aux_tranz.get_dinspre())
                {
                    if (Aux_tranz.get_cost()=="e")
                    {
                        Remove_Epsilon(Aux_tranz.get_catre(), automat, fake_state,NonEpsilon);
                    }
                else
                    {
                        var tranz_noua = new CTranzitie(Aux_tranz.get_catre(), fake_state, Aux_tranz.get_cost());
                        NonEpsilon.Accept_Tranzitii(tranz_noua);
                    }
                }
            }
        }



        static void Main(string[] args)
        {
            CAutomat automat = new CAutomat();
            Citeste_automatul(args[0],automat);
            Afiseaza_automatul(automat);
            CAutomat NonEpsilon = new CAutomat(automat);

            foreach (CTranzitie Aux_tranz in automat.get_Tranzitii())
            {
                Remove_Epsilon(Aux_tranz.get_dinspre(), automat, Aux_tranz.get_dinspre(), NonEpsilon);
            }
           
            File.AppendAllText("out.txt", Environment.NewLine);
            File.AppendAllText("out.txt", Environment.NewLine);
            NonEpsilon.Clean_automat();
            NonEpsilon.Remove_useless_states();

  //        NonEpsilon.Clean_automat();
            Afiseaza_automatul(NonEpsilon);

        }
    }
}
