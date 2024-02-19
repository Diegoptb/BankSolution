using BankConsole;
using System.Text.RegularExpressions;


if (args.Length == 0)
    EmailService.SendMail();
else
    ShowMenu();

void ShowMenu()
{
    Console.Clear();
    Console.WriteLine("Selecciona una opción:");
    Console.WriteLine("1 - Crear un Usuario nuevo.");
    Console.WriteLine("2 - Eliminar un Usuario existente.");
    Console.WriteLine("3 - Salir.");

    int option = 0;
    do
    {
        string input = Console.ReadLine();

        if (!int.TryParse(input, out option))
            Console.WriteLine("Debes ingresar un número (1, 2 o 3).");
        else if (option > 3)
            Console.WriteLine("Debes ingresar un número válido (1, 2 o 3).");
    } 
    while (option == 0 || option > 3);

    switch (option)
    {
        case 1:
            CreateUser();
            break;
        case 2:
            DeleteUser();
            break;
        case 3:
            Environment.Exit(0);
            break;
    }
}
void CreateUser()
{
    Console.Clear();
    Console.WriteLine("Ingresa la información del usuario:");

    int ID = 0;
    do
    {
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out ID) || ID <= 0)
        {
            Console.WriteLine("Debes ingresar un entero positivo para el ID.");
            ID = 0;
        }
        else if (Storage.CheckUserIdExists(ID))
        {
            Console.WriteLine("Ya existe un usuario con ese ID. Introduce un ID diferente.");
            ID = 0;
        }
    } while (ID == 0);

    Console.Write("Nombre: ");
    string name = Console.ReadLine();

    Console.Write("Email: ");
    string email = Console.ReadLine();

    static bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
        return Regex.IsMatch(email, pattern);
    }

    while (!IsValidEmail(email))
    {
        Console.WriteLine("Debes ingresar un correo electrónico válido.");
        Console.Write("Email: ");
        email = Console.ReadLine();
    }

    


    Console.Write("Saldo: ");
    decimal balance;
    while (!decimal.TryParse(Console.ReadLine(), out balance) || balance < 0)
    {
        Console.WriteLine("Debes ingresar un valor decimal positivo para el saldo.");
        Console.Write("Saldo: ");
    }


    char userType;
    do
    {
        Console.Write("Escribe 'c' si el usuario es Cliente, 'e' si es Empleado: ");
        string input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input) && input.Length == 1)
        {
            userType = char.ToLower(input[0]);
            if (userType != 'c' && userType != 'e')
            {
                Console.WriteLine($"Entrada inválida '{input}'. Por favor, ingresa 'c' para Cliente o 'e' para Empleado.");
            }
        }
        else
        {
            Console.WriteLine("Por favor, ingresa exactamente un carácter.");
            userType = '\0';
        }
    } while (userType != 'c' && userType != 'e');

    User newUser;
    if (userType == 'c')
    {
        Console.Write("Régimen fiscal: ");
        char taxRegime = char.Parse(Console.ReadLine());
        newUser = new Client(ID, name, email, balance, taxRegime);
    }
    else
    {
        Console.Write("Departamento: ");
        string department = Console.ReadLine();
        newUser = new Employee(ID, name, email, balance, department);
    }
    Storage.AddUser(newUser);

    Console.WriteLine("Usuario creado.");
    Thread.Sleep(2000);
    ShowMenu();
}

void DeleteUser()
{
    Console.Clear();
    Console.Write("Ingresa el ID del usuario a eliminar: ");
    int ID;
    while (!int.TryParse(Console.ReadLine(), out ID) || ID <= 0 || !Storage.CheckUserIdExists(ID))
    {
        Console.WriteLine("Debes ingresar un ID válido que exista en el registro.");
        Console.Write("Ingresa el ID del usuario a eliminar: ");
    }


    string result = Storage.DeleteUser(ID);

    if (result.Equals("Success"))
    {
        Console.Write("Usuario eliminado.");
        Thread.Sleep(2000);
        ShowMenu();
    }
}



