using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiMingAI.Tools;

namespace TaiMingAI.WindowsService.Job
{
    public abstract class BaseJob : IJob
    {
        public abstract void ExecuteJob();
        public Task Execute(IJobExecutionContext context)
        {
            ExecuteJob();
            return Task.Delay(-1);
        }
    }
}
