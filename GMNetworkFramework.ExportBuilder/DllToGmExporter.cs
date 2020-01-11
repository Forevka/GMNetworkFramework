using GMNetworkFramework.Export;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace GMNetworkFramework.ExportBuilder
{
    public class DllToGmExporter
    {
        private readonly string _extName;
        private readonly string _version;
        private readonly string _dllName;
        private readonly string _fileName;
        private readonly string _gmProjectName;

        private readonly XmlWriter _writer;

        public DllToGmExporter(string extName, string version, string dllName, string fileName, string gmProjectName)
        {
            this._extName = extName;
            this._version = version;
            this._dllName = dllName;
            this._fileName = fileName;
            this._gmProjectName = gmProjectName;

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true,
                OmitXmlDeclaration = true,
                ConformanceLevel = ConformanceLevel.Fragment,
            };

            this._writer = XmlWriter.Create(fileName, xmlWriterSettings);
        }

        public void MoveDllAndExt()
        {
            string basePath = Directory.GetCurrentDirectory(); // its already with bin\BuildMode(Debug/Release)
            var directoryInfo = Directory.GetParent(basePath).Parent;
            if (directoryInfo == null) return;
            var basePath2 = directoryInfo.Parent; //move up to .sln directory

            //moving dll to {GM_project_name}.gmx\extensions\{extName}
            if (File.Exists(basePath2.FullName + $"\\{_gmProjectName}.gmx\\" + @"\\extensions\\" + _extName + @"\\" + _dllName))
            {
                File.Delete(basePath2.FullName + $"\\{_gmProjectName}.gmx\\" + @"\\extensions\\" + _extName + @"\\" + _dllName);
            }

            File.Move(basePath + "\\" + _dllName, basePath2.FullName + $"\\{_gmProjectName}.gmx\\" + @"\\extensions\\" + _extName + @"\\"+ _dllName);

            if (File.Exists(basePath2.FullName + $"\\{_gmProjectName}.gmx\\" + @"\\extensions\\" + _extName + ".extension.gmx"))
            {
                File.Delete(basePath2.FullName + $"\\{_gmProjectName}.gmx\\" + @"\\extensions\\" + _extName + ".extension.gmx");
            }

            File.Move(basePath + "\\" + _fileName, basePath2.FullName + $"\\{_gmProjectName}.gmx\\" + @"\\extensions\\" + _extName +".extension.gmx");
        }

        private void WriteMetaInfo()
        {
            DateTime curDate = DateTime.Today;

            _writer.WriteStartElement("extension");
            _writer.WriteElementString("name", _extName);
            _writer.WriteElementString("version", _version);
            _writer.WriteElementString("packageID", "");
            _writer.WriteElementString("ProductID", "");
            _writer.WriteElementString("date", curDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture));
            _writer.WriteElementString("license", "MIT");
            _writer.WriteElementString("description", "");
            _writer.WriteElementString("helpfile", "");
            _writer.WriteElementString("installdir", "");
            _writer.WriteElementString("classname", "");
            _writer.WriteElementString("sourcedir", "");
            _writer.WriteElementString("androidsourcedir", "");
            _writer.WriteElementString("macsourcedir", "");
            _writer.WriteElementString("maclinkerflags", "");
            _writer.WriteElementString("maccompilerflags", "");
            _writer.WriteElementString("androidinject", "");
            _writer.WriteElementString("androidmanifestinject", "");
            _writer.WriteElementString("iosplistinject", "");
            _writer.WriteElementString("androidactivityinject", "");
            _writer.WriteElementString("gradleinject", "");
            _writer.WriteStartElement("iosSystemFrameworks");
            _writer.WriteEndElement();
            _writer.WriteStartElement("iosThirdPartyFrameworks");
            _writer.WriteEndElement();
            _writer.WriteStartElement("ConfigOptions");
                _writer.WriteStartElement("Config");
                    _writer.WriteAttributeString("name", "Default");
                    _writer.WriteElementString("CopyToMask", "105553895358702");
                _writer.WriteEndElement();
            _writer.WriteEndElement();

            _writer.WriteStartElement("androidPermissions");
            _writer.WriteEndElement();

            _writer.WriteStartElement("IncludedResources");
            _writer.WriteEndElement();

            /*functions and extensions must be written in 'files' Element*/
            _writer.WriteStartElement("files");
                _writer.WriteStartElement("file");
                    _writer.WriteElementString("filename", _dllName);
                    _writer.WriteElementString("origname", $"extensions\\{_dllName}");
                    _writer.WriteElementString("init", "");
                    _writer.WriteElementString("final", "");
                    _writer.WriteElementString("kind", "1");
                    _writer.WriteElementString("uncompress", "0");
                    _writer.WriteStartElement("ConfigOptions");
                        _writer.WriteStartElement("Config");
                            _writer.WriteAttributeString("name", "Default");
                            _writer.WriteElementString("CopyToMask", "105553895358702");
                        _writer.WriteEndElement();
                _writer.WriteEndElement();//file end

            _writer.WriteStartElement("ProxyFiles");
            _writer.WriteEndElement();

            _writer.WriteStartElement("functions");
        }

        private void WriteFunctionInfo(MethInfo methodInfo)
        {
            _writer.WriteStartElement("function");

                _writer.WriteElementString("name", methodInfo.Attribute.Name);
                _writer.WriteElementString("externalName", methodInfo.Attribute.ExternalName);
                _writer.WriteElementString("kind", "12");
                _writer.WriteElementString("help", methodInfo.Attribute.Help);
                _writer.WriteElementString("returnType", ((int)methodInfo.Attribute.ReturnType).ToString());
                _writer.WriteElementString("argCount", methodInfo.Attribute.ArgCount.ToString());

                    _writer.WriteStartElement("args");
                    foreach (var arg in methodInfo.Attribute.Args)
                    {
                        _writer.WriteElementString("arg", ((int)arg).ToString());
                    }
                    _writer.WriteEndElement();

            _writer.WriteEndElement();
        }

        private static List<MethInfo> FindPropertyToGm(Type @class)
        {
            var methods = @class.GetMethods().ToList();

            var thisMethods = new List<MethInfo>();

            foreach (var method in methods)
            {
                var attrs = Attribute.GetCustomAttributes(method, typeof(ToGMAttribute));

                foreach (var attr in attrs)
                {
                    var p = (ToGMAttribute)attr;
                    var paramss = method.GetParameters();
                    p.Args = new GMType[paramss.Length];
                    for (var i = 0; i < paramss.Length; i += 1)// (var par in paramss)
                    {
                        p.Args[i] = ToGMAttribute.TypeFromString(paramss[i].ParameterType.Name);
                        Console.WriteLine(p.Args[i]);
                    }

                    p.ReturnType = ToGMAttribute.TypeFromString(method.ReturnType.Name);
                    p.ArgCount = p.Args.Length > 0 ? p.Args.Length : -1;
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
            _writer.WriteEndElement();
            _writer.WriteEndElement();
            _writer.WriteEndElement();
            _writer.WriteEndElement();
            _writer.Flush();

            _writer.Close();
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
