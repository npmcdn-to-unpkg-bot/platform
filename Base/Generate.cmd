@SET PATH=%PATH%;C:\Program Files\MSBuild\14.0\Bin;C:\Program Files (x86)\MSBuild\14.0\Bin;C:\Windows\Microsoft.NET\Framework64\v4.0.30319;C:\Windows\Microsoft.NET\Framework\v4.0.3031

msbuild repository/repository.sln /target:Clean /verbosity:minimal

..\Tools\dist\Allors.Tools.Cmd.exe repository generate base.sln repository templates/meta.cs.stg meta/generated

@pause
