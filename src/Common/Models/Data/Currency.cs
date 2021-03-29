namespace Common.Models.Data
{
    /// <summary>
    /// Currency with code
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// DigitCode
        /// </summary>
        /// <example>840</example>
        public string Number { get; set; }
        
        /// <summary>
        /// Code
        /// </summary>
        /// <example>USD</example>
        public string Code { get; set; }
    }
}