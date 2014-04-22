/*
 * SudoGen
 * by Mikescher
 *
 * 29.03.2014
*/

program example : display[17, 17]
	const
		char CHR_EMPTY     := ' ';
		char CHR_UNKNOWN   := ' ';
		char CHR_BORDER    := '#';
		char CHR_INTERSECT := '+';
		char CHR_HORZ      := '-';
		char CHR_VERT      := '|';
	begin
		Init();
		
		Create();
		
		Obfuscate();
	end

	void Init()
	var
		int x;
		int y;
	begin
		for (y = 0; y < DISPLAY_HEIGHT; y++) do
			for (x = 0; x < DISPLAY_WIDTH; x++) do
				if (x % 2 == 0 && y % 2 == 0) then
					display[x, y] = CHR_EMPTY;
				elsif ((x + 1) % 6 == 0 || (y + 1) % 6 == 0) then
					display[x, y] = CHR_BORDER;
				elsif ((x - 1) % 2 == 0 && (y - 1) % 2 == 0) then
					display[x, y] = CHR_INTERSECT;
				elsif ((x - 1) % 2 == 0 && y % 2 == 0) then
					display[x, y] = CHR_VERT;
				elsif (x % 2 == 0 && (y - 1) % 2 == 0) then
					display[x, y] = CHR_HORZ;
				end
			end
		end
	end

	bool Create()
	var
		int x;
		int y;

		int on;
		int n;
	begin
		if (!IsValid()) then
			return false;
		end

		on = rand[3] % 9;

		for (y = 0; y < 9; y++) do
			for (x = 0; x < 9; x++) do
				if (display[x * 2, y * 2] == CHR_EMPTY) then
					for (n = 0; n < 9; n++) do
						display[x * 2, y * 2] = (char)((int)'1' + ((n + on) % 9));

						if (Create()) then
							return true;
						end

						display[x * 2, y * 2] = CHR_EMPTY;
					end

					return false;
				end
			end
		end

		return true;
	end

	bool IsValid()
	var
		int x;
		int y;
		int p;
		int c;
		int[9] vals;
	begin
		// Rows

		for (y = 0; y < 9; y++) do
			for (p = 0; p < 9; ) do
				vals[p++] = 0;
			end

			for (x = 0; x < 9; x++) do
				if (display[x * 2, y * 2] != CHR_EMPTY) then
					vals[((int)display[x * 2, y * 2]) - ((int)'1')]++;
				end
			end

			for (p = 0; p < 9; p++) do
				if (vals[p] > 1) then
					return false;
				end
			end
		end

		// Cols

		for (x = 0; x < 9; x++) do
			for (p = 0; p < 9; ) do
				vals[p++] = 0;
			end

			for (y = 0; y < 9; y++) do
				if (display[x * 2, y * 2] != CHR_EMPTY) then
					vals[((int)display[x * 2, y * 2]) - ((int)'1')]++;
				end
			end

			for (p = 0; p < 9; p++) do
				if (vals[p] > 1) then
					return false;
				end
			end
		end

		// Rects

		for (c = 0; c < 9; c++) do
			for (p = 0; p < 9; ) do
				vals[p++] = 0;
			end

			for (x = (c / 3) * 3; x < (c / 3 + 1) * 3; x++) do
				for (y = (c % 3) * 3; y < (c % 3 + 1) * 3; y++) do
					if (display[x * 2, y * 2] != CHR_EMPTY) then
						vals[((int)display[x * 2, y * 2]) - ((int)'1')]++;
					end
				end
			end

			for (p = 0; p < 9; p++) do
				if (vals[p] > 1) then
					return false;
				end
			end
		end

		return true;

	end

	bool isRemovable(int x, int y)
	var
		int v;
		int rx;
		int ry;
		int p;
		bool[9] vals;
	begin
		

		v = ((int)display[x * 2, y * 2]) - ((int)'1');

		for (p = 0; p < 9; ) do
			vals[p++] = false;
		end

		// Row
		for (p = 0; p < 9; p++) do
			if (display[p * 2, y * 2] != CHR_UNKNOWN) then
				vals[((int)display[p * 2, y * 2]) - ((int)'1')] = true;
			end
		end

		// Col
		for (p = 0; p < 9; p++) do
			if (display[x * 2, p * 2] != CHR_UNKNOWN) then
				vals[((int)display[x * 2, p * 2]) - ((int)'1')] = true;
			end
		end

		//Rect
		for (rx = (x / 3) * 3; rx < (x / 3 + 1) * 3; rx++) do
			for (ry = (y / 3) * 3; ry < (y / 3 + 1) * 3; ry++) do
				if (display[rx * 2, rx * 2] != CHR_UNKNOWN) then
					vals[((int)display[rx * 2, ry * 2]) - ((int)'1')] = true;
				end
			end
		end

		//Test
		for (p = 0; p < 9; p++) do
			if (!vals[p] && p != v) then
				return false;
			end
		end

		return true;
	end

	void Obfuscate()
	var
		int ox;
		int oy;

		int x;
		int y;
	begin
		ox = rand[3];
		oy = rand[3];

		for (x = ox; x < ox + 9; x++) do
			for (y = oy; y < oy + 9; y++) do
				if (display[(x % 9) * 2, (y % 9) * 2] != CHR_UNKNOWN) then
					if (isRemovable(x % 9, y % 9)) then
						display[(x % 9) * 2, (y % 9) * 2] = CHR_UNKNOWN;
						Obfuscate();
						return;
					end
				end
			end
		end

		return;
	end
end