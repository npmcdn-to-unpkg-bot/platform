@SET PATH=%PATH%;C:\Program Files\MSBuild\14.0\Bin;C:\Program Files (x86)\MSBuild\14.0\Bin;C:\Windows\Microsoft.NET\Framework\v4.0.30319

@msbuild Adapters.sln /target:Meta:Rebuild /p:Configuration="Debug" /verbosity:minimal
@msbuild Domain\Generate.proj /verbosity:minimal

@if NOT ["%errorlevel%"]==["0"] (
    pause
    exit /b %errorlevel%
)
