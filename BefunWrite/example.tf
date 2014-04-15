/*
 * Example Program II
 * 4 Grammar Testing
 *
*/

program example : display[51, 19]
	CONST
		CHAR CHR_UNSET := '@';
		CHAR CHR_WALL  := '#';
		CHAR CHR_FLOOR := ' ';
		CHAR CHR_PATH  := '+';
		CHAR[22] TEST := "Example Project 00\r\n\r\n";
	GLOBAl
		int glubul;
	var 
		char[32] name;
		int i := 0;
	begin
		out "Example Project 00\r\n\r\n";
		
		getRandAverage(512);
		
		OUT "\r\n";

		//Insert Name
		name = getInputStr();
		
		i = 0;
		while (i < 32) do
			out name[i++];
		end

		OUT "\r\n";
		
		// Print Fibbonacci
		
		if (name[0] == 'n') then
			goto lbl2;
			return;
		end

		OUT "\r\n";
		
		doFiber(100);
		
		OUT "\r\n";

		lbl2:
		
		OUT "\r\n";

		out euclid(44, 12);

		OUT "\r\n";

		Init();
		Create();

		OUT "\r\n";
		OUT "\r\n";

		fizzbuzz();

	end

	void getRandAverage(int SamSize)
	var
	 	int i;
		int rv;
		int rs;
	begin

		rs = 0;
		FOR (i = 0 ; i < SamSize ; i++) DO
			rv = RAND[4];
			rs += rv;
			//OUT rv;

			//OUT "\r\n";
		END

		
		OUT "Sum: ";
		OUT rs;
		OUT "\r\n";

		OUT "Averag: ";
		OUT (rs / SamSize);
		OUT "\r\n";
	END

	void DoFiber(int max)
	var
		int last := 0;
		int curr := 1;
		int tmp;
	begin
		repeat
			out curr;
			
			tmp = curr + last;
			last = curr;
			curr = tmp;
		until (last > max)
	end

	int euclid(int a, int b) 
	begin
		if (a == 0) then
			return b;
		else 
			if (b == 0) then
				return a;
			else 
				if (a > b) then
					return euclid(a - b, b);
				else
					return euclid(a, b - a);
				end
			end
		end
	end
	 
	char[32] getInputStr()
	var
		char[32] input;
		int current := 0;
		char last := 'X';
	begin
		IN last;

		while(current < 32 && current >= 0 ) do
			input[current] = last;
			last++;
			last = (char)(((int)last - (int)' ') % ((int)'~' - (int)' ') + (int)' ');
			current++;
		end

		return input;
	end
	
	VOID Init()
	VAR
		INT x;
		INT y;
	BEGIN
		FOR(x = 0; x < DISPLAY_WIDTH; x++) DO
			FOR(y = 0; y < DISPLAY_HEIGHT; y++) DO
				IF (x == 0 || y == 0 || x == DISPLAY_WIDTH - 1 || y == DISPLAY_HEIGHT - 1 || (x % 2 == 0 && y % 2 == 0)) THEN
					DISPLAY[x,y] = CHR_WALL;
				ELSE
					DISPLAY[x,y] = CHR_UNSET;
				END
	 		END
		END
	END

	void fizzbuzz()
	var
		int i := 0;
	begin
		i = 1;				

		while (i < 100) do
			if (i % 3 == 0 && i % 5 == 0) then
				out "FizzBuzz";
			elsif (i % 3 == 0) then
				out "Fizz";
			elsif (i % 5 == 0) then
				out "Buzz";
			else
				out i;
			end

			out "\r\n";

			i++;
		end
	end

	VOID Create()
	BEGIN
		Kill(1, 1);
	END
	 
	VOID Kill(int x, int y)
	VAR
		BOOL top;
		BOOL left;
		BOOL bot;
		BOOL right;
		INT possibleDirections;
		INT direction;
	BEGIN
		FOR(;;) DO
			top   = DISPLAY[x + 0, y - 1] == CHR_UNSET;
			left  = DISPLAY[x - 1, y + 0] == CHR_UNSET;
			bot   = DISPLAY[x + 0, y + 1] == CHR_UNSET;
			right = DISPLAY[x + 1, y + 0] == CHR_UNSET;

			possibleDirections = 0;
			possibleDirections = possibleDirections + (int)top;
			possibleDirections = possibleDirections + (int)left;
			possibleDirections = possibleDirections + (int)bot;
			possibleDirections = possibleDirections + (int)right;

	 
			IF (possibleDirections == 0) THEN
				DISPLAY[x, y] = CHR_FLOOR;

				Hunt();

				RETURN;
			END
	 
	 		direction = (((int)RAND * 2 + (int)RAND) * 2 + (int)RAND) % possibleDirections;
	 
			IF (top) THEN
				IF (direction == 0) THEN
					IF (DISPLAY[x, y + 1] == CHR_UNSET) THEN
						DISPLAY[x, y + 1] = CHR_WALL;
					END
					IF (DISPLAY[x + 1, y] == CHR_UNSET) THEN
						DISPLAY[x + 1, y] = CHR_WALL;
					END
					IF (DISPLAY[x - 1, y] == CHR_UNSET) THEN
						DISPLAY[x - 1, y] = CHR_WALL;
					END
					DISPLAY[x, y] = CHR_FLOOR;
					y--;
				END
				direction--;
			END
			
			IF (left) THEN
				IF (direction == 0) THEN
					IF (DISPLAY[x + 1, y] == CHR_UNSET) THEN
						DISPLAY[x + 1, y] = CHR_WALL;
					END
					IF (DISPLAY[x, y + 1] == CHR_UNSET) THEN
						DISPLAY[x, y + 1] = CHR_WALL;
					END
					IF (DISPLAY[x, y - 1] == CHR_UNSET) THEN
						DISPLAY[x, y - 1] = CHR_WALL;
					END
					DISPLAY[x, y] = CHR_FLOOR;
					x--;
				END
				direction--;
			END
			
			IF (bot) THEN
				IF (direction == 0) THEN
					IF (DISPLAY[x, y - 1] == CHR_UNSET) THEN
						DISPLAY[x, y - 1] = CHR_WALL;
					END
					IF (DISPLAY[x + 1, y] == CHR_UNSET) THEN
						DISPLAY[x + 1, y] = CHR_WALL;
					END
					IF (DISPLAY[x - 1, y] == CHR_UNSET) THEN
						DISPLAY[x - 1, y] = CHR_WALL;
					END
					DISPLAY[x, y] = CHR_FLOOR;
					y++;
				END
				direction--;
			END
			
			IF (right) THEN
				IF (direction == 0) THEN
					IF (DISPLAY[x - 1, y] == CHR_UNSET) THEN
						DISPLAY[x - 1, y] = CHR_WALL;
					END
					IF (DISPLAY[x, y + 1] == CHR_UNSET) THEN
						DISPLAY[x, y + 1] = CHR_WALL;
					END
					IF (DISPLAY[x, y - 1] == CHR_UNSET) THEN
						DISPLAY[x, y - 1] = CHR_WALL;
					END
					DISPLAY[x, y] = CHR_FLOOR;
					x++;
				END
				direction--;
			END
		END
	END
	 
	VOID Hunt()
	VAR
	 INT ox;
	 INT oy;
	 INT fx;
	 INT fy;
	 INT x;
	 INT y;
	BEGIN
		ox = (((((int)RAND*2 + (int)RAND)*2 + (int)RAND)*2 + (int)RAND)*2 + (int)RAND)*2 + (int)RAND;
		oy = (((((int)RAND*2 + (int)RAND)*2 + (int)RAND)*2 + (int)RAND)*2 + (int)RAND)*2 + (int)RAND;
	
	 	ox = ox % DISPLAY_WIDTH;
	 	oy = oy % DISPLAY_HEIGHT;
	 
		FOR (fy = 0; fy < DISPLAY_HEIGHT; fy++) DO
			FOR (fx = 0; fx < DISPLAY_WIDTH; fx++) DO
				x = (fx + ox) % DISPLAY_WIDTH;
				y = (fy + oy) % DISPLAY_HEIGHT;

				IF (DISPLAY[x, y] == CHR_UNSET) THEN
					IF (y > 1 && ((x) % 2 != 0 || (y - 1) % 2 != 0)) THEN
						IF (DISPLAY[x, y - 1] == CHR_WALL && DISPLAY[x, y - 2] == CHR_FLOOR) THEN
							DISPLAY[x, y - 1] = CHR_FLOOR;
							Kill(x, y - 1);
							return;
						END
					END
	
					IF (x > 1 && ((x - 1) % 2 != 0 || (y) % 2 != 0)) THEN
						IF (DISPLAY[x - 1, y] == CHR_WALL && DISPLAY[x - 2, y] == CHR_FLOOR) THEN
							DISPLAY[x - 1, y] = CHR_FLOOR;
							Kill(x - 1, y);
							return;
						END
					END
	
					IF (y < DISPLAY_HEIGHT - 2 && ((x) % 2 != 0 || (y + 1) % 2 != 0)) THEN
						IF (DISPLAY[x, y + 1] == CHR_WALL && DISPLAY[x, y + 2] == CHR_FLOOR) THEN
							DISPLAY[x, y + 1] = CHR_FLOOR;
							Kill(x, y + 1);
							return;
						END
					END
	
					IF (x < DISPLAY_WIDTH - 2 && ((x + 1) % 2 != 0 || (y) % 2 != 0)) THEN
						IF (DISPLAY[x + 1, y] == CHR_WALL && DISPLAY[x + 2, y] == CHR_FLOOR) THEN
							DISPLAY[x + 1, y] = CHR_FLOOR;
							Kill(x + 1, y);
							return;
						END
					END
				END
			END
		END
	END

end