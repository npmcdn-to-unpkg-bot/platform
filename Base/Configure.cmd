@SET PATH=%PATH%;C:\Program Files\MSBuild\14.0\Bin;C:\Program Files (x86)\MSBuild\14.0\Bin;C:\Windows\Microsoft.NET\Framework64\v4.0.30319;C:\Windows\Microsoft.NET\Framework\v4.0.30319

msbuild repository.sln /target:Clean /verbosity:minimal

..\Tools\dist\Allors.Tools.Cmd.exe repository generate repository.sln repository templates/meta.cs.stg meta/generated

msbuild Base.sln /target:Clean /verbosity:minimal

msbuild Base.sln /target:Merge:Rebuild /p:Configuration="Debug" /verbosity:minimal
msbuild Resources/Merge.proj /verbosity:minimal
msbuild Base.sln /target:Meta:Rebuild /p:Configuration="Debug" /verbosity:minimal

msbuild Domain/Generate.proj /verbosity:minimal
msbuild Domain.Diagrams/Generate.proj /verbosity:minimal

msbuild Workspace.Diagrams/Generate.proj /verbosity:minimal
msbuild Workspace.Meta/Generate.proj /verbosity:minimal
msbuild Workspace.Domain/Generate.proj /verbosity:minimal
msbuild Workspace.Web/Generate.proj /verbosity:minimal

@pause