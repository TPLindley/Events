using EventBase.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
// ReSharper disable EmptyGeneralCatchClause
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable ExplicitCallerInfoArgument

namespace Events.ViewModel
{
    /// <summary>
    /// Abstract base class for all view models in this solution
    /// </summary>
    public abstract class BaseViewModel : ViewModelBase
    {
        #region Private
        private bool HasFocus { get; set; }
        private readonly ILogger _logger;
        #endregion
        #region Public
        public virtual void Loaded()
        {
            HasFocus = true;
        }
        public virtual void Unloaded()
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
                _logger.LogException(module, ex);
            if (_showErrors)
                await ShowException(ex, module, sourceLineNumber);
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
                _logger.LogError(message, module);
            }
            catch { }
            if (_showErrors)
                await dialog.ShowMessage(message, module);
        }
        public void LogInfo(
            string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var module = $"{GetFileName(sourceFilePath)}:{memberName}@{sourceLineNumber}";
            _logger.LogInfo(message, module);
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
            var module = sourceLineNumber < 0 ? sourceFilePath : $"Exception: {GetFileName(sourceFilePath)}:{memberName}@{sourceLineNumber}";
            string message = ex.Message;
            if (ex.InnerException != null)
                message = ex.InnerException.Message;
            var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
            await dialog.ShowMessage(message, module);
        }
        public override void Cleanup()
        {
            Unloaded();
            _logger.Stop();
            base.Cleanup();
        }
        #endregion
        #region Properties
        private RelayCommand _onLoaded;
        public RelayCommand OnLoaded => _onLoaded ?? (_onLoaded = new RelayCommand(Loaded));
        private RelayCommand _onUnloaded;
        public RelayCommand OnUnloaded => _onUnloaded ?? (_onUnloaded = new RelayCommand(Unloaded));
        private bool _showErrors = true;
        public bool ShowErrors { get { return _showErrors; } set { _showErrors = value; } }
        #endregion
        #region Protected
        protected BaseViewModel(ILogger logger)
        {
            _logger = logger;
        }
        protected string GetFileName(string filePath)
        {
            return filePath.Substring(filePath.LastIndexOf('\\') + 1);
        }
        #endregion
    }
}
