namespace RSLib.GE;

public static class FileUtils
{
    /// <summary>
    /// Returns the content of a file as a string.
    /// </summary>
    /// <param name="resRelativePath">File path relative to res:// directory.</param>
    /// <returns>File context as string.</returns>
    public static string ReadText(string resRelativePath)
    {
        Godot.FileAccess file = Godot.FileAccess.Open($"res://{resRelativePath}", Godot.FileAccess.ModeFlags.Read);
        string text = file.GetAsText();
        file.Close();
        return text;
    }

    /// <summary>
    /// Returns the main element of a .xml file.
    /// </summary>
    /// <param name="resRelativePath">File path relative to res:// directory.</param>
    /// <param name="mainElementName">Main element name.</param>
    /// <returns>Main file element as XElement.</returns>
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