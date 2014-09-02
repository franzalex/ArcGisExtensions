using System.ComponentModel;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    /// <summary>
    /// Executes an operation on a separate thread while displaying the progress in a ProgressBar.
    /// </summary>
    [DefaultEvent("DoWork")]
    public class ProgressWorker : System.Windows.Forms.ProgressBar
    {
        #region Exposed Events from BackgroundWorker Class

        /// <summary>Occurs when <see cref="RunWorkerAsync" /> is called.</summary>
        [Description("Event handler to be run on a different thread when the operation begins.")]
        public event DoWorkEventHandler DoWork;

        /// <summary>Occurs when <see cref="ReportProgress" /> is called.</summary>
        [Description("Raised when the worker thread indicates that some progress has been made.")]
        public event ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// Occurs when the background operation has completed, has been canceled or has raised an exception.
        /// </summary>
        [Description("Raised when the worker has been completed (either through success, failure or cancellation).")]
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;

        #endregion Exposed Events from BackgroundWorker Class

        private ProgressBarStyle userStyle;
        private BackgroundWorker worker;
        private bool wasCancelled;

        private delegate void SetProgressStyle();

        /// <summary>Initializes a new instance of the <see cref="ProgressWorker" /> class.</summary>
        public ProgressWorker()
            : base()
        {
            worker = new BackgroundWorker() {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        /// <summary>
        /// Gets a value indicating whether the application has requested cancellation of a
        /// background operation.
        /// </summary>
        /// <value>
        /// <c>true</c> if the application has requested cancellation of a background operation;
        /// otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool CancellationPending { get { return worker.CancellationPending; } }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ProgressWorker" /> is running an
        /// asynchronous operation.
        /// </summary>
        /// <value>
        /// <c>true</c> if the <see cref="ProgressWorker" /> is running an asynchronous operation;
        /// otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsBusy { get { return worker.IsBusy; } }

        /// <summary>Requests cancellation of a pending background operation.</summary>
        public void CancelAsync()
        {
            worker.CancelAsync();
            wasCancelled = true;
        }

        /// <summary>Raises the <see cref="ProgressChanged" /> event.</summary>
        /// <param name="percentProgress">
        /// The percentage, from 0 to 100, of the background operation that is complete.
        /// </param>
        public void ReportProgress(int percentProgress)
        {
            ReportProgress(percentProgress, null);
        }

        /// <summary>Raises the <see cref="ProgressChanged" /> event.</summary>
        /// <param name="percentProgress">
        /// The percentage, from 0 to 100, of the background operation that is complete.
        /// </param>
        /// <param name="userState">The state object passed to <see cref="RunWorkerAsync" /></param>
        public void ReportProgress(int percentProgress, object userState)
        {
            worker.ReportProgress(percentProgress, userState);
        }

        /// <summary>Starts asynchronous execution of a background operation.</summary>
        public void RunWorkerAsync()
        {
            this.RunWorkerAsync(null);
        }

        /// <summary>Starts asynchronous execution of a background operation.</summary>
        /// <param name="argument">
        /// A parameter for use by the background operation to be executed in the <see
        /// cref="ProgressWorker.DoWork" /> event handler.
        /// </param>
        public void RunWorkerAsync(object argument)
        {
            worker.RunWorkerAsync(argument);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control"
        /// /> and its child controls and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; false to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            worker.Dispose();
        }

        #region BackgroundWorker-related Event Raisers

        /// <summary>Raises the <see cref="E:DoWork" /> event.</summary>
        /// <param name="e">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
        protected virtual void OnDoWork(DoWorkEventArgs e)
        {
            var handler = DoWork;
            if (handler != null)
            {
                // save the user's progress bar style and then set marquee before running async work.
                userStyle = this.Style;
                this.Invoke(new SetProgressStyle(() => this.Style = ProgressBarStyle.Marquee));

                handler(this, e);
            }
        }

        /// <summary>Raises the <see cref="E:ProgressChanged" /> event.</summary>
        /// <param name="e">
        /// The <see cref="ProgressChangedEventArgs" /> instance containing the event data.
        /// </param>
        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            var handler = ProgressChanged;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>Raises the <see cref="E:RunWorkerCompleted" /> event.</summary>
        /// <param name="e">
        /// The <see cref="RunWorkerCompletedEventArgs" /> instance containing the event data.
        /// </param>
        protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
        {
            var handler = RunWorkerCompleted;
            if (handler != null)
                handler(this, e);
        }

        #endregion BackgroundWorker-related Event Raisers

        #region BackroundWorker Event Handlers

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            OnDoWork(e);
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // update the progress bar value
            this.Value = this.Minimum + (int)((this.Maximum - this.Minimum) * (e.ProgressPercentage / 100.0));
            this.Style = userStyle;

            OnProgressChanged(e);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // set the value to 100% if process was not canceled
            this.Style = userStyle;
            this.Value = wasCancelled ? this.Value : this.Maximum;

            OnRunWorkerCompleted(e);
        }

        #endregion BackroundWorker Event Handlers
    }
}