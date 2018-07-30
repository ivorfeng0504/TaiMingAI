using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.model
{
    public delegate void CatShoutEventHandle(object sender, CatShoutEventArgs e);
    public class Cat
    {
        private string name;
        public Cat(string name)
        {
            this.name = name;
        }
        public event CatShoutEventHandle CatShout;

        public void Shout()
        {
            Console.WriteLine("喵~，我是{0}！", name);
            if (CatShout != null)
            {
                var e = new CatShoutEventArgs
                {
                    Name = name
                };
                CatShout(this, e);
            }
        }
    }

    public class Mouse
    {
        public Mouse(string name)
        {
            this.name = name;
        }
        public string name;

        public void Run(object sender, CatShoutEventArgs e)
        {
            Console.WriteLine("我是{0},老猫{1}来了，快跑！", name, e.Name);
        }
    }
    public class CatShoutEventArgs : EventArgs
    {
        public string Name { get; set; }
    }
}
