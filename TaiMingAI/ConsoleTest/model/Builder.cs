using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.model
{
    /// <summary>
    /// 产品类
    /// </summary>
    public class Product
    {
        List<string> parts = new List<string>();
        /// <summary>
        /// 添加产品部件
        /// </summary>
        /// <param name="part"></param>
        public void AddPart(string part)
        {
            parts.Add(part);
        }
        /// <summary>
        /// 列举所有产品部件
        /// </summary>
        public void Show()
        {
            Console.WriteLine("产品创建-----------");
            parts.ForEach(x => Console.WriteLine(x));
        }
    }
    /// <summary>
    /// 抽象建造者类
    /// </summary>
    public abstract class Builder
    {
        /// <summary>
        /// 部件A
        /// </summary>
        public abstract void BuildPartA();
        /// <summary>
        /// 部件B
        /// </summary>
        public abstract void BuildPartB();
        /// <summary>
        /// 获取产品
        /// </summary>
        /// <returns>具体产品</returns>
        public abstract Product GetResult();
    }
    public class ConcreteBuilder1 : Builder
    {
        private static Product product = new Product();
        public override void BuildPartA()
        {
            product.AddPart("部件A");
        }

        public override void BuildPartB()
        {
            product.AddPart("部件B");
        }

        public override Product GetResult()
        {
            return product;
        }
    }

    public class ConcreteBuilder2 : Builder
    {
        private Product product = new Product();
        public override void BuildPartA()
        {
            product.AddPart("部件X");
        }

        public override void BuildPartB()
        {
            product.AddPart("部件Y");
        }

        public override Product GetResult()
        {
            return product;
        }
    }
    /// <summary>
    /// 指挥者类
    /// </summary>
    public class Director
    {
        public void Construct(Builder builder)
        {
            builder.BuildPartA();
            builder.BuildPartB();
        }
    }

    /// <summary>
    /// 套餐
    /// </summary>
    public class Meal
    {
        List<string> parts = new List<string>();
        public void AddPart(string part)
        {
            parts.Add(part);
        }

        public void Show()
        {
            Console.WriteLine("您的套餐齐了：");
            parts.ForEach(x => Console.Write(x + "　"));
            Console.WriteLine("\n**************");
        }
    }
    /// <summary>
    /// 创建套餐
    /// </summary>
    public abstract class BuildMeal
    {
        protected Meal meal = new Meal();
        public abstract void BuildePartA();
        public abstract void BuildePartB();
        public abstract void BuildePartC();
        public Meal GetMeal()
        {
            return meal;
        }
    }
    /// <summary>
    /// 经典套餐
    /// </summary>
    public class BuildClassicalMeal : BuildMeal
    {
        public override void BuildePartA()
        {
            meal.AddPart("肉夹馍");
        }

        public override void BuildePartB()
        {
            meal.AddPart("凉皮");
        }

        public override void BuildePartC()
        {
            meal.AddPart("冰峰");
        }
    }
    /// <summary>
    /// 豪华套餐
    /// </summary>
    public class BuildLuxuriousMeal : BuildMeal
    {
        public override void BuildePartA()
        {
            meal.AddPart("羊肉泡馍");
        }

        public override void BuildePartB()
        {
            meal.AddPart("糖蒜");
        }

        public override void BuildePartC()
        {
            meal.AddPart("黑米粥");
        }
    }

    /// <summary>
    /// 厨师
    /// </summary>
    public class Chef
    {
        /// <summary>
        /// 生成套餐的过程
        /// </summary>
        /// <param name="buildMeal"></param>
        public void GiveMeal(BuildMeal buildMeal)
        {
            buildMeal.BuildePartA();
            buildMeal.BuildePartB();
            buildMeal.BuildePartC();
        }
    }
    public delegate void DelBuilder();
}
