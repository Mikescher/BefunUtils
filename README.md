BefunGen
========

A tool for generating Befunge-93 Programs out of a imperative-programming-language (TextFunge)

*Download:* [mikescher.de](http://www.mikescher.de/programs/view/BefunGen)  
*Manual:* [mikescher.de](http://www.mikescher.de/programs/view/BefunGen)  
*Examples:* [mikescher.de](http://www.mikescher.de/programs/view/BefunGen) (= BefunWrite/Examples)  
*Example of compiled code:*  
[Maze Generator and Solver](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/examples/MazeGen.b98) | 
[Game Of Life](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/examples/GameOfLife.b98) |
[Sieve Of Eratosthenes](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/examples/Sieve_Of_Eratosthenes.b98) | 
[Befunge93-Interpreter](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/examples/Bef93Interpreter.b98) | 
[PI Approximation](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/examples/PI_Calc.b98) | 
[Euclidian algorithm](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/examples/EuclidianAlgorithm.b98) | 
[Fibonacci numbers](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/examples/FibonacciNumbers.b98) | 
[Fizz buzz](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/examples/FizzBuzz.b98) | 
[Square It with KI](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/examples/SquareIt.b98) | 
[Sudoku Generator](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/examples/SudoGen.b98)

###Table of contents

- [BefunWrite](#-befunwrite)
- [BefunGen](#-befungen)
- [BefunHighlight](#-befunhighlight)
- [BefunExec](#-befunexec)
- [BefunRep](#-befunrep)

Overview
========

*BefunGen, a Befunge-93 code generator from a procedural C-like language*

> **NOTE:**  
>  
> BefunGen is a collection of multiple programs and tools: 
> 
> - **BefunGen**: A TextFunge to Befunge-93 compiler  
> - **BefunExec**: A fast Befunge-93 interpreter  
> - **BefunHighlight**: A intelligent Befunge-93 syntax-highlighter  
> - **BefunWrite**: An IDE for TextFunge  
> - **BefunRep**: An commandline tool to calculate number representations in Befunge

&nbsp;

> **NOTE:**  
>  
> If you don't know the esoteric language Befunge I can refer you to the [esolang page](http://esolangs.org/wiki/Befunge) and my own [tutorial](http://www.mikescher.de/blog/2/Lets_do_Befunge93)

##![](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/icon_BefunWrite.png) BefunWrite

BefunWrite is the main program that uses all the other parts.  
It's an IDE in which you can write a program in *TextFunge*, and compile it to valid Befunge-98 code.

> **NOTE:**  
>  
> While the generated Code practically is Befunge-98, you can use it in nearly every Befunge-93 interpreter.  
> Because it doesn't use a single command, which was not defined in Befunge-93, the only non-Befunge-93 feature is the extended file size.  
> Because this tool can generate fairly big Befunge-93 code, it often exceeds the size of 80x25, and is so no longer totally valid Befunge-93 code.
> But for the sake of confusion I will refer in the rest of these documents to it as Befunge-93 code.

![](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/BefunWrite_Main.png)

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

##![](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/icon_BefunGen.png) BefunGen

BefunGen is the core of this compilations. It's a compiler for for TextFunge and a code generator for Befunge.  
Essentially it performs the conversion between TextFunge and Befunge. Most of the generated Befunge programs could be a lot smaller if an actual person would take the time writing them.  
That is the case because the generated Befunge code has a lot of organisation code included. It needs to manage the global variables and also the local ones. The local variables need initialization and in case of a different method call their current state needs to be saved. Also there has to be a call-stack to return to previous methods and re-initialization code when you jump back into methods.

This is important to understand, while I always try to optimize the generated code as much as I can it will always be a good amount bigger (and slower) than actual human-made code. This is also the case because there are neat little "tricks" and design concepts in Befunge that you just can't express in a procedural language.

But thats not really the problem, because the target of BefunGen is **not** generate code that could also be made by hand. The target code size is code so big that it would be totally impractical to write by hand (without spending days and weeks on it).

BefunGen itself is not a standalone program, but a simple library. You are free to use the DLL in your own program (but beware of the license, please give me credits...). If you need help how to use it you can either simply look at the source code (of BefunGen or BefunWrite) or write me a friendly mail.

##![](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/icon_BefunExec.png) BefunExec

BefunExec is a fast Befunge-93 interpreter (without any program-size limitations) and a simple tool to execute and debug the generated Befunge-93 files.

While developing BefunGen I encountered the problem that not many interpreters where able to execute my programs.  
Because of the properties of my generated code I needed an interpreter that was

- really fast, it needed to execute really many executions per second
- able to open and display large to *extremely* large programs
- able too zoom in programs (because they can be large)
- able to debug the program (show stack, output, input, breakpoints, single-step ...)

As you can imagine, I didn't find an interpreter that fitted my needs and so I wrote my own.  
The big point that makes BefunExec unique is it's very high speed. On my machine (and its not really a good one) I reach a maximum speed of **6.5 MHz**. This are over **6 million** executions per second, enough for most of my programs :D.  

![](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/BefunExec_Main.png)

Some other features are (as stated above) the ability to set breakpoints, step slowly through the program and zoom into specific parts.  
Also you are able to capture the program execution as a gif animation.  
One other big feature is the integration of BefunHighlight, which will be explained in the BefunHighlight section below.

##![](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/icon_BefunHighlight.png) BefunHighlight

BefunHighlight is just a helper library mainly for BefunExec.  
BefunExec has normal Befunge syntax-highlighting implemented, that means that every character is colored based on its function.
The problem here is that there may be code that never gets executed (not part of the real program), or there may be code that only gets used inside of string-mode (so the function doesn't matter, only the ASCII value).

BefunHighlight tries to solve this by evaluating every possible path an program can execute and so calculating the ways a single command is used.
Based on these informations it's now possible for another program to better highlight the source code.

![](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/BefunExec_ESH_example.png)

Be aware that put (**p**) and get (**g**) operations will invalidate the calculated values and it is needed to update them.

##![](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/icon_BefunRep.png) BefunRep

A common problem with Befunge is the *(efficient)* representation of big numbers. *(= putting a number on the stack with the least amount of instructions)*

BefunRep is a commandline tool to generate a list of representations for all numbers in a specified range. I'm pretty sure the calculation of the optimal representation is a NP complete. But BefunRep tries to to find good representations for all numbers via various algorithms. And it does a pretty good job. It finds for all the numbers between -1 million and +1 million representations with a maximum of eleven characters.

![](https://raw.githubusercontent.com/Mikescher/BefunGen/master/README-FILES/BefunRep_Main.png)

Here an example call to BefunRep:

> **\> BefunRep.exe -lower=0 -upper=1000 -iterations=3 -stats=3 -q -safe="safe.bin" -out="out.csv" -reset**

This calculates the numbers from **0** to **1000**.  
With a maximum of  **3** iterations *(use -1 to calculate until no more improvmenets can be found)*
Safe the results in binary format in the file **safe.bin**  
If the safe already exists it will be reseted (-**reset**)
And exports the results readable to **out.csv**  

You can also update an existing safe

> **\> BefunRep.exe -safe="safe.bin" -iterations=-1**

Or don't calculate anything and only output an existing safe into another format

> **\> BefunRep.exe -safe="safe.bin" -iterations=0 -stats=3 -out="out.json"**

Here an example of a few calculated values:

Number | Representation
-------|----------------
113564 | `"tY!"**3/`
113565 | `"qC-"**3/`
113566 | `"[' "**2-`
113567 | `"[' "**1-`
113568 | `"[' "**`
113569 | `"~~U"++:*`
113570 | `"[' "**2+`
113571 | `"wj"*5+9*`
113572 | `"[' "**4+`
113573 | `"[' "**5+`
113574 | `"E~(&"*+*`
