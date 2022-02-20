using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace RevitAPITraining_CopyGroup
{
    [Transaction(TransactionMode.Manual)]
    public class CopyGroup : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            Reference reference = uiDoc.Selection.PickObject(ObjectType.Element, "Выберите группу объектов");
            Element element = doc.GetElement(reference);
            Group group = element as Group;

            XYZ point = uiDoc.Selection.PickPoint("Выберите точку");
            Transaction tr = new Transaction(doc);
            tr.Start("Копирование группы объектов");
            doc.Create.PlaceGroup(point, group.GroupType);
            tr.Commit();

            return Result.Succeeded;
        }
    }
}
