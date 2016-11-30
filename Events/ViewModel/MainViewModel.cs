using GalaSoft.MvvmLight;
using Events.Model;
using EventBase.Interfaces;
using System.Threading.Tasks;
using System.Configuration;
using GalaSoft.MvvmLight.Command;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows;
using System.Collections.ObjectModel;

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
                var libs = listHandlers.Split(';');
                foreach (var lib in libs)
                {
                    EventHandlers.Add(new MyEventHandler() { EventAssembly = Assembly.Load(lib), Name = lib.Substring(0, lib.IndexOf("Event")) });
                }
                RaisePropertyChanged("EventHandlers");
            }
        }
        private void ProcessEvent()
        {
            IsProcessing = true;
            Results.Clear();
            for (int seed = 1; seed < 101; seed++)
                Results.Add(new Result() { Seed = seed, Value = SelectedEventHandler?.ProcessEvent(seed) });
            RaisePropertyChanged("Results");
            IsProcessing = false;
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
            : base(logger)
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
            LoadEventHandlers(_settings.GetEventLibraries());
            SelectedEventHandler = EventHandlers.First();
            return result;
        }
        public override void onUnloaded()
        {
            base.onUnloaded();
        }
        public override void Cleanup()
        {
            base.Cleanup();
        }
        #endregion
        #region Properties
        private ObservableCollection<MyEventHandler> _eventHandlers = new ObservableCollection<MyEventHandler>();
        public ObservableCollection<MyEventHandler> EventHandlers
        {
            get { return _eventHandlers; }
            set { _eventHandlers = value; RaisePropertyChanged(); }
        }
        private MyEventHandler _selectedEventHandler;
        public MyEventHandler SelectedEventHandler
        {
            get { return _selectedEventHandler; }
            set
            {
                _selectedEventHandler = value;
                RaisePropertyChanged();
                Results = new ObservableCollection<Result>();
            }
        }
        private RelayCommand _raiseEvent;
        public RelayCommand RaiseEvent
        {
            get
            {
                return _raiseEvent ?? (_raiseEvent = new RelayCommand(ProcessEvent));
            }
        }
        private string _resultString;
        public string ResultString
        {
            get { return _resultString; }
            set { _resultString = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<Result> _results = new ObservableCollection<Result>();
        public ObservableCollection<Result> Results
        {
            get { return _results; }
            set { _results = value; RaisePropertyChanged(); }
        }
        private bool _isProcessing = false;
        public bool IsProcessing
        {
            get { return _isProcessing; }
            set
            {
                _isProcessing = value;
                RaisePropertyChanged();
                RaisePropertyChanged("OkToProcess");
            }
        }
        public bool OkToProcess
        {
            get { return !IsProcessing; }
        }
        #endregion
    }
}