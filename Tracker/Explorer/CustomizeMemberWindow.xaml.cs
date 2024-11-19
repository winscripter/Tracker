using System.Windows;
using System.Windows.Controls;

namespace Tracker.Explorer
{
    /// <summary>
    /// Interaction logic for CustomizeMemberWindow.xaml
    /// </summary>
    public partial class CustomizeMemberWindow : Window
    {
        private enum SelectedIndexes
        {
            Color,
            Value
        }

        private ColorCustomizationView _colorCustomizationView;
        private ValueCustomizationView _valueCustomizationView;

        public CustomizeMemberWindow()
        {
            _colorCustomizationView = new ColorCustomizationView();
            _valueCustomizationView = new ValueCustomizationView();
            InitializeComponent();
        }

        public ColorCustomizationView ColorCustomizationView
        {
            get => _colorCustomizationView;
            set => _colorCustomizationView = value;
        }

        public ValueCustomizationView ValueCustomizationView
        {
            get => _valueCustomizationView;
            set => _valueCustomizationView = value;
        }

        public bool Returned { get; set; } = false;

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (contentPresenter is null)
            {
                return;
            }

            switch ((SelectedIndexes)tabControl.SelectedIndex)
            {
                case SelectedIndexes.Color:
                    {
                        ValueCustomizationView? valueCustomizationView = contentPresenter.Content as ValueCustomizationView;
                        if (valueCustomizationView is not null)
                        {
                            _valueCustomizationView = valueCustomizationView;
                        }
                        contentPresenter.Content = _colorCustomizationView;
                        break;
                    }

                case SelectedIndexes.Value:
                    {
                        ColorCustomizationView? colorCustomizationView = contentPresenter.Content as ColorCustomizationView;
                        if (colorCustomizationView is not null)
                        {
                            _colorCustomizationView = colorCustomizationView;
                        }
                        contentPresenter.Content = _valueCustomizationView;
                        break;
                    }
            }
        }

        // Confirm button
        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!ValidateSettings())
            {
                return;
            }

            Returned = true;
            Close();
        }

        private bool ValidateSettings()
        {
            if (!int.TryParse(_valueCustomizationView.completedBox.Text, null, out int completed))
            {
                MessageBox.Show(
                    "Under 'Value':\n\nInvalid field 'Completed': expected valid number",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return false;
            }

            if (completed > 100 || completed < 0)
            {
                MessageBox.Show(
                    "Under 'Value':\n\nInvalid field 'Completed': number must range between 0 and 100",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return false;
            }

            if (!ColorCustomizationView.IsValidHex(_colorCustomizationView.hexValue.Text))
            {
                MessageBox.Show(
                    "Under 'Color':\n\nInvalid field: expected valid hexadecimal format",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return false;
            }

            return true;
        }
    }
}
