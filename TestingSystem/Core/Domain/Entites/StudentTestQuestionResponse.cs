using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    /// <summary>
    /// Klasa koja predstavlja odgovor studenta na pitanje na testu 
    /// </summary>
    public class StudentTestQuestionResponse
    {
     
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Odgovor na pitanje na testu
        /// </summary>
        public string Response { get; private set; }
        /// <summary>
        /// Ostvaren rezultat studenta u odnosu na date odgovore na pitanje
        /// </summary>
        public int ResponseScore { get; private set; }
        /// <summary>
        /// Id pitanje na testu na koje student daje odgovor
        /// </summary>
        public int StudentTestQuestionId { get; private set; }
        public StudentTestQuestion StudentTestQuestion { get; private set; }

        public StudentTestQuestionResponse(string response, int responseScore)
        {
            Response = response;
            ResponseScore = responseScore;
        }
    }
}
