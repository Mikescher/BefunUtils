/*
 * Example Program
 * 4 Grammar Testing
 *
*/

program example
  var
    digit[16] txt;
    int[4] test := {1, 2, 3, 4};
    int dt;
    digit x;
  begin
    dt = test[1];
    dt = getVal(dt + rand, 3);
    x = (digit)dt;

    if (rand) then // 50\50 Chance
      out getInputStr(); 
    end

    if (x) then
      return true;
    else
      return false;
    end
  end

  int getVal(int param, int expo)
  var
    int result;
  begin
    result = param;
    while(expo) do
    begin
      result = result * param;
      expo = expo - 1;
    end 

    return param;
  end

  char[16] getInputStr()
  var
    int count := 16;
    char[16] result;
  begin
    repeat
    begin
      in result[16-count];
      count--;
    end
    until (count == 0)
  end
end