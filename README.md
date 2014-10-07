BefunGen
========

A tool for generating Befunge-93 Programs out of a imperative-programming-language (TextFunge)

*Download:* [mikescher.de](http://www.mikescher.de/programs/view/BefunGen)  
*Manual:* [mikescher.de](http://www.mikescher.de/programs/view/BefunGen)  
*Examples:* [mikescher.de](http://www.mikescher.de/programs/view/BefunGen) (= BefunWrite/Examples)  
*Example of compiled code:* [pastebin (Square-It)](http://pastebin.com/raw.php?i=1d2U399C)  

Overview
========

*BefunGen, a Befunge-93 code generator from a procedural C-like language*

> **NOTE:**  
>  
> BefunGen is a collection of multiple programs:  
> - **BefunGen**: A TextFunge to Befunge-93 compiler  
> - **BefunExec**: A fast Befunge-93 interpreter  
> - **BefunHighlight**: A intelligent Befunge-93 syntax-highlighter  
> - **BefunWrite**: An IDE for TextFunge  

&nbsp;

> **NOTE:**  
>  
> If you don't know the esoteric language Befunge I can refer you to the [esolang page](http://esolangs.org/wiki/Befunge) and my own [tutorial](http://www.mikescher.de/blog/2/Lets_do_Befunge93)

## BefunWrite

BefunWrite is the main program that uses all the other parts.  
It's an IDE in which you can write a program in *TextFunge*, and compile it to valid Befunge-98 code.

> **NOTE:**  
>  
> While the generated Code practically is Befunge-98, you can use it in nearly every Befunge-93 interpreter.  
> Because it doesn't use a single command, which was not defined in Befunge-93, the only non-Befunge-93 feature is the extended file size.  
> Because this tool can generate fairly big Befunge-93 code, it often exceeds the size of 80x25, and is so no longer totally valid Befunge-93 code.
> But for the sake of confusion I will refer in the rest of these documents to it as Befunge-93 code.

In BefunWrite you write your source code in the, specially for this developed, language **TextFunge**.
BefunWrite supports you in this process with a lot of the basic IDE features you already know from other IDE's.  
After written you can compile your TextFunge code into BEfunge-93 code and execute it in **BefunExec**.

The main advantage for you is how you can easily generate even complex programs in Befunge.
Because TextFunge supports all the basic features of a procedural language you can use constructs like:

- if-clauses and switch-cases
- for-loops, while-loops, repeat-until-loops
- methods
- recursion
- global and local variables with different data-types
- GOTO's
- arithmetical and logical expressions

BefunWrite also provides an easy interface for the multiple compiler-settings, for more information please visit the individual manuals.

## BefunGen

BefunGen is the core of this compilations. It's a compiler for for TextFunge and a code generator for Befunge.  
Essentially it performs the conversion between TextFunge and Befunge. Most of the generated Befunge programs could be a lot smaller if an actual person would take the time writing them.  
That is the case because the generated Befunge code has a lot of organisation code included. It needs to manage the global variables and also the local ones. The local variables need initialization and in case of a different method call their current state needs to be saved. Also there has to be a call-stack to return to previous methods and re-initialization code when you jump back into methods.

This is important to understand, while I always try to optimize the generated code as much as I can it will always be a good amount bigger (and slower) than actual human-made code. This is also the case because there are neat little "tricks" and design concepts in Befunge that you just can't express in a procedural language.

But thats not really the problem, because the target of BefunGen is **not** generate code that could also be made by hand. The target code size is code so big that it would be totally impractical to write by hand (without spending days and weeks on it).

BefunGen itself is not a standalone program, but a simple library. You are free to use the DLL in your own program (but beware of the license, please give me credits...). If you need help how to use it you can either simply look at the source code (of BefunGen or BefunWrite) or write me a friendly mail.

## BefunExec

BefunExec is a fast Befunge-93 interpreter (without any program-size limitations) and a simple tool to execute and debug the generated Befunge-93 files.

While developing BefunGen I encountered the problem that not many interpreters where able to execute my programs.  
Because of the properties of my generated code I needed an interpreter that was

- really fast, it needed to execute really many executions per second
- able to open and display large to *extremely* large programs
- able too zoom in programs (because they can be large)
- able to debug the program (show stack, output, input, breakpoints, single-step ...)

As you can imagine, I didn't find an interpreter that fitted my needs and so I wrote my own.  
The big point that makes BefunExec unique is it's very high speed. On my machine (and its not really a good one) I reach a maximum speed of **6.5 MHz**. This are over **6 million** executions per second, enough for most of my programs :D.  

Some other features are (as stated above) the ability to set breakpoints, step slowly through the program and zoom into specific parts.  
Also you are able to capture the program execution as a gif animation.  
One other big feature is the integration of BefunHighlight, which will be explained in the BefunHighlight section below.

## BefunHighlight

BefunHighlight is just a helper library mainly for BefunExec.  
BefunExec has normal Befunge syntax-highlighting implemented, that means that every character is colored based on its function.
The problem here is that there may be code that never gets executed (not part of the real program), or there may be code that only gets used inside of string-mode (so the function doesn't matter, only the ASCII value).

BefunHighlight tries to solve this by evaluating every possible path an program can execute and so calculating the ways a single command is used.
Based on these informations it's now possible for another program to better highlight the source code.

Be aware that put (**p**) and get (**g**) operations will invalidate the calculated values and it is needed to update them.
