using System;

public class HabitacionHotel
{
    private int numero;
    private string tipo;
    private double precioPorNoche;
    private bool disponible;
    private string clienteActual;

    public int Numero
    {
        get { return numero; }
    }

    public string Tipo
    {
        get { return tipo; }
        set
        {
            switch (value)
            {
                case "Individual":
                case "Doble":
                case "Suite":
                    tipo = value;
                    break;

                default:
                    Console.WriteLine("El tipo ingresado no existe");
                    break;
            }
        }
    }

    public double PrecioPorNoche
    {
        get { return precioPorNoche; }
        set
        {
            if (value > 0)
            {
                precioPorNoche = value;
            }
            else
            {
                Console.WriteLine("Ingrese un precio valido");
            }
        }
    }

    public bool Disponible
    {
        get { return disponible; }
    }

    public string ClienteActual
    {
        get { return clienteActual; }
    }

    public HabitacionHotel(int numero, string tipo, double precioPorNoche)
    {
        this.numero = numero;
        disponible = true;
        clienteActual = string.Empty;

        Tipo = tipo;
        PrecioPorNoche = precioPorNoche;
    }

    public double Reservar(string nombreCliente, int noches)
    {
        if (!disponible)
        {
            return -1;
        }

        if (string.IsNullOrWhiteSpace(nombreCliente) || noches <= 0)
        {
            return -1;
        }

        clienteActual = nombreCliente;
        disponible = false;

        double total = precioPorNoche * noches;

        return total;
    }

    public bool Liberar()
    {
        if (disponible == true)
        {
            return false;
        }

        clienteActual = string.Empty;
        disponible = true;

        return true;
    }

    public string MostrarInfo()
    {
        string estado;

        if (disponible)
        {
            estado = "Disponible";
        }
        else
        {
            estado = "Ocupada";
        }

        return $"Numero: {numero}\n" +
               $"Tipo: {tipo}\n" +
               $"Precio por noche: ${precioPorNoche:N0}\n" +
               $"Estado: {estado}\n" +
               $"Cliente: {(disponible ? "Ninguno" : clienteActual)}";
    }

    public void AplicarDescuento(int porcentaje)
    {
        if (porcentaje < 0)
        {
            Console.WriteLine("El descuento no puede ser negativo");
            return;
        }

        double rebaja = precioPorNoche * porcentaje / 100;

        precioPorNoche -= rebaja;

        if (precioPorNoche < 0)
        {
            precioPorNoche = 0;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        HabitacionHotel[] habitaciones =
        {
            new HabitacionHotel(101, "Individual", 50000),
            new HabitacionHotel(202, "Doble", 80000),
            new HabitacionHotel(303, "Suite", 150000)
        };

        Console.WriteLine("SISTEMA DE RESERVAS");

        Console.WriteLine("\nESTADO INICIAL");

        foreach (HabitacionHotel habitacion in habitaciones)
        {
            Console.WriteLine("\n" + habitacion.MostrarInfo());
            Console.WriteLine("--------------------------");
        }

        Console.WriteLine("\n REGISTRO DE RESERVAS");

        double reserva1 = habitaciones[0].Reservar("Juana Perez", 3);

        Console.WriteLine("\nReserva de Juana Perez:");
        Console.WriteLine("Total a pagar: $" + reserva1.ToString("N0"));

        double reserva2 = habitaciones[1].Reservar("Luis Gomez", 2);

        Console.WriteLine("\nReserva de Luis Gomez:");
        Console.WriteLine("Total a pagar: $" + reserva2.ToString("N0"));

        double reserva3 = habitaciones[0].Reservar("Otro cliente", 1);

        Console.WriteLine("\nIntento de segunda reserva en la habitacion 101:");

        if (reserva3 == -1)
        {
            Console.WriteLine("No se pudo realizar la reserva.");
        }
        else
        {
            Console.WriteLine("Total a pagar: $" + reserva3.ToString("N0"));
        }

        Console.WriteLine("\n LIBERACION DE HABITACION");

        bool resultado = habitaciones[0].Liberar();

        if (resultado)
        {
            Console.WriteLine("La habitacion 101 fue liberada.");
        }
        else
        {
            Console.WriteLine("La habitacion ya estaba disponible.");
        }

        Console.WriteLine("\n APLICACION DE DESCUENTO");

        habitaciones[2].AplicarDescuento(10);

        Console.WriteLine("Nuevo precio de la suite: $" +
                          habitaciones[2].PrecioPorNoche.ToString("N0"));

        double reserva4 = habitaciones[0].Reservar("Carlos Ruiz", 1);

        Console.WriteLine("\nReserva de Carlos Ruiz:");
        Console.WriteLine("Total a pagar: $" + reserva4.ToString("N0"));

        Console.WriteLine("\n ESTADO FINAL");

        int totalOcupadas = 0;

        foreach (HabitacionHotel habitacion in habitaciones)
        {
            Console.WriteLine("\n" + habitacion.MostrarInfo());
            Console.WriteLine("--------------------------");

            if (habitacion.Disponible == false)
            {
                totalOcupadas++;
            }
        }

        Console.WriteLine("\nTotal de habitaciones ocupadas: " + totalOcupadas);
    }
}
