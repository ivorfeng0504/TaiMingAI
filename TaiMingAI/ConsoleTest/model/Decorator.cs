using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.model
{
    /// <summary>
    /// 元件类
    /// 被装饰的抽象对象
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// 对象的抽象操作
        /// </summary>
        public abstract void Operation();
    }

    /// <summary>
    /// 具体元件
    /// </summary>
    public class ConcreteComponent : Component
    {
        /// <summary>
        /// 对象的具体操作
        /// </summary>
        public override void Operation()
        {
            Console.WriteLine("元件具体操作！");
        }
    }

    /// <summary>
    /// 装饰类
    /// 装饰操作的抽象类
    /// </summary>
    public abstract class Decorator : Component
    {
        /// <summary>
        /// 被装饰的元件
        /// </summary>
        protected Component component;

        /// <summary>
        /// 设置元件
        /// </summary>
        /// <param name="component">被装饰的对象</param>
        public void SetComponent(Component component)
        {
            this.component = component;
        }

        /// <summary>
        /// 装饰操作
        /// 重新Operation(),实际执行的是Component的Operation()
        /// </summary>
        public override void Operation()
        {
            if (component != null)
            {
                component.Operation();
            }
        }
    }

    /// <summary>
    /// 具体装饰类A
    /// </summary>
    public class ConcreteDecoratorA : Decorator
    {
        /// <summary>
        /// 装饰A独有的属性
        /// 区别其他装饰类
        /// </summary>
        private string addedState;
        /// <summary>
        /// 装饰类B的操作
        /// </summary>
        public override void Operation()
        {
            base.Operation();
            addedState = "我是装饰A";
            Console.WriteLine(addedState);
        }
    }

    /// <summary>
    /// 具体装饰类B
    /// </summary>
    public class ConcreteDecoratorB : Decorator
    {
        /// <summary>
        /// 装饰类B的独有操作
        /// 区别其他装饰类
        /// </summary>
        private void AddedBehavior()
        {
            Console.WriteLine("装饰类B的独有操作");
        }
        /// <summary>
        /// 装饰类B的操作
        /// </summary>
        public override void Operation()
        {
            base.Operation();
            AddedBehavior();
            Console.WriteLine("装饰类B的操作");
        }
    }

    /// <summary>
    /// 英雄抽象类
    /// </summary>

    public abstract class Hero
    {
        public string HeroName;
        public abstract void LearnSkill();
    }
    /// <summary>
    /// 具体英雄
    /// 剑圣
    /// </summary>
    public class JUGG : Hero
    {
        public JUGG(string heroName)
        {
            HeroName = heroName;
        }
        public override void LearnSkill()
        {
            Console.WriteLine(HeroName + "学习了以上技能");
        }
    }

    /// <summary>
    /// 技能栏，继续学技能
    /// </summary>
    public abstract class SkillDecorator : Hero
    {
        private Hero hero;
        public string skillName;
        public SkillDecorator(Hero hero, string skillName)
        {
            this.hero = hero;
            this.skillName = skillName;
        }
        public override void LearnSkill()
        {
            if (hero != null)
            {
                hero.LearnSkill();
            }
        }
    }
    /// <summary>
    /// 具体的技能Q
    /// </summary>
    public class QSkill : SkillDecorator
    {
        public QSkill(Hero hero, string skillName) : base(hero, skillName) { }
        public override void LearnSkill()
        {
            LearnQSkill();
            base.LearnSkill();
        }
        /// <summary>
        /// Q 技能 特性
        /// </summary>
        public void LearnQSkill()
        {
            Console.WriteLine("学习了{0}技能！", skillName);
        }
    }

    /// <summary>
    /// 具体的技能W
    /// </summary>
    public class WSkill : SkillDecorator
    {
        public WSkill(Hero hero, string skillName) : base(hero, skillName) { }
        public override void LearnSkill()
        {
            LearnWSkill();
            base.LearnSkill();
        }
        /// <summary>
        /// W 技能 特性
        /// </summary>
        public void LearnWSkill()
        {
            Console.WriteLine("学习了{0}技能！", skillName);
        }
    }
}
