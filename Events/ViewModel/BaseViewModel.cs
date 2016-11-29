using EventBase.Interfaces;
using GalaSoft.MvvmLight;
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

        virtual public void onUnloaded()
        {
            HasFocus = false;
        }
    }
}
