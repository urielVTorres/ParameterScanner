
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitAPI_TEST;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ParameterScanner
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ParameterScanner : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            ParameterScannerWindow parameterScannerWindow = new ParameterScannerWindow(doc, uidoc);
            parameterScannerWindow.ShowDialog();

            return Result.Succeeded;
        }
    }

}
