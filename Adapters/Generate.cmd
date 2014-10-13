@SET PATH=%PATH%;C:\Program Files\MSBuild\12.0\Bin;C:\Program Files (x86)\MSBuild\12.0\Bin;C:\Windows\Microsoft.NET\Framework\v4.0.30319

@msbuild Adapters.sln /target:Meta:Rebuild /p:Configuration="Debug" /verbosity:minimal
@msbuild Meta\Generate.proj /verbosity:minimal

@if NOT ["%errorlevel%"]==["0"] (
    pause
    exit /b %errorlevel%
)
