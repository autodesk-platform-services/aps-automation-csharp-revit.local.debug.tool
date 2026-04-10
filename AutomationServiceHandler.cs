using System.Runtime.Versioning;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DesignAutomationFramework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

[assembly: SupportedOSPlatform("windows")]

namespace AutomationServiceHandler
{
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class AutomationServiceHandlerApp : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var app = commandData.Application.Application;
            string[] files = Directory.GetFiles(app.AllUsersAddinsLocation, "*.addin");
            foreach (string file in files)
            {
                XElement addin = XElement.Load(file);
                IEnumerable<XElement> childList = from el in addin.Elements() select el;
                foreach (XElement e in childList)
                {
                    try
                    {
                        var assemblyEntity = e.Element("Assembly");
                        if (assemblyEntity == null) continue;

                        System.Reflection.Assembly a = System.Reflection.Assembly.LoadFile(assemblyEntity.Value);
                        bool automationServiceBridge = false;
                        bool revitAPIUI = false;
                        foreach (System.Reflection.AssemblyName an in a.GetReferencedAssemblies())
                        {
                            string assemblyName = an.Name;
                            if (assemblyName == "DesignAutomationBridge" || assemblyName == "AutomationServiceBridge")
                                automationServiceBridge = true;
                            else if (assemblyName == "RevitAPIUI")
                                revitAPIUI = true;
                        }
                        if (automationServiceBridge && revitAPIUI)
                            MessageBox.Show($"RevitAPIUI detected in DA Plugin: {e.Element("Assembly").Value}", "AutomationServiceHandler");
                    }
                    catch
                    {
                        // in case we can't open the dll for some reason, just continue
                        continue;
                    }
                }
            }
            var doc = commandData.Application.ActiveUIDocument?.Document;
            HandleDAApplication(app, doc);
            return Result.Succeeded;
        }

        public void HandleDAApplication(Autodesk.Revit.ApplicationServices.Application app, Document doc)
        {
            try
            {
                var filename = doc?.PathName;
                var currentDir = Directory.GetCurrentDirectory();
                var message = string.Empty;
                if (string.IsNullOrEmpty(filename))
                {
                    message = $"No input file.\nIf you have json file for parameters, now copy it under the current folder:\n{currentDir}";
                    MessageBox.Show(message, "AutomationServiceHandler");
                }

                bool automationServiceResult = DesignAutomationBridge.SetDesignAutomationReady(app, filename);

                if (automationServiceResult)
                {
                    var resultFolder = string.IsNullOrEmpty(filename) ? currentDir : Path.GetDirectoryName(filename);
                    message = $"Succeed!\nFind the results at folder: {resultFolder}";
                }
                else
                {
                    message = $"Failed! You may debug the addin dll.";
                }

                MessageBox.Show(message, "AutomationServiceHandler");

            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.ToString(), "AutomationServiceHandler");
            }
        }
    }
}
