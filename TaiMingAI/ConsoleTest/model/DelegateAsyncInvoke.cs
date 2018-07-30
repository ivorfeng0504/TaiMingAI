using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest.model
{
    /*
     * 委托的异步调用
     * 场景：
     */
    public class Heater
    {
        /// <summary>
        /// 烧水是否完成
        /// </summary>
        public bool IsDone { get; private set; } = false;
        private int nowTemp = 28;
        /// <summary>
        /// 设置的水温
        /// </summary>
        private int setTemp;
        public Heater(int setTemp)
        {
            this.setTemp = setTemp;
            Console.WriteLine("准备烧水");
        }
        public void HeatWater()
        {
            while (nowTemp < setTemp)
            {
                Thread.Sleep(500);
                nowTemp++;
                Console.WriteLine("烧水中（{0}°C）...", nowTemp);
            }
            IsDone = true;
        }
    }
}
