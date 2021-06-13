using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public enum QuestionType: byte
    {

        YesNo = 1,
        FreeText = 2,
        MultipleChoice = 3
    }
}
