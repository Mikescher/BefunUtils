Let's do Befunge-93
=====================

Hello, this is my try to teach *you* a little bit of Befunge-93.

---------

[TOC]

---------

Preamble
--------

You may ask why someone should learn such a esoteric language as Befunge-93 ?
It has close to no real world application and is far away from every other language you may already know.

But wait, Befunge has one really neat property *(at least in my opinion)* its really fun to write in it. And that **because** it's totally different from every other language. Writing code in Befunge has a lot to do with planning and laying out your code.
Yes physically planning how your code will look and where you have to write specific subroutines.
But enough, let's go look at some code.

Chapter 1: A whole new Dimension
--------------------------------

### Choosing the right Tools

Okay, wait - first you need an interpreter, you know, to run the programs you will write.
For the beginning i recommend to simply use an javascript interpreter - they are missing a lot of important features, like manually jump in your program, breakpoints, or the most simple property: speed.
But they are easy too use, and for now you will write programs the size you don't need all these fancy features.
So just Google for an online-interpreter - there are many out there,  and if you need an more advanced interpreter later you can take a look at my own interpreter *BefunExec*.

### The simplest program

OKay, now let's start - this time for real.
You will need an empty ASCII-encoded textfile - this will be our code.
You have to understand that Befunge operates in an 2-dimensional space - like in normal programming you have a PC (Programm Counter) that describes your current position.
In normal programs (you know, C, Phyton, Java etc) this PC is 1dimensional, in Befunge it's 2dimensional. At the begining its positioned in the top-left corner and with every tick its moving one field to the left.
So every Character in your textdocument is a command - the most simple command is a space. The space is the `NOP` Command - the No operation

> **NOTE:**
> 
> - Befunge operates in a 2-dimensional Grid
> - The Programmcounter starts topleft and initially moves right
> - Every character in yout document is then an individual command


So our first programm can be as simple as that:

```

```

### And it loops around

You see correct - an empty file is indeed a valid Befunge Program.
You may ask what it does. That's kinda simple, it just executes NOP over and over again.
A Befunge-93 program has the size of 80x25, and when the PC reaches the right edge it just wraps around and comes back in on the left edge.

> **NOTE:**
> 
> - Befunge-93 programs have a fixed size of 80x25
> - The Befunge space is infinity - it wraps around its edges

Ok, but now we want to write *real* commands to our file - there are 4 basic commands ( `v` , `>` , `^` and `<` ).
With these "arrows" you are able to change the direction of the PC.
So with these commands we are now able to write a rather simple endless-loop program:

```
>      v


^      <
```

![img01][1]

### Stack it up

We now have a Grid where we can layout our program and a PC that runs through this grid to execute our precious programm. Isn't that wonderful?

*But wait! There's more*

In fact Befunge has one more element you should know about: **The stack**.

Every Befunge Command has a Stack you can manipulate in your program, you can do all the normal stackoperations on it, push, pop, peek etc.

The Commands to push something on the stack are `0` - `9`. So if you write

```
0 1 9 9
```

it will push a zero, a one, and two nines on the stack.
If you want to end the program after that you can use the **@** Command.

> **NOTE:**
> 
> - You can push a digit to the stack with the commands `1`, `2`, `3`, `4`, `5`, `6`, `7`, `8`, `9`, `0`
> - The Command `@` stops the program 

You also easily push values to the stack with the so called *stringmode*
With the command `"` you start the stringmode and with the same command `"` you can also end it again.
While the stringmode is active every Character the PC encounters will be pushed as an ASCII-Value to the stack.

The Program
```
"abc" @
```

will so leave the stack in the following state after completion:

```
| 99 |
| 98 |
| 97 |
```

> **NOTE:**
> 
> - A **"** starts and ends the stringmode
> - While in stringmode every Command will be pushed as its ASCII Value to the stack

### Finally a real program

Okay, okay - I highly appreciate it that you are still following me. I know i promised you a Hello-World program. And I'm happy to announce that we are finally at the point where we can write it.
Only one last command is missing, the CharacterOutput (`,`) Command. This command takes one Value of the Stack - interprets it as an ASCII-Value, and outputs it to wherever the interpreter show the output.

So now - lets write this program:

```
"dlroW olleH" ,,,,,,,,,,, @
```

![img02][2]

