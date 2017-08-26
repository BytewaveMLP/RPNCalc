using System;
using System.Collections.Generic;

namespace RPNCalc {
	class CommandsResolver {
		private Dictionary<string, Func<Stack<double>, Stack<double>>> commands;

		public CommandsResolver() {
			commands = new Dictionary<string, Func<Stack<double>, Stack<double>>>();
		}

		/// <summary>
		/// Registers a new command without an alias.
		/// </summary>
		/// <param name="name">The name for the new command</param>
		/// <param name="handler">A callback function to handle the command</param>
		/// <see cref="RegisterCommand(string, string, Func{Stack{double}, Stack{double}})"/>
		public void RegisterCommand(string name, Func<Stack<double>, Stack<double>> handler) {
			RegisterCommand(name, null, handler);
		}

		/// <summary>
		/// Registers a new command.
		/// </summary>
		/// <param name="name">The name for the new command</param>
		/// <param name="alias">An optional alias to give the new command</param>
		/// <param name="handler">A callback function to handle the command - this accepts the current stack as a parameter, and returns the stack after modifying it</param>
		public void RegisterCommand(string name, string alias, Func<Stack<double>, Stack<double>> handler) {
			if (commands.TryGetValue(name, out _)) {
				throw new ArgumentException($"A command is already registered with the key {name}.");
			}

			if (alias != null && commands.TryGetValue(alias, out _)) {
				throw new ArgumentException($"A command is already registered with the key {alias}.");
			}

			commands.Add(name, handler);

			if (alias != null) {
				commands.Add(alias, handler);
			}
		}

		/// <summary>
		/// Registers a new constant.
		/// </summary>
		/// <remarks>
		/// This is shorthand for a command that pushes some value onto the stack and exits.
		/// </remarks>
		/// <param name="name">The name for the constant</param>
		/// <param name="value">The value of the constant</param>
		public void RegisterConstant(string name, double value) {
			RegisterCommand(name, (stack) => {
				stack.Push(value);
				return stack;
			});
		}

		/// <summary>
		/// Resolves a command name or alias into the command's handler.
		/// </summary>
		/// <param name="name">The name or alias of the command to look up</param>
		/// <returns>The handler function for the command, or null if the command doesn't exist</returns>
		public Func<Stack<double>, Stack<double>> ResolveCommand(string name) {
			Func<Stack<double>, Stack<double>> handler;

			if (!commands.TryGetValue(name, out handler)) {
				return null;
			}

			return handler;
		}
	}
}
