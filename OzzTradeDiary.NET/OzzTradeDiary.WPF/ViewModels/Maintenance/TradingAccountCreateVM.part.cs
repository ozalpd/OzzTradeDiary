namespace TD.WPF.ViewModels.Maintenance
{
    public partial class TradingAccountCreateVM
    {
        partial void OnInitialized()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ExchangeId) && ExchangeId > 0 && !titleSetManually)
            {
                AutoSetTitle();
            }
            else if (e.PropertyName == nameof(Title) && !settingTitle)
            {
                if (!string.IsNullOrWhiteSpace(Title))
                {
                    titleSetManually = true;
                }
                else
                {
                    titleSetManually = false;
                    AutoSetTitle();
                }
            }
        }

        private void AutoSetTitle()
        {
            var exchange = Exchanges.FirstOrDefault(x => x.Id == ExchangeId);
            if (exchange != null)
            {
                settingTitle = true;
                Title = $"{exchange.ExchangeCode} Account";
                settingTitle = false;
            }
        }

        bool settingTitle = false;
        bool titleSetManually = false;
    }
}