And thats it - a really simple Hello World. As you can see the program has two parts, first we fill the stack with the ASCII values for the two words "Hello World", and then we print each char to the output.
You may ask why Hello World is written backwards - that's rather simple, because we work with a stack the last pushed value is the first popped value (LI-FO principle ). And so - to output something in the correct order - we have to input it in the reversed order

Chapter 2: Let's golf
---------------------

### What we did wrong

Some of you probably know *Codegolf* . These are programming challenges with the target, to write programcode with the least amount of bytes.

Because of the limited amount of space in Befunge, this is here more important than ever. In our last example you saw how to output a string, but as you can imagine the longer the string gets the more commas you have to write and faster than you can imagine you have filled the whole space.
So now let's try to optimize our Hello World a little bit 

> **NOTE:**
> *"Good code is small code"*

### Decision, Decisions, Decisions

So you may ask what is the basic feature of a programming language, what feature can't be scrapped away without loosing something essentials.
One of these basics things is the possibility to do conditional logic. Your program has to react to something and react differently depending on the results.

So now it's time to introduce 2 new commands `|` and `_` . These commands are called "Decision making". They pop a value from the stack and change the PC direction depending on its value. The value is interpreted as an boolean, the conversion used is the same you probably know from C. If the value is Zero, its false - otherwise its true.
The `|` Command routes the PC to the top if the value is true and to the bottom if the value is false. Respective behaves the `_` command with left and right.

An other command is the `:`, what it does is pretty simple - it duplicates the top-stack value.
And the last command is `$`. It's also a kinda simple command: it pops a value from the stack - nothing more, it justs pops it and then forgets about it.
With this knowledge we can now finally optimize our *Hello World*:

```
v

                   v  ,  <
>    "dlroW olleH" >  :  |
                         >  $  @
```

![img03][3]

So what happens here ?

- First we put "Hello World" reversed on the stack
- The we go in a loop, in every loop we do
    - Duplicate the TOS[^tos]
    - Test if the TOS is != 0 (true for every ASCII Character)
    - Output the TOS
- This loop goes on until the stack is empty. When we now try to access the empty stack (with the duplicate command) it results in two zeros on the stack. This is so because an access to an empty stack will always result in a zero
- The returned zero is now interpreted as false and we exit the loop, the remaining zero on the stack is removed with the pop command and the program exits on the `@` command

> **NOTE:**
>
> - **_** and **|** change the PC direction depending on a value popped from the stack
> - **:** duplicates the top-of-stack-value
> - **$** removes the top-of-stack-value
> - Accessing an empty stack will result in a *zero*

### One step further

So our Hello World is now a lot smaller. Especially if you remove all the unneeded NOPs in the previous example.
But our standard shall be to become the very best *like no one ever was ...*.

So now I shall introduce you to one of the more interesting commands - **the trampoline** `#`
After your PC encounters the trampoline it "jumps" over the next command, this results in a few very neat tricks you can do in your code.

The next Hello World i will show you shall be our last one. This is the standard code to output a string of variable length and i don't believe that there is a more optimized way to do it. <!--- Note that for Befunge small code mostly equals fast code ! -->

```
`"dlroW olleH">:#,_@
```

![img04][4]

Here you can see another speciality of Befunge - the jump command is used twice, one time from left to right and the second time from right to left, so its used to skip two different commands (`:` and `,`) although only being one command.

Chapter 3: Everything is mathematical
-------------------------------------

### Simple Calculus 101

Our programs are still rather static, we want them to actually do some work, for example calculate something.
Luckily Befunge has a few operational commands in its repertoire.

- `+` The *ADD* command: Pops two values from the stack and pushed the result of the addition
- `-` The *SUB* command: Pops two values from the stack and pushed the result of the subtraction
- `*` The *MULT* command: Pops two values from the stack and pushed the result of the multiplication
- `/` The *DIV* command: Pops two values from the stack and pushed the result of the division
- `%` The *MOD* command: Pops two values from the stack and pushed the result of the modulo operation
- `!` The *NOT* command: Pops one value from the stack, interprets it as a boolean and pushes the negation
- `´` The *COMP* command: Pops two values from the stack and pushed the result of the compare "a>b"

So if you want to calculate `4+5` just write

```
45+
```
*speak:*

- Push 4 to stack
- Push 5 to stack
- Pop 4 and 5 -> Push (4+5=)9 to the stack

And if you want `(4+5)*6+7` you write

```
45+6*7+
```

or

```
4567+*+
```

Some of you may say that this notation seems familiar. That's true - it's the so called postfix[^pfix] notation.
This is one of Befunge's very neat "features" writing a mathematical expression will always result in a postfix notation and you can simply write a postfix notation in Befunge.

> **NOTE:**
>
> - +, *, - ,/, %, !, ` are the operator commands
> - These pop their arguments from the stack and push the result back
> - You can write mathematical expressions in postfix notation

