namespace Common.Models.Data
{
    /// <summary>
    /// Product Price with Currency
    /// </summary>
    public class Price
    {
        public Currency Currency { get; set; }

        public decimal Amount { get; set; }
    }
}