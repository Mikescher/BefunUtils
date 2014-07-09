/*
 * Square It
 * by Mike Schwörer 2014
*/

program Square_It : display[34, 36] // 1 + 1+2*16 || 1 + 1+2*16 + 2
	const
		int FIELD_WIDTH := 16;
		int FIELD_HEIGHT := 16;
		
		char CHAR_VERTICAL := '|';
		char CHAR_HORIZONTAL := '-';
		char CHAR_EMPTY := ' ';
		char CHAR_JUNCTION := '+';
		char CHAR_EMPTYFIELD := ' ';
		char CHAR_PLAYER_P1 := 'X';
		char CHAR_PLAYER_P2 := 'O';
		char CHAR_PLAYER_NEUTRAL := '#';
	
	global
		char currPlayer;
		int p1_c, p2_c;
	begin
		restart();
	end
	
	void restart()
	var
		int choice;
	begin
		for(;;) do
			Init();
			
			outf	"WHAT DO YOU WANT TO PLAY?", 
					"\r\n", 
					"0: Player vs Player", 
					"\r\n", 
					"1: Player vs Computer", 
					"\r\n", 
					"2: Computer vs Computer",
					"\r\n",
					"\r\n",
					"\r\n";
					
			in choice;
			
			if (choice == 0) then
				Game_P_v_P();
			elsif (choice == 1) then
				Game_P_v_NP();
			elsif (choice == 2) then
				Game_NP_v_NP();
			end
		end
	end
	
	void Game_P_v_P()
	var
		char winner;
	begin
		currPlayer = CHAR_PLAYER_P1;
	
		repeat
			outf "PLAYER ", currPlayer, ":\r\n";
			DoPlayerTurn();
		until (GameFinished())
		
		winner = GetWinningPlayer();
		
		if (winner == CHAR_PLAYER_P1) then
			out ">> PLAYER 1 (X) WINS !\r\n\r\n";
		elsif (winner == CHAR_PLAYER_P2) then
			out ">> PLAYER 2 (O) WINS !\r\n\r\n";
		else
			out ">> DRAW !\r\n\r\n";
		end
		
		return;
	end
	
	void Game_P_v_NP()
	var
		char winner;
	begin
		currPlayer = CHAR_PLAYER_P1;
	
		repeat
			if (currPlayer == CHAR_PLAYER_P1) then
				outf "PLAYER ", currPlayer, ":\r\n";
				DoPlayerTurn();
			else
				outf "COMPUTER ", currPlayer, ":\r\n";
				DoComputerTurn();
			end
		until (GameFinished())
		
		winner = GetWinningPlayer();
		
		if (winner == CHAR_PLAYER_P1) then
			out ">> YOU WIN !\r\n\r\n";
		elsif (winner == CHAR_PLAYER_P2) then
			out ">> YOU LOOSE !\r\n\r\n";
		else
			out ">> DRAW !\r\n\r\n";
		end
		
		return;
	end
	
	void Game_NP_v_NP()
	var
		char winner;
	begin
		currPlayer = CHAR_PLAYER_P1;
	
		repeat
			outf "COMPUTER ", currPlayer, ":\r\n";
			DoComputerTurn();
		until (GameFinished())
		
		winner = GetWinningPlayer();
		
		if (winner == CHAR_PLAYER_P1) then
			out ">> PC1 (X) WINS !\r\n\r\n";
		elsif (winner == CHAR_PLAYER_P2) then
			out ">> PC2 (O) WINS !\r\n\r\n";
		else
			out ">> DRAW !\r\n\r\n";
		end
		
		return;
	end
	
	void Init()
	var
		int x, y;
		int px, py;
	begin
		for(x = 0; x < FIELD_WIDTH; x++) do
			if (x > 9) then
				display[2 + x*2, 0] = (char)(x + (int)'7');
			else
				display[2 + x*2, 0] = (char)(x + (int)'0');
			end
		end
		
		for(y = 0; y < FIELD_HEIGHT; y++) do
			if (y > 9) then
				display[0, 2 + y*2] = (char)(y + (int)'7');
			else
				display[0, 2 + y*2] = (char)(y + (int)'0');
			end
		end
	
		for(x = 0; x < FIELD_WIDTH; x++) do
			for(y = 0; y < FIELD_HEIGHT; y++) do
				px = 2 + x*2;
				py = 2 + y*2;
				
				// CENTER
				display[px + 0, py + 0] = CHAR_EMPTYFIELD; 
				
				// TOP RIGHT
				display[px + 1, py + 1] = CHAR_JUNCTION;
			
				// BOTTOM RIGHT
				display[px - 1, py + 1] = CHAR_JUNCTION;
				
				// BOTTOM LEFT
				display[px - 1, py - 1] = CHAR_JUNCTION;
				
				// TOP LEFT
				display[px + 1, py - 1] = CHAR_JUNCTION; 
				
				// TOP
				if (y == 0) then
					display[px + 0, py - 1] = CHAR_HORIZONTAL; 
				else
					display[px + 0, py - 1] = CHAR_EMPTY; 
				end
				
				// RIGHT
				if (x == FIELD_WIDTH - 1) then
					display[px + 1, py + 0] = CHAR_VERTICAL; 
				else
					display[px + 1, py + 0] = CHAR_EMPTY; 
				end
				
				// BOTTOM
				if (y == FIELD_HEIGHT - 1) then
					display[px + 0, py + 1] = CHAR_HORIZONTAL;
				else
					display[px + 0, py + 1] = CHAR_EMPTY; 
				end
				
				// LEFT
				if (x == 0) then
					display[px - 1, py + 0] = CHAR_VERTICAL;
				else
					display[px - 1, py + 0] = CHAR_EMPTY;
				end
	 		end
		end
	end
	
	void DoPlayerTurn()
	var
		char x,y,d;
		
		int ix, iy, idx, idy;
		
		int posx, posy;
	begin
		out "    ";
		out "X: ";
		in x;
		out x;
		out " Y: ";
		in y;
		out y;
		out " Direction (U/D/L/R): ";
		in d;
		outf d, "\r\n";
		
		if (x >= '0' && x <= '9') then
			ix = (int)x - (int)'0';
		elsif (x >= 'A' && x <= 'Z') then
			ix = (int)x - (int)'A';
		elsif (x >= 'a' && x <= 'z') then
			ix = (int)x - (int)'a';
		else
			out "    ";
			out "ERROR - CANT PARSE INPUT (X)\r\n";
			DoPlayerTurn();
			return;
		end
		
		if (y >= '0' && y <= '9') then
			iy = (int)y - (int)'0';
		elsif (y >= 'A' && y <= 'Z') then
			iy = (int)y - (int)'A';
		elsif (y >= 'a' && y <= 'z') then
			iy = (int)y - (int)'a';
		else
			out "ERROR - CANT PARSE INPUT (Y)\r\n";
			DoPlayerTurn();
			return;
		end
		
		if (d == 'U' || d == 'u') then
			idx = 0;
			idy = -1;
		elsif (d == 'R' || d == 'r') then
			idx = 1;
			idy = 0;
		elsif (d == 'D' || d == 'd') then
			idx = 0;
			idy = 1;
		elsif (d == 'L' || d == 'l') then
			idx = -1;
			idy = 0;
		else
			out "    ";
			out "ERROR - CANT PARSE INPUT (DIRECTION)\r\n";
			DoPlayerTurn();
			return;
		end
		
		posx = 2 + ix*2 + idx;
		posy = 2 + iy*2 + idy;
		
		if (display[posx, posy] == CHAR_EMPTY) then
			DoTurn(ix, iy, idx, idy);
			
			return;
		else
			out "    ";
			out "ERROR - FIELD ALREADY SET\r\n";
			DoPlayerTurn();
			return;
		end
	end
	
	void DoTurn(int x, int y, int dx, int dy)
	var
		int posx, posy;
		
		bool t_a, t_b;
	begin
		posx = 2 + 2*x;
		posy = 2 + 2*y;
	
		if (dx == 0) then
			display[posx + dx, posy + dy] = CHAR_HORIZONTAL;
		else
			display[posx + dx, posy + dy] = CHAR_VERTICAL;
		end
		
		t_a = testField(x, y);
		t_b = testField(x + dx, y + dy);
		
		if (! (t_a || t_b)) then
			SwitchPlayer();
		end
	end
	
	void DoComputerTurn()
	begin
		if (FindComputerTurn(3)) then 
			return;
		end
	
		if (FindComputerTurn(1)) then 
			return;
		end
	
		if (FindComputerTurn(0)) then 
			return;
		end
	
		if (FindComputerTurn(2)) then 
			return;
		end
		
		while (true) do out "ERROR"; end
	end
	
	bool FindComputerTurn(int target_surr)
	var 
		int c_x, c_y;
		int x, y;
		int r_x, r_y, r_d;
		
		int c_i, i;
		int dx, dy;
	begin
		r_x = RAND[4];
		r_y = RAND[4];
		r_d = RAND[1];
	
		for(c_x = 0; c_x < FIELD_WIDTH; c_x++) do
			for(c_y = 0; c_y < FIELD_HEIGHT; c_y++) do
				x = (c_x + r_x) % FIELD_WIDTH;
				y = (c_y + r_y) % FIELD_HEIGHT;
				
				if (getSurrounding(x, y) == target_surr) then
					for(c_i = 0; c_i < 4; c_i++) do
						i = (c_i + r_d) % 4;
						
						switch(i)
						begin
							case 0:
								dx = 0;
								dy = -1;
							end 
							case 1:
								dx = 0;
								dy = 1;
							end 
							case 2:
								dx = -1;
								dy = 0;
							end 
							case 3:
								dx = 1;
								dy = 0;
							end 
						end
						
						if (display[2+2*x + dx, 2+2*y + dy] == CHAR_EMPTY) then
							switch(i)
							begin
								case 0:
									outf "  X: ", x, " Y: ", y, " D: [UP]", "\r\n";
								end 
								case 1:
									outf "  X: ", x, " Y: ", y, " D: [DOWN]", "\r\n";
								end 
								case 2:
									outf "  X: ", x, " Y: ", y, " D: [LEFT]", "\r\n";
								end 
								case 3:
									outf "  X: ", x, " Y: ", y, " D: [RIGHT]", "\r\n";
								end 
							end
							
							DoTurn(x, y, dx, dy);
							
							return true;
						end
					end
				end
			end
		end
		
		return false;
	end
	
	bool TestField(int x, int y)
	var
		bool result;
	begin
		x = 2 + x*2;
		y = 2 + y*2;
		
		result = true;
		
		result &= (display[x, y] == CHAR_EMPTYFIELD);
		
		result &= (display[x + 1, y] != CHAR_EMPTY);
		result &= (display[x - 1, y] != CHAR_EMPTY);
		result &= (display[x, y + 1] != CHAR_EMPTY);
		result &= (display[x, y - 1] != CHAR_EMPTY);
		
		if (result) then
			display[x, y] = currplayer;
			return true;
		else
			return false;
		end
	end
	
	void SwitchPlayer() 
	begin
		if (currplayer == CHAR_PLAYER_P1) then
			currplayer = CHAR_PLAYER_P2;
		else
			currplayer = CHAR_PLAYER_P1;
		end
		
		display[DISPLAY_WIDTH - 9, DISPLAY_HEIGHT - 1] = 'P';
		display[DISPLAY_WIDTH - 8, DISPLAY_HEIGHT - 1] = 'L';
		display[DISPLAY_WIDTH - 7, DISPLAY_HEIGHT - 1] = 'A';
		display[DISPLAY_WIDTH - 6, DISPLAY_HEIGHT - 1] = 'Y';
		display[DISPLAY_WIDTH - 5, DISPLAY_HEIGHT - 1] = 'E';
		display[DISPLAY_WIDTH - 4, DISPLAY_HEIGHT - 1] = 'R';
		display[DISPLAY_WIDTH - 3, DISPLAY_HEIGHT - 1] = ' ';
		display[DISPLAY_WIDTH - 2, DISPLAY_HEIGHT - 1] = currplayer;
		
		display[1, DISPLAY_HEIGHT - 1] = (char)((p1_c/100)%10 + 48);
		display[2, DISPLAY_HEIGHT - 1] = (char)((p1_c/10)%10 + 48);
		display[3, DISPLAY_HEIGHT - 1] = (char)((p1_c/1)%10 + 48);
		display[4, DISPLAY_HEIGHT - 1] = ' ';
		display[5, DISPLAY_HEIGHT - 1] = '-';
		display[6, DISPLAY_HEIGHT - 1] = ' ';
		display[7, DISPLAY_HEIGHT - 1] = (char)((p2_c/100)%10 + 48);
		display[8, DISPLAY_HEIGHT - 1] = (char)((p2_c/10)%10 + 48);
		display[9, DISPLAY_HEIGHT - 1] = (char)((p2_c/1)%10 + 48);
	end
	
	int GetSurrounding(int x, int y)
	var
		int result;
	begin
		result = 0;
		
		x = 2 + x*2;
		y = 2 + y*2;
		
		result += (int)(display[x + 1, y] != CHAR_EMPTY);
		result += (int)(display[x - 1, y] != CHAR_EMPTY);
		result += (int)(display[x, y + 1] != CHAR_EMPTY);
		result += (int)(display[x, y - 1] != CHAR_EMPTY);
		
		return result;
	end
	
	bool GameFinished()
	var
		int x, y;
	begin
		p1_c = 0;
		p2_c = 0;
		
		for(x = 0; x < FIELD_WIDTH; x++) do
			for(y = 0; y < FIELD_HEIGHT; y++) do
				if (display[2+2*x, 2+2*y] == CHAR_PLAYER_P1) then
					p1_c++;
				elsif (display[2+2*x, 2+2*y] == CHAR_PLAYER_P2) then
					p2_c++;
				end
			end
		end
		
		return p1_c + p2_c == FIELD_WIDTH * FIELD_HEIGHT;
	end
	
	char GetWinningPlayer()
	begin
		if (p1_c > p2_c) then
			return CHAR_PLAYER_P1;
		elsif (p1_c < p2_c) then
			return CHAR_PLAYER_P2;
		else
			return CHAR_PLAYER_NEUTRAL;
		end
	end
end