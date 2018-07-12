using System.Collections.Generic;

namespace TaiMingAI.Manager.Model
{
    public class NavbarDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentId { get; set; }
        public string title { get; set; }
        private string _icon;
        public string icon
        {
            get { return string.IsNullOrEmpty(_icon) ? "" : _icon; }
            set { _icon = value; }
        }
        private string _href;
        public string href
        {
            get { return string.IsNullOrEmpty(_href) ? "" : _href; }
            set { _href = value; }
        }
        public bool target { get; set; }
        public bool spread { get; set; }
        public int Sort { get; set; }
        public bool IsShow { get; set; }
        public List<NavbarDto> children { get; set; }
    }
}
