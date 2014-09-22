#!/bin/sh

xbuild Apps.sln /target:Meta:Rebuild /p:Configuration="Debug" /verbosity:minimal
xbuild Meta/Generate.proj /verbosity:minimal
