using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.DTOs
{
    public class AnswerOptionDTO
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public AnswerOptionDTO(int id, string optionText, bool isCorrect, int questionId)
        {
            Id = id;
            OptionText = optionText;
            IsCorrect = isCorrect;
            QuestionId = questionId;
        }
    }
}
