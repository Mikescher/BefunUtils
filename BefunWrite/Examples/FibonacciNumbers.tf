/*
 * Fibonacci Numbers
 * by Mike Schwörer 2014
*/

program Fibbonacci
	var
		int i;
	begin
		out "Input the maximum\r\n";
	
		in i;
		
		doFiber(i);
		
		quit;
	end
	
	void doFiber(int max)
	var
		int last := 0;
		int curr := 1;
		int tmp;
	begin
		repeat
			if (last > 0) then
				out ",";
			end
			
			out curr;
			
			tmp = curr + last;
			last = curr;
			curr = tmp;
		until (last > max)
	end
end