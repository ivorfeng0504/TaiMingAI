using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using TaiMingAI.Tools;

namespace TaiMingAI.WindowsService
{
    public partial class MainService : ServiceBase
    {
        //调度器
        private IScheduler scheduler;
        public MainService()
        {
            LogHelper.InfoLog("Quartz服务--MainService");

            InitializeComponent();
            RunProgram().GetAwaiter().GetResult();
        }

        protected override void OnStart(string[] args)
        {
            scheduler.Start();
            LogHelper.InfoLog("Quartz服务成功启动");
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
            LogHelper.InfoLog("Quartz服务成功终止");
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

        private async Task RunProgram()
        {
            try
            {
                LogHelper.InfoLog("Quartz服务--RunProgram");
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                scheduler = await factory.GetScheduler();
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }
    }
}
