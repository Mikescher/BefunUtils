/*
 * MazeGen
 * by Mikescher
 *
 * 21.03.2014
*/

program MazeGen : display[131, 51]
	const
		char CHR_UNSET := '@';
		char CHR_WALL  := '#';
		char CHR_FLOOR := ' ';
		char CHR_PATH  := '+';
	var
		bool succ;
	begin
		Init();
		
		Create();
		
		succ = Solve(1, 1, DISPLAY_WIDTH - 2, DISPLAY_HEIGHT - 2);
		
		if (succ) then
			out "Maze solved";
		else
			out "Fatal Error: Maze could not be solved";
		end
	end
	 
	void Init()
	var
		int x;
		int y;
	begin
		for(x = 0; x < DISPLAY_WIDTH; x++) do
			for(y = 0; y < DISPLAY_HEIGHT; y++) do
				if (x == 0 || y == 0 || x == DISPLAY_WIDTH - 1 || y == DISPLAY_HEIGHT - 1 || (x % 2 == 0 && y % 2 == 0)) then
					display[x,y] = CHR_WALL;
				ELSE
					display[x,y] = CHR_UNSET;
				end
	 		end
		end
	end
	 
	void Create()
	begin
		Kill(1, 1);
	end
	 
	void Kill(int x, int y)
	var
		bool top;
		bool left;
		bool bot;
		bool right;
		int possibleDirections;
		int direction;
	begin
		for(;;) do
			top   = (display[x + 0, y - 1] == CHR_UNSET);
			left  = (display[x - 1, y + 0] == CHR_UNSET);
			bot   = (display[x + 0, y + 1] == CHR_UNSET);
			right = (display[x + 1, y + 0] == CHR_UNSET);

			possibleDirections = 0;
			possibleDirections += (int)top;
			possibleDirections += (int)left;
			possibleDirections += (int)bot;
			possibleDirections += (int)right;

	 
			if (possibleDirections == 0) then
				display[x, y] = CHR_FLOOR;

				Hunt();

				return;
			end
	 
	 		direction = rand[3] % possibleDirections;
	 
			if (top) then
				if (direction == 0) then
					if (display[x, y + 1] == CHR_UNSET) then
						display[x, y + 1] = CHR_WALL;
					end
					if (display[x + 1, y] == CHR_UNSET) then
						display[x + 1, y] = CHR_WALL;
					end
					if (display[x - 1, y] == CHR_UNSET) then
						display[x - 1, y] = CHR_WALL;
					end
					display[x, y] = CHR_FLOOR;
					y--;
				end
				direction--;
			end
			
			if (left) then
				if (direction == 0) then
					if (display[x + 1, y] == CHR_UNSET) then
						display[x + 1, y] = CHR_WALL;
					end
					if (display[x, y + 1] == CHR_UNSET) then
						display[x, y + 1] = CHR_WALL;
					end
					if (display[x, y - 1] == CHR_UNSET) then
						display[x, y - 1] = CHR_WALL;
					end
					display[x, y] = CHR_FLOOR;
					x--;
				end
				direction--;
			end
			
			if (bot) then
				if (direction == 0) then
					if (display[x, y - 1] == CHR_UNSET) then
						display[x, y - 1] = CHR_WALL;
					end
					if (display[x + 1, y] == CHR_UNSET) then
						display[x + 1, y] = CHR_WALL;
					end
					if (display[x - 1, y] == CHR_UNSET) then
						display[x - 1, y] = CHR_WALL;
					end
					display[x, y] = CHR_FLOOR;
					y++;
				end
				direction--;
			end
			
			if (right) then
				if (direction == 0) then
					if (display[x - 1, y] == CHR_UNSET) then
						display[x - 1, y] = CHR_WALL;
					end
					if (display[x, y + 1] == CHR_UNSET) then
						display[x, y + 1] = CHR_WALL;
					end
					if (display[x, y - 1] == CHR_UNSET) then
						display[x, y - 1] = CHR_WALL;
					end
					display[x, y] = CHR_FLOOR;
					x++;
				end
				direction--;
			end
		end
	end
	 
	void Hunt()
	var
	 int ox;
	 int oy;
	 int fx;
	 int fy;
	 int x;
	 int y;
	begin
		ox = rand[6];
		oy = rand[6];
	
	 	ox %= DISPLAY_WIDTH;
	 	oy %= DISPLAY_HEIGHT;
	 
		for (fy = 0; fy < DISPLAY_HEIGHT; fy++) do
			for (fx = 0; fx < DISPLAY_WIDTH; fx++) do
				x = (fx + ox) % DISPLAY_WIDTH;
				y = (fy + oy) % DISPLAY_HEIGHT;

				if (display[x, y] == CHR_UNSET) then
					if (y > 1 && ((x) % 2 != 0 || (y - 1) % 2 != 0)) then
						if (display[x, y - 1] == CHR_WALL && display[x, y - 2] == CHR_FLOOR) then
							display[x, y - 1] = CHR_FLOOR;
							Kill(x, y - 1);
							return;
						end
					end
	
					if (x > 1 && ((x - 1) % 2 != 0 || (y) % 2 != 0)) then
						if (display[x - 1, y] == CHR_WALL && display[x - 2, y] == CHR_FLOOR) then
							display[x - 1, y] = CHR_FLOOR;
							Kill(x - 1, y);
							return;
						end
					end
	
					if (y < DISPLAY_HEIGHT - 2 && ((x) % 2 != 0 || (y + 1) % 2 != 0)) then
						if (display[x, y + 1] == CHR_WALL && display[x, y + 2] == CHR_FLOOR) then
							display[x, y + 1] = CHR_FLOOR;
							Kill(x, y + 1);
							return;
						end
					end
	
					if (x < DISPLAY_WIDTH - 2 && ((x + 1) % 2 != 0 || (y) % 2 != 0)) then
						if (display[x + 1, y] == CHR_WALL && display[x + 2, y] == CHR_FLOOR) then
							display[x + 1, y] = CHR_FLOOR;
							Kill(x + 1, y);
							return;
						end
					end
				end
			end
		end
	end
	 
	bool Solve(int x, int y, int tx, int ty)
	var 
		bool top;
		bool left;
		bool bot;
		bool right;
	begin
			top   = display[x + 0, y - 1] == CHR_FLOOR;
			left  = display[x - 1, y + 0] == CHR_FLOOR;
			bot   = display[x + 0, y + 1] == CHR_FLOOR;
			right = display[x + 1, y + 0] == CHR_FLOOR;
			
			display[x, y] = CHR_PATH;

			if (x == tx && y == ty) then
				return true;
			end

			if (top) then
				if (Solve(x, y - 1, tx, ty)) then
					return true;
				end
			end

			if (left) then
				if (Solve(x - 1, y, tx, ty)) then
					return true;
				end
			end

			if (bot) then
				if (Solve(x, y + 1, tx, ty)) then
					return true;
				end
			end

			if (right) then
				if (Solve(x + 1, y, tx, ty)) then
					return true;
				end
			end

			display[x, y] = CHR_FLOOR;

			return false;
	end
	 
	 
end