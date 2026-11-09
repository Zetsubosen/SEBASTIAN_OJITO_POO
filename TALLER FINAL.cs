using System;
using System.Globalization;
using System.Text;

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
                    Console.WriteLine("El tipo ingresado no existe.");
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
                Console.WriteLine("Ingrese un precio valido.");
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
        if (porcentaje <= 0)
        {
            Console.WriteLine("Aviso: el descuento debe ser mayor que 0%. El precio no cambio.");
            return;
        }

        if (porcentaje >= 100)
        {
            Console.WriteLine("Aviso: un descuento de 100% o mas dejaria el precio en cero o negativo. El precio no cambio.");
            return;
        }

        double rebaja = precioPorNoche * porcentaje / 100.0;
        double nuevoPrecio = precioPorNoche - rebaja;

        if (nuevoPrecio <= 0)
        {
            Console.WriteLine("Aviso: el precio resultante no puede ser cero ni negativo. El precio no cambio.");
            return;
        }

        precioPorNoche = nuevoPrecio;
    }

    public string ObtenerDatosResumen()
    {
        string precioFormateado = precioPorNoche
            .ToString("N0", CultureInfo.InvariantCulture)
            .Replace(",", ".");

        string estado;

        if (disponible)
        {
            estado = "Disponible";
        }
        else
        {
            estado = "Ocupada por: " + clienteActual;
        }

        return $"[{numero}] {tipo} - ${precioFormateado} - {estado}";
    }
}

public class Program
{
    static HabitacionHotel BuscarHabitacion(HabitacionHotel[] habitaciones, int numero)
    {
        foreach (HabitacionHotel habitacion in habitaciones)
        {
            if (habitacion.Numero == numero)
            {
                return habitacion;
            }
        }

        return null;
    }

    static int LeerEntero(string mensaje)
    {
        int valor;

        Console.Write(mensaje);

        while (!int.TryParse(Console.ReadLine(), out valor))
        {
            Console.Write("Entrada invalida. " + mensaje);
        }

        return valor;
    }

    static void ListarHabitaciones(HabitacionHotel[] habitaciones)
    {
        Console.WriteLine("\n----- LISTADO DE HABITACIONES -----");

        foreach (HabitacionHotel habitacion in habitaciones)
        {
            Console.WriteLine(habitacion.ObtenerDatosResumen());
        }
    }

    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        HabitacionHotel[] habitaciones =
        {
            new HabitacionHotel(101, "Individual", 50000),
            new HabitacionHotel(202, "Doble", 80000),
            new HabitacionHotel(303, "Suite", 150000)
        };

        Console.WriteLine("===== SISTEMA DE RESERVAS - HOTEL NOMADAS DEL CARIBE =====");

        int opcion;

        do
        {
            Console.WriteLine("\n========== MENU ==========");
            Console.WriteLine("1. Ver habitaciones");
            Console.WriteLine("2. Reservar habitacion");
            Console.WriteLine("3. Liberar habitacion");
            Console.WriteLine("4. Aplicar descuento a una habitacion");
            Console.WriteLine("0. Salir y ver metricas");
            Console.WriteLine("==========================");

            opcion = LeerEntero("Seleccione una opcion: ");

            switch (opcion)
            {
                case 1:
                    ListarHabitaciones(habitaciones);
                    break;

                case 2:
                {
                    int numReserva = LeerEntero("Numero de habitacion a reservar: ");
                    HabitacionHotel habitacion = BuscarHabitacion(habitaciones, numReserva);

                    if (habitacion == null)
                    {
                        Console.WriteLine("La habitacion no existe.");
                        break;
                    }

                    Console.Write("Nombre del cliente: ");
                    string cliente = Console.ReadLine();
                    int noches = LeerEntero("Cantidad de noches: ");

                    double total = habitacion.Reservar(cliente, noches);

                    if (total == -1)
                    {
                        Console.WriteLine("No se pudo realizar la reserva (habitacion ocupada o datos invalidos).");
                    }
                    else
                    {
                        Console.WriteLine("Reserva exitosa. Total a pagar: $" +
                                          total.ToString("N0", CultureInfo.InvariantCulture).Replace(",", "."));
                    }
                    break;
                }

                case 3:
                {
                    int numLiberar = LeerEntero("Numero de habitacion a liberar: ");
                    HabitacionHotel habitacion = BuscarHabitacion(habitaciones, numLiberar);

                    if (habitacion == null)
                    {
                        Console.WriteLine("La habitacion no existe.");
                        break;
                    }

                    if (habitacion.Liberar())
                    {
                        Console.WriteLine("La habitacion " + numLiberar + " fue liberada.");
                    }
                    else
                    {
                        Console.WriteLine("La habitacion ya estaba disponible.");
                    }
                    break;
                }

                case 4:
                {
                    int numDescuento = LeerEntero("Numero de habitacion: ");
                    HabitacionHotel habitacion = BuscarHabitacion(habitaciones, numDescuento);

                    if (habitacion == null)
                    {
                        Console.WriteLine("La habitacion no existe.");
                        break;
                    }

                    int porcentaje = LeerEntero("Porcentaje de descuento: ");
                    habitacion.AplicarDescuento(porcentaje);

                    Console.WriteLine("Resumen actual: " + habitacion.ObtenerDatosResumen());
                    break;
                }

                case 0:
                    Console.WriteLine("\nSaliendo del sistema...");
                    break;

                default:
                    Console.WriteLine("Opcion no valida.");
                    break;
            }

        } while (opcion != 0);

        Console.WriteLine("\n===== ESTADO FINAL =====");
        ListarHabitaciones(habitaciones);

        int libres = 0;
        int ocupadas = 0;

        foreach (HabitacionHotel habitacion in habitaciones)
        {
            if (habitacion.Disponible)
            {
                libres++;
            }
            else
            {
                ocupadas++;
            }
        }

        double sumaPrecios = 0;

        foreach (HabitacionHotel habitacion in habitaciones)
        {
            sumaPrecios += habitacion.PrecioPorNoche;
        }

        double promedio = sumaPrecios / habitaciones.Length;

        Console.WriteLine("\n===== METRICAS =====");
        Console.WriteLine("Habitaciones libres: " + libres);
        Console.WriteLine("Habitaciones ocupadas: " + ocupadas);
        Console.WriteLine("Precio promedio por noche: $" +
                          promedio.ToString("N0", CultureInfo.InvariantCulture).Replace(",", "."));
    }
}
