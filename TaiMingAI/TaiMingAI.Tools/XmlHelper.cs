using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TaiMingAI.Tools.Xml
{
    public class XmlHelper
    {
        #region 构造函数+私有属性
        private readonly string _xmlPath;
        public XmlDocument Doc;
        /// <param name="xmlPath">xml文件路径</param>
        public XmlHelper(string xmlPath)
        {
            _xmlPath = xmlPath;
        }
        #endregion

        #region xml读写
        /// <summary>
        /// 创建xml文件
        /// </summary>
        /// <param name="treeModel">文件数据</param>
        public bool CreatXmlTree(XmlElementModel treeModel)
        {
            if (string.IsNullOrEmpty(_xmlPath) || treeModel == null) return false;
            try
            {
                //创建一个XML文档对象
                Doc = new XmlDocument();
                //声明XML头部信息
                var declaration = Doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                //添加进doc对象顶级节点
                Doc.AppendChild(declaration);
                var root = Doc.CreateElement(treeModel.Name);
                Doc.AppendChild(root);
                if (!string.IsNullOrEmpty(treeModel.Value))
                {
                    var text = Doc.CreateTextNode(treeModel.Value);
                    root.AppendChild(text);
                }
                if (treeModel.Attrs != null && treeModel.Attrs.Keys.Count > 0)
                {
                    foreach (var dic in treeModel.Attrs)
                    {
                        root.SetAttribute(dic.Key, dic.Value);
                    }
                }
                if (treeModel.EleList != null && treeModel.EleList.Count > 0)
                {
                    //递归添加子节点
                    AddElement(root, treeModel.EleList);
                }
                //doc文档对象保存写入
                Doc.Save(_xmlPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="root">父节点</param>
        /// <param name="eleList">子节点数据</param>
        private void AddElement(XmlElement root, List<XmlElementModel> eleList)
        {
            foreach (var item in eleList)
            {
                XmlElement childRoot = Doc.CreateElement(item.Name);
                if (item.Attrs != null && item.Attrs.Keys.Count > 0)
                {
                    foreach (var dic in item.Attrs)
                    {
                        childRoot.SetAttribute(dic.Key, dic.Value);
                    }
                }
                if (!string.IsNullOrEmpty(item.Value))
                {
                    var text = Doc.CreateTextNode(item.Value);
                    childRoot.AppendChild(text);
                }
                root.AppendChild(childRoot);
                if (item.EleList != null && item.EleList.Count > 0)
                {
                    AddElement(childRoot, item.EleList);
                }
            }
        }
        /// <summary>
        /// 读取xml的节点
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="name">属性名称</param>
        /// <returns>属性值</returns>
        public static string ReadXml(string path, string nodeName, string name)
        {
            var xmlValue = string.Empty;
            FileStream myFile = null;
            XmlTextReader xmlTextReader = null;
            try
            {
                if (!File.Exists(path)) return xmlValue;

                myFile = new FileStream(path, FileMode.Open); //打开xml文件 
                xmlTextReader = new XmlTextReader(myFile); //xml文件阅读器 
                while (xmlTextReader.Read())
                {
                    if (xmlTextReader.Name == nodeName)
                    {
                        xmlValue = xmlTextReader.GetAttribute(name);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("读取xml的节点异常", ex);
            }
            finally
            {
                if (myFile != null)
                    xmlTextReader.Close();
                if (xmlTextReader != null)
                    myFile.Close();
            }
            return xmlValue;
        }
        #endregion

        #region xml反序列化
        /// <summary>
        /// xml_TO_model
        /// </summary>
        /// <typeparam name="T">反序列化后类型</typeparam>
        /// <param name="filePath">xml文件路径</param>
        /// <returns></returns>
        public static T DeserializeFromXml<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }
                using (StreamReader reader = new StreamReader(filePath))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    T ret = (T)xs.Deserialize(reader);
                    return ret;
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("DeserializeFromXml反序列化异常", ex);
                return default(T);
            }
        }
        #endregion

        #region xml序列化
        /// <summary>
        /// model_TO_xml
        /// </summary>
        /// <typeparam name="T">需要序列化的对象类型，必须声明[Serializable]特征</typeparam>
        /// <param name="filePath">保存xml路径</param>
        /// <param name="obj">需要序列化的对象</param>
        public static void SerializeToXml<T>(string filePath, T obj)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    xs.Serialize(writer, obj);
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("SerializeToXml序列化异常", ex);
            }
        }

        /// <summary>
        /// model_TO_xmlStr
        /// </summary>
        /// <typeparam name="T">需要序列化的对象类型，必须声明[Serializable]特征</typeparam>
        /// <param name="obj">需要序列化的对象</param>
        /// <param name="omitXmlDeclaration">true:省略XML声明;否则为false.默认false，即编写 XML 声明。</param>
        /// <returns>序列化后的字符串</returns>
        public static string XmlSerialize<T>(T obj, bool omitXmlDeclaration = false)
        {
            try
            {
                //This property only applies to XmlWriter instances that output text content to a stream; otherwise, this setting is ignored.
                //可能很多朋友遇见过 不能转换成Xml不能反序列化成为UTF8XML声明的情况，就是这个原因。
                var xmlSettings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = omitXmlDeclaration,
                    Encoding = new UTF8Encoding(false)
                };
                var stream = new MemoryStream();
                var xmlwriter = XmlWriter.Create(stream, xmlSettings);
                //这里如果直接写成：Encoding = Encoding.UTF8 会在生成的xml中加入BOM(Byte-order Mark) 信息(Unicode 字节顺序标记),
                //所以new System.Text.UTF8Encoding(false)是最佳方式，省得再做替换的麻烦
                var xmlSerializerNamespaces = new XmlSerializerNamespaces();

                //在XML序列化时去除默认命名空间xmlns:xsd和xmlns:xsi
                xmlSerializerNamespaces.Add(string.Empty, string.Empty);
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(xmlwriter, obj, xmlSerializerNamespaces);

                return Encoding.UTF8.GetString(stream.ToArray());
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("XmlSerialize序列化异常", ex);
                return string.Empty;
            }

        }
        #endregion
    }
    /// <summary>
    /// 创建xml实体类型
    /// </summary>
    public class XmlElementModel
    {
        //节点名称
        public string Name { get; set; }
        //节点问题
        public string Value { get; set; }
        //节点属性
        public Dictionary<string, string> Attrs { get; set; }
        //子节点
        public List<XmlElementModel> EleList { get; set; }
    }
}
