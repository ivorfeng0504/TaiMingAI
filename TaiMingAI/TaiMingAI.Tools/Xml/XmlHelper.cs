using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace TaiMingAI.Tools.Xml
{
    public class XmlHelper
    {
        private readonly string _xmlPath;
        public XmlDocument Doc;
        /// <summary>
        /// xml文件路径
        /// </summary>
        /// <param name="xmlPath"></param>
        public XmlHelper(string xmlPath)
        {
            _xmlPath = xmlPath;
        }

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
                    throw new ArgumentNullException(filePath + " not Exists");
                using (StreamReader reader = new StreamReader(filePath))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    T ret = (T)xs.Deserialize(reader);
                    return ret;
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// model_TO_xml
        /// </summary>
        /// <typeparam name="T">待反序列化类型</typeparam>
        /// <param name="filePath">保存xml路径</param>
        /// <param name="obj">待反序列化数据</param>
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
            catch (Exception)
            {
                // ignored
            }
        }
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
