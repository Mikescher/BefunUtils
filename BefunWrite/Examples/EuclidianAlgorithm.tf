/*
 * Hello World
 * by Mike Schwörer 2014
*/

program EuclidianAlgo
	var
		int a;
		int b;
		
		int eucl;
	begin
		out "Please insert numer [a]\r\n";
		in a;
		out "Please insert numer [b]\r\n";
		in b;
		
		eucl = euclid(a, b);
		
		outf "euclid(" + a + "," + b + ") = " + euclid; 
		
		quit;
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
end