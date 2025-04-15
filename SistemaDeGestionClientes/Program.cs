using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Channels;

namespace SistemaDeGestionClientes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Usuario = new Cliente();
            var empleado = new Empleado();

            int menu = 0;

            while (menu != 4)
            {   
                ShowMenu();
                
                menu = InNumero();
                switch(menu)
                {

                    case 1:
                    Limpiar();
                    MenuCliente();
                    menu = InNumero();
                    MenuCliente(menu,Usuario);
                    break;

                    case 2:
                    Limpiar();
                    MenuEmpleado();
                    menu = InNumero();
                    MenuEmpleado(menu,empleado);
                    break;

                    case 3:
                    Limpiar();
                    MenuInventario();
                    break;

                    case 4:
                    Console.WriteLine("Saliendo del Programa");
                    return;

                    default:
                    Console.WriteLine("[!] Numero Invalido");
                    break;
                }
                Limpiar();

            }

        }


        static void ShowMenu()
        {
            Console.WriteLine("------------------Sistema de Gestion de Supermercado--------------------");
            Console.WriteLine("1.Clientes\n2.Empleados\n3.Inventario\n4.Salir");
        }
        static void MenuCliente()
        {
            Console.WriteLine("------------------Sistema de Clientes--------------------");
            Console.WriteLine("1.Crear Clinete\n2.Listar Cliente\n3.Comprar Producto\n4.Ver Compras\n5.Eliminar Cliente\n6.Salir");
        }
        static void MenuEmpleado() 
        {
            Console.WriteLine("------------------Sistema de Empleados--------------------");
            Console.WriteLine("1.Crear Empleado\n2.Listar Empleado\n3.Eliminar Empleado\n4.Calcular Salario Anual\n5.Salir");
        }
        static void MenuInventario()
        {
            Console.WriteLine("------------------INVENTARIO--------------------");
            Console.WriteLine("1.Agregar Productos\n2.Listar productos\n3.Salir");
        }

        static void MenuCliente(int entrada, Cliente Consumidor) 
        {
            switch(entrada)
            {
                case 1:
                    string name = Indata("Ingrese su Nombre");
                    string cell = Indata("Ingrese su Numero de Telefono");
                    string email = Indata("Ingrese su Correo");

                    Cliente clientes = new(name,email,cell);
                    Consumidor.SaveClient(clientes);

                    Console.WriteLine("Cliente Añadido");
                    break;

                case 2:
                    Console.WriteLine("----------Lista de Clientes-----------");
                    Consumidor.Listandocliente();

                    break;

                case 3:

                    string buscar = Indata("Escriba el Nombre del cliente");
                    var existecliente = Consumidor.Listclientes.Any(c => c.Name == buscar);

                    if(!existecliente) 
                    {
                        Console.WriteLine("Cliente No Existente\nVolviendo al menu Principal");
                        break; //Saliendo del case 5
                    }

                    while(existecliente){
                        Console.WriteLine("BIENVENIDO AL SISTEMA DE COMPRAS");
                        
                        var Encontrado = Consumidor.Listclientes.FirstOrDefault(c => c.Name == buscar);

                        name = Indata("Ingrese el Nombre del producto");
                        decimal Precio = Indecimal("Ingrese el precio del Producto");
                        int Cantidad = decimal.ToInt32(Indecimal("Ingrese la cantidad del Producto"));

                        var Product = new Producto(name,Precio,Cantidad);

                        switch(Encontrado)
                        {
                            case not null:
                            Consumidor.AgregarCompra(Product, Encontrado);
                            Console.WriteLine("Compra Guardada");
                            break;

                            default:
                            Console.WriteLine("Cliente no existente.");
                            break;
                        }
                        
                        string comprando = Indata("Selecione la opcion:\n1.Continuar Comprando\n2.Salir del Sistema");
                        switch(comprando)
                        {
                            case "1":
                            Limpiar();
                            break;

                            case "2":
                            existecliente = false;
                            break;

                            default:
                            Console.WriteLine("[!] Ingrese una opcion valida");
                            break;
                        }

                    }
                    break; //saliendo del case 5        

                case 4:
                    name = Indata("Escriba el nombre del cliente");
                    Consumidor.ListandoProductos(name);
                    break;
                
                case 5:
                    name = Indata("Ingrese el Cliente a Eliminar");
                    Consumidor.Listclientes.RemoveAll(c => c.Name == name);

                    Console.WriteLine("Cliente Eliminado");
                    break;

                case 6:
                    Console.WriteLine("Saliendo del Programa");
                return;

                default: 
                    Console.WriteLine("Valor no valido");
                    break;
            }
        }
        static void MenuEmpleado(int entrada,Empleado employee)
        {
            
            switch (entrada)
            {
                 
                case 5:
                    Console.WriteLine("Saliendo del Programa");
                return;
                case 1:
                    string name = Indata("Ingrese Su Nombre");
                    string cell = Indata("Ingrese su Numero de Telefono");
                    string email = Indata("Ingrese su Correo");
                    string Cargo = Indata("Ingrese su Posicion");
                    decimal Salario = Indecimal("Ingrese su Sueldo");

                    Empleado info = new (name,email,cell,Cargo,Salario);
                    employee.AgregarEmpleado(info);

                    Console.WriteLine("Empleado Añadido");

                    break;
                case 2:
                    Console.WriteLine("----------Lista de Empleados-----------");
                    employee.ListandoEmpleados();

                    break;

                    case 3:
                        name = Indata("Ingrese el Empleado a Eliminar");
                        employee.Empleados.RemoveAll(c => c.Name == name);

                        Console.WriteLine("Empleado Eliminado");
                        break;

                    case 4:
                        name = Indata("Ingrese el Nombre del Empleado");
                        employee.CalcularSalarioAnual(name);
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
            catch(InvalidOperationException)
            {
                Console.WriteLine("[!] Consola No Disponible");
            }
        }
        static string Indata(string entrada)
        {
           string salida; 

            do{
                Console.WriteLine(entrada);
                salida = Console.ReadLine()!;

                if(string.IsNullOrWhiteSpace(salida))
                {
                    Console.WriteLine("La entrada no puede estar vacia. Intenta Nuevamente");
                }
            }
            while(string.IsNullOrWhiteSpace(salida));
            return salida;
        }
        static decimal Indecimal(string entrada)
        {
            string busqueda = Indata(entrada);

           do{
             if(decimal.TryParse(busqueda, out decimal salida))
            {
                return salida;
            }
            else
            {
                Console.WriteLine("La entrada debe de ser un numero valido.");
            }
           }while(true);
            
        
        }
        static int InNumero()
        {
            string entrada = Indata("\nSeleccione una opcion");

            do{
                if(int.TryParse(entrada, out int salida))
                {
                    return salida;
                }
                else
                {
                    Console.WriteLine("[!] Numero No valido");
                }

            }while(true);

        }
    }
    class Cliente : Persona
    {

        public List<Cliente> Listclientes {get; private set;} = [];
        private readonly List<Producto> Productos;

        public string Name{get; private set;}

        public Cliente() : base ()
        {
            Productos = [];
            Name = "";
        }

        public Cliente(string Nombre, string Email, string Telefono) : base (Nombre, Email, Telefono)
        {
            Productos = [];
            Name = Nombre;
        }
        

        public void AgregarCompra(Producto producto,Cliente cliente)
        {
            cliente.Productos.Add(producto);
            
        }

        public void Listandocliente()
        {
            foreach(var salida in Listclientes)
            {
                salida.ShowInfo();
            }

            

        }
        public void ListandoProductos(string name)
        {
            var EncontrarCliente = Listclientes.FirstOrDefault(c => c.Name == name);

            if(EncontrarCliente != null)
            {
                var MostrarP= EncontrarCliente.Productos;

                foreach(var imprimir in MostrarP)
                {
                    imprimir.ShowInfo();
                }
            }
            else
            {
                Console.WriteLine("Cliente No Encontrado/Existente");
            }
        }
        public void SaveClient(Cliente entrada)
        {
            Listclientes.Add(entrada);
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Nombre: {Nombre}\nCorreo: {Email}\nTelefono: {Telefono}\n");

        }
    
    }
    class Empleado : Persona
    {
        private string Cargo;
        private decimal Salario{get; set;}
        public List<Empleado> Empleados {get; private set;} = [];
        public string Name{get; private set;}

        public Empleado() : base ()
        {
            Cargo = "";
            Salario = 0;
            Name = "";
        }

        public Empleado(string Nombre, string Email, string Telefono, string Cargo, decimal Salario) : base (Nombre, Email, Telefono)
        {
            this.Cargo = Cargo;
            this.Salario = Salario;
            Name = Nombre;
        }

        public decimal CalcularSalarioAnual(string name)
        {
            
            var buscarempleado = Empleados.FirstOrDefault(c => c.Name == name);
            decimal salarioanual = 0;
            switch(buscarempleado)
            {
                case not null:
                salarioanual = buscarempleado.Salario * 12;

                Console.WriteLine($"El salario Anual del empleado {name} es: {salarioanual}");
                return salarioanual;

                default:
                    Console.WriteLine("[!] Empleado No Encontrado/Existente");
                return salarioanual;

            }
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Nombre: {Nombre}\nCorreo: {Email}\nTelefono: {Telefono}");
            Console.WriteLine($"Cargo: {Cargo}\nSalario: {Salario}\n");
        }

        public void AgregarEmpleado(Empleado empleado)
        {
            Empleados.Add(empleado);
        }
        public void ListandoEmpleados()
        {
            foreach(var salida in Empleados)
            {
                salida.ShowInfo();
            }
        }

    }

    class Producto 
    {
        private string Nombre{get; set;}
        private decimal Precio {get; set;}
        private int Cantidad {get; set;}

        public Producto()
        {

            Nombre = "";
            Precio = 0;
            Cantidad = 0;

        }
        public Producto(string Nombre, decimal Precio, int Cantidad)
        {
            this.Nombre = Nombre;
            this.Precio = Precio;
            this.Cantidad = Cantidad;
        }

        public void ShowInfo() => Console.WriteLine($"Nombre: {Nombre}\nPrecio: {Precio}\nCantidad: {Cantidad}");

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
