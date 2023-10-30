using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BolilleroCaro
{
    public class Simulacion
    {
        private Bolillero bolillero;
        private readonly IRandomNumberGenerator azar;
        public Simulacion(Bolillero bolillero, IRandomNumberGenerator azar) // toma un bolillero y un generador de números aleatorios
        {
            this.bolillero = bolillero;
            this.azar = azar;
        }

        public bool Jugar(List<int> jugadas) => bolillero.sacarJugada(jugadas);

        public int JugarNVeces(List<int> jugadas, int cantVeces)
        {
            int cantGanados = 0;
            for (int i = 0; i < cantVeces; i++)
            {
                bolillero.regresarBolillas();
                if (this.Jugar(jugadas) == true)
                {
                    cantGanados++;
                }
            }
            return cantGanados;
        }

        public long SimularSinHilos(Bolillero bolillero, List<int> jugada, int cantSimulaciones)
        {
            long cantGanados = 0;
            for (int i = 0; i < cantSimulaciones; i++)
            {
                Bolillero bolilleroClone = (Bolillero)bolillero.Clone();
                bolilleroClone.regresarBolillas();
                if (bolilleroClone.sacarJugada(jugada))
                {
                    cantGanados++;
                }
            }
            return cantGanados;
        }

        public int SimularConHilos(Bolillero bolillero, List<int> jugada, int jugarNVeces, int cantHilos)
        {
            Task<int>[] vectorTarea = GenerarVectorProcesos(bolillero, jugada, jugarNVeces, cantHilos);
            Task.WaitAll(vectorTarea);
            /*
            foreach (var tarea in vectorTarea)
            {
                if (tarea.Exception != null)
                {
                    Console.WriteLine("Ocurrió un error durante la simulación: " + tarea.Exception.Message);
                    return 0;
                }
            }*/

            int totalGanados = vectorTarea.Sum(t => t.Result);
            return totalGanados;
        }
        //nuevo
        private Task<int>[] GenerarVectorProcesos(Bolillero bolillero, List<int> jugada, int jugarNVeces, int cantHilos)
        {
            var vectorTarea = new Task<int>[cantHilos];
            for (int i = 0; i < cantHilos; i++)
            {
                Bolillero clon = (Bolillero)bolillero.Clone();
                vectorTarea[i] =
                    Task.Run(() => (int)this.SimularSinHilos(clon, jugada, jugarNVeces / cantHilos)
                );
            }

            return vectorTarea;
        }
        public async Task<long> SimularConHilosAsync(Bolillero bolillero, List<int> jugada, int cantSimulaciones, int cantHilos)
        {
            Task<int>[] vectorTarea = GenerarVectorProcesos(bolillero, jugada, cantSimulaciones, cantHilos);

            //TODO a partir de esta parte, se parece al anterior y hay que esperar que termine de correr el vector
            //de procesos de una forma asincronica sin bloqueo.
            //Cuando este, borrar la siguiente linea
            //throw new NotImplementedException();
            await Task.WhenAll(vectorTarea);
            long totalGanados = vectorTarea.Sum(t => t.Result);

            return totalGanados;
        }
    }
}