### ... Eight, Nine, Ten ?

Perhaps you remember Chapter 2, we learned how to push digits to the stack. But probably you will one day feel the need to have values greater `9` on the stack. Now you can't just simply write `12` to push a twelve.

```
12
```

would just push a `1` and a `2` on the stack.
The only option we have is two write a formula which results in a twelve on the stack, for example:

```
66+
```
or
```
62*
```
or
```
93+
```

You see there are a lot of ways displaying the same value and we sure would like to know the best (= shortest) way to do so.

Now there are a few recipes how to do so: 

#### Addition

The most simple way of displaying numbers is expressing them with addition.
So
```
12 => 93+
19 => 991+
100 => 999999999991+
```

As you can see this method does not scale very well, and you will reach the acceptable limit pretty fast when the number becomes high enough.

#### Base-9

Base-9 is by far my favorite way of expressing numbers.
You probably know about different bases in number representation. `14` is `14` when written in base-10 (our everyday standard). But its also `D` in base-16 (or hexadecimal as you may know it). Or its `16` in base-8 (= octal), or its `1110` in base-2 (= binary). 
Know is base-9 something you probably never heard of, its not one of the bases you meet when walking normally through life, but in this case it has its justification to exist.

Now back to work, we want to encode `105` (base-10).
`105` is `126` in base-9 and you can easily convert bases with this formula:

```
(((1*9) + 2) * 9) + 6  ==>  105
```

or in Befunge (Postfix):

```
69 29 1 *+ *+
```
gekürzt:
```
69291*+*+
```

Now you see why we use base-9, because we need to write the `9` in Befunge and `9` is the highest number we can push onto the stack in a single command.

This representation is nice because its really fast (and easy) to calculate the representation - even for big numbers, an because you can easily tell for every number you want how long its base-9-representation will be.

#### Factorization

Base-9 is nice in most cases, but often its not the optimal way. Factorization is often more compact.

The idea here is that you can split a number into its factors (and the factors should hopefully be `<10` ) and then multiply them.

So `196` `(=4*7*7)` becomes

```
477*
```

Its said that this only works nice when you can factorize the number with factors smaller ten. Otherwise you would need to incorporate other mathematical operation to get to your result, finding the optimal combination here can be very CPU-intensive.

So for `107` a way of displaying it is:

```
92+9*8+
```

#### Stringify

At last we can use a neat little trick. When we printed "Hello World", we put the ASCII values of the individual chars onto the stack. We can use that to express greater values. For example expressing `107` is as easy as `"k"`.

And for greater values you can even go on and write expressions based on the ASCII values of character:

```
"~~)'"*++  (= 1851)
```

### Example

