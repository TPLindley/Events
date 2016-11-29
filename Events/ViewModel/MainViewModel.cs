using GalaSoft.MvvmLight;
using Events.Model;
using EventBase.Interfaces;
using System.Threading.Tasks;
using System.Configuration;
using GalaSoft.MvvmLight.Command;
using System.Reflection;

namespace Events.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        #region Private
        private readonly IDataService _dataService;
        private string _welcomeTitle = string.Empty;
        private ISettings _settings;
        private void LoadEventHandlers(string listHandlers)
        {
            if (!string.IsNullOrEmpty(listHandlers))
            {
                var libs = listHandlers.Split(',');
                foreach (var lib in libs)
                {
                    var library = Assembly.Load(lib);
                    foreach (var ty in library.ExportedTypes)
                    {
                        var temp = ty.Name;
                    }
                }
            }
        }
        #endregion
        #region Public
        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";
        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }
            set
            {
                Set(ref _welcomeTitle, value);
            }
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ILogger logger, ISettings settings, IDataService dataService)
            :base(logger)
        {
            _settings = settings;
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    WelcomeTitle = item.Title;
                });
        }
        public override Task onLoaded()
        {
            var result = base.onLoaded();
            LoadEventHandlers(ConfigurationManager.AppSettings["EventLibraries"]);
            LoadEventHandlers(_settings.GetEventLibraries());
            return result;
        }
        public override void onUnloaded()
        {
            base.onUnloaded();
        }
        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
        #endregion
    }
}