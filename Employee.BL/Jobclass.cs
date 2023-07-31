using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Employee.BL
{
    public class Jobclass : IJob
    {
        Task IJob.Execute(IJobExecutionContext context) 
        {
            return Task.Run(() =>
            {
                WriteLogFile.WriteLog();    
            }); 
        }
    }
}
