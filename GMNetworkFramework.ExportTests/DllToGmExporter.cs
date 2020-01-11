using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using GMNetworkFramework.Export;

namespace GMNetworkFramework.Tests
{
    public class DllToGmExporter
    {
        private string extName;
        private string version;
        private string dllName;
        private string fileName;
        private string GMProjectName;

        private XmlWriter writer;

        public DllToGmExporter(string extName, string version, string dllName, string fileName, string GMProjectName)
        {
            this.extName = extName;
            this.version = version;
            this.dllName = dllName;
            this.fileName = fileName;
            this.GMProjectName = GMProjectName;

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true,
                OmitXmlDeclaration = true,
                ConformanceLevel = ConformanceLevel.Fragment,
            };

            this.writer = XmlWriter.Create(fileName, xmlWriterSettings);
        }

        public void MoveDllAndExt()
        {
            writer.Close();

            string basePath = Directory.GetCurrentDirectory(); // its already with bin\BuildMode(Debug/Release)
            var basePath2 = Directory.GetParent(basePath).Parent.Parent; //move up to .sln directory
            Console.WriteLine(basePath);
            Console.WriteLine(basePath2.FullName);
            //moving dll to {GM_project_name}.gmx\extensions\{extName}
            if (File.Exists(basePath2.FullName + $"\\{GMProjectName}.gmx\\" + @"\\extensions\\" + extName + @"\\" + dllName))
            {
                File.Delete(basePath2.FullName + $"\\{GMProjectName}.gmx\\" + @"\\extensions\\" + extName + @"\\" + dllName);
            }

            File.Move(basePath + "\\" + dllName, basePath2.FullName + @"\\GMLoggerClient.gmx\\" + @"\\extensions\\" + extName + @"\\"+ dllName);

            if (File.Exists(basePath2.FullName + $"\\{GMProjectName}.gmx\\" + @"\\extensions\\" + extName + ".extension.gmx"))
            {
                File.Delete(basePath2.FullName + $"\\{GMProjectName}.gmx\\" + @"\\extensions\\" + extName + ".extension.gmx");
            }

            File.Move(basePath + "\\" + fileName, basePath2.FullName + $"\\{GMProjectName}.gmx\\" + @"\\extensions\\" + extName +".extension.gmx");
        }

        private void WriteMetaInfo()
        {
            DateTime curDate = DateTime.Today;

            writer.WriteStartElement("extension");
            writer.WriteElementString("name", extName);
            writer.WriteElementString("version", version);
            writer.WriteElementString("packageID", "");
            writer.WriteElementString("ProductID", "");
            writer.WriteElementString("date", curDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture));
            writer.WriteElementString("license", "MIT");
            writer.WriteElementString("description", "");
            writer.WriteElementString("helpfile", "");
            writer.WriteElementString("installdir", "");
            writer.WriteElementString("classname", "");
            writer.WriteElementString("sourcedir", "");
            writer.WriteElementString("androidsourcedir", "");
            writer.WriteElementString("macsourcedir", "");
            writer.WriteElementString("maclinkerflags", "");
            writer.WriteElementString("maccompilerflags", "");
            writer.WriteElementString("androidinject", "");
            writer.WriteElementString("androidmanifestinject", "");
            writer.WriteElementString("iosplistinject", "");
            writer.WriteElementString("androidactivityinject", "");
            writer.WriteElementString("gradleinject", "");
            writer.WriteStartElement("iosSystemFrameworks");
            writer.WriteEndElement();
            writer.WriteStartElement("iosThirdPartyFrameworks");
            writer.WriteEndElement();
            writer.WriteStartElement("ConfigOptions");
                writer.WriteStartElement("Config");
                    writer.WriteAttributeString("name", "Default");
                    writer.WriteElementString("CopyToMask", "105553895358702");
                writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteStartElement("androidPermissions");
            writer.WriteEndElement();

            writer.WriteStartElement("IncludedResources");
            writer.WriteEndElement();

            /*functions and extensions must be written in 'files' Element*/
            writer.WriteStartElement("files");
                writer.WriteStartElement("file");
                    writer.WriteElementString("filename", dllName);
                    writer.WriteElementString("origname", $"extensions\\{dllName}");
                    writer.WriteElementString("init", "");
                    writer.WriteElementString("final", "");
                    writer.WriteElementString("kind", "1");
                    writer.WriteElementString("uncompress", "0");
                    writer.WriteStartElement("ConfigOptions");
                        writer.WriteStartElement("Config");
                            writer.WriteAttributeString("name", "Default");
                            writer.WriteElementString("CopyToMask", "105553895358702");
                        writer.WriteEndElement();
                writer.WriteEndElement();//file end

            writer.WriteStartElement("ProxyFiles");
            writer.WriteEndElement();

            writer.WriteStartElement("functions");
        }

        private void WriteFunctionInfo(MethInfo methodInfo)
        {
            writer.WriteStartElement("function");

                writer.WriteElementString("name", methodInfo.Attribute.Name);
                writer.WriteElementString("externalName", methodInfo.Attribute.ExternalName);
                writer.WriteElementString("kind", "12");
                writer.WriteElementString("help", methodInfo.Attribute.Help);
                writer.WriteElementString("returnType", ((int)methodInfo.Attribute.ReturnType).ToString());
                writer.WriteElementString("argCount", methodInfo.Attribute.ArgCount.ToString());

                    writer.WriteStartElement("args");
                    foreach (var arg in methodInfo.Attribute.args)
                    {
                        writer.WriteElementString("arg", ((int)arg).ToString());
                    }
                    writer.WriteEndElement();

            writer.WriteEndElement();
        }

        private static List<MethInfo> FindPropertyToGm(Type Class)
        {
            var methods = Class.GetMethods().ToList();///.GetProperties(BindingFlags. | BindingFlags.Instance).ToList();

            var thisMethods = new List<MethInfo>();

            foreach (var method in methods)
            {
                var attrs = System.Attribute.GetCustomAttributes(method, typeof(ToGMAttribute));

                foreach (var attr in attrs)
                {
                    var p = (ToGMAttribute)attr;
                    var paramss = method.GetParameters();
                    p.args = new GMType[paramss.Length];
                    for (var i = 0; i < paramss.Length; i += 1)// (var par in paramss)
                    {
                        p.args[i] = ToGMAttribute.TypeFromString(paramss[i].ParameterType.Name);
                        Console.WriteLine(p.args[i]);
                    }

                    p.ReturnType = ToGMAttribute.TypeFromString(method.ReturnType.Name);
                    p.ArgCount = p.args.Length > 0 ? p.args.Length : -1;
                    p.Name = method.Name;

                    if (p.ExternalName == null)
                        p.ExternalName = p.Name;

                    thisMethods.Add(new MethInfo() { Method = method, Attribute = p });
                }
            }

            return thisMethods;
        }

        private void EndWriteInfo()
        {
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
        }

        

        public void ExportToExtension(Assembly asm)
        {
            WriteMetaInfo();

            var theList = asm.GetTypes()
                .Where(x => x.IsClass)
                .ToList();


            foreach (var typelist in theList)
            {
                foreach (var prop in FindPropertyToGm(typelist))
                {
                    WriteFunctionInfo(prop);
                }
            }

            EndWriteInfo();
        }
    }

    public class MethInfo
    {
        public MethodInfo Method;
        public ToGMAttribute Attribute;
    }
}
