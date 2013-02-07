Crisp
=

A C# .NET framework for writing interactive shells / REPLs.

**Examples**  
Getting started is extremely straight forward. This will start the REPL with just two commands: 'help' and 'exit'

    static void Main(string[] args)
    {
        Crisp.Console crispApp = new Crisp.Console("Welcome to Crisp", "#");
        crispApp.Run();
    }

-
    Welcome to Crisp
    #>help
     Available commands:
        help    Type 'help' followed by the command you'd like further information about.
        exit    Exits the application.

    #>_

You add commands by registering CommandHandlers (ICommandHandler).

    crispApp.RegisterCommandHandler(new LoadServiceCommandHandler(), "load", "Loads a service.");
    crispApp.RegisterCommandHandler(new ListServiceCommandHandler(), "list", "Lists all available services.");
    crispApp.RegisterCommandHandler(new ShowServiceCommandHandler(), "show", "Shows currently running services.");
    crispApp.RegisterCommandHandler(new KillServiceCommandHandler(), "kill", "Kills the specified service.");

Commands can be called with options, switches and parameters. The interface also provides a Help() function 
that can be implemented to provide further information on a command. This is called with "help [command]".

**TODO**
 * Name completion
 * Session logging
 * Remote connections