Number|    Method      | Code           |Number|    Method      | Code           |Number|    Method      | Code            
------|----------------|----------------|------|----------------|----------------|------|----------------|----------------
 0    | Boolean        | `0`            | 40   | Factorization  | `58*`          | 400  | Factorization  | `25*5*8*`
 1    | Boolean        | `1`            | 41   | Stringmode     | `")"`          | 401  | Base9          | `59894*+*+`
 2    | Digit          | `2`            | 42   | Factorization  | `67*`          | 402  | Factorization  | `79*4+6*`
 3    | Digit          | `3`            | 43   | Stringmode     | `"+"`          | 403  | Base9          | `79894*+*+`
 4    | Digit          | `4`            | 44   | Stringmode     | `","`          | 404  | Base9          | `89894*+*+`
 5    | Digit          | `5`            | 45   | Factorization  | `59*`          | 405  | Factorization  | `59*9*`
 6    | Digit          | `6`            | 46   | Stringmode     | `"."`          | 406  | Base9          | `19095*+*+`
 7    | Digit          | `7`            | 47   | Stringmode     | `"/"`          | 407  | Factorization  | `59*9*2+`
 8    | Digit          | `8`            | 48   | Factorization  | `68*`          | 408  | Factorization  | `98+3*8*`
 9    | Digit          | `9`            | 49   | Factorization  | `77*`          | 409  | Factorization  | `59*9*4+`
 10   | Factorization  | `25*`          | 50   | Stringmode     | `"2"`          | 410  | Base9          | `59095*+*+`
 11   | Factorization  | `92+`          | 51   | Stringmode     | `"3"`          | 411  | Base9          | `69095*+*+`
 12   | Factorization  | `26*`          | 52   | Stringmode     | `"4"`          | 412  | Base9          | `79095*+*+`
 13   | Factorization  | `94+`          | 53   | Stringmode     | `"5"`          | 413  | Factorization  | `69*5+7*`
 14   | Factorization  | `27*`          | 54   | Factorization  | `69*`          | 414  | Base9          | `09195*+*+`
 15   | Factorization  | `35*`          | 55   | Stringmode     | `"7"`          | 415  | Factorization  | `99*2+5*`
 16   | Factorization  | `28*`          | 56   | Factorization  | `78*`          | 416  | Factorization  | `94+4*8*`
 17   | Factorization  | `98+`          | 57   | Stringmode     | `"9"`          | 417  | Base9          | `39195*+*+`
 18   | Factorization  | `29*`          | 58   | Stringmode     | `":"`          | 418  | Base9          | `49195*+*+`
 19   | Base9          | `192*+`        | 59   | Stringmode     | `";"`          | 419  | Base9          | `59195*+*+`
 20   | Factorization  | `45*`          | 60   | Stringmode     | `"<"`          | 420  | Factorization  | `25*6*7*`
 21   | Factorization  | `37*`          | 61   | Stringmode     | `"="`          | 421  | Base9          | `79195*+*+`
 22   | Base9          | `492*+`        | 62   | Stringmode     | `">"`          | 422  | Base9          | `89195*+*+`
 23   | Base9          | `592*+`        | 63   | Factorization  | `79*`          | 423  | Factorization  | `59*2+9*`
 24   | Factorization  | `38*`          | 64   | Factorization  | `88*`          | 424  | Factorization  | `59*8+8*`
 25   | Factorization  | `55*`          | 65   | Stringmode     | `"A"`          | 425  | Factorization  | `98+5*5*`
 26   | Base9          | `892*+`        | 66   | Stringmode     | `"B"`          | 426  | Factorization  | `79*8+6*`
 27   | Factorization  | `39*`          | 67   | Stringmode     | `"C"`          | 427  | Factorization  | 69*7+7*`
 28   | Factorization  | `47*`          | 68   | Stringmode     | `"D"`          | 428  | Base9          | 59295*+*+`
 29   | Base9          | `293*+`        | 69   | Stringmode     | `"E"`          | 429  | Base9          | 69295*+*+`
 30   | Factorization  | `56*`          | 70   | Stringmode     | `"F"`          | 430  | Base9          | 79295*+*+`
 31   | Base9          | `493*+`        | 71   | Stringmode     | `"G"`          | 431  | Base9          | 89295*+*+`
 32   | Factorization  | `48*`          | 72   | Factorization  | `89*`          | 432  | Factorization  | 68*9*`
 33   | Stringmode     | `"!"`          | 73   | Stringmode     | `"I"`          | 433  | Factorization  | 68*9*1+`
 34   | Base9          | `793*+`        | 74   | Stringmode     | `"J"`          | 434  | Base9          | 29395*+*+`
 35   | Factorization  | `57*`          | 75   | Stringmode     | `"K"`          | 435  | Base9          | 39395*+*+`
 36   | Factorization  | `49*`          | 76   | Stringmode     | `"L"`          | 436  | Base9          | 49395*+*+`
 37   | Stringmode     | `"%"`          | 77   | Stringmode     | `"M"`          | 437  | Factorization  | 68*9*5+`
 38   | Stringmode     | `"&"`          | 78   | Stringmode     | `"N"`          | 438  | Factorization  | 89*1+6*`
 39   | Stringmode     | `"'"`          | 79   | Stringmode     | `"O"`          | 439  | Factorization  | 68*9*7+`
                                                                        
	 

Chapter 4: One small step for a man
-----------------------------------

As you might see in the TableOfContents this tutorial is reaching its end.
There is only one more topic i want to present you and after that only one more example to show.
I hope I could inspire you to do a little bit more with a little bit unconventional languages. Also I hope you had as much fun reading this as I had writing this. Or - wait, no - I hope you had more fun than I ...

### Be the change you want to see

Befunge has one pretty big feature you haven't even seen now ... **self-modification**.
Yes you heard right, Befunge is capable of modifying its own code while running.

The responsible commands are `p` and `g`, namely *put* and *get*.

With *put* you can modify a specific command. *put* gets 3 Values from the stack `x`, `y` and `value`. `x` and `y` describe the position of the command and `value` is the new value of this field.

The *get* command works the other way around, it gets the value of a command at a specific position. *get* gets 2 values from the stack `x` and `y`, these values describe a specific field in the code, the command then gets the ASCII value of this command and pushed is onto the stack.

### A more complex example

Perhaps you want to try to solve the next task on your own. You already know everything you need and at the bottom of this tutorial you will find a full table of every command.

> You shall write a program that outputs the fibonacci numbers, one after the other
> An example output would look like:
> `1,1,2,3,5,8  ...`

I write an simple solution that does this task. Note that i didn't highly optimize it on purpose, so its easier to follow whats actually going on:

```
>> 100p 110p 1. ",",   1.",",     >     00g 10g: 00p + :. ",",  10p       v
                                  ^                                       <
