#!/bin/sh

xbuild Drivers.sln /target:Meta:Rebuild /p:Configuration="Debug" /verbosity:minimal
xbuild Meta/Generate.proj /verbosity:minimal
