using System.Windows;
using System.Windows.Input;

namespace Tracker.Explorer
{
    /// <summary>
    /// Interaction logic for SetDocumentPropertiesDialog.xaml
    /// </summary>
    public partial class SetDocumentPropertiesDialog : Window
    {
        public DocumentPropertiesDialogResult? Result { get; set; }

        public SetDocumentPropertiesDialog()
        {
            Result = null;
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ValidateFields())
            {
                Result = new DocumentPropertiesDialogResult(description.Text, author.Text);
                Close();
            }
        }

        private bool ValidateFields()
        {
            string descriptionField = description.Text;
            string authorField = author.Text;

            if (string.IsNullOrEmpty(descriptionField) || string.IsNullOrWhiteSpace(descriptionField))
            {
                MessageBox.Show(
                    "Error in field 'Description':\n\nField cannot be empty.",
                    "Tracker",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return false;
            }

            if (string.IsNullOrEmpty(authorField) || string.IsNullOrWhiteSpace(authorField))
            {
                MessageBox.Show(
                    "Error in field 'Author':\n\nField cannot be empty.",
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
