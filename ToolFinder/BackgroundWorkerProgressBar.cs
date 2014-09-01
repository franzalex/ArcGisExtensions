using System.ComponentModel;

namespace ToolFinder
{
    /// <summary>
    /// Executes an operation on a separate thread while displaying the progress in a ProgressBar.
    /// </summary>
    public class BackgroundWorkerProgressBar : System.Windows.Forms.ProgressBar
    {
        private BackgroundWorker worker;

        /// <summary>Occurs when the BackgroundWorkerProgressBar.RunWorkerAsync() is called.</summary>
        public event DoWorkEventHandler DoWork;
        public event ProgressChangedEventHandler ProgressChanged;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundWorkerProgressBar" /> class.
        /// </summary>
        public BackgroundWorkerProgressBar()
            : base()
        {
            worker = new BackgroundWorker() { WorkerReportsProgress = true };

            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new System.NotImplementedException();
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


    }
}