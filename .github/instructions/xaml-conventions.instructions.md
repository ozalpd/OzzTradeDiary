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
