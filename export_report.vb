'#####=========================================================================
'## Title: Export Report and FTP send
'## Rev: 1.0
'##       
'## Purpose:
'##        1. This script takes a file from OBIEE and saves to the file system
'##        2. Creates a further pdf file with name based on current date
'##        3. Deliver the file to an ftp server using ftp.bat batch file
'##                    
'## Inputs (specified in Actions tab of OBIEE Delivers Agent):
'##        1. Parameter(0) - This actual file to be exported
'##
'#####=========================================================================


Dim sBasePath
sBasePath = "D:\temp\reports"


Dim objFSO
Set objFSO = CreateObject("Scripting.FileSystemObject")


'build string to get date in yyyy-mm-dd format
Dim sDate, sDateFull, sFtpPath
sDate = Now
sDateFull = DatePart("yyyy", sDate) & "-"
If Len(DatePart("m", sDate))=1 Then sDateFull = sDateFull & "0" End If
sDateFull = sDateFull & DatePart("m", sDate) & "-"
If Len(DatePart("d", sDate))=1 Then sDateFull = sDateFull & "0" End If
sDateFull = sDateFull & DatePart("d", sDate) & "_"
sDateFull = sDateFull & DatePart("h", sDate) & "-"
sDateFull = sDateFull & DatePart("n", sDate) & "-"
sDateFull = sDateFull & DatePart("s", sDate)


'Create the filename for the 
Dim sFileName, sExtension
sExtension = "bat"
sFileName = sBasePath & "\" & objFSO.GetFileName(Parameter(0)) & "_" & sDateFull & "." & sExtension

Dim objFile
'Change here if you want to send the content to a network location
objFSO.CopyFile Parameter(0), sFileName, True

'run the bat file to send the data
sFtpPath = sBasePath & "\" & "ftp.bat"

Dim objFSOFTP
Set objFSOFTP = CreateObject("WScript.Shell")
objFSOFTP.Run sFtpPath & " " & sFileName & " " & sBasePath


'destroy de objects
Set objFile = Nothing
Set objFSO = Nothing
Set objFSOFTP = Nothing