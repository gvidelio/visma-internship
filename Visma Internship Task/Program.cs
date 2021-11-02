using Services;
using System;

namespace Visma_Internship_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            var actions = new Actions();
            actions.Setup();

            Console.WriteLine("Welcome to VISMA book library!\n");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("The commands are: Add, Take, Return, List, Delete.\n");
            Console.WriteLine("Your action:");
            var action = Console.ReadLine();

            //check for the required action
            while (true)
            {
                if (action == "Add")
                {
                    actions.Add();
                    Console.WriteLine("Your action:");
                    action = Console.ReadLine();
                }
                else if (action == "Take")
                {
                    actions.Take();
                    Console.WriteLine("Your action:");
                    action = Console.ReadLine();
                }
                else if (action == "Return")
                {
                    actions.Return();
                    Console.WriteLine("Your action:");
                    action = Console.ReadLine();
                }
                else if (action == "List")
                {
                    actions.List();
                    Console.WriteLine("Your action:");
                    action = Console.ReadLine();
                }
                else if (action == "Delete")
                {
                    actions.Delete();
                    Console.WriteLine("Your action:");
                    action = Console.ReadLine();
                }
                else if (action == "Exit")
                {
                    actions.Close();
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong command. Try again: ");
                    action = Console.ReadLine();
                }
            }
            actions.Close();
            
        }
    }
}
