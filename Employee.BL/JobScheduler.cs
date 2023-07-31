using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace Employee.BL
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<Jobclass>().Build();

            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("triggerNotiFication", "GroupNotification")
            .StartNow()
            .WithSimpleSchedule(x=>x.WithIntervalInSeconds(5)
            .RepeatForever())
            .Build();
            scheduler.ScheduleJob(job, trigger);
        }
    }
}
