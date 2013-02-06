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
        #region Variables

        private Dictionary<string, ICommandHandler> commandHandlers_ = new Dictionary<string, ICommandHandler>();

        #endregion

        #region Properties

        public string CommandLinePrefix { get; private set; }
        public string Introduction { get; private set; }

        #endregion

        #region Public Methods

        public Console()
        {
            // register default handlers
            RegisterCommandHandler<HelpCommandHandler>("help");
        }

        public Console(string introMessage) : this()
        {
            Introduction = introMessage;
        }

        /// <summary>
        /// Starts the Crisp console loop.
        /// </summary>
        public void Run()
        {
            // intro
            if (!string.IsNullOrWhiteSpace(Introduction))
                System.Console.Write(Introduction);

            // enter loop
            for (; ; )
            {
                // print
                System.Console.Write("#" + CommandLinePrefix + ">");

                string input = System.Console.ReadLine();
                if (input == "exit")
                    break;
                else if (!string.IsNullOrWhiteSpace(input))
                {
                    try
                    {
                        System.Console.Write(" ");
                        PerformCommand(input);
                    }
                    catch (System.Exception ex)
                    {
                        System.Console.WriteLine("An exception occurred: " + ex.Message);
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Binds a command to a command handler
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        private void RegisterCommandHandler<T>(string command) where T : ICommandHandler, new()
        {
            if (command == "exit")
                throw new ArgumentException("The 'exit' command is reserved!");

            commandHandlers_.Add(command, new T());
        }

        /// <summary>
        /// Finds the appropriate handler and performs
        /// </summary>
        /// <param name="input"></param>
        private void PerformCommand(string input)
        {
            string[] commandStrings = input.Split(' ');
            ICommandHandler handler;
            if (!commandHandlers_.TryGetValue(commandStrings[0], out handler))
                throw new ArgumentException("Command '" + commandStrings[0] + "' not recognised.");
            handler.Run(commandStrings[0], commandStrings.Skip(1).ToArray());
        }

        #endregion
    }
}
