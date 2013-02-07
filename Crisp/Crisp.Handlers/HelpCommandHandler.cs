using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crisp.Interfaces;

namespace Crisp.Handlers
{
    class HelpCommandHandler : ICommandHandler
    {
        static public readonly string Command = "help";

        public void Run(Console console, string command, string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("Available commands:");
                foreach (Console.Command c in console.Commands)
                    System.Console.WriteLine("\t" + c.CommandKey + "\t" + c.Description);
            }
            else
            {
                string needHelpWith = args[0];
                Console.Command c = console.Commands.First(cx=>cx.CommandKey == needHelpWith);
                if (c == null)
                {
                    System.Console.WriteLine("Unknown command: " + needHelpWith);
                }
                else
                {
                    try
                    {
                        string helpString = c.Handler.Help(console, needHelpWith);
                        helpString = helpString.Replace("\n","\n\t");
                        System.Console.WriteLine("Displaying further information for the '" + needHelpWith + "' command:" + System.Environment.NewLine);
                        System.Console.WriteLine("\t" + helpString);
                    }
                    catch (System.Exception)
                    {
                        System.Console.WriteLine("There is no further information on the '" + needHelpWith + "' command.");
                    }
                }
            }
        }

        public string Help(Console console, string command)
        {
            return "Type '" + Command + "' followed by\nthe command you'd like further information about.";
        }
    }
}
