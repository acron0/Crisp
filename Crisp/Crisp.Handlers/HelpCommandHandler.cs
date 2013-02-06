using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crisp.Interfaces;

namespace Crisp.Handlers
{
    class HelpCommandHandler : ICommandHandler
    {
        public void Run(string command, string[] args)
        {
            System.Console.WriteLine("Available commands");
        }
    }
}
