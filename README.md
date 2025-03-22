# ProcKillerService

Simple app/service that monitors running processes in the system and kills the specified process.(For now supported only one process)

![Screenshot](https://github.com/axel92b/ProcKillerService/blob/master/screenshots/sc1.png)


## Compilation

`dotnet publish -r win10-x64 -c Release`

## Run

`ProcKillerService.exe run`

## Install as service

`ProcKillerService.exe install` and to run the service after installation `ProcKillerService.exe start`
After first run, open C:\ProgramData\Axel\ProcessKillerService\settings.json and set the process name that you want to kill.
