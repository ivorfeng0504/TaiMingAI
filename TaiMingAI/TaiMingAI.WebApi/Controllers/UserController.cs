using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TaiMingAI.WebApi.Models;
using TaiMingAI.Tools.Xml;

namespace TaiMingAI.WebApi.Controllers
{
    public class UserController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public JsonResult<Data> Get(int id)
        {
            var userInfo = XmlHelper.DeserializeFromXml<Data>("G://xml//test.xml");
            return Json(userInfo);
        }

        // POST api/<controller>
        [HttpGet]
        public bool Posts()
        {
            XmlHelper xmlHelper = new XmlHelper("G://xml//test.xml");
            var tree = new XmlElementModel
            {
                Name = "Data",
                EleList = new List<XmlElementModel>(),
                Attrs = new Dictionary<string, string>()
            };
            tree.Attrs.Add("addTime", DateTime.Now.ToString("s"));
            tree.Attrs.Add("userId", "00001");
            XmlElementModel model = new XmlElementModel
            {
                Name = "Persons",
                EleList = new List<XmlElementModel>()
            };
            tree.EleList.Add(model);
            var e1 = new XmlElementModel
            {
                Name = "Person",
                Attrs = new Dictionary<string, string>(),
                EleList = new List<XmlElementModel>{
                            new XmlElementModel{Name="Name",Value="张三"},
                            new XmlElementModel{Name="Age",Value="18"}
                          }
            };
            e1.Attrs.Add("id", "1");
            model.EleList.Add(e1);
            var e2 = new XmlElementModel
            {
                Name = "Person",
                Attrs = new Dictionary<string, string>(),
                EleList = new List<XmlElementModel>{
                            new XmlElementModel{Name="Name",Value="张2"},
                            new XmlElementModel{Name="Age",Value="12"}
                          }
            };
            e2.Attrs.Add("id", "2");
            model.EleList.Add(e2);
            var result = xmlHelper.CreatXmlTree(tree);
            return result;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}