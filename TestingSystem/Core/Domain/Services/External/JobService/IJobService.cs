using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Services.External.JobService
{
    /// <summary>
    /// Interfejs job servisa
    /// </summary>
    public interface IJobService
    {
        /// <summary>
        /// Metod kreira job koji se trenutno izvrsava
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodCall"></param>
        /// <returns></returns>
        Task<string> EnqueueJob<T>(Expression<Action<T>> methodCall);
        /// <summary>
        /// Metod kreira job koji se izvrsava u zakazano vreme
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodCall"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        Task<string> ScheduleJob<T>(Expression<Action<T>> methodCall, TimeSpan delay);
        /// <summary>
        /// Metod kreira job koji se ponavlja svakog meseca
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodCall"></param>
        /// <returns></returns>
        Task CreateMonthlyRecurringJob<T>(Expression<Action<T>> methodCall);

    }
}
