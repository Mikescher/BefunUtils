#!/bin/bash

xbuild BefunUtils.sln /nologo /t:Clean,Build /p:Platform="Any CPU" /p:Configuration=Release /verbosity:m /p:VisualStudioVersion=14.0

ead -p "Press [Enter] key to continue..."