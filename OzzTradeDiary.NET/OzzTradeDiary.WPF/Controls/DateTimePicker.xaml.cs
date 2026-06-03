using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TD.WPF.Controls
{
    /// <summary>
    /// Interaction logic for DateTimePicker.xaml
    /// </summary>
    public partial class DateTimePicker : UserControl
    {
        public DateTimePicker()
        {
            InitializeComponent();
            RefreshSubControls();
        }
        private DateTime? _selectedDate;
        private int _hour;
        private int _minute;

        public static readonly DependencyProperty MinuteIntervalProperty = DependencyProperty.Register(nameof(MinuteInterval),
                                                                                                       typeof(int),
                                                                                                       typeof(DateTimePicker),
                                                                                                       new PropertyMetadata(1));

        public int MinuteInterval
        {
            get => (int)GetValue(MinuteIntervalProperty);
            set => SetValue(MinuteIntervalProperty, value);
        }

        public static readonly DependencyProperty DisallowFutureProperty =
            DependencyProperty.Register(nameof(DisallowFuture),
                                        typeof(bool),
                                        typeof(DateTimePicker),
                                        new PropertyMetadata(false, OnDisallowFutureChanged));

        public bool DisallowFuture
        {
            get => (bool)GetValue(DisallowFutureProperty);
            set => SetValue(DisallowFutureProperty, value);
        }

        private static void OnDisallowFutureChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DateTimePicker picker)
                picker.RefreshSubControls();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == IsEnabledProperty)
                RefreshSubControls();
        }

        public static readonly DependencyProperty SelectedDateTimeProperty = DependencyProperty.Register(nameof(SelectedDateTime),
                                                                                                         typeof(DateTime?),
                                                                                                         typeof(DateTimePicker),
                                                                                                         new FrameworkPropertyMetadata(
                                                                                                         null,
                                                                                                         FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                                                                         OnSelectedDateTimeChanged));

        public DateTime? SelectedDateTime
        {
            get => (DateTime?)GetValue(SelectedDateTimeProperty);
            set => SetValue(SelectedDateTimeProperty, value);
        }

        private static void OnSelectedDateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DateTimePicker picker)
                picker.UpdateInternalFields((DateTime?)e.NewValue);
        }

        private void UpdateInternalFields(DateTime? value)
        {
            _selectedDate = value?.Date;
            _hour = value?.Hour ?? 0;
            _minute = value?.Minute ?? 0;
            RefreshSubControls();
        }

        private void RefreshSubControls()
        {
            if (datePicker == null)
                return;

            datePicker.DisplayDateEnd = DisallowFuture ? DateTime.Today : null;
            datePicker.SelectedDate = _selectedDate;
            hourTextBox.Text = _hour.ToString("D2");
            minuteTextBox.Text = _minute.ToString("D2");
        }

        private void RebuildSelectedDateTime()
        {
            if (_selectedDate is DateTime date)
            {
                var result = date.AddHours(_hour).AddMinutes(_minute);
                if (DisallowFuture && result > DateTime.Now)
                    result = DateTime.Now;
                SelectedDateTime = result;
            }
            else
            {
                SelectedDateTime = null;
            }
        }

        private void HourMinuteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button)
                return;

            string? tag = button.Tag as string;
            if (string.IsNullOrEmpty(tag))
                return;

            if (tag == "MinuteUp")
            {
                _minute = (_minute + MinuteInterval) % 60;
                _minute = (int)(Math.Round((double)_minute / MinuteInterval) * MinuteInterval) % 60;
            }
            else if (tag == "MinuteDown")
            {
                _minute = (_minute - MinuteInterval + 60) % 60;
                _minute = (int)(Math.Round((double)_minute / MinuteInterval) * MinuteInterval) % 60;
            }
            else if (tag == "HourUp")
            {
                _hour = (_hour + 1) % 24;
            }
            else if (tag == "HourDown")
            {
                _hour = (_hour - 1 + 24) % 24;
            }
            else if (tag == "Clear")
            {
                _hour = 0;
                _minute = 0;
                _selectedDate = null;
            }

            RefreshSubControls();
            RebuildSelectedDateTime();
        }

        private void IntegerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDate = datePicker.SelectedDate;
            RebuildSelectedDateTime();
        }

        private void HourTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(hourTextBox.Text, out int value))
                _hour = Math.Clamp(value, 0, 23);
            RefreshSubControls();
            RebuildSelectedDateTime();
        }

        private void MinuteTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(minuteTextBox.Text, out int value))
                _minute = Math.Clamp(value, 0, 59);
            RefreshSubControls();
            RebuildSelectedDateTime();
        }
    }
}
