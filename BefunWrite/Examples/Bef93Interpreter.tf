/*
 * Befunge-93 Interpreter
 * by Mike Schwörer 2014
*/

program BefInterpreter : display[60, 30]
	const
		int STACKSIZE := 16;
	global
		int[4] DELTA_IDX_X;
		int[4] DELTA_IDX_Y;
		
		bool running;
	
		int PC_X;
		int PC_Y;
		
		int D_X;
		int D_Y;
		
		bool stringmode;
		
		int[16] stack;
		int stackHead;
	begin
		Init();
		
		while (running) do
			execute();
			move();
		end
		
		quit;
	end
	
	void Init()
	begin
		DELTA_IDX_X[0] =  1;
		DELTA_IDX_X[1] =  0;
		DELTA_IDX_X[2] = -1;
		DELTA_IDX_X[3] =  0;
		
		DELTA_IDX_Y[0] =  0;
		DELTA_IDX_Y[1] = -1;
		DELTA_IDX_Y[2] =  0;
		DELTA_IDX_Y[3] =  1;
	
		stackHead = 0;
	
		PC_X = 0;
		PC_Y = 0;
		
		D_X = 1;
		D_Y = 0;
		
		stringmode = false;
		
		running = true;
	end
	
	void execute()
	var
		char c;
	
		int tmp;
		int tmp2;
		char tmp3;
	begin
		c = display[PC_X, PC_Y];

		if (stringmode && c != '"') then
			push((int)c);
			return;
		end

		switch(c)
		begin
			case ' ':
				// NOP
			end 
			case '0':
				push(0);
			end
			case '1':
				push(1);
			end
			case '2':
				push(2);
			end
			case '3':
				push(3);
			end
			case '4':
				push(4);
			end
			case '5':
				push(5);
			end
			case '6':
				push(6);
			end
			case '7':
				push(7);
			end
			case '8':
				push(8);
			end
			case '9':
				push(9);
			end
		end
		
		switch(c)
		begin
			case '+':
				push(pop() + pop());
			end
			case '-':
				tmp = pop();
				push(pop() - tmp);
			end
			case '*':
				push(pop() * pop());
			end
			case '/':
				tmp = pop();
				push(pop() / tmp);
			end
			case '%':
				tmp = pop();
				push(pop() % tmp);
			end
			case '!':
				push((int)(!popBool()));
			end
			case '`':
				tmp = pop();
				push((int)(pop() > tmp));
			end
			case '>':
				D_X = 1;
				D_Y = 0;
			end
			case '<':
				D_X = -1;
				D_Y = 0;
			end
			case '^':
				D_X = 0;
				D_Y = -1;
			end
			case 'v':
				D_X = 0;
				D_Y = 1;
			end
			case '?':
				tmp = RAND[1];
				D_X = DELTA_IDX_X[tmp];
				D_Y = DELTA_IDX_Y[tmp];
			end
		end
		
		switch(c)
		begin
			case '_':
				if (popBool()) then
					D_X = DELTA_IDX_X[2];
					D_Y = DELTA_IDX_Y[2];
				else
					D_X = DELTA_IDX_X[0];
					D_Y = DELTA_IDX_Y[0];
				end
			end
			case '|':
				if (popBool()) then
					D_X = DELTA_IDX_X[1];
					D_Y = DELTA_IDX_Y[1];
				else
					D_X = DELTA_IDX_X[3];
					D_Y = DELTA_IDX_Y[3];
				end
			end
			case '"':
				stringmode = !stringmode;
			end
			case ':':
				push(peek());
			end
			case '\\':
				tmp = pop();
				tmp2 = pop();
				push(tmp);
				push(tmp2);
			end
			case '$':
				pop();
			end
			case '.':
				out pop();
			end
			case ',':
				out popChar();
			end
			case '#':
				move();
			end
			case 'g':
				tmp = pop();
				tmp2 = pop();
				if (tmp >= 0 && tmp2 >= 0 && tmp2 < DISPLAY_WIDTH && tmp < DISPLAY_HEIGHT) then
					push((int)display[tmp2, tmp]);
				else
					push(0);
				end
			end
			case 'p':
				tmp = pop();
				tmp2 = pop();
				if (tmp >= 0 && tmp2 >= 0 && tmp2 < DISPLAY_WIDTH && tmp < DISPLAY_HEIGHT) then
					display[tmp2, tmp] = popChar();
				else
					pop();
				end
			end
			case '&':
				in tmp;
				push(tmp);
			end
			case '~':
				in tmp3;
				push((int)tmp3);
			end
			case '@':
				out "\r\n\r\n >> FINISHED";
				running = false;
			end
			default:
				// NOP
			end
		end
	end
	
	void move()
	begin
		PC_X += D_X + DISPLAY_WIDTH;
		PC_Y += D_Y + DISPLAY_HEIGHT;
		
		PC_X %= DISPLAY_WIDTH;
		PC_Y %= DISPLAY_HEIGHT;
	end
	
	void push(int v)
	begin
		stack[stackhead++] = v;
	end
	
	int pop()
	begin
		if (stackhead == 0) then
			return 0;
		end
		
		return stack[--stackhead];
	end
	
	char popChar()
	begin
		return (char)pop();
	end
	
	bool popBool()
	begin
		return (bool)pop();
	end
	
	int peek()
	begin
		if (stackhead == 0) then
			return 0;
		end
		
		return stack[stackhead - 1];
	end
end