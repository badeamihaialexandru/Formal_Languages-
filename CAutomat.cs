using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_Compilatoare
{
    class CAutomat
    {
        string[] MAlfabet;
        string[] MStari;
        string[] MStari_finale;
        string MStare_initiala;
        List<String> MTextAutomat = new List<string>();
        List<CTranzitie> MTranzitii=new List<CTranzitie>();

        public string[] get_Stari()
        {
            return MStari;
        }
        public string[] get_Stari_Finale()
        {
            return MStari_finale;
        }
        public string get_Stare_Initiala()
        {
            return MStare_initiala;
        }
        public void load_Alfabet(string linie)
        {
            MAlfabet = linie.Split(' ');
        }
        public void load_Stari(string linie)
        {
            MStari = linie.Split(' ');
        }
        public void load_Stari_Finale(string linie)
        {
            MStari_finale = linie.Split(' ');
        }
        public void load_Stare_Initiala(string linie)
        {
            MStare_initiala = linie.Split('\n', ' ')[0];
        }
        public void load_Tranzitii(string linie,int stare)
        {
            int a_cui_tranzitie=0;
            string[] catre = linie.Split(' ');
            if (MTranzitii!= null)
            {
                a_cui_tranzitie = MTranzitii.Count;
            }
            for (int i = 0; i < catre.Length; i++)
            {
                if (catre[i] != "-")
                {
                    string[] sub_catre = catre[i].Split('_');
                    if (sub_catre.Length > 0)
                    {
                        for (int j = 0; j < sub_catre.Length; j++)
                        {
                            var nou = new CTranzitie(sub_catre[j], MStari[stare - 4], MAlfabet[i]);
                            MTranzitii.Add(nou);
                        }
                    }
                    else
                    {
                        var nou = new CTranzitie(catre[i], MStari[stare - 4], MAlfabet[i]);
                        MTranzitii.Add(nou);
                    }
                 }
          }
        }
        public void load_TAutomat(string linie)
        {
            MTextAutomat.Add(linie);
        }
        public void Accept_Tranzitii(CTranzitie Tranzitie)
        {
            MTranzitii.Add(Tranzitie);
        }
        public string [] get_alfabet()
        {
            return MAlfabet;
        }
        public List<CTranzitie> get_Tranzitii()
        {
            return MTranzitii;
        }
        public List<string> get_TAutomat()
        {
            return MTextAutomat;
        }
        public void Clean_automat()
        {
            for(int i=0;i<MTranzitii.Count-1;i++)
            {
                for(int j=i+1;j<MTranzitii.Count;j++)
                {
                      if(MTranzitii[i]==MTranzitii[j])
                    {
                        MTranzitii.Remove(MTranzitii[j]);
                        j--;
                    }
                }
            }
        }
        public string check_type()
        {
            string result = "D";
            for (int i = 0; i < MTranzitii.Count-1; i++)
            {
                if (MTranzitii[i].get_dinspre() == MTranzitii[i + 1].get_dinspre())
                    result = "N";
            }

            foreach (CTranzitie tranz in MTranzitii)
            {
                if (tranz.get_cost() == "e")
                    result = "E";
            }
            return result;
        }
        public void Remove_useless_states()
        {
            
            foreach(string stare in MStari)
            {
                bool ok = false;
                foreach (CTranzitie Aux_tranz in MTranzitii)
                {
                    if(stare==Aux_tranz.get_catre())
                    {
                       if(Aux_tranz.get_cost()!="e")
                        {
                            ok = true;
                        }
                    }
                }
                if (stare == MStare_initiala)
                    ok = true;
                if (ok==false)
                {
                    foreach(string q in MStari)
                    {
                        if (q==stare)
                        {
                            List<string> aux = MStari.ToList();
                            aux.Remove(q);
                            MStari = aux.ToArray();

                        }
                    }
                    for(int i=0;i<MTranzitii.Count;i++)
                    {
                        if (MTranzitii[i].get_dinspre()==stare)
                        {
                            MTranzitii.Remove(MTranzitii[i]);
                            i--;
                        }
                    }
                }
            }
        }

        public CAutomat(CAutomat a)
        {
            MStari = a.get_Stari();
            MStari_finale = a.get_Stari_Finale();
            MStare_initiala = a.get_Stare_Initiala();
            MAlfabet = a.get_alfabet();
        }
        public CAutomat()
        {

        }
    }
}
