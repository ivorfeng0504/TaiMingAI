using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using TaiMingAI.Tools;

namespace TaiMingAI.WindowsService.Job
{
    public class DemoJob :  IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            LogHelper.InfoLog("hello Quartz!");
            return Task.FromResult(true);
        }
    }
}
