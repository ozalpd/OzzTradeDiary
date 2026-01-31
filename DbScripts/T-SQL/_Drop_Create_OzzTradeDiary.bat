@echo .
@echo Deleting Database...
@sqlcmd -S ".\SqlExpress" -d Master -E -Q "IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'OzzTradeDiary') DROP DATABASE [OzzTradeDiary]"

@echo .
@call _Create_OzzTradeDiary.bat
