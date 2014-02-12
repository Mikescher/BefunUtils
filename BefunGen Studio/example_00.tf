/*
 * Example Program II
 * 4 Grammar Testing
 *
*/

program example
	var 
		char[32] name;
		int i := 0;
	begin
		//Insert Name
		name = getInputStr();
		
		while (i < 32) do
		begin
			out name[i];
			i++;
		end
		
		// Print Fibbonacci
		
		/* doFiber(100); */
		
		out doFiber(44, 12);
	end

	void doFiber(int max)
	var
		int last := 0;
		int curr := 1;
		int tmp;
	begin

		repeat
		begin
			out curr;
			
			tmp = curr + last;
			last = curr;
			curr = tmp;
		end until (last > max)
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
		
		while(current < 32) do
		begin
		in input[current];
			current++;
		end

		return input;
	end
end