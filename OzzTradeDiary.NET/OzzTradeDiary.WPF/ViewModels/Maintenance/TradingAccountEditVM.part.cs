namespace TD.WPF.ViewModels.Maintenance
{
    public partial class TradingAccountEditVM
    {
        public string ExchangeCode => _tradingAccount.Exchange?.ExchangeCode ?? string.Empty;
    }
}
