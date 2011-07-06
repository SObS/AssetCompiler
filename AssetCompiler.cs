using System;
using System.IO;
using System.Xml;

namespace AssetCompiler
{
    class AssetCompiler
    {
        static string folderID;
        static FileInfo[] files;

        static void Main()
        {
            folderID = Guid.NewGuid().ToString();
            files = new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles("*.jp2");

            WriteMySiteLibraryFolders(folderID);
            WriteMyAssetSet(files);
            WriteMySiteLibraryItems(folderID, files);
        }

        static void WriteMySiteLibraryFolders(string folderID)
        {
            XmlTextWriter xmlwriter = new XmlTextWriter("MySiteLibraryFolders.xml", null);

            xmlwriter.Formatting = System.Xml.Formatting.Indented;
            xmlwriter.IndentChar = ' ';
            xmlwriter.Indentation = 2;

            xmlwriter.WriteStartElement("Nini");
            xmlwriter.WriteStartElement("Section");
            xmlwriter.WriteStartAttribute("Name");
            xmlwriter.WriteValue("MySiteLibraryFolder");
            xmlwriter.WriteEndAttribute();

            WriteKey(xmlwriter, "folderID", folderID);
            WriteKey(xmlwriter, "parentFolderID", "00000112-000f-0000-0000-000100bba000");
            WriteKey(xmlwriter, "name", "MySiteLibraryFolder");
            WriteKey(xmlwriter, "type", "0");

            xmlwriter.WriteEndElement();
            xmlwriter.WriteEndElement();
            xmlwriter.Close();
        }

        static void WriteMyAssetSet(FileInfo[] files)
        {
            string shortfile;

            XmlTextWriter xmlwriter = new XmlTextWriter("MyAssetSet.xml", null);

            xmlwriter.Formatting = System.Xml.Formatting.Indented;
            xmlwriter.IndentChar = ' ';
            xmlwriter.Indentation = 2;

            xmlwriter.WriteStartElement("Nini");

            foreach (FileInfo f in files)
            {
                shortfile = Path.GetFileNameWithoutExtension(f.FullName);

                xmlwriter.WriteStartElement("Section");
                xmlwriter.WriteStartAttribute("Name");
                xmlwriter.WriteValue(shortfile);
                xmlwriter.WriteEndAttribute();

                WriteKey(xmlwriter, "assetID", shortfile);
                WriteKey(xmlwriter, "name", shortfile);
                WriteKey(xmlwriter, "assetType", "0");
                WriteKey(xmlwriter, "inventoryType", "0");
                WriteKey(xmlwriter, "fileName", f.Name);

                xmlwriter.WriteEndElement();
            }

            xmlwriter.WriteEndElement();
            xmlwriter.Close();
        }

        static void WriteMySiteLibraryItems(string folderID, FileInfo[] files)
        {
            string shortfile;

            XmlTextWriter xmlwriter = new XmlTextWriter("MySiteLibraryItems.xml", null);

            xmlwriter.Formatting = System.Xml.Formatting.Indented;
            xmlwriter.IndentChar = ' ';
            xmlwriter.Indentation = 2;

            xmlwriter.WriteStartElement("Nini");

            foreach (FileInfo f in files)
            {
                shortfile = Path.GetFileNameWithoutExtension(f.FullName);

                xmlwriter.WriteStartElement("Section");
                xmlwriter.WriteStartAttribute("Name");
                xmlwriter.WriteValue(shortfile);
                xmlwriter.WriteEndAttribute();

                WriteKey(xmlwriter, "inventoryID", Guid.NewGuid().ToString());
                WriteKey(xmlwriter, "assetID", shortfile);
                WriteKey(xmlwriter, "folderID", folderID);
                WriteKey(xmlwriter, "description", shortfile);
                WriteKey(xmlwriter, "name", shortfile);
                WriteKey(xmlwriter, "assetType", "0");
                WriteKey(xmlwriter, "inventoryType", "0");
                WriteKey(xmlwriter, "currentPermissions", "2147483647");
                WriteKey(xmlwriter, "nextPermissions", "2147483647");
                WriteKey(xmlwriter, "everyonePermissions", "2147483647");
                WriteKey(xmlwriter, "basePermissions", "2147483647");

                xmlwriter.WriteEndElement();
            }

            xmlwriter.WriteEndElement();
            xmlwriter.Close();
        }

        static void WriteKey(XmlWriter handle, string keyName, string keyValue)
        {
            handle.WriteStartElement("Key");
            handle.WriteStartAttribute("Name");
            handle.WriteValue(keyName);
            handle.WriteEndAttribute();
            handle.WriteStartAttribute("Value");
            handle.WriteValue(keyValue);
            handle.WriteEndAttribute();
            handle.WriteEndElement();
        }
    }
}
