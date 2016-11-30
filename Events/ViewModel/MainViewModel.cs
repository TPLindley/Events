using System;
using Events.Model;
using EventBase.Interfaces;
using GalaSoft.MvvmLight.Command;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
// ReSharper disable ExplicitCallerInfoArgument
// ReSharper disable RedundantOverridenMember

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

        private string _welcomeTitle = string.Empty;
        private readonly ISettings _settings;
        private void LoadEventHandlers(List<string> listHandlers)
        {
            if (listHandlers!=null && listHandlers.Any())
            {
                foreach (var library in listHandlers)
                {
                    EventHandlers.Add(new MyEventHandler() { EventAssembly = Assembly.Load(library), Name = library.Substring(0, library.IndexOf("Event", StringComparison.Ordinal)) });
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
            dataService.GetData(
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
        public override void Loaded()
        {
            base.Loaded();
            LoadEventHandlers(_settings.GetEventLibraries());
            SelectedEventHandler = EventHandlers.First();
        }
        public override void Unloaded()
        {
            base.Unloaded();
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
        public RelayCommand RaiseEvent => _raiseEvent ?? (_raiseEvent = new RelayCommand(ProcessEvent));
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
        private bool _isProcessing;
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