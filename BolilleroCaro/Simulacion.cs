using System;
using System.Collections.Generic;
using System.Text;

namespace BolilleroCaro
{
    public class Simulacion
    {
        Bolillero bolillero = new Bolillero();
        public bool Jugar(List<int> jugadas)
        {
            var comparar = 0;
            bolillero.regresarBolillas();
            foreach (var bolillaJugada in jugadas)
            {
                comparar = bolillero.sacarBolilla();
                if (comparar != bolillaJugada)
                {
                    return false;
                }
            }
            return true;
        }

        public int JugarNVeces(List<int> jugadas, int cantVeces)
        {
            int cantGanados = 0;
            for (int i = 0; i < cantVeces; i++)
            {

                if (this.Jugar(jugadas) == true)
                {
                    cantGanados++;
                }


            }
            return cantGanados;

        }
    }
}
