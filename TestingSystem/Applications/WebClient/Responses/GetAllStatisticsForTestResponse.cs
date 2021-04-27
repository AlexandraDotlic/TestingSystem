using Applications.WebClient.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Responses
{
    public class GetAllStatisticsForTestResponse
    {
        public ICollection<TestStatisticDTO> TestStatistics { get; set; }
    }
}
