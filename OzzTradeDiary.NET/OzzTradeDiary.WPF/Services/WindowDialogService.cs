using System.Windows;
using TD.AppInfra.Services;
using TD.Models;
using TD.WPF.Views.Maintenance;

namespace TD.WPF.Services;

internal class WindowDialogService : IWindowDialogService
{
    /// <inheritdoc />
    public (bool IsConfirmed, Currency? Currency) ShowCurrencyCreateDialog(Window owner)
    {
        var dialog = new CurrencyCreate()
        {
            Owner = owner
        };

        var result = dialog.ShowDialog() == true;
        return (result, result ? dialog.Currency : null);
    }

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
    public (bool IsConfirmed, Exchange? Exchange) ShowExchangeCreateDialog(Window owner)
    {
        var dialog = new ExchangeCreate()
        {
            Owner = owner
        };

        var result = dialog.ShowDialog() == true;
        return (result, result ? dialog.Exchange : null);
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
    public (bool IsConfirmed, Symbol? Symbol) ShowSymbolCreateDialog(Window owner, IExchangeLookupService exchangeLookupService, ICurrencyLookupService currencyLookupService, Exchange? preselectedExchange)
    {
        var dialog = new SymbolCreate(exchangeLookupService, currencyLookupService, preselectedExchange)
        {
            Owner = owner
        };

        var result = dialog.ShowDialog() == true;
        return (result, result ? dialog.Symbol : null);
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

    /// <inheritdoc />
    public (bool IsConfirmed, TradingAccount? TradingAccount) ShowTradingAccountCreateDialog(Window owner, IExchangeLookupService exchangeLookupService, Exchange? preselectedExchange)
    {
        var dialog = new TradingAccountCreate(exchangeLookupService, preselectedExchange)
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
}
