using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using ESRI.ArcGIS.Geoprocessing;
using Path = System.IO.Path;
using IGxApplication = ESRI.ArcGIS.CatalogUI.IGxApplication;
using IDockableWindowManager = ESRI.ArcGIS.Framework.IDockableWindowManager;
using IWorkspaceFactory = ESRI.ArcGIS.Geodatabase.IWorkspaceFactory;
using GPToolCommandHelperClass = ESRI.ArcGIS.GeoprocessingUI.GPToolCommandHelperClass;

namespace ToolFinder
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class ToolFinderWindow : UserControl
    {

        private string _toolboxDir = "";
        private List<IGPTool> gpTools;
        private IGPToolCommandHelper2 toolInvoker;

        public ToolFinderWindow(object hook)
        {
            InitializeComponent();
            this.Hook = hook;

            //// get reference to catalog
            //var catalog = (ArcMap.Application as IGxApplication).Catalog;
            toolInvoker = new GPToolCommandHelperClass() as IGPToolCommandHelper2;
            gpTools = new List<IGPTool>();

            BuildGeoprocessingToolList();
        }

        /// <summary>Host object of the dockable window</summary>
        private object Hook { get; set; }

        /// <summary>Gets the system toolbox directory.</summary>
        private string ToolboxDir
        {
            get
            {
                if (_toolboxDir == "")
                {
                    // exe is at ArcGIS\Desktop10.x\bin
                    // toolbox dir is ArcGIS\Desktop10.x\ArcToolbox\Toolboxes
                    // exit to Desktop10.x dir and then access toolbox dir
                    var exeDir = System.Windows.Forms.Application.StartupPath;
                    _toolboxDir = Path.Combine(Path.GetDirectoryName(exeDir),
                                               "ArcToolbox\\Toolboxes");
                }
                return _toolboxDir;
            }
        }

        /// <summary>Creates the tool database.
        /// </summary>
        private void BuildGeoprocessingToolList()
        {
            if (!System.IO.Directory.Exists(ToolboxDir))
            {
                txtSearchTerm.PromptText = "System toolbox could not be located.";
                return;
            }

            Type factoryType = Type.GetTypeFromProgID("esriGeoprocessing.ToolboxWorkspaceFactory");
            IWorkspaceFactory factory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            IToolboxWorkspace wkspc = (IToolboxWorkspace)factory.OpenFromFile(ToolboxDir, 0);

            foreach (var tbx in wkspc.Toolboxes.GetEnumerator())
            {
                gpTools.AddRange(tbx.Tools.GetEnumerator());
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            var splitChars = new[] { ' ', ',', '.' };
            var searchTerms = txtSearchTerm.Text.ToLower().Split(splitChars).Distinct().ToArray();

            // don't do any search if there are no search terms
            if (!searchTerms.Any()) return;

            // list all the tools whose names contains the keyword
            var toolList = gpTools.Select((t, i) => new
            {
                Name = string.Format("{0} ({1})", t.DisplayName, t.Toolbox.GetName()),
                Index = i,
                NameParts = t.DisplayName.ToLower().Split(splitChars).Distinct(),
                DescrParts = t.Description.ToLower().Split(splitChars).Distinct()
            });
            var rslt = from tool in toolList
                       let nameRelevance = SearchHelper.Relevance(searchTerms, tool.NameParts)
                       let descrRelevance = SearchHelper.Relevance(searchTerms, tool.DescrParts)
                       where (nameRelevance > 0 || descrRelevance > 0)
                       orderby nameRelevance descending, descrRelevance descending, tool.Name ascending
                       select new { tool, nameRelevance, descrRelevance };

            // populate result list with matching tools
            lvwResults.Items.Clear();
            foreach (var tool in rslt.Select(r => r.tool))
            {
                var item = lvwResults.Items.Add(tool.Name);
                item.ToolTipText = gpTools[tool.Index].Description;
                item.Tag = tool.Index;
            }
        }

        private void lvwResults_DoubleClick(object sender, EventArgs e)
        {
            if (lvwResults.SelectedItems.Count == 0) return;

            // launch the tool the user double clicked on

            var tool = gpTools[(int)lvwResults.SelectedItems[0].Tag];
            var namestring = tool.PathName;

            // Ensure we can run the tool before attempting to invoke it
            if (tool.IsValid())
            {
                toolInvoker.SetTool(tool);
                try
                {
                    toolInvoker.Invoke(null);
                }
                catch (Exception ex)
                {
                    var msg = "An exception occured while opening {0}.\r\n\r\n" +
                              "Exception Details: \r\n";
                    MessageBox.Show(msg + ex.Message, "Unable to open tool",
                                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                // prepare message detailing reason for failure to execute
                var msg = "Cannot execute {0}.\r\n";
                if (!tool.IsLicensed())
                    msg += "Ensure you have a license to execute it.";
                else if (!tool.IsLicensedForProduct())
                    msg += "The tool is not licensed for this product.";
                else if (tool.IsDeleted)
                    msg += "The tool has been deleted.";
                else
                    msg = "An unknown error is preventing {0} from being executed.";

                MessageBox.Show(string.Format(msg, tool.DisplayName), "Unable to Execute Tool",
                                MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void lvwResults_KeyDown(object sender, KeyEventArgs e)
        {
            // launch the tool the user pressed enter on
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                if (lvwResults.SelectedItems.Count > 0)
                    lvwResults_DoubleClick(sender, e);
            }
        }

        private void txtSearchTerm_KeyDown(object sender, KeyEventArgs e)
        {
            // invoke a click on the search button when Enter is pressed
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                btnFind.PerformClick();
            }
        }
    }
}