```

If you want an explanation whats going on here, i also wrote an annotated version:

```
[0,0] ist das erste "Speicherfeld" - es enthält den vorletzten Wert
[1,0] ist das zweite "Speicherfeld" - es enthält den letzten Wert

100p		Feld[0,0] mit '1' initialisieren
110p		Feld[1,0] mit '1' initialisieren
1.",",		'1,' ausgeben
1.",",		'1,' ausgeben

> 			Anfang Schleife
00g				Hole [0,0]
10g				Hole [1,0]
:				Dupliziere [1,0]
00p				Setze Feld [0,0] auf den Wert von [1,0]
+				Addiere [0,0] + [1,0]
:				Dupliziere das Ergebnis
.",",			Gib das Ergebnis und ein Komma aus
10p				Schreibe das Ergebnis in [1,0]
v			Ende Schleife
```

Note that we use the Fields `[0,0]` and `[0,1]` are used as temporary "variable" fields. We can put there values with the `p` comamnd and later read them with the `g` command

### Command overview

Charakter | Name    | Description
----------|---------|---------------------------------------------------------------------
`+`       | ADD     | Adds two values from the stack together and pushes the result back
`-`       | SUB     | Subtracts two values from the stack from each other and pushes the result back
`*`       | MULT    | Multiplicates two values from the stack and pushes the result back
`/`       | DIV     | Divides two values from the stack and pushes the result back
`%`       | MOD     | Executes Modulo on two values from the stack and pushed teh result back
`!`       | NOT     | Gets a (boolean) value from the stack and pushes it negated back
`´`       | GT      | Pushes the result of a greater than over two value from the stack, to the stack
`^`       | PCT     | Set PC-Delta to *up*
`>`       | PCL     | Set PC-Delta to *left*
`v`       | PCB     | Set PC-Delta to *down*
`<`       | PCR     | Set PC-Delta to *right*
`?`       | PCRAND  | Set PC-Delta to a random direction
`\#`      | JMP     | Jumps over the next command
`_`       | IFH     | A horizontal If
`\|`      | IFV     | A vertical If
`:`       | DUP     | Dupliactes the TOS
`\`       | SWAP    | Swaps two values from the stack
`$`       | POP     | Removes the TOS
`.`       | OUT-INT | Outputs the TOS as a number
`,`       | OUT-ASC | Outputs the TOS as a character
`&`       | IN-INT  | Asks the user for a number and puts it on the stack
`~`       | IN-ASC  | Asks the user for a character and puts it on the stack
`p`       | PUT     | Sets a field to a specific value
`g`       | GET     | Gets the value of a field and pushes it onto the stack
`@`       | STOP    | Stops the programm execution
`"`       | STRMODE | Starts/Stops the stringmode
`0`-`9`   | NUMBERS | Pushes the respective number onto the stack                    

[^tos]: TOS = top of stack

[^pfix]: Postfix = [Postfix notation][5]


  [1]: http://i.imgur.com/Jkks7Uy.gif?1
  [2]: http://i.imgur.com/Z5Ljr5Z.gif?1
  [3]: http://i.imgur.com/82FKwkM.gif?1
  [4]: http://i.imgur.com/AqpsPRW.gif?1
  [5]: http://en.wikipedia.org/wiki/Reverse_Polish_notation