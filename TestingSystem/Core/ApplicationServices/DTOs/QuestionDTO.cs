using Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ApplicationServices.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }

        public string QuestionText { get; set; }

        public ICollection<AnswerOptionDTO> AnswerOptions { get; set; }

        public short TestId { get; set; }

        public byte QuestionScore { get; set; }

        public QuestionDTO(int id, string text, short testId, byte score, ICollection<AnswerOption> answerOptions)
        {
            Id = id;
            QuestionText = text;
            TestId = testId;
            QuestionScore = score;
            AnswerOptions = answerOptions.Select(ao => new AnswerOptionDTO(ao.Id, ao.OptionText, ao.IsCorrect, ao.QuestionId)).ToList();
        }
    }
}
