﻿using Applications.WebClient.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Applications.WebClient.Responses
{
    [DataContract]
    public class GetAllStudentsResponse
    {
        public ICollection<StudentDTO> Students { get; set; }
    }
}
