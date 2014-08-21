using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geoprocessing;

namespace ToolFinder
{
    internal static class ExtensionMethods
    {
        /// <summary>Enumerates the IGPToolbox of the items in the enumeration.</summary>
        /// <param name="toolbox">IEnumGPToolbox instance to be enumerated.</param>
        public static IEnumerable<IGPToolbox> GetEnumerator(this IEnumGPToolbox toolbox)
        {
            while (true)
            {
                var name = toolbox.Next();
                if (name != null)
                    yield return name;
                else
                    break;
            };
        }
        
        /// <summary>Enumerates the IGPTool of the items in the enumeration.</summary>
        /// <param name="toolboxName">IEnumGPTool instance to be enumerated.</param>
        public static IEnumerable<IGPTool> GetEnumerator(this IEnumGPTool tool)
        {
            while (true)
            {
                var name = tool.Next();
                if (name != null)
                    yield return name;
                else
                    break;
            };
        }

        /// <summary>Gets the ArcGIS-style name of the specified toolbox.</summary>
        /// <param name="tbx">The toolbox whose name is to be returned.</param>
        /// <param name="stripTail">Option to strip trailing "Tools" from name.</param>
        public static string GetName(this IGPToolbox tbx, bool stripTail = true){
            var name = System.IO.Path.GetFileNameWithoutExtension(tbx.PathName).Trim();
            var tail = " tools";

            // strip trailing " Tools" if present
            if (stripTail && 
                name.EndsWith(tail, StringComparison.CurrentCultureIgnoreCase))
                name = name.Substring(0, name.Length - tail.Length);


            return name;
        }
    }
}
