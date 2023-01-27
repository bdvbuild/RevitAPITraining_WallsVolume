using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RevitAPITraining_WallsVolume
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> selectedElementRefList = uidoc.Selection.PickObjects(ObjectType.Element, new WallFilter(), "Выберите стены");

            double sumVolume = 0;

            foreach (var selectedElement in selectedElementRefList)
            {
                Wall oWall = doc.GetElement(selectedElement) as Wall;
                Parameter wallVolume = oWall.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED);
                double volume = UnitUtils.ConvertFromInternalUnits(wallVolume.AsDouble(), UnitTypeId.CubicMeters);
                sumVolume += volume;
            }

            TaskDialog.Show("Selection", $"Объем выбранных стен: {sumVolume}м^3");

            return Result.Succeeded;
        }
    }
}
