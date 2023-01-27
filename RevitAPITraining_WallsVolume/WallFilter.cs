using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace RevitAPITraining_WallsVolume
{
    internal class WallFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem is Wall;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}