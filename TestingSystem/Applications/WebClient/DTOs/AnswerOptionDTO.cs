using System.Runtime.Serialization;

namespace Applications.WebClient.DTOs
{
    [DataContract]
    public class AnswerOptionDTO
    {
        public AnswerOptionDTO(string optionText, bool isCorrect)
        {
            OptionText = optionText;
            IsCorrect = isCorrect;
        }
        public AnswerOptionDTO()
        {

        }

        [DataMember]
        public string OptionText { get; set; }
        [DataMember]
        public bool IsCorrect { get; set; }
    }
}