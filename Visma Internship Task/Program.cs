using Services;
using System;

namespace Visma_Internship_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            var methods = new Methods();
            methods.Setup();
            var program = new Program();

            Console.WriteLine("Welcome to VISMA book library!");
            Console.WriteLine();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("The commands are: Add, Take, Return, List, Delete.");
            Console.WriteLine("Your action:");
            var action = Console.ReadLine();

            while (true)
            {
                if (action == "Add")
                {
                    methods.Add();
                    Console.WriteLine("Do you want to proceed? (Yes/No)");
                    var answer = Console.ReadLine();
                    if (answer.ToLower() == "no")
                        break;
                    else if (answer.ToLower() == "yes")
                    {
                        Console.WriteLine("Your action:");
                        action = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Wrong action. Try again (yes/no): ");
                        action = Console.ReadLine();
                    }
                }
                else if (action == "Take")
                {
                    methods.Take();
                    Console.WriteLine("Do you want to proceed? (Yes/No)");
                    var answer = Console.ReadLine();
                    if (answer.ToLower() == "no")
                        break;
                    else if (answer.ToLower() == "yes")
                    {
                        Console.WriteLine("Your action:");
                        action = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Wrong action. Try again (yes/no): ");
                        action = Console.ReadLine();
                    }
                }
                else if (action == "Return")
                {
                    methods.Return();
                    Console.WriteLine("Do you want to proceed? (Yes/No)");
                    var answer = Console.ReadLine();
                    if (answer.ToLower() == "no")
                        break;
                    else if (answer.ToLower() == "yes")
                    {
                        Console.WriteLine("Your action:");
                        action = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Wrong action. Try again (yes/no): ");
                        action = Console.ReadLine();
                    }
                }
                else if (action == "List")
                {
                    methods.List();
                    Console.WriteLine("Do you want to proceed? (Yes/No)");
                    var answer = Console.ReadLine();
                    if (answer.ToLower() == "no")
                        break;
                    else if (answer.ToLower() == "yes")
                    {
                        Console.WriteLine("Your action:");
                        action = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Wrong action. Try again (yes/no): ");
                        action = Console.ReadLine();
                    }
                }
                else if (action == "Delete")
                {
                    methods.Delete();
                    Console.WriteLine("Do you want to proceed? (Yes/No)");
                    var answer = Console.ReadLine();
                    if (answer.ToLower() == "no")
                        break;
                    else if (answer.ToLower() == "yes")
                    {
                        Console.WriteLine("Your action:");
                        action = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Wrong action. Try again (yes/no): ");
                        action = Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Wrong command. Try again: ");
                    action = Console.ReadLine();
                }
            }

            methods.Close();
        }
    }
}
