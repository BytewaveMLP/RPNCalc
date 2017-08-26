using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RPNCalc {
	class Program {
		/// <summary>
		/// Prompts the user for input using a specified prompt string.
		/// </summary>
		/// <param name="prompt">The prompt string to use</param>
		/// <returns>The string the user entered - "" if null</returns>
		static string Prompt(string prompt) {
			Console.Write(prompt);
			return Console.ReadLine() ?? "";
		}

		/// <summary>
		/// Prompts the user for input using the default prompt string.
		/// </summary>
		/// <returns>The string the user entered - "" if null</returns>
		/// <see cref="Prompt(string)"/>
		static string Prompt() {
			return Prompt("> ");
		}

		static void Main(string[] args) {
			CommandsResolver commands = new CommandsResolver();

			Commands.Register(commands);

			Stack<double> stack = new Stack<double>();

			AssemblyName asm = typeof(Program).Assembly.GetName();

			bool cont = true;

			Console.WriteLine($"{asm.Name} {asm.Version}, by Bytewave");
			Console.WriteLine();
			Console.WriteLine("h/help for help");
			Console.WriteLine("e/exit to exit");
			Console.WriteLine();

			while (cont) {
				string input = Prompt();

				if (Double.TryParse(input, out double inputNumber)) {
					if (Double.IsNaN(inputNumber) || Double.IsPositiveInfinity(inputNumber) || Double.IsNegativeInfinity(inputNumber)) {
						Console.Error.WriteLine("Invalid number.");
					} else {
						stack.Push(inputNumber);
					}
				} else {
					Func<Stack<double>, Stack<double>> resolvedCommand = commands.ResolveCommand(input);

					if (resolvedCommand == null) {
						Console.Error.WriteLine("Command not found / invalid number.");
					} else {
						stack = resolvedCommand(stack);

						if (stack == null) {
							cont = false;
						}
					}
				}
			}
		}
	}
}
