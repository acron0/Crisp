using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crisp.Interfaces;
using Crisp.Handlers;
using System.IO;

namespace Crisp
{
    public class Console
    {
        #region Command Class
        internal class Command
        {
            public string CommandKey;
            public string Description;
            public ICommandHandler Handler;
        }
        #endregion

        #region Variables

        private List<Command> commandHandlers_ = new List<Command>();

        private bool isRunning_ = false;
        private string initialCommand_ = null;

        #endregion

        #region Properties

        public string CursorPrefix { get; private set; }
        public string Introduction { get; private set; }

        internal Command[] Commands { get { return commandHandlers_.ToArray(); } }

        #endregion

        #region Public Methods

        public Console()
        {
            // register default handlers
            RegisterCommandHandler(new HelpCommandHandler(), HelpCommandHandler.Command, "Type '" + HelpCommandHandler.Command + "' followed by the command you'd like further information about.");
            RegisterCommandHandler(new ExitCommandHandler(), ExitCommandHandler.Command, "Exits the application.");
        }

        public Console(string introMessage) 
            : this()
        {
            Introduction = introMessage;
        }

        public Console(string introMessage, string cursorPrefix)
            : this()
        {
            Introduction = introMessage;
            CursorPrefix = cursorPrefix;
        }
        public Console(string introMessage, string cursorPrefix, string initialCommand)
            : this()
        {
            Introduction = introMessage;
            CursorPrefix = cursorPrefix;
            initialCommand_ = initialCommand;
        }

        /// <summary>
        /// Starts the Crisp console loop.
        /// </summary>
        public void Run()
        {
            isRunning_ = true;

            // intro
            if (!string.IsNullOrWhiteSpace(Introduction))
            {
                System.Console.Write(Introduction);
                System.Console.Write(System.Environment.NewLine);
            }

            // enter loop
            while (isRunning_)
            {
                // print
                System.Console.Write(CursorPrefix + ">");

                string input = null;
                if(!string.IsNullOrWhiteSpace(initialCommand_))
                {
                    input = initialCommand_;
                    System.Console.Write(initialCommand_+System.Environment.NewLine);
                    initialCommand_ = null;
                }
                else
                {
                    input = System.Console.ReadLine();
                }

                if (!string.IsNullOrWhiteSpace(input))
                {
                    try
                    {
                        System.Console.Write(" ");
                        PerformCommand(input);
                        System.Console.Write(System.Environment.NewLine);
                    }
                    catch (System.Exception ex)
                    {
                        System.Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.Error.WriteLine("An exception occurred: " + ex.Message);
                        System.Console.ResetColor();
                    }
                }
            }
        }

        /// <summary>
        /// Binds a command to a command handler
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        public void RegisterCommandHandler(ICommandHandler handler, string command, string description)
        {
            if (commandHandlers_.Any(c => c.CommandKey == command))
                throw new ArgumentException("The '" + command + "' command is already registered.");

            commandHandlers_.Add(new Command()
            {
                CommandKey = command,
                Description = description,
                Handler = handler
            });
        }

        #endregion

        #region Internal Methods

        internal void Exit()
        {
            isRunning_ = false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Finds the appropriate handler and performs
        /// </summary>
        /// <param name="input"></param>
        private void PerformCommand(string input)
        {
            string[] commandStrings = input.Split(' ');
            string commandKey = commandStrings[0].ToLower();
            Command command = commandHandlers_.Find(c => c.CommandKey == commandKey);

            if (command == null)
                throw new ArgumentException("Command '" + commandKey + "' not recognised.");

            command.Handler.Run(this, commandKey, commandStrings.Skip(1).ToArray());
        }

        #endregion
    }
}
