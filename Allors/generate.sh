#!/bin/sh

xbuild Allors.sln /target:Meta:Rebuild /p:Configuration="Debug" /verbosity:minimal
xbuild Meta/Generate.proj /verbosity:minimal
