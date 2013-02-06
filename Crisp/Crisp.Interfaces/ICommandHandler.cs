using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crisp.Interfaces
{
    public interface ICommandHandler
    {
        void Run(string command, string[] args);
    }
}
