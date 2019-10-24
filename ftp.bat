@echo off

SET base=%2

REM Generate the script. Will overwrite any existing temp.txt
echo open {Hostname}> %base%\temp.txt
echo user {username} {password}>> %base%\temp.txt
echo cd {target_directory_on_FTP_server}>> %base%\temp.txt
echo binary>> %base%\temp.txt
echo mput %1>> %base%\temp.txt
echo bye>> %base%\temp.txt

REM Launch FTP and pass it the script
rem ftp -n -i -s:%base%\temp.txt

REM Clean up.
rem del %base%\temp.txt