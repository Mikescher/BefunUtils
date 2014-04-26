/*
 * Hello World
 * by Mike Schwörer 2014
*/

program SieveOfEratosthenes : display[240, 80]
	begin
		// Init all Fields with '?'
		init(); 
		
		// Set an 'X' to every Primenumberfield
		calculate();
		
		// Output the primes
		output();
	end
	
	void init()
	var 
		int i;
	begin
		for(i = 0; i < DISPLAY_SIZE; i++) do
			display[i % DISPLAY_WIDTH, i / DISPLAY_WIDTH] = '?';
		end
		
		display[0, 0] = ' ';
		display[1, 0] = ' ';
	end
	
	void calculate()
	var 
		int i;
	begin
		for(i = 2; i < DISPLAY_SIZE; i++) do
			doNumber(i);
		end
	end
	
	void doNumber(int i) 
	var
		char c;
	begin
		c = display[i % DISPLAY_WIDTH, i / DISPLAY_WIDTH];
		
		if (c == 'X' || c == ' ') then
			return;
		elsif (c == '?') then
			display[i % DISPLAY_WIDTH, i / DISPLAY_WIDTH] = 'X';
			
			clear(i);
		end
	end
	
	void clear(int n)
	var
		int p;
	begin
		for(p = 2*n; p < DISPLAY_SIZE; p += n) do
			display[p % DISPLAY_WIDTH, p / DISPLAY_WIDTH] = ' ';
		end
	end
	
	void output()
	var
		int i;
	begin
		out "Prime Numbers:\r\n";
		for(i = 2; i < DISPLAY_SIZE; i++) do
			if (display[i % DISPLAY_WIDTH, i / DISPLAY_WIDTH] == 'X') then
				outf i, "\r\n";
			end
		end
	end
end