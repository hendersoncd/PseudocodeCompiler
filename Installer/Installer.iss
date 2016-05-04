#define AppName "Pseudocode Compiler"
#define SrcApp ".\..\PsudoCodeCompiler\bin\Release\PseudoCodeCompiler.exe"
#define FileVerStr GetFileVersion(SrcApp)
#define StripBuild(str VerStr) Copy(VerStr, 1, RPos(".", VerStr)-1)
#define AppVerStr StripBuild(FileVerStr)

[Setup]
AppName={#AppName}
AppVersion={#AppVerStr}
AppVerName={#AppName} {#AppVerStr}
UninstallDisplayName={#AppName} {#AppVerStr}
VersionInfoVersion={#FileVerStr}
VersionInfoTextVersion={#AppVerStr}
OutputBaseFilename=PseudoCompiler-{#AppVerStr}-Setup
DefaultDirName={pf}\PseudoCodeCompiler
DefaultGroupName=PseudoCodeCompiler
OutputDir=..\Releases
[Files]
Source: ..\PsudoCodeCompiler\bin\Release\PseudoCodeCompiler.exe; DestDir: {app}
Source: ..\PsudoCodeCompiler\bin\Release\PseudoCodeCompiler.exe.config; DestDir: {app}; Flags: onlyifdoesntexist
Source: isxdl.dll; DestDir: {tmp}; Flags: deleteafterinstall
[Icons]
Name: {group}\PseudoCodeCompiler; Filename: {app}\PseudoCodeCompiler.exe; WorkingDir: {app}; Comment: PseudoCodeCompiler; Flags: createonlyiffileexists; IconFilename: {app}\PseudoCodeCompiler.exe
Name: {group}\{cm:UninstallProgram, {#AppName}}; Filename: {uninstallexe}
[Messages]
WelcomeLabel2=This will install [name/ver] on your computer.%n%nPlease visit www.hendersontech.com for additional information and support.%n%nIt is recommended that you close all other applications before continuing.
[Run]
Filename: {app}\PseudoCodeCompiler.exe; WorkingDir: {app}; Description: Launch {#AppName}; Flags: nowait postinstall skipifsilent
[Code]
var
    dotnetRedistPath: string;
    downloadNeeded: boolean;
    dotNetNeeded: boolean;
    memoDependenciesNeeded: string;
    Version: TWindowsVersion;

procedure isxdl_AddFile(URL, Filename: PChar);
external 'isxdl_AddFile@files:isxdl.dll stdcall';
function isxdl_DownloadFiles(hWnd: Integer): Integer;
external 'isxdl_DownloadFiles@files:isxdl.dll stdcall';
function isxdl_SetOption(Option, Value: PChar): Integer;
external 'isxdl_SetOption@files:isxdl.dll stdcall';

const
    dotnetRedistURL = 'http://download.microsoft.com/download/7/0/3/703455ee-a747-4cc8-bd3e-98a615c3aedb/dotNetFx35setup.exe';

// Indicates whether .NET Framework 3.5 SP1 is installed.
function IsDotNETDetected(): boolean;
var
    success1: boolean;
    success2: boolean;
    install: cardinal;
    servicePack: cardinal;
begin
    success1 := RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5', 'Install', install);
    success2 := RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5', 'SP', servicePack);
    Result := (success1 and (install = 1)) and (success2 and (servicePack >= 1));
end;

//*********************************************************************************
// This is where all starts.
//*********************************************************************************
function InitializeSetup(): Boolean;

begin

    Result := true;
    dotNetNeeded := false;
    GetWindowsVersionEx(Version);

    //*********************************************************************************
    // Check for the existance of .NET 3.5 SP1
    //*********************************************************************************
    if (not IsDotNETDetected()) then
        begin
            dotNetNeeded := true;

            if (not IsAdminLoggedOn()) then
                begin
                    MsgBox('This program needs the Microsoft .NET Framework 3.5 SP1 to be installed by an Administrator', mbInformation, MB_OK);
                    Result := false;
                end
            else
                begin
                    memoDependenciesNeeded := memoDependenciesNeeded + '      .NET Framework 3.5 SP1' #13;
                    dotnetRedistPath := ExpandConstant('{src}\dotNetFx35setup.exe');
                    if not FileExists(dotnetRedistPath) then
                        begin
                            dotnetRedistPath := ExpandConstant('{tmp}\dotNetFx35setup.exe');
                            if not FileExists(dotnetRedistPath) then
                                begin
                                    isxdl_AddFile(dotnetRedistURL, dotnetRedistPath);
                                    downloadNeeded := true;
                                end
                        end

                    SetIniString('install', 'dotnetRedist', dotnetRedistPath, ExpandConstant('{tmp}\dep.ini'));
                end
        end;

end;

function NextButtonClick(CurPage: Integer): Boolean;

var
  hWnd: Integer;
  ResultCode: Integer;
  ResultXP: boolean;
  Result2003: boolean;

begin

  Result := true;
  ResultXP := true;
  Result2003 := true;

  //*********************************************************************************
  // Only run this at the "Ready To Install" wizard page.
  //*********************************************************************************
  if CurPage = wpReady then
    begin

        hWnd := StrToInt(ExpandConstant('{wizardhwnd}'));

        // don't try to init isxdl if it's not needed because it will error on < ie 3

        //*********************************************************************************
        // Download the .NET 3.5 redistribution file.
        //*********************************************************************************
        if downloadNeeded and (dotNetNeeded = true) then
            begin
                isxdl_SetOption('label', 'Downloading Microsoft .NET Framework 3.5 SP1');
                isxdl_SetOption('description', 'This program needs to install the Microsoft .NET Framework 3.5 SP1. Please wait while Setup is downloading extra files to your computer.');
                if isxdl_DownloadFiles(hWnd) = 0 then Result := false;
            end;

        //*********************************************************************************
        // Run the install file for .NET Framework 3.5 SP1.
        //*********************************************************************************
            if (dotNetNeeded = true) then
            begin

                if Exec(ExpandConstant(dotnetRedistPath), '', '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then
                    begin

                        // handle success if necessary; ResultCode contains the exit code
                        if not (ResultCode = 0) then
                            begin

                                Result := false;

                            end
                    end
                    else
                        begin

                            // handle failure if necessary; ResultCode contains the error code
                            Result := false;

                        end
            end;

    end;

end;

//*********************************************************************************
// Updates the memo box shown right before the install actuall starts.
//*********************************************************************************
function UpdateReadyMemo(Space, NewLine, MemoUserInfoInfo, MemoDirInfo, MemoTypeInfo, MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
var
  s: string;

begin

  if memoDependenciesNeeded <> '' then s := s + 'Dependencies that will be automatically downloaded And installed:' + NewLine + memoDependenciesNeeded + NewLine;
  s := s + MemoDirInfo + NewLine + NewLine;

  Result := s

end;
