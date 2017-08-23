using System;
using System.Collections.Generic;
using System.Linq;

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
		/// Helper function to pop off 2 values from the stack.
		/// </summary>
		/// <param name="stack">The stack to pop elements off of</param>
		/// <returns>An array containing the top 2 elements from the stack</returns>
		/// <see cref="GetOperands(Stack{double}, int)"/>
		static double[] GetOperands(Stack<double> stack) {
			return GetOperands(stack, 2);
		}

		static void Main(string[] args) {
			Stack<double> stack = new Stack<double>();

			bool cont = true;

			Console.WriteLine("RPNCalc 1.0, by Bytewave");
			Console.WriteLine();
			Console.WriteLine("h/help for help");
			Console.WriteLine("e/exit to exit");
			Console.WriteLine();

			while (cont) {
				string input = Prompt();

				int counter = stack.Count - 1;

				double[] nums = null;

				if (Double.TryParse(input, out double inputNumber)) {
					if (Double.IsNaN(inputNumber) || Double.IsPositiveInfinity(inputNumber) || Double.IsNegativeInfinity(inputNumber)) {
						Console.Error.WriteLine("Invalid number.");
					} else {
						stack.Push(inputNumber);
					}
				} else {
					switch (input) {
						case "rot":
						case "r": {
								if (stack.Count < 2) {
									Console.Error.WriteLine("Not enough elements to rotate stack.");
									break;
								}

								Stack<double> newStack = new Stack<double>();

								newStack.Push(stack.Pop());

								foreach (double num in stack.Reverse()) {
									newStack.Push(num);
								}

								stack = newStack;

								break;
							}
						case "swp":
						case "s": {
								if (stack.Count < 2) {
									Console.Error.WriteLine("Cannot swap items without 2 items to swap.");
									break;
								}

								nums = GetOperands(stack);

								stack.Push(nums[1]);
								stack.Push(nums[0]);

								break;
							}
						case "dup":
						case "d": {
								if (stack.Count < 1) {
									Console.Error.WriteLine("Cannot duplicate items from an empty stack.");
									break;
								}

								stack.Push(stack.Peek());

								break;
							}
						case "print":
						case "p": {
								foreach (double num in stack.Reverse()) {
									Console.WriteLine($"{counter} : {num}");
									counter--;
								}

								break;
							}
						case "clear":
						case "c": {
								stack.Clear();

								break;
							}
						case "mul":
						case "*": {
								nums = GetOperands(stack);

								if (nums == null)
									break;

								stack.Push(nums[0] * nums[1]);

								break;
							}
						case "add":
						case "+": {
								nums = GetOperands(stack);

								if (nums == null)
									break;

								stack.Push(nums[0] + nums[1]);

								break;
							}
						case "div":
						case "/": {
								nums = GetOperands(stack);

								if (nums == null)
									break;

								stack.Push(nums[0] / nums[1]);

								break;
							}
						case "sub":
						case "-": {
								nums = GetOperands(stack);

								if (nums == null)
									break;

								stack.Push(nums[0] - nums[1]);

								break;
							}
						case "pow":
						case "^": {
								nums = GetOperands(stack);

								if (nums == null)
									break;

								stack.Push(Math.Pow(nums[0], nums[1]));

								break;
							}
						case "sin": {
								nums = GetOperands(stack, 1);

								if (nums == null)
									break;

								stack.Push(Math.Sin(nums[0]));

								break;
							}
						case "cos": {
								nums = GetOperands(stack, 1);

								if (nums == null)
									break;

								stack.Push(Math.Cos(nums[0]));

								break;
							}
						case "tan": {
								nums = GetOperands(stack, 1);

								if (nums == null)
									break;

								stack.Push(Math.Tan(nums[0]));

								break;
							}
						case "cls":
						case "z": {
								try {
									Console.Clear();
								} catch {
									Console.WriteLine("cls is not supported here");
								}

								break;
							}
						case "help":
						case "h": {
								Console.WriteLine("STACK OPERATIONS");
								Console.WriteLine("[number] - push a number onto the stack");
								Console.WriteLine("rot, r - rotate the stack (pop top off, move to bottom)");
								Console.WriteLine("swp, s - swap the top two elements on the stack");
								Console.WriteLine("dup, d - duplicate the top number on the stack");
								Console.WriteLine("print, p - show the current stack contents");
								Console.WriteLine("clear, c - clear the stack");
								Console.WriteLine();
								Console.WriteLine("ARITHMETIC AND FUNCTIONS");
								Console.WriteLine("mul, * - multiply the top two numbers on the stack; push result");
								Console.WriteLine("add, + - add the top two numbers on the stack");
								Console.WriteLine("div, / - divide the top two numbers (1 / 0)");
								Console.WriteLine("sub, - - subtract the top two numbers (1 - 0)");
								Console.WriteLine("pow, ^ - raise the second top number to the top number (1 ^ 0)");
								Console.WriteLine("sin, cos, tan - trig functions");
								Console.WriteLine();
								Console.WriteLine("CALCULATOR COMMANDS");
								Console.WriteLine("cls, z - clear the screen");
								Console.WriteLine("help, h - show this help");
								Console.WriteLine("exit, e - exit the calculator");

								break;
							}
						case "exit":
						case "e": {
								cont = false;
								break;
							}
						default:
							Console.Error.WriteLine("Unknown command or invalid number.");
							break;
					}
				}
			}
		}
	}
}
