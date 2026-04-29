using System.Windows;
using TD.AppInfra.Services;
using TD.Models;
using TD.WPF.Views.Maintenance;

namespace TD.WPF.Services;

internal class WindowDialogService : IWindowDialogService
{
    /// <inheritdoc />
    public (bool IsConfirmed, bool IsDirty) ShowCurrencyEditDialog(Window owner, Currency currency)
    {
        var dialog = new CurrencyEdit(currency)
        {
            Owner = owner
        };

        var result = dialog.ShowDialog() == true;
        return (result, dialog.IsDirty);
    }

    /// <inheritdoc />
    public (bool IsConfirmed, bool IsDirty) ShowExchangeEditDialog(Window owner, Exchange exchange)
    {
        var dialog = new ExchangeEdit(exchange)
        {
            Owner = owner
        };

        var result = dialog.ShowDialog() == true;
        return (result, dialog.IsDirty);
    }

    /// <inheritdoc />
    public (bool IsConfirmed, TradingAccount? TradingAccount) ShowTradingAccountCreateDialog(Window owner, IExchangeLookupService exchangeLookupService)
    {
        var dialog = new TradingAccountCreate(exchangeLookupService)
        {
            Owner = owner
        };

        var result = dialog.ShowDialog() == true;
        return (result, result ? dialog.TradingAccount : null);
    }

    /// <inheritdoc />
    public (bool IsConfirmed, bool IsDirty) ShowTradingAccountEditDialog(Window owner, TradingAccount tradingAccount)
    {
        var dialog = new TradingAccountEdit(tradingAccount)
        {
            Owner = owner
        };

        var result = dialog.ShowDialog() == true;
        return (result, dialog.IsDirty);
    }

    /// <inheritdoc />
    public (bool IsConfirmed, bool IsDirty) ShowSymbolEditDialog(Window owner, Symbol symbol)
    {
        var dialog = new SymbolEdit(symbol)
        {
            Owner = owner
        };

        var result = dialog.ShowDialog() == true;
        return (result, dialog.IsDirty);
    }
}
