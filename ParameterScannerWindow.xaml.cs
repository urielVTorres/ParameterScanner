using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RevitAPI_TEST
{
    /// <summary>
    /// Lógica de interacción para ParameterScannerWindow.xaml
    /// </summary>
    public partial class ParameterScannerWindow : Window
    {
        private readonly Document _doc;
        private readonly UIDocument _uidoc;
        private List<Element> matching_elements;

        public ParameterScannerWindow(Document doc, UIDocument uidoc)
        {
            InitializeComponent();
            _doc = doc;
            _uidoc = uidoc;
        }

        private void IsolateButtonOnClick(object sender, RoutedEventArgs e)
        {
            string parameter_name = Name_TexBox.Text;
            string parameter_value = Value_TexBox.Text;

            // If parameter name is empty, show a message
            if (parameter_name == "" || parameter_name == null)
            {
                MessageBox.Show("Parameter Name can't be empty");
                return;
            }

            // Search for the matchingElements
            matching_elements = FindElementsByParameter(_doc, parameter_name, parameter_value);

            // Isolate elements in the current view
            using (Transaction transaction = new Transaction(_doc, "Isolate in View"))
            {
                transaction.Start();
                if (matching_elements.Count > 0)
                {
                    _uidoc.ActiveView.IsolateElementsTemporary(matching_elements.ConvertAll(element => element.Id));
                    MessageBox.Show($"{matching_elements.Count} elements have been isolated in the current view");
                }
                else
                {
                    MessageBox.Show("No matching elements found.");
                }
                transaction.Commit();
            }
        }

        private void SelectButtonOnClick(object sender, RoutedEventArgs e)
        {
            string parameter_name = Name_TexBox.Text;
            string parameter_value = Value_TexBox.Text;

            // Clear current selection
            _uidoc.Selection.SetElementIds(new List<ElementId>());
            // If parameter name is empty, show a message
            if (parameter_name == "" || parameter_name == null)
            {
                MessageBox.Show("Parameter Name can't be empty");
                return;
            }

            // Search for matching elements
            matching_elements = FindElementsByParameter(_doc, parameter_name, parameter_value);

            // Select elements in Revit if they exists.
            if (matching_elements.Count > 0)
            {
                _uidoc.Selection.SetElementIds(new List<ElementId>(matching_elements.ConvertAll(element => element.Id)));
                MessageBox.Show($"{matching_elements.Count} have been selected.");
            }
            else
            {
                MessageBox.Show("No matching elements found.");
            }
        }

        // Search by parameters
        private List<Element> FindElementsByParameter(Document doc, string parameter_name, string parameter_value)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            IList<Element> all_elements = collector.WhereElementIsNotElementType().ToElements();
            List<Element> matching_elements = new List<Element>();

            foreach (Element element in all_elements)
            {

                Parameter param = element.LookupParameter(parameter_name);
                if (parameter_value == ""|| parameter_value == null)
                {
                    if (param != null)
                    {
                        matching_elements.Add(element);
                    }
                }else
                {
                    
                    if (param != null && param.AsValueString() == parameter_value)
                    {
                        matching_elements.Add(element);
                    }
                }
            }
            return matching_elements;
        }
    }
}
