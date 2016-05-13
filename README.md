BefunUtils
========

This is my collection of tools, libraries and transcompilers for the esoteric programming language [Befunge](http://esolangs.org/wiki/Befunge).	

It consists of the following core components:

###![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunGen.png) [BefunGen](https://github.com/Mikescher/BefunGen):  
> A Befunge-93 to multiple procedural languages (c, java, csharp, python) transcompiler

###![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunWrite.png) [BefunWrite](https://github.com/Mikescher/BefunWrite):  
> A small editor for Textfunge, the language used by BefunGen - use this if you want to try BefunGen for yourself

###![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunHighlight.png) [BefunHighlight](https://github.com/Mikescher/BefunHighlight):  
> A dynamic Befunge-93 syntax highlighting library. Highlights your sourcecode intelligent and context-sensitive

###![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunExec.png) [BefunExec](https://github.com/Mikescher/BefunExec):  
> A (fast) Befunge-93 interpreter and debugger

###![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunRep.png) [BefunRep](https://github.com/Mikescher/BefunRep):  
> A tool to calculate (the best) number-representation in Befunge-93 space

###![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunCompile.png) [BefunCompile](https://github.com/Mikescher/BefunCompile):  
> An *(non-general)* Befunge-93 compiler. Compile your Befunge-93 code to C, C# or Python

###![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunRun.png) [BefunRun](https://github.com/Mikescher/BefunRun):  
> A simple terminal tool to run a befunge93 program (with extended grid size) and output its output.

###![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunDebug.png) [BefunDebug](https://github.com/Mikescher/BefunDebug):  
> A debug and test tool for BefunGen, BefunCompile, BefunHighlight, etc


Set up
==========

You can either download the binaries from [www.mikescher.com](http://www.mikescher.com/programs/view/BefunUtils).

Or you can setup the Solution by yourself:

- Clone the **BefunUtils** repository
- Clone all the subproject repositories into subfolder *(or simply execute CLONE_ALL.bat)*
- *(eg clone BefunExec into the folder /BefunExec)*
- Open the solution file in Visual Studio *(or build all projects with the BUILD_ALL.bat script)*

Screenshots
==========

BefunExec:  
![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/BefunExec_Main.png)

BefunWrite:  
![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/BefunWrite_Main.png)

BefunRep:  
![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/BefunRep_Main.png)

BefunHighlight:  
![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/BefunExec_ESH_example.png)

BefunCompile:  
![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/BefunCompile_Main_example.png)

BefunCompile (Graph display of [Euler_Problem-002](https://github.com/Mikescher/Project-Euler_Befunge/blob/master/Euler_Problem-002.b93) Level **0**) *(via [BefunDebug](https://github.com/Mikescher/BefunDebug))*:  
![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/BefunCompile_Graph-0_example.png)

BefunCompile (Graph display of [Euler_Problem-002](https://github.com/Mikescher/Project-Euler_Befunge/blob/master/Euler_Problem-002.b93) Level **2**) *(via [BefunDebug](https://github.com/Mikescher/BefunDebug))*:  
![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/BefunCompile_Graph-2_example.png)

BefunCompile (Graph display of [Euler_Problem-002](https://github.com/Mikescher/Project-Euler_Befunge/blob/master/Euler_Problem-002.b93) Level **3**) *(via [BefunDebug](https://github.com/Mikescher/BefunDebug))*:  
![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/BefunCompile_Graph-3_example.png)

BefunCompile (Graph display of [Euler_Problem-002](https://github.com/Mikescher/Project-Euler_Befunge/blob/master/Euler_Problem-002.b93) Level **5**) *(via [BefunDebug](https://github.com/Mikescher/BefunDebug))*:  
![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/BefunCompile_Graph-5_example.png)
