@ECHO OFF

echo ========BefunCompile========
cd BefunCompile   & git reset --hard & git clean -fd & git checkout . & git pull & git checkout . & git clean -fd & cd ..

echo ========BefunDebug========
cd BefunDebug     & git reset --hard & git clean -fd & git checkout . & git pull & git checkout . & git clean -fd & cd ..

echo ========BefunExec========
cd BefunExec      & git reset --hard & git clean -fd & git checkout . & git pull & git checkout . & git clean -fd & cd ..

echo ========BefunGen========
cd BefunGen       & git reset --hard & git clean -fd & git checkout . & git pull & git checkout . & git clean -fd & cd ..

echo ========BefunHighlight========
cd BefunHighlight & git reset --hard & git clean -fd & git checkout . & git pull & git checkout . & git clean -fd & cd ..

echo ========BefunRep========
cd BefunRep       & git reset --hard & git clean -fd & git checkout . & git pull & git checkout . & git clean -fd & cd ..

echo ========BefunWrite========
cd BefunWrite     & git reset --hard & git clean -fd & git checkout . & git pull & git checkout . & git clean -fd & cd ..

echo ========BefunRun========
cd BefunRun       & git reset --hard & git clean -fd & git checkout . & git pull & git checkout . & git clean -fd & cd ..

PAUSE