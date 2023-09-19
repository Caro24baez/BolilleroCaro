using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int JugarNVeces(List<int> jugadas, int cantVeces, Bolillero bolillero)
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
        public int simularSinHilos(Bolillero bolillero, List<int> jugadas, int CantSimulación, int cantVeces)
        {
            return JugarNVeces(jugadas, cantVeces, bolillero);
        }
        public int simularConHilos(Bolillero bolillero, List<int> jugadas, int CanSimulación, int canHilos, int cantVeces)
        {
            var vectorTarea = new Task<int>[canHilos];
            int resto = cantVeces % canHilos;
            for (int i = 0; i < canHilos; i++)
            {
                Bolillero clon = (Bolillero)bolillero.Clone();
                vectorTarea[i] = Task.Run(() => JugarNVeces(jugadas, cantVeces / canHilos, clon));
            }
            Task.WaitAll(vectorTarea);
            Bolillero clon1 = (Bolillero)bolillero.Clone();
            vectorTarea[canHilos - 1] = Task.Run(() => JugarNVeces(jugadas, cantVeces / canHilos + resto , clon1));
            return vectorTarea.Sum(T => T.Result);
            
        }
    }
}
