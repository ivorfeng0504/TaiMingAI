using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace TaiMingAI.WebApi.Models
{
    [XmlType("Data")]
    public class Data
    {
        [XmlAttribute("userId")]
        public int UserId { get; set; }
        [XmlAttribute("addTime")]
        public DateTime AddTime { get; set; }
        [XmlArray("Persons")]
        public Person[] PersonList { get; set; }
    }
    [XmlType("Person")]
    public class Person
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public int Age { get; set; }
    }
}