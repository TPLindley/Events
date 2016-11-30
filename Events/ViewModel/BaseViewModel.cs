using EventBase.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Events.ViewModel
{
    public class BaseViewModel : ViewModelBase
    {
        #region Private
        private bool HasFocus { get; set; }
        private ILogger _logger;
        private bool _showErrors = true;
        #endregion
        #region Public
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
        public async Task LogException(
            Exception ex,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var module = $"{GetFileName(sourceFilePath)}:{memberName}@{sourceLineNumber}";
            if (_logger != null)
                await _logger.LogException(module, ex);
            if (_showErrors)
                await ShowException(ex, module, -1);
        }
        public async Task LogError(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var module = $"Error: {GetFileName(sourceFilePath)}:{memberName}@{sourceLineNumber}";
            var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
            try
            {
                await _logger.LogError(message, module);
            }
            catch { }
            if (_showErrors)
                await dialog.ShowMessage(message, module);
        }
        public async Task LogInfo(
            string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var module = $"{GetFileName(sourceFilePath)}:{memberName}@{sourceLineNumber}";
            await _logger.LogInfo(message, module);
        }
        public async Task ShowError(string message)
        {
            var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
            await dialog.ShowMessage(message, "Events Error");
        }
        public async Task ShowException(
            Exception ex,
            [CallerFilePath] string sourceFilePath = null,
            [CallerLineNumber] int sourceLineNumber = 0,
            [CallerMemberName] string memberName = null)
        {
            string module;
            if (sourceLineNumber < 0)
                module = sourceFilePath;
            else
                module = $"Exception: {GetFileName(sourceFilePath)}:{memberName}@{sourceLineNumber}";
            string message = ex.Message;
            if (ex.InnerException != null)
                message = ex.InnerException.Message;
            var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
            await dialog.ShowMessage(message, module);
        }
        #endregion
        #region Properties
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
        #endregion
        #region Protected
        protected string GetFileName(string filePath)
        {
            return filePath.Substring(filePath.LastIndexOf('\\') + 1);
        }
        #endregion
    }
}
