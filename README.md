BefunUtils 
========

This is my collection of tools, libraries and transcompilers for the esoteric programming language [Befunge](http://esolangs.org/wiki/Befunge).	

It consists of the following core components:

### ![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunGen.png) [BefunGen](https://github.com/Mikescher/BefunGen):  [![Build status](https://ci.appveyor.com/api/projects/status/2a0bp9dem42uru2j/branch/master?svg=true)](https://ci.appveyor.com/project/Mikescher/befungen/branch/master)
> A Befunge-93 to multiple procedural languages (c, java, csharp, python) transcompiler  
> [**[DOWNLOAD LATEST NIGHTLY BUILD]**](https://ci.appveyor.com/project/Mikescher/BefunGen/build/artifacts)

### ![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunWrite.png) [BefunWrite](https://github.com/Mikescher/BefunWrite):  [![Build status](https://ci.appveyor.com/api/projects/status/wriqnc42kc8cwosu/branch/master?svg=true)](https://ci.appveyor.com/project/Mikescher/befunwrite/branch/master)
> A small editor for Textfunge, the language used by BefunGen - use this if you want to try BefunGen for yourself  
> [**[DOWNLOAD LATEST NIGHTLY BUILD]**](https://ci.appveyor.com/project/Mikescher/BefunWrite/build/artifacts)

### ![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunHighlight.png) [BefunHighlight](https://github.com/Mikescher/BefunHighlight):  
> A dynamic Befunge-93 syntax highlighting library. Highlights your sourcecode intelligent and context-sensitive

### ![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunExec.png) [BefunExec](https://github.com/Mikescher/BefunExec):  [![Build status](https://ci.appveyor.com/api/projects/status/u10tua2nyn5pyr6x?svg=true)](https://ci.appveyor.com/project/Mikescher/befunexec)
> A (fast) Befunge-93 interpreter and debugger  
> [**[DOWNLOAD LATEST NIGHTLY BUILD]**](https://ci.appveyor.com/project/Mikescher/BefunExec/build/artifacts)

### ![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunRep.png) [BefunRep](https://github.com/Mikescher/BefunRep):  [![Build status](https://ci.appveyor.com/api/projects/status/1xhmo6m4qpawo5vi/branch/master?svg=true)](https://ci.appveyor.com/project/Mikescher/befunrep/branch/master)
> A tool to calculate (the best) number-representation in Befunge-93 space  
> I calculated a big set of numbers and uploaded then to [github](https://github.com/Mikescher/Befunge_Number_Representations) you can access them [with this online app](https://mikescher.github.io/Befunge_Number_Representations/)  
> [**[DOWNLOAD LATEST NIGHTLY BUILD]**](https://ci.appveyor.com/project/Mikescher/BefunRep/build/artifacts)

### ![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunCompile.png) [BefunCompile](https://github.com/Mikescher/BefunCompile):  [![Build status](https://ci.appveyor.com/api/projects/status/990qb5py2anch1lg/branch/master?svg=true)](https://ci.appveyor.com/project/Mikescher/befuncompile/branch/master)
> An *(non-general)* Befunge-93 compiler. Compile your Befunge-93 code to C, C# or Python  
> For an example usage see the compiled versions of my [Project-Euler solutions](https://github.com/Mikescher/Project-Euler_Befunge)  
> [**[DOWNLOAD LATEST NIGHTLY BUILD]**](https://ci.appveyor.com/project/Mikescher/BefunCompile/build/artifacts)

### ![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunRun.png) [BefunRun](https://github.com/Mikescher/BefunRun):  [![Build status](https://ci.appveyor.com/api/projects/status/7x53ceio5fa6bsbv/branch/master?svg=true)](https://ci.appveyor.com/project/Mikescher/befunrun/branch/master)
> A simple terminal tool to run a befunge93 program (with extended grid size) and output its output.  
> [**[DOWNLOAD LATEST NIGHTLY BUILD]**](https://ci.appveyor.com/project/Mikescher/BefunRun/build/artifacts)

### ![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/icon_BefunDebug.png) [BefunDebug](https://github.com/Mikescher/BefunDebug):  [![Build status](https://ci.appveyor.com/api/projects/status/74d7eukglosfvxfn/branch/master?svg=true)](https://ci.appveyor.com/project/Mikescher/befundebug/branch/master)
> A debug and test tool for BefunGen, BefunCompile, BefunHighlight, etc  
> [**[DOWNLOAD LATEST NIGHTLY BUILD]**](https://ci.appveyor.com/project/Mikescher/BefunDebug/build/artifacts)


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

BefunDebug (Some of the pages to debug and complement the other tools):  
![](https://raw.githubusercontent.com/Mikescher/BefunUtils/master/README-FILES/BefunDebug_All.png)


Contributions
=============

Yes, please