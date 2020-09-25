# ProcKillerService

Simple app/service that monitors running processes in the system and kills the specified process.(For now supported only one process)

![Screenshot](https://github.com/axel92b/ProcKillerService/blob/master/screenshots/sc1.png)


## Compilation

`dotnet publish -r win10-x64 -c Release`

## Run

`ProcKillerService.exe run -name:"Specify process name without .exe"`

## Install as service

`ProcKillerService.exe install -name:"Specify process name without .exe"` and to run the service after installation `ProcKillerService.exe start`

 ### Example

`ProcKillerService.exe install -name:"CompatTelRunner"`
