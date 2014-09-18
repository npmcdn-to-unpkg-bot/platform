@SET PATH=%PATH%;C:\Windows\Microsoft.NET\Framework64\v4.0.30319;C:\Windows\Microsoft.NET\Framework\v4.0.30319

@msbuild Allors.sln /target:Meta:Rebuild /p:Configuration="Debug" /verbosity:minimal
@msbuild Meta\Generate.proj /verbosity:minimal

@if NOT ["%errorlevel%"]==["0"] (
    pause
    exit /b %errorlevel%
)
