using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMNetworkFramework.Export;
using System.Reflection;
using System.Xml;

namespace GMNetworkFramework.ExportTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Utils.GetCurrentTimestamp());

            var theList = Assembly.GetAssembly(typeof(Utils)).GetTypes()
                //.Where(t => t.Namespace.Contains("GMNetworkFramework"))
                .Where(x => x.IsClass)
                .ToList();

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true,
                OmitXmlDeclaration = true,
                ConformanceLevel = ConformanceLevel.Fragment,
            };
            using (XmlWriter writer = XmlWriter.Create("GMNetworkFramework.extension.gmx", xmlWriterSettings))
            {
                writer.WriteStartElement("extension");
                writer.WriteElementString("name", "GMNetworkFramework");
                writer.WriteElementString("version", "1.0.0");
                writer.WriteElementString("packageID", "");
                writer.WriteElementString("ProductID", "");
                writer.WriteElementString("date", "09/01/20");
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
                writer.WriteStartElement("files");
                writer.WriteStartElement("file");

                writer.WriteElementString("filename", "GMNetworkFramework.Export.dll");
                writer.WriteElementString("origname", @"extensions\GMNetworkFramework.Export.dll");
                writer.WriteElementString("init", "");
                writer.WriteElementString("final", "");
                writer.WriteElementString("kind", "1");
                writer.WriteElementString("uncompress", "0");
                writer.WriteStartElement("ConfigOptions");
                writer.WriteStartElement("Config");
                writer.WriteAttributeString("name", "Default");
                writer.WriteElementString("CopyToMask", "105553895358702");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteStartElement("ProxyFiles");
                writer.WriteEndElement();
                writer.WriteStartElement("functions");
                foreach (var typelist in theList)
                {
                    foreach (var prop in FindPropertyToGm(typelist))
                    {
                        //Console.WriteLine(prop.Name);
                        
                            writer.WriteStartElement("function");
                            writer.WriteElementString("name", prop.Attribute.Name);
                            writer.WriteElementString("externalName", prop.Attribute.ExternalName);
                            writer.WriteElementString("kind", "12");
                            writer.WriteElementString("help", prop.Attribute.Help);
                            writer.WriteElementString("returnType", ((int)prop.Attribute.ReturnType).ToString());
                            writer.WriteElementString("argCount", prop.Attribute.ArgCount.ToString());
                            writer.WriteStartElement("args");
                            foreach (var arg in prop.Attribute.args)
                            {
                                writer.WriteElementString("arg",((int)arg).ToString());
                            }
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Flush();
            }

            Console.ReadKey();
        }

        public class MethInfo
        {
            public MethodInfo Method;
            public ToGMAttribute Attribute;
        }

        public static List<MethInfo> FindPropertyToGm(Type Class)
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
                    for(var i=0;i<paramss.Length;i+=1)// (var par in paramss)
                    {
                        p.args[i] = ToGMAttribute.typeFromString(paramss[i].ParameterType.Name);
                        Console.WriteLine(p.args[i]);
                    }

                    p.ReturnType = ToGMAttribute.typeFromString(method.ReturnType.Name);
                    p.ArgCount = p.args.Length > 0 ? p.args.Length : -1;
                    p.Name = method.Name;

                    if (p.ExternalName == null)
                        p.ExternalName = p.Name;

                    thisMethods.Add(new MethInfo(){Method = method, Attribute = p});
                }
            }

            return thisMethods;
        }
        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
                assembly.GetTypes()
                    .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                    .ToArray();
        }
}
}
