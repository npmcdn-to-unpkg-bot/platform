@SET PATH=%PATH%;C:\Windows\Microsoft.NET\Framework64\v4.0.30319;C:\Windows\Microsoft.NET\Framework\v4.0.30319

@msbuild Adapters.sln /target:Meta:Rebuild /p:Configuration="Debug" /verbosity:minimal
@msbuild Meta\GenerateAdapters.proj /verbosity:minimal

pause
