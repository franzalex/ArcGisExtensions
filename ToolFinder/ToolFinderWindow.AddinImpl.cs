using System;

namespace ToolFinder
{
    partial class ToolFinderWindow
    {
        #region Implementation class of the dockable window addin

        /// <summary>
        /// Implementation class of the dockable window add-in. It is responsible for 
        /// creating and disposing the user interface class of the dockable window.
        /// </summary>
        public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
        {
            private ToolFinderWindow m_windowUI;

            public AddinImpl()            {            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new ToolFinderWindow(this.Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);

                base.Dispose(disposing);
            }

        }
        #endregion
    }
}