using System.Threading.Channels;

namespace SistemaDeGestionClientes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Listar = new Cliente();
            var ListEmpleado = new Empleado();

            int menu = 0;

            while (menu != 5)
            {   
                System.Console.WriteLine("------------------Sistema de Clientes--------------------");
                System.Console.WriteLine("1. Crear Cliente\n2. Crear Empleado\n3. Listar Clientes\n4. Listar Empleados\n5. Salir");
                
                menu = int.Parse(Console.ReadLine()!);
                Menu(menu,Listar,ListEmpleado);
                Console.ReadLine();
                Limpiar();
            }



        }

        static void Menu(int entrada,Cliente Listar,Empleado employee)
        {
            string name = "";
            string cell = "";
            string email = "";
            string Cargo = "";
            decimal Salario = 0;
            
            switch (entrada)
            {
                 
                case 1:
                    name = Indata("Ingrese su Nombre");
                    cell = Indata("Ingrese su Numero de Telefono");
                    email = Indata("Ingrese su Correo");

                    Cliente clientes = new(name,email,cell);
                    Listar.SaveClient(clientes);

                    Console.WriteLine("Cliente Añadido");
                    break;
                case 3:
                    Console.WriteLine("----------Lista de Clientes-----------");
                    Listar.Listandocliente();

                    break;
                case 5:
                    Console.WriteLine("Saliendo del Programa");
                    return;
                case 2:
                    name = Indata("Ingrese Su Nombre");
                    cell = Indata("Ingrese su Numero de Telefono");
                    email = Indata("Ingrese su Correo");
                    Cargo = Indata("Ingrese su Posicion");
                    Salario = Indecimal("Ingrese su Sueldo");

                    Empleado info = new (name,email,cell,Cargo,Salario);
                    employee.AgregarEmpleado(info);

                    Console.WriteLine("Empleado Añadido");

                    break;
                case 4:
                    Console.WriteLine("----------Lista de Empleados-----------");
                    employee.ListandoEmpleados();

                    break;
                default: 
                    Console.WriteLine("Valor no valido");
                    break;

            }
        }

        static void Limpiar()
        {
            try
            {
                Console.Clear();
            }
            catch(IOException)
            {
                Console.WriteLine("[!] No se pudo limpiar la consola. Probablemente no hay consola disponible.");
            }
        }

        static string Indata(string entrada)
        {
            Console.WriteLine(entrada);

            return Console.ReadLine()!;
        }
        static decimal Indecimal(string entrada)
        {
            System.Console.WriteLine(entrada);
            return decimal.Parse(Console.ReadLine()!);
        }
    }
    class Cliente : Persona
    {

        private readonly List<string> Compras = [];
        private readonly List<Cliente> listclientes = [];
        public Cliente() : base ()
        {

        }

        public Cliente(string Nombre, string Email, string Telefono) : base (Nombre, Email, Telefono)
        {
            
        }

        public void AgregarCompra(string Producto)
        {
            Compras.Add(Producto);
        }

        public void Listandocliente()
        {
            foreach(var salida in listclientes)
            {
                salida.ShowInfo();
            }
        }
        public void SaveClient(Cliente entrada)
        {
            listclientes.Add(entrada);
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Nombre: {Nombre}\nCorreo: {Email}\nTelefono: {Telefono}\n");

            foreach(var mostrar in Compras)
            {
                System.Console.WriteLine(mostrar + "\n");
            }
        }
    
    }
    class Empleado : Persona
    {
        private string Cargo;
        private decimal Salario{get; set;}
        private readonly List<Empleado> Stuff = [];

        public Empleado() : base ()
        {
            Cargo = "";
            Salario = 0;
        }

        public Empleado(string Nombre, string Email, string Telefono, string Cargo, decimal Salario) : base (Nombre, Email, Telefono)
        {
            this.Cargo = Cargo;
            this.Salario = Salario;
        }

        public decimal CalcularSalarioAnual()
        {
            return Salario * 12;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Nombre: {Nombre}\nCorreo: {Email}\nTelefono: {Telefono}");
            Console.WriteLine($"Cargo: {Cargo}\nSalario: {Salario}\n");
        }

        public void AgregarEmpleado(Empleado empleado)
        {
            Stuff.Add(empleado);
        }
        public void ListandoEmpleados()
        {
            foreach(var salida in Stuff)
            {
                salida.ShowInfo();
            }
        }

    }

    class Producto 
    {
        private string Nombre{get; set;}
        private float Precio {get; set;}

        public Producto()
        {

            Nombre = "";
            Precio = 0;

        }

        public void ShowInfo() => System.Console.WriteLine($"Nombre: {Nombre}\nPrecio: {Precio}");

        public static bool Validar(float valor)
        {
            return valor > 0;
        }

    }
    class Persona
    {
        protected string Nombre{ get; set;}
        protected string Email{ get; set; }
        protected string Telefono {get; set;}


        public Persona()
        {
            Nombre = "";
            Email = "";
            Telefono = "";
        }

        public Persona(string Nombre, string Email, string Telefono)
        {
            this.Nombre = Nombre;
            this.Email = Email;
            this.Telefono = Telefono;
        }

        public virtual void ShowInfo() => System.Console.WriteLine($"Nombre: {Nombre}\nCorreo: {Email}\nTelefono: {Telefono}\n");
    }


}
