using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crisp.Interfaces;
using Crisp.Handlers;

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

        internal bool isRunning_ = false;

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
            RegisterCommandHandler<HelpCommandHandler>(HelpCommandHandler.Command, "Displays this information.");
            RegisterCommandHandler<ExitCommandHandler>(ExitCommandHandler.Command, "Exits the application.");
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

        /// <summary>
        /// Starts the Crisp console loop.
        /// </summary>
        public void Run()
        {
            isRunning_ = true;

            // intro
            if (!string.IsNullOrWhiteSpace(Introduction))
                System.Console.Write(Introduction);

            // enter loop
            while (isRunning_)
            {
                // print
                System.Console.Write(CursorPrefix + ">");

                string input = System.Console.ReadLine();
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
                        System.Console.WriteLine("An exception occurred: " + ex.Message);
                    }
                }
            }
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
        /// Binds a command to a command handler
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        private void RegisterCommandHandler<T>(string command, string description) where T : ICommandHandler, new()
        {
            if (commandHandlers_.Any(c=>c.CommandKey == command))
                throw new ArgumentException("The '"+command+"' command is already registered.");

            commandHandlers_.Add(new Command() {
                CommandKey = command, 
                Description = description, 
                Handler = new T() } );
        }

        /// <summary>
        /// Finds the appropriate handler and performs
        /// </summary>
        /// <param name="input"></param>
        private void PerformCommand(string input)
        {
            string[] commandStrings = input.Split(' ');
            Command command = commandHandlers_.Find(c => c.CommandKey == commandStrings[0]);

            if (command == null)
                throw new ArgumentException("Command '" + commandStrings[0] + "' not recognised.");

            command.Handler.Run(this, commandStrings[0], commandStrings.Skip(1).ToArray());
        }

        #endregion
    }
}
