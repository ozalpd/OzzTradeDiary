using System.Windows;
using TD.AppInfra.Services;
using TD.Models;

namespace TD.WPF.Services;

internal interface IWindowDialogService
{
    /// <summary>
    /// Displays the Currency creation dialog and returns the result indicating whether the user confirmed the dialog
    /// and the created Currency, if any.
    /// </summary>
    /// <param name="owner">The window that will own the dialog. This determines the dialog's parent window for modality and positioning.</param>
    /// <returns>A tuple containing a Boolean value that is <see langword="true"/> if the user confirmed the dialog; otherwise, <see
    /// langword="false"/>. The second item is the created Currency if confirmed; otherwise, <see langword="null"/>.</returns>
    (bool IsConfirmed, Currency? Currency) ShowCurrencyCreateDialog(Window owner);

    /// <summary>
    /// Displays a modal dialog for editing the specified currency and returns the result indicating whether the dialog
    /// was confirmed and if any changes were made.
    /// </summary>
    /// <param name="owner">The window that will own the dialog. This parameter determines the parent window for modal behavior and dialog
    /// positioning.</param>
    /// <param name="currency">The currency entity to edit. The dialog will display and allow editing of this currency's properties.</param>
    /// <returns>A tuple containing two values: IsConfirmed is <see langword="true"/> if the user confirmed the dialog;
    /// otherwise, <see langword="false"/>. IsDirty is <see langword="true"/> if any changes were made to the currency
    /// during editing; otherwise, <see langword="false"/>.</returns>
    (bool IsConfirmed, bool IsDirty) ShowCurrencyEditDialog(Window owner, Currency currency);

    /// <summary>
    /// Displays the Exchange creation dialog and returns the result indicating whether the user confirmed the dialog
    /// and the created Exchange, if any.
    /// </summary>
    /// <param name="owner">The window that will own the dialog. This determines the dialog's parent window for modality and positioning.</param>
    /// <returns>A tuple containing a Boolean value that is <see langword="true"/> if the user confirmed the dialog; otherwise, <see
    /// langword="false"/>. The second item is the created Exchange if confirmed; otherwise, <see langword="null"/>.</returns>
    (bool IsConfirmed, Exchange? Exchange) ShowExchangeCreateDialog(Window owner);

    /// <summary>
    /// Displays a modal dialog for editing the specified exchange and returns the result indicating whether the dialog
    /// was confirmed and if any changes were made.
    /// </summary>
    /// <param name="owner">The window that will own the dialog. This parameter determines the parent window for modal behavior and dialog
    /// positioning.</param>
    /// <param name="exchange">The exchange entity to edit. The dialog will display and allow editing of this exchange's properties.</param>
    /// <returns>A tuple containing two values: IsConfirmed is <see langword="true"/> if the user confirmed the dialog;
    /// otherwise, <see langword="false"/>. IsDirty is <see langword="true"/> if any changes were made to the exchange
    /// during editing; otherwise, <see langword="false"/>.</returns>
    (bool IsConfirmed, bool IsDirty) ShowExchangeEditDialog(Window owner, Exchange exchange);

    /// <summary>
    /// Displays the Symbol creation dialog and returns the result indicating whether the user confirmed the dialog
    /// and the created Symbol, if any.
    /// </summary>
    /// <param name="owner">The window that will own the dialog. This determines the dialog's parent window for modality and positioning.</param>
    /// <param name="exchangeLookupService">The exchange lookup service used to provide exchange selection options within the dialog.</param>
    /// <param name="currencyLookupService">The currency lookup service used to provide currency selection options within the dialog.</param>
    /// <returns>A tuple containing a Boolean value that is <see langword="true"/> if the user confirmed the dialog; otherwise, <see
    /// langword="false"/>. The second item is the created Symbol if confirmed; otherwise, <see langword="null"/>.</returns>
    (bool IsConfirmed, Symbol? Symbol) ShowSymbolCreateDialog(Window owner, IExchangeLookupService exchangeLookupService, ICurrencyLookupService currencyLookupService, Exchange? preselectedExchange);

    /// <summary>
    /// Displays a modal dialog for editing the specified symbol and returns the result indicating whether the dialog
    /// was confirmed and if any changes were made.
    /// </summary>
    /// <param name="owner">The window that will own the symbol edit dialog. This determines the dialog's parent window and modality.</param>
    /// <param name="symbol">The symbol to edit. The dialog will display and allow editing of this symbol's properties.</param>
    /// <returns>A tuple containing two values: IsConfirmed is <see langword="true"/> if the user confirmed the dialog;
    /// otherwise, <see langword="false"/>. IsDirty is <see langword="true"/> if any changes were made to the symbol
    /// during editing; otherwise, <see langword="false"/>.</returns>
    (bool IsConfirmed, bool IsDirty) ShowSymbolEditDialog(Window owner, Symbol symbol);

    /// <summary>
    /// Displays the Trading Account creation dialog and returns the result indicating whether the user confirmed the dialog
    /// and the created TradingAccount, if any.
    /// </summary>
    /// <param name="owner">The window that will own the dialog. This determines the dialog's parent window for modality and positioning.</param>
    /// <param name="exchangeLookupService">The exchange lookup service used to provide exchange selection options within the dialog.</param>
    /// <returns>A tuple containing a Boolean value that is <see langword="true"/> if the user confirmed the dialog; otherwise, <see
    /// langword="false"/>. The second item is the created TradingAccount if confirmed; otherwise, <see langword="null"/>.</returns>
    (bool IsConfirmed, TradingAccount? TradingAccount) ShowTradingAccountCreateDialog(Window owner, IExchangeLookupService exchangeLookupService, Exchange? preselectedExchange);

    /// <summary>
    /// Displays the Trading Account edit dialog for the specified account and returns the result indicating whether the
    /// dialog was confirmed and if any changes were made.
    /// </summary>
    /// <remarks>Use this method to present the Trading Account edit dialog modally. The dialog will not
    /// update the TradingAccount instance unless the user confirms the changes.</remarks>
    /// <param name="owner">The window that will own the dialog. This determines window modality and placement.</param>
    /// <param name="tradingAccount">The TradingAccount instance to edit. The dialog will display and allow editing of this account's properties.</param>
    /// <returns>A tuple containing two values: IsConfirmed is true if the user confirmed the dialog; otherwise, false. IsDirty
    /// is true if any changes were made to the TradingAccount during the dialog session; otherwise, false.</returns>
    (bool IsConfirmed, bool IsDirty) ShowTradingAccountEditDialog(Window owner, TradingAccount tradingAccount);
}
