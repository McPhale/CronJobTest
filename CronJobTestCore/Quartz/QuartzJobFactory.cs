﻿using Quartz;
using Quartz.Spi;
using System;

public class QuartzJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public QuartzJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        var jobDetail = bundle.JobDetail;

        var job = (IJob)_serviceProvider.GetService(jobDetail.JobType);
        return job;
    }

    public void ReturnJob(IJob job)
    {
        var disposable = job as IDisposable;
        if (disposable != null)
        {
            disposable.Dispose();
        }
    }
}
