using System;
using System.Collections.Generic;

class Chofer
{
    public string Nombre { get; set; }
    private int edad;
    private string tipoLicencia;

    public int Edad
    {
        get { return edad; }
        set { edad = value; ValidarLicencia(); }
    }

    public string TipoLicencia
    {
        get { return tipoLicencia; }
        set
        {
            tipoLicencia = value;
            ValidarLicencia();
        }
    }

    public Chofer(string nombre, int edad, string tipoLicencia)
    {
        Nombre = nombre;
        Edad = edad;
        TipoLicencia = tipoLicencia;
    }

    private void ValidarLicencia()
    {
        var requisitos = new Dictionary<string, int>
        {
            { "A", 18 },
            { "B", 18 },
            { "C", 21 },
            { "M", 18 },
            { "E", 23 }
        };

        if (requisitos.ContainsKey(tipoLicencia) && Edad < requisitos[tipoLicencia])
        {
            throw new Exception($"Edad insuficiente para la licencia {tipoLicencia}.");
        }
    }
}

class Vehiculo
{
    protected int VelocidadMaxima;
    protected int CapacidadTanque;
    protected int ConsumoCombustible;
    public int VelocidadActual { get; private set; } = 0;
    public bool Encendido { get; private set; } = false;
    public Chofer Piloto { get; set; }

    public Vehiculo(int velocidadMaxima, int capacidadTanque, int consumoCombustible)
    {
        VelocidadMaxima = velocidadMaxima;
        CapacidadTanque = capacidadTanque;
        ConsumoCombustible = consumoCombustible;
    }

    public void Encender() => Encendido = true;
    public void Apagar()
    {
        if (VelocidadActual > 0)
            Console.WriteLine("No se puede apagar el vehículo en movimiento.");
        else
            Encendido = false;
    }

    public void Acelerar(int incremento)
    {
        if (VelocidadActual + incremento > VelocidadMaxima)
            Console.WriteLine($"¡Alerta! No se puede superar la velocidad máxima de {VelocidadMaxima} km/h.");
        else
            VelocidadActual += incremento;
    }

    public void Frenar(int decremento)
    {
        VelocidadActual -= decremento;
        if (VelocidadActual <= 0)
        {
            VelocidadActual = 0;
            Console.WriteLine("El vehículo se ha detenido.");
        }
    }

    public void InformacionVehiculo()
    {
        Console.WriteLine($"Velocidad Actual: {VelocidadActual} km/h");
        Console.WriteLine($"Velocidad Máxima: {VelocidadMaxima} km/h");
        Console.WriteLine($"Estado: {(Encendido ? (VelocidadActual > 0 ? "En movimiento" : "Encendido") : "Apagado")}");
        Console.WriteLine($"Piloto: {(Piloto != null ? Piloto.Nombre : "Ninguno")}");
    }
}

class Moto : Vehiculo
{
    public Moto() : base(120, 10, 1) { }
    
    public void HacerCaballito()
    {
        if (VelocidadActual > 0)
            Console.WriteLine("¡Haciendo un caballito!");
        else
            Console.WriteLine("No se puede hacer un caballito con la moto detenida.");
    }
}
