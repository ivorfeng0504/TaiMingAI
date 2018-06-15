using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaiMingAI.Manager.Models
{
    public class NavBar
    {
        public string title { get; set; }
        public string icon { get; set; }
        public string href { get; set; }
        public bool spread { get; set; }
        public List<NavBar> children { get; set; }
    }
}