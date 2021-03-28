﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    public class AnswerOption
    {
        public byte Id { get; private set; }
        public string OptionText { get; private set; }
        public bool IsCorrect { get; private set; }
        public int QuestionId { get; private set; }
        public Question Question { get; private set; }

        public AnswerOption(string optionText, bool isCorrect)
        {
            OptionText = optionText;
            IsCorrect = isCorrect;
        }
    }
}
