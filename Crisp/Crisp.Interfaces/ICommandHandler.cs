using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crisp.Interfaces
{
    public interface ICommandHandler
    {
        void   Run(Console console, string command, string[] args);
        string Help(Console console, string command);
    }
}
