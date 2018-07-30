using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.model
{
    /// <summary>
    /// 策略类
    /// </summary>
    public abstract class Strategy
    {
        /// <summary>
        /// 算法接口
        /// </summary>
        public abstract void AlgorithmInterface();
    }

    public class ConcreteStrategyA : Strategy
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine("我是策略算法A");
        }
    }

    public class ConcreteStrategyB : Strategy
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine("我是策略算法B");
        }
    }

    public class ConcreteStrategyC : Strategy
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine("我是策略算法C");
        }
    }
    /// <summary>
    /// 上下文
    /// </summary>
    public class Context
    {
        private Strategy strategy;

        public Context(Strategy strategy)
        {
            this.strategy = strategy;
        }
        /// <summary>
        /// 上下文接口
        /// </summary>
        public void ContextInterface()
        {
            strategy.AlgorithmInterface();
        }
    }
    /// <summary>
    /// 撩妹策略接口
    /// </summary>
    public interface IChaseGirlStrategy
    {
        void ChaseGirl();
    }
    /// <summary>
    /// 相见时的策略
    /// </summary>
    public class XiangjianStrategy : IChaseGirlStrategy
    {
        public void ChaseGirl()
        {
            Console.WriteLine("1.了解女孩儿的爱好");
            Console.WriteLine("2.多和女孩聊天");
        }
    }
    /// <summary>
    /// 相识时的策略
    /// </summary>
    public class XiangshiStrategy : IChaseGirlStrategy
    {
        public void ChaseGirl()
        {
            Console.WriteLine("1.约女孩儿看电影");
            Console.WriteLine("2.约女孩儿吃饭");
        }
    }
    /// <summary>
    /// 相知时的策略
    /// </summary>
    public class XiangzhiStrategy : IChaseGirlStrategy
    {
        public void ChaseGirl()
        {
            Console.WriteLine("1.多送小礼物");
            Console.WriteLine("2.相约出去旅游");
        }
    }
    /// <summary>
    /// 智囊
    /// </summary>
    public class BigThink
    {
        private IChaseGirlStrategy strategy;
        /// <summary>
        /// 策略模式和工厂模式结合
        /// </summary>
        /// <param name="type"></param>
        public BigThink(string type)
        {
            switch (type)
            {
                case "1": strategy = new XiangjianStrategy(); break;
                case "2": strategy = new XiangshiStrategy(); break;
                case "3": strategy = new XiangzhiStrategy(); break;
            }
        }
        /// <summary>
        /// 获取策略
        /// </summary>
        public void GetStrategy()
        {
            strategy.ChaseGirl();
        }
    }
}
