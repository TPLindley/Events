using System.Windows;
using Events.ViewModel;

namespace Events
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        /// 
        private MainViewModel ViewModel => DataContext as MainViewModel;
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
            Closing += (s, e) => ViewModel.onUnloaded();
        }
    }
}