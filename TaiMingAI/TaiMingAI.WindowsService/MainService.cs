using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace TaiMingAI.WindowsService
{
    public partial class MainService : ServiceBase
    {
        //调度器
        IScheduler scheduler;
        public MainService()
        {
            InitializeComponent();

            //调度器工厂
            ISchedulerFactory factory = new StdSchedulerFactory();
            //创建一个调度器
            scheduler = factory.GetScheduler().Result;
        }

        protected override void OnStart(string[] args)
        {
            scheduler.Start();
        }
        /// <summary>
        /// 服务停止
        /// </summary>
        protected override void OnStop()
        {
            if (!scheduler.IsShutdown)
            {
                scheduler.Shutdown();
            }
        }
        /// <summary>
        /// 暂停
        /// </summary>
        protected override void OnPause()
        {
            scheduler.PauseAll();
            base.OnPause();
        }
        /// <summary>
        /// 继续
        /// </summary>
        protected override void OnContinue()
        {
            scheduler.ResumeAll();
            base.OnContinue();
        }
    }
}
