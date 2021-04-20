using Core.Domain.Services.External.JobService;
using Hangfire;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HangfireJobService
{
    public class HangfireJobService : IJobService
    {
        public async Task CreateRecurringJob<T>(Expression<Action<T>> methodCall, Func<string> cronExpression)
        {
            RecurringJob.AddOrUpdate(methodCall, cronExpression);
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
