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

		fizzbuzz();

	end
end