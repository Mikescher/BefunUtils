/*
 * PICalc
 *
 * Calculates PI to a certain degree with the Monte Carlo algorithm
 *
 * by Mike Schwörer 2014
*/

program PICalc : display[256, 256]
	const
		// WIDTH = HEIGHT = 4^SIZE
		int SIZE := 4; 
		
		// Total Count
		int COUNT := 65536;
		
		// After n Steps print out intermediate result
		int STEPS := 4096;
		
	global
		int hit;
		int miss;
	var
		int i;
	begin
		hit = 0;
		miss = 0;
		
		for(i = 0; i < COUNT; i++) do
			drop();
			
			if (i % STEPS == 0) then
				output();
			end
		end
		
		output();
	end
	
	void output()
	begin
		// PI := (4 * hit)/(total)
		outf "PI = ", (4*hit), "/", (hit+miss), " = ", floatDiv(4 * hit, miss + hit), "\r\n";
	end
	
	void drop()
	var
		int x, y;
		
		char c;
	begin
		x = RAND[SIZE];
		y = RAND[SIZE];
	
		c = display[x, y];
		
		display[x, y] = 'X';
	
		if (c == '#') then
			hit++;
		elsif (c == ' ') then
			miss++;
		else
			out "FATAL ERROR 0x01";
		end
		
		display[x, y] = c;
	end
	
	// Gives a string containing a/b as float back
	char[10] floatDiv(int a, int b)
	var
		char[10] result;
		int mantissa;
		int pos := 0;
	begin
		mantissa = a / b;
	
		repeat
			if (pos == 10) then
				return result;
			end
			
			result[pos++] = (char)((int)'0' + (mantissa % 10));
			mantissa /= 10;
		until (mantissa == 0)

		if (pos == 10) then
			return result;
		end
		result[pos++] = ',';

		for(;;) do
			if (pos == 10) then
				return result;
			end
			
			a %= b;
			a *= 10;
			result[pos++] = (char)((int)'0' + (a / b));
		end
	
		return result;
	end
end
