using System.Threading.Channels;

namespace SistemaDeGestionClientes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Listar = new Cliente();
            int menu = 0;

            while (menu != 3)
            {   
                System.Console.WriteLine("------------------Sistema de Clientes--------------------\n1. Crear Cliente\n2. Listar Clientes\n3. Salir");
                
                menu = int.Parse(Console.ReadLine()!);
                Menu(menu,Listar);
                Console.ReadLine();
                Limpiar();
            }



        }

        static void Menu(int entrada,Cliente Listar)
        {
            string name = "";
            string cell = "";
            string email = "";
            
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
                case 2:
                    Console.WriteLine("----------Lista de Clientes-----------");
                    Listar.Listandocliente();

                    break;
                case 3:
                    Console.WriteLine("Saliendo del Programa");
                    return;
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
                System.Console.WriteLine("[!] No se pudo limpiar la consola. Probablemente no hay consola disponible.");
            }
        }

        static string Indata(string entrada)
        {
            Console.WriteLine(entrada);

            return Console.ReadLine()!;
        }
    }
    class Cliente : Persona
    {

        private static List<Cliente> listclientes = [];
        public Cliente() : base ()
        {
        
        }

        public Cliente(string Nombre, string Email, string Telefono) : base (Nombre,Email,Telefono)
        {
            
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

        public void ShowInfo() => System.Console.WriteLine($"Nombre: {Nombre}\nCorreo: {Email}\nTelefono: {Telefono}\n");
    }
}
