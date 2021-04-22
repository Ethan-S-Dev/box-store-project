using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BoxStoreView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox box = (TextBox)sender;
            Regex regex = new Regex(@"[^0-9.]"); // ^[0-9]+(\.[0-9]{1,2})?$
            if (!regex.IsMatch(e.Text))
            {
                if (e.Text.Contains("."))
                {
                    if (box.Text.Contains("."))
                    {
                        e.Handled = true;
                    }
                }
            }
            else
                e.Handled = true;
        }
        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            TextBox box = (TextBox)sender;
            Regex regex = new Regex(@"[^0-9]");
            if (regex.IsMatch(e.Text)) e.Handled = true;
        }
    }
}
