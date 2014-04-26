/*
 * Fizz Buzz
 * by Mike Schwörer 2014
*/

program FizzBuzz
	begin
		fizzbuzz();
		quit;
	end
	
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
end