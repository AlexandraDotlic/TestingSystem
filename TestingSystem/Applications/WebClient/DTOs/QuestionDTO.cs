using Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Applications.WebClient.DTOs
{
    [DataContract]
    public class QuestionDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string QuestionText { get; set; }
        [DataMember]
        public ICollection<AnswerOptionDTO> AnswerOptions { get; set; }


        public byte QuestionScore { get; set; }
        public QuestionDTO()
        {

        }
        public QuestionDTO(int id, string text, byte score, ICollection<Core.ApplicationServices.DTOs.AnswerOptionDTO> answerOptions)
        {
            Id = id;
            QuestionText = text;
            QuestionScore = score;
            AnswerOptions = answerOptions.Select(ao => new AnswerOptionDTO(ao.OptionText, ao.IsCorrect)).ToList();
        }
    }
}
