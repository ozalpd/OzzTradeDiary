---
applyTo: "**/*.xaml"
description: "Use when: editing XAML views, creating new WPF pages, adding controls, styling UI elements"
---

# WPF XAML Conventions

## Resource Dictionaries

- **Icons**: Use `BootstrapIcons.xaml` geometry resources — reference via `{StaticResource icon-name}` (Bootstrap Icons v1.13.1 naming)
- **Styles**: Use `Styles.xaml` for shared styles — `DisabledWhenNullTextBoxStyle`, `ValidationTextBoxStyle`, `ValidationComboBoxStyle`, `IconButtonStyle-22x18`, `IconButtonStyle-28x24`
- Merge resource dictionaries in `App.xaml`, not in individual views

## Patterns

- Follow MVVM — **no code-behind logic** except `InitializeComponent()` and window lifecycle
- Bind to ViewModels using `{Binding}` markup — never set data directly in XAML code-behind
- Use `DataTrigger` for null-based disable patterns (see `DisabledWhenNullTextBoxStyle`)
- Validation errors display below controls using `Validation.ErrorTemplate` with `AdornedElementPlaceholder`
- When a `TextBox` uses `ReadOnlyTextBoxStyle`, set `Text` binding `Mode=OneWay`

## Naming

- View files: `{EntityName}View.xaml` or `{Feature}Window.xaml`
- Style keys: `{Description}Style` or `{Description}Style-{size}` (e.g., `IconButtonStyle-28x24`)
- DataContext: set to corresponding ViewModel class from `TD.WPF.ViewModels`

## Layout

- Use `Grid` with explicit row/column definitions for structured layouts
- Use `StackPanel` only for simple linear arrangements
- Icon buttons use `Path` with `Data="{StaticResource icon-key}"` inside a `Viewbox`

## Maintenance Toolbar Buttons

- For maintenance tab action bars (Add, Save, Refresh), use a horizontal `StackPanel` docked to top with `Margin="0,0,0,8"`
- Use `Button` style `IconButtonStyle-28x24`
- Set localized `ToolTip` values from `TD.i18n` resource classes (for example `LocalizedStrings.*` and `ActionStrings.*`)
- Render icons as:
  - `Viewbox Width="14" Height="14"`
  - child `Path Fill="#0044D7" Data="{StaticResource <icon-key>}"`
- Use icon keys consistent with `MaintenanceWindow.xaml` actions:
  - Add: `plus-circle`
  - Save: `floppy-fill`
  - Refresh: `arrow-clockwise`
- Keep action button click handlers named by feature + action (for example `AddCurrency_Click`, `SaveSymbols_Click`, `RefreshExchanges_Click`)

## Maintenance Delete Button Standard

- Destructive actions (for example Delete in maintenance toolbars) must use a command binding (`Command="{Binding <DeleteCommand>}"`) instead of click handlers.
- Command `CanExecute` must control enabled/disabled state based on selection and domain safety checks.
- Destructive command execution must ask for user confirmation with a `MessageBox` before performing deletion (for example text: `Are you sure to delete?`, with `Yes/No` options).
- Use `IconButtonStyle-28x24` with red destructive background (`Background="#C80000"`).
- Use a localized delete tooltip from `LocalizedStrings` (for example `LocalizedStrings.DeleteExchange`).
- Render the delete icon with `trash3` inside `Viewbox Width="14" Height="14"`.
- Use hover inversion for icon color via `Path.Style` trigger:
  - normal: `Fill="#F8F8F8"`
  - hover: `Fill="#C80000"`.
- Keep this visual/behavior pattern consistent across all delete buttons in the WPF app.
