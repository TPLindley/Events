using EventBase.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;

namespace Events.ViewModel
{
    public class BaseViewModel : ViewModelBase
    {
        private bool HasFocus { get; set; }
        private ILogger _logger;
        public BaseViewModel(ILogger logger)
        {
            _logger = logger;
        }
        virtual public Task onLoaded()
        {
            HasFocus = true;
            return Task.FromResult(0);
        }
        private RelayCommand _onLoaded;
        public RelayCommand OnLoaded
        {
            get
            {
                return _onLoaded ?? (_onLoaded = new RelayCommand(() =>
                {
                    onLoaded();
                }));
            }
        }
        virtual public void onUnloaded()
        {
            HasFocus = false;
        }
        private RelayCommand _onUnloaded;
        public RelayCommand OnUnloaded
        {
            get
            {
                return _onUnloaded ?? (_onUnloaded = new RelayCommand(() =>
                {
                    onUnloaded();
                }));
            }
        }
    }
}
