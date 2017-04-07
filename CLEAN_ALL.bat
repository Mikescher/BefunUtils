@ECHO OFF

echo ========BefunCompile========
cd BefunCompile   & git clean -ndX & git clean -fdX & cd ..

echo ========BefunDebug========
cd BefunDebug     & git clean -ndX & git clean -fdX & cd ..

echo ========BefunExec========
cd BefunExec      & git clean -ndX & git clean -fdX & cd ..

echo ========BefunGen========
cd BefunGen       & git clean -ndX & git clean -fdX & cd ..

echo ========BefunHighlight========
cd BefunHighlight & git clean -ndX & git clean -fdX & cd ..

echo ========BefunRep========
cd BefunRep       & git clean -ndX & git clean -fdX & cd ..

echo ========BefunWrite========
cd BefunWrite     & git clean -ndX & git clean -fdX & cd ..

echo ========BefunRun========
cd BefunRun       & git clean -ndX & git clean -fdX & cd ..

PAUSE