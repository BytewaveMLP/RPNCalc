using System;
using System.Collections.Generic;
using System.Linq;

namespace RPNCalc {
	/// <summary>
	/// Helper class which registers all commands and constants used in RPNCalc.
	/// </summary>
	class CalculatorCommands {
		/// <summary>
		/// Helper function to pop off 2 values from the stack.
		/// </summary>
		/// <param name="stack">The stack to pop elements off of</param>
		/// <returns>An array containing the top 2 elements from the stack</returns>
		/// <see cref="GetOperands(Stack{double}, int)"/>
		static double[] GetOperands(Stack<double> stack) {
			return GetOperands(stack, 2);
		}

		/// <summary>
		/// Helper function to pop off numOperands values from the stack.
		/// </summary>
		/// <param name="stack">The stack to pop elements off of</param>
		/// <param name="numOperands">The number of elements to pop off</param>
		/// <returns>An array containing the popped elements</returns>
		static double[] GetOperands(Stack<double> stack, int numOperands) {
			if (stack.Count < numOperands) {
				Console.Error.WriteLine("Missing operand(s).");
				return null;
			}

			double[] arr = new double[numOperands];

			for (int i = arr.Length - 1; i >= 0; i--) {
				arr[i] = stack.Pop();
			}

			return arr;
		}

		/// <summary>
		/// Reigsters the commands for RPNCalc.
		/// </summary>
		/// <param name="commands">The current CommandsResolver object.</param>
		public static void Register(CommandsResolver commands) {
			commands.RegisterCommand("rot", "r", (stack) => {
				if (stack.Count < 2) {
					Console.Error.WriteLine("Not enough elements to rotate stack.");
					return stack;
				}

				Stack<double> newStack = new Stack<double>();

				newStack.Push(stack.Pop());

				foreach (double num in stack.Reverse()) {
					newStack.Push(num);
				}

				return newStack;
			});

			commands.RegisterCommand("swp", "s", (stack) => {
				if (stack.Count < 2) {
					Console.Error.WriteLine("Cannot swap items without 2 items to swap.");
					return stack;
				}

				double[] nums = GetOperands(stack);

				stack.Push(nums[1]);
				stack.Push(nums[0]);

				return stack;
			});

			commands.RegisterCommand("dup", "d", (stack) => {
				if (stack.Count < 1) {
					Console.Error.WriteLine("Cannot duplicate items from an empty stack.");
					return stack;
				}

				stack.Push(stack.Peek());

				return stack;
			});

			commands.RegisterCommand("print", "p", (stack) => {
				if (stack.Count == 0) {
					Console.WriteLine("Empty stack.");
					return stack;
				}

				int counter = stack.Count - 1;

				foreach (double num in stack.Reverse()) {
					Console.WriteLine($"{counter} : {num:0.##########}");
					counter--;
				}

				return stack;
			});

			commands.RegisterCommand("pop", "x", (stack) => {
				if (stack.Count == 0) {
					Console.Error.WriteLine("Cannot pop from an empty stack.");
					return stack;
				}

				stack.Pop();

				return stack;
			});

			commands.RegisterCommand("clear", "c", (stack) => {
				stack.Clear();

				return stack;
			});

			commands.RegisterCommand("add", "+", (stack) => {
				double[] nums = GetOperands(stack);

				if (nums == null)
					return stack;

				stack.Push(nums[0] + nums[1]);

				return stack;
			});

			commands.RegisterCommand("mul", "*", (stack) => {
				double[] nums = GetOperands(stack);

				if (nums == null)
					return stack;

				stack.Push(nums[0] * nums[1]);

				return stack;
			});

			commands.RegisterCommand("sub", "-", (stack) => {
				double[] nums = GetOperands(stack);

				if (nums == null)
					return stack;

				stack.Push(nums[0] - nums[1]);

				return stack;
			});

			commands.RegisterCommand("div", "/", (stack) => {
				double[] nums = GetOperands(stack);

				if (nums == null)
					return stack;

				stack.Push(nums[0] / nums[1]);

				return stack;
			});

			commands.RegisterCommand("pow", "^", (stack) => {
				double[] nums = GetOperands(stack);

				if (nums == null)
					return stack;

				stack.Push(Math.Pow(nums[0], nums[1]));

				return stack;
			});

			commands.RegisterCommand("sin", (stack) => {
				double[] nums = GetOperands(stack, 1);

				if (nums == null)
					return stack;

				stack.Push(Math.Sin(nums[0]));

				return stack;
			});

			commands.RegisterCommand("cos", (stack) => {
				double[] nums = GetOperands(stack, 1);

				if (nums == null)
					return stack;

				stack.Push(Math.Cos(nums[0]));

				return stack;
			});

			commands.RegisterCommand("tan", (stack) => {
				double[] nums = GetOperands(stack, 1);

				if (nums == null)
					return stack;

				stack.Push(Math.Tan(nums[0]));

				return stack;
			});

			commands.RegisterCommand("cls", "z", (stack) => {
				try {
					Console.Clear();
				} catch {
					Console.WriteLine("cls is not supported here");
				}

				return stack;
			});

			commands.RegisterCommand("log10", "log", (stack) => {
				double[] nums = GetOperands(stack, 1);

				if (nums == null)
					return stack;

				stack.Push(Math.Log10(nums[0]));

				return stack;
			});

			commands.RegisterCommand("loge", "ln", (stack) => {
				double[] nums = GetOperands(stack, 1);

				if (nums == null)
					return stack;

				stack.Push(Math.Log(nums[0]));

				return stack;
			});

			commands.RegisterCommand("logbase", "lb", (stack) => {
				double[] nums = GetOperands(stack, 2);
				
				if (nums == null)
					return stack;

				stack.Push(Math.Log(nums[1], nums[0]));

				return stack;
			});

			commands.RegisterCommand("help", "h", (stack) => {
				Console.WriteLine("STACK OPERATIONS");
				Console.WriteLine("[number] - push a number onto the stack");
				Console.WriteLine("rot, r - rotate the stack (pop top off, move to bottom)");
				Console.WriteLine("swp, s - swap the top two elements on the stack");
				Console.WriteLine("dup, d - duplicate the top number on the stack");
				Console.WriteLine("print, p - show the current stack contents");
				Console.WriteLine("pop, x - pop the last element off the stack");
				Console.WriteLine("clear, c - clear the stack");
				Console.WriteLine();
				Console.WriteLine("ARITHMETIC AND FUNCTIONS");
				Console.WriteLine("mul, * - multiply the top two numbers on the stack; push result");
				Console.WriteLine("add, + - add the top two numbers on the stack");
				Console.WriteLine("div, / - divide the top two numbers (1 / 0)");
				Console.WriteLine("sub, - - subtract the top two numbers (1 - 0)");
				Console.WriteLine("pow, ^ - raise the second top number to the top number (1 ^ 0)");
				Console.WriteLine("sin, cos, tan - trig functions");
				Console.WriteLine("log10, log - take the log base 10 of the top number");
				Console.WriteLine("loge, ln - take the natural log of the top number");
				Console.WriteLine("logbase, lb - take the log base (1) of (0)");
				Console.WriteLine();
				Console.WriteLine("CALCULATOR COMMANDS");
				Console.WriteLine("cls, z - clear the screen");
				Console.WriteLine("help, h - show this help");
				Console.WriteLine("exit - exit the calculator");
				Console.WriteLine();
				Console.WriteLine("CONSTANTS");
				Console.WriteLine("pi, e");

				return stack;
			});

			commands.RegisterCommand("exit", (stack) => {
				return null;
			});
		}

		/// <summary>
		/// Registers the constants used in RPNCalc.
		/// </summary>
		/// <param name="commands">The current CommandsResolver object.</param>
		public static void RegisterConstants(CommandsResolver commands) {
			commands.RegisterConstant("pi", Math.PI);
			commands.RegisterConstant("e", Math.E);
		}
	}
}
