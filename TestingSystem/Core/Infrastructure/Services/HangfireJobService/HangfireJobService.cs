using Core.Domain.Services.External.JobService;
using Hangfire;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Infrastructure.Services.HangfireJobService
{
    public class HangfireJobService : IJobService
    {
        public async Task CreateMonthlyRecurringJob<T>(Expression<Action<T>> methodCall)
        {
            RecurringJob.AddOrUpdate(methodCall, Cron.Monthly);
        }

        public async Task<string> EnqueueJob<T>(Expression<Action<T>> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }

        public async Task<string> ScheduleJob<T>(Expression<Action<T>> methodCall, TimeSpan delay)
        {
            return BackgroundJob.Schedule(methodCall, delay);
        }
    }
}
