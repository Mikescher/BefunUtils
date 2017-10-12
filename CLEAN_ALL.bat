@ECHO OFF

echo ========BefunCompile========
cd BefunCompile   & git reset --hard & git clean -ndX & git clean -fdX & cd ..

echo ========BefunDebug========
cd BefunDebug     & git reset --hard & git clean -ndX & git clean -fdX & cd ..

echo ========BefunExec========
cd BefunExec      & git reset --hard & git clean -ndX & git clean -fdX & cd ..

echo ========BefunGen========
cd BefunGen       & git reset --hard & git clean -ndX & git clean -fdX & cd ..

echo ========BefunHighlight========
cd BefunHighlight & git reset --hard & git clean -ndX & git clean -fdX & cd ..

echo ========BefunRep========
cd BefunRep       & git reset --hard & git clean -ndX & git clean -fdX & cd ..

echo ========BefunWrite========
cd BefunWrite     & git reset --hard & git clean -ndX & git clean -fdX & cd ..

echo ========BefunRun========
cd BefunRun       & git reset --hard & git clean -ndX & git clean -fdX & cd ..

PAUSE