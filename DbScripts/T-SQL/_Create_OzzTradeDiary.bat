@echo .
@echo Creating Database...
@sqlcmd -S .\SqlExpress -d Master -E -Q "CREATE DATABASE [OzzTradeDiary]"
@echo .

@echo .
@echo .
@echo 1 - Executing _Schema.sql...
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i _Schema.sql

@echo .
@echo .
@echo 2 - Executing AppSetting.sql...
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i AppSetting.sql
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i AppSettings-SP.sql
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i AppSettings.Data.sql
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i AppSettings-AddtSP.sql

@echo .
@echo .
@echo 3 - Executing Exchange.sql...
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i Exchange.sql

@echo .
@echo .
@echo 4 - Executing TradingAccount.sql...
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i TradingAccount.sql
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i TradingAccounts-SP.sql

@echo .
@echo .
@echo 5 - Executing Currency.sql...
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i Currency.sql
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i Currencies-SP.sql

@echo .
@echo .
@echo 6 - Executing Symbol.sql...
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i Symbol.sql
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i Symbols-SP.sql

@echo .
@echo .
@echo 7 - Executing Trade.sql...
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i Trade.sql
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i Trades-SP.sql

@echo .
@echo .
@echo 8 - Executing EntryOrder.sql...
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i EntryOrder.sql
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i EntryOrders-SP.sql

@echo .
@echo .
@echo 9 - Executing TakeProfitOrder.sql...
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i TakeProfitOrder.sql
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i TakeProfitOrders-SP.sql

@echo .
@echo .
@echo 10 - Executing StopLossOrder.sql...
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i StopLossOrder.sql
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i StopLossOrders-SP.sql

@echo .
@echo .
@echo 11 - Executing TradePlan.sql...
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i TradePlan.sql
@sqlcmd -S .\SqlExpress -d OzzTradeDiary -E -i TradePlans-SP.sql

@echo .
@echo * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
@echo *
@echo * Create scripts for database OzzTradeDiary executed on Server .\SqlExpress
@echo *
@echo * Please check history for errors.
@echo *
@echo * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
@echo .
@pause
