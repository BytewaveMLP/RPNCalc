# RPNCalc

[![standard-readme compliant](https://img.shields.io/badge/readme%20style-standard-brightgreen.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)

> A console-based RPN calculator, written in C#

RPNCalc is a simple C# RPN calculator I wrote because I wanted to. There really isn't much more to it than that. I wanted to experiment with C#.NET, so I wrote an RPN-based calculator using `System.Collections.Generic.Stack<T>`.

# Install

Just make sure you have a recent version of the .NET Framework installed. If you're running a recent version of Windows, this should already be the case.

# Usage

Just run the executable. You'll be greeted with a basic prompt. Type `h` or `help` at the prompt to see all available commands.

For your convenience, the commands list is provided below:

```
> h
STACK OPERATIONS
[number] - push a number onto the stack
rot, r - rotate the stack (pop top off, move to bottom)
swp, x - swap the top two elements on the stack
dup, d - duplicate the top number on the stack
print, p - show the current stack contents
clear, c - clear the stack

ARITHMETIC AND FUNCTIONS
mul, * - multiply the top two numbers on the stack; push result
add, + - add the top two numbers on the stack
div, / - divide the top two numbers (1 / 0)
sub, - - subtract the top two numbers (1 - 0)
pow, ^ - raise the second top number to the top number (1 ^ 0)
sin, cos, tan - trig functions

CALCULATOR COMMANDS
cls, z - clear the screen
help, h - show this help
exit, e - exit the calculator
```

# Maintainers

- [BytewaveMLP](https://github.com/BytewaveMLP)

# Contribute

**Questions? Comments? Concerns?** Shoot me an issue!

**Want to add something?** Shoot me a PR!

# License

Copyright (c) Eliot Partridge, 2017. Licensed under the [MIT license](/LICENSE).