using System;
using System.Reflection;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;

namespace ParameterScanner
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class App : IExternalApplication
    {
        void AddRibbonPanel(UIControlledApplication application)
        {
            string tab_name = "Parameter";
            application.CreateRibbonTab(tab_name);
            RibbonPanel ribbon_panel = application.CreateRibbonPanel(tab_name, "Parameter Tools");
            string this_assembly_path = Assembly.GetExecutingAssembly().Location;

            PushButtonData A1 = new PushButtonData(
                "ParameterScanner",
                "Parameter Scanner", 
                this_assembly_path,
                "ParameterScanner.ParameterScanner");

            Uri iconPath = new Uri("pack://application:,,,/ParameterScanner;component/assets/icons8-parameter-64.png");
            BitmapImage iconImage = new BitmapImage(iconPath);
            A1.LargeImage = iconImage;

            PushButton pb1 = ribbon_panel.AddItem(A1) as PushButton;
            pb1.ToolTip = "Select or Isolate parameters based on its value.";
        }

        public Result OnShutdown(UIControlledApplication application)
        {

            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            AddRibbonPanel(application);
            return Result.Succeeded;
        }
    }
}
