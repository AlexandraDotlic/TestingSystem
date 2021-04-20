using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Services.External.JobService
{
    public interface IJobService
    {
        Task<string> EnqueueJob<T>(Expression<Action<T>> methodCall);
        Task<string> ScheduleJob<T>(Expression<Action<T>> methodCall, TimeSpan delay);
        Task CreateRecurringJob<T>(Expression<Action<T>> methodCall, Func<string> cronExpression);

    }
}
