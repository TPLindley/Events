using EventBase.Interfaces;
using EventBase.Models;
using System;
using System.Threading.Tasks;

namespace EventBase.Logger
{
    public class EtwLogger : ILog, IDisposable
    {
        public EtwLogger()
        {
            Resume();
        }
        public Task Log(LogEntry logEntry)
        {
            return Task.FromResult(0);
        }
        public async Task Suspend()
        {
            Dispose(true);
        }

        public Task Resume()
        {
            return Task.FromResult(0);
        }
        /// <summary>
        /// Helper function for other methods to call to check Dispose() state.
        /// </summary>
        private void CheckDisposed()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException("Events.Logging");
            }
        }
        private string GetTimeStamp()
        {
            DateTime now = DateTime.Now;
            return string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                 "{0:D2}{1:D2}{2:D2}-{3:D2}{4:D2}{5:D2}",
                                 now.Year - 2000,
                                 now.Month,
                                 now.Day,
                                 now.Hour,
                                 now.Minute,
                                 now.Second);
        }
        #region IDisposable Support
        private bool isDisposed = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    if (isDisposed == false)
                    {
                        isDisposed = true;
                    }
                    GC.SuppressFinalize(this);
                }
                isDisposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EtwLogger() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
