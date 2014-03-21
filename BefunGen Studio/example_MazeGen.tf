/*
 * MazeGen
 * by Mikescher
 *
 * 21.03.2014
*/

PROGRAM MazeGen : DISPLAY[131, 51]
	CONST
		CHAR CHR_UNSET := '@';
		CHAR CHR_WALL  := '#';
		CHAR CHR_FLOOR := ' ';
		CHAR CHR_PATH  := '+';
	BEGIN
		Init();
		
		Create();
	END
	 
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
	 
		FOR (fy = 1; fy < DISPLAY_HEIGHT - 1; fy++) DO
			FOR (fx = 1; fx < DISPLAY_WIDTH - 1; fx++) DO
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
END