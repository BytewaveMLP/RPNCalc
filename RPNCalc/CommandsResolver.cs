﻿using System;
using System.Collections.Generic;

namespace RPNCalc {
	class CommandsResolver {
		private Dictionary<string, Func<Stack<double>, Stack<double>>> commands;

		public CommandsResolver() {
			commands = new Dictionary<string, Func<Stack<double>, Stack<double>>>();
		}

		public void RegisterCommand(string name, Func<Stack<double>, Stack<double>> handler) {
			RegisterCommand(name, null, handler);
		}

		public void RegisterCommand(string name, string alias, Func<Stack<double>, Stack<double>> handler) {
			commands.Add(name, handler);

			if (alias != null) {
				commands.Add(alias, handler);
			}
		}

		public Func<Stack<double>, Stack<double>> ResolveCommand(string name) {
			Func<Stack<double>, Stack<double>> handler;

			if (!commands.TryGetValue(name, out handler)) {
				return null;
			}

			return handler;
		}
	}
}
