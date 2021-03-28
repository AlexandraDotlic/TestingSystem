using System.Runtime.Serialization;

namespace Applications.WebClient.DTOs
{
    [DataContract]
    public class AnswerOptionDTO
    {
        [DataMember]
        public string OptionText { get; set; }
        [DataMember]
        public bool IsCorrect { get; set; }
    }
}