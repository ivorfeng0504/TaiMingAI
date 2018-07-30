using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.model
{
    /// <summary>
    /// 主题
    /// </summary>
    public interface ISubject
    {
        void Request();
    }
    /// <summary>
    /// 真实主题
    /// </summary>
    public class RealSubject : ISubject
    {
        public void Request()
        {
            Console.WriteLine("真实的请求");
        }
    }
    /// <summary>
    /// 代理类
    /// </summary>
    public class Proxy1 : ISubject
    {
        RealSubject real;
        public void Request()
        {
            if (real == null) real = new RealSubject();
            real.Request();
        }
    }

    /// <summary>
    /// 美女
    /// </summary>
    public class Beauty
    {
        public string Name;
        public Beauty(string name)
        {
            Name = name;
        }
    }
    /// <summary>
    /// 撩妹技巧
    /// </summary>
    public interface IChaseGirl
    {
        /// <summary>
        /// 送花
        /// </summary>
        void GiveFlowers();
        /// <summary>
        /// 送巧克力
        /// </summary>
        void GiveChocolate();
    }
    /// <summary>
    /// 追求者
    /// </summary>
    public class Pursuit : IChaseGirl
    {
        private Beauty mm;
        public Pursuit(Beauty mm)
        {
            this.mm = mm;
        }
        public void GiveChocolate()
        {
            Console.WriteLine("送给{0}的巧克力~", mm.Name);
        }
        public void GiveFlowers()
        {
            Console.WriteLine("送给{0}的花~", mm.Name);
        }
    }
    /// <summary>
    /// 代理者
    /// </summary>
    public class Proxy: IChaseGirl
    {
        /// <summary>
        /// 真实追求者
        /// </summary>
        private Pursuit pursuit;
        public Proxy(Beauty mm)
        {
            pursuit = new Pursuit(mm);
        }
        public void GiveChocolate()
        {
            pursuit.GiveChocolate();
        }
        public void GiveFlowers()
        {
            pursuit.GiveFlowers();
        }
    }
}
