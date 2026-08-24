using System;

namespace P00_Unidad1
{
    public class Moto
    {
        // Atributos
        public string Marca;
        public string Modelo;
        public int Anio;
        public double VelocidadActual;

        // Métodos
        public void Acelerar(double inc)
        {
            VelocidadActual += inc;
        }

        public void Frenar(double dec)
        {
            if (VelocidadActual - dec >= 0)
                VelocidadActual -= dec;
            else
                VelocidadActual = 0;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("Marca: " + Marca);
            Console.WriteLine("Modelo: " + Modelo);
            Console.WriteLine("Año: " + Anio);
            Console.WriteLine("Velocidad actual: " + VelocidadActual + " km/h");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Moto miMoto = new Moto();

            miMoto.Marca = "Susuki";
            miMoto.Modelo = "GSX-8T";
            miMoto.Anio = 2026;
            miMoto.VelocidadActual = 0;

            miMoto.MostrarInformacion();
        }
    }
}
