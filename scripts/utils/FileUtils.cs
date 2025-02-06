namespace RSLib.GE;

public static class FileUtils
{
    public static string ReadText(string resRelativePath)
    {
        Godot.FileAccess file = Godot.FileAccess.Open($"res://{resRelativePath}", Godot.FileAccess.ModeFlags.Read);
        string text = file.GetAsText();
        file.Close();
        return text;
    }

    public static System.Xml.Linq.XElement ReadXML(string resRelativePath, string mainElementName)
    {
        string xmlString = ReadText(resRelativePath);
        
        System.Xml.Linq.XDocument xDocument = System.Xml.Linq.XDocument.Parse(xmlString, System.Xml.Linq.LoadOptions.SetBaseUri);
        System.Xml.Linq.XElement mainElement = xDocument.Element(mainElementName);

        if (mainElement == null)
            Godot.GD.Print($"Unable to get element {mainElementName} from XML file res://{resRelativePath}");

        return mainElement;
    }
}