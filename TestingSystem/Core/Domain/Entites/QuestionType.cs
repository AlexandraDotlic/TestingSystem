using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entites
{
    /// <summary>
    /// Klasa koja sluzi za definisanje tipa mogucih odgovora na pitanja
    /// </summary>
    public enum QuestionType: byte
    {
        /// <summary>
        /// Dva ponudjena odgovora, gde je jedan tacan a jedan netacan
        /// </summary>
        YesNo = 1,
        /// <summary>
        /// Tekstualni odgovor na pitanje
        /// </summary>
        FreeText = 2,
        /// <summary>
        /// Vise mogucih odgovora na pitanje sa proizvoljnim brojem mogucih tacnih odgovora
        /// </summary>
        MultipleChoice = 3
    }
}
