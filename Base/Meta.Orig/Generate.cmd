@SET PATH=%PATH%;C:\Program Files\MSBuild\14.0\Bin;C:\Program Files (x86)\MSBuild\14.0\Bin;C:\Windows\Microsoft.NET\Framework64\v4.0.30319;C:\Windows\Microsoft.NET\Framework\v4.0.30319

msbuild ../Base.sln /target:Clean /verbosity:minimal
msbuild ../Base.sln /target:Meta_Orig:Rebuild /p:Configuration="Debug" /verbosity:minimal
msbuild Generate.proj /verbosity:minimal

@pause