using System;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;

namespace ToolFinder
{
    public class OpenToolFinderWindow_ArcMap : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public OpenToolFinderWindow_ArcMap()
        {
        }

        protected override void OnClick()
        {
            // save the currently selected tool
            var prevTool = ArcMap.Application.CurrentTool;

            //Get a reference to the Tool Finder Dockable Window and show it
            UID dockWinID = new UIDClass();
            dockWinID.Value = ThisAddIn.IDs.ToolFinderWindow;
            IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
            dockWindow.Show(true);
            
            // restore the prevously selected tool
            ArcMap.Application.CurrentTool = prevTool;
        }
        protected override void OnUpdate()
        {
            this.Enabled = ArcMap.Application != null;
        }
    }
}
