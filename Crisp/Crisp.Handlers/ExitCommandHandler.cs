using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crisp.Interfaces;

namespace Crisp.Handlers
{
    class ExitCommandHandler : ICommandHandler
    {
        static public readonly string Command = "exit";

        public void Run(Console console, string command, string[] args)
        {
            console.Exit();
        }

        public string Help(Console console, string command)
        {
            throw new NotImplementedException();
        }
    }
}
