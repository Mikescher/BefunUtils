@ECHO OFF

set DFN=%DATE%

echo.
echo.
echo --------------------------------------------------------------------
echo.
echo ============ Base ============
echo.
                    git add . & git diff HEAD --binary > patch_base_%DFN%.diff          & git status
for %%F in ("patch_base_%DFN%.diff") do if %%~zF equ 0 del "%%F"
echo.
echo.
echo --------------------------------------------------------------------
echo.
echo ============ BefunCompile ============
echo.
cd BefunCompile   & git add . & git diff HEAD --binary > ../patch_befuncompile_%DFN%.diff   & git status & cd ..
for %%F in ("patch_befuncompile_%DFN%.diff") do if %%~zF equ 0 del "%%F"
echo.
echo.
echo --------------------------------------------------------------------
echo.
echo ============ BefunDebug ============
echo.
cd BefunDebug     & git add . & git diff HEAD --binary > ../patch_befundebug_%DFN%.diff   & git status & cd ..
for %%F in ("patch_befundebug_%DFN%.diff") do if %%~zF equ 0 del "%%F"
echo.
echo.
echo --------------------------------------------------------------------
echo.
echo ============ BefunExec ============
echo.
cd BefunExec      & git add . & git diff HEAD --binary > ../patch_befunexec_%DFN%.diff   & git status & cd ..
for %%F in ("patch_befunexec_%DFN%.diff") do if %%~zF equ 0 del "%%F"
echo.
echo.
echo --------------------------------------------------------------------
echo.
echo ============ BefunGen ============
echo.
cd BefunGen       & git add . & git diff HEAD --binary > ../patch_befungen_%DFN%.diff   & git status & cd ..
for %%F in ("patch_befungen_%DFN%.diff") do if %%~zF equ 0 del "%%F"
echo.
echo.
echo --------------------------------------------------------------------
echo.
echo ============ BefunHighlight ============
echo.
cd BefunHighlight & git add . & git diff HEAD --binary > ../patch_befunhighlight_%DFN%.diff   & git status & cd ..
for %%F in ("patch_befunhighlight_%DFN%.diff") do if %%~zF equ 0 del "%%F"
echo.
echo.
echo --------------------------------------------------------------------
echo.
echo ============ BefunRep ============
echo.
cd BefunRep       & git add . & git diff HEAD --binary > ../patch_befunrep_%DFN%.diff   & git status & cd ..
for %%F in ("patch_befunrep_%DFN%.diff") do if %%~zF equ 0 del "%%F"
echo.
echo.
echo --------------------------------------------------------------------
echo.
echo ============ BefunWrite ============
echo.
cd BefunWrite     & git add . & git diff HEAD --binary > ../patch_befunwrite_%DFN%.diff   & git status & cd ..
for %%F in ("patch_befunwrite_%DFN%.diff") do if %%~zF equ 0 del "%%F"
echo.
echo.
echo --------------------------------------------------------------------
echo.
echo ============ BefunRun ============
echo.
cd BefunRun      & git add . & git diff HEAD --binary > ../patch_befunrun_%DFN%.diff   & git status & cd ..
for %%F in ("patch_befunrun_%DFN%.diff") do if %%~zF equ 0 del "%%F"
echo.
echo.
echo --------------------------------------------------------------------
echo.


PAUSE