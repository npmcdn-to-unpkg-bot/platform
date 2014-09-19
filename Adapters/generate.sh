#!/bin/sh

xbuild Adapters.sln /target:Meta:Rebuild /p:Configuration="Debug" /verbosity:minimal
xbuild Meta/Generate.proj /verbosity:minimal
