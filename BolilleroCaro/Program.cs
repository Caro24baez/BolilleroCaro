using BolilleroCaro;

namespace BolilleroProgram
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Ingrese la cantidad de bolillas que tiene el bolillero: ");
            int cantBolillas;
            while (!int.TryParse(Console.ReadLine(), out cantBolillas) || cantBolillas <= 0)
            {
                Console.WriteLine("Por favor, ingrese un número válido mayor que 0.");
                Console.Write("Ingrese la cantidad de bolillas que tiene el bolillero: ");
            }

            Console.Write("Ingrese la cantidad de bolillas en una Jugada: ");
            int lengJugada = Convert.ToInt32(Console.ReadLine());


            IRandomNumberGenerator azar = new RandomNumberGenerator();
            Bolillero bolillero = new Bolillero(cantBolillas, azar, lengJugada);
            Simulacion simulacion = new Simulacion(bolillero, azar);

            Console.Write("Ingrese la cantidad de veces a jugar: ");
            int jugarNVeces = Convert.ToInt32(Console.ReadLine());

            
            var jugada = new List<int>();
            jugada.Add(1);
            jugada.Add(8);
            Console.WriteLine($"Jugada: {string.Join(", ", jugada)}"); 

            // Simular sin hilos
            long SinHilos = simulacion.SimularSinHilos(bolillero, jugada, jugarNVeces);
            Console.WriteLine("Cantidad de veces que la jugada salió (sin hilos): " + SinHilos);

            // Simular con hilos
            Console.Write("Ingrese la cantidad de hilos: ");
            int cantHilos = Convert.ToInt32(Console.ReadLine());
            int resultadoConHilos = simulacion.SimularConHilos(bolillero, jugada, jugarNVeces, cantHilos);
            Console.WriteLine("Cantidad de veces que la jugada salió (con hilos): " + resultadoConHilos);
            
            bool resultado = simulacion.Jugar(jugada);
            Console.WriteLine("¿La jugada fue exitosa?: " + resultado);
            
            Console.ReadLine();
           // return Task.CompletedTask;
        }
    }
}
