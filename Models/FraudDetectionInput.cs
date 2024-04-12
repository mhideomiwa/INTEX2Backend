namespace Intex2Backend.Models
{
    public class FraudDetectionInput
    {
        public float Time { get; set; }
        public float Amount { get; set; }
        public float ProductId { get; set; }
        public float Qty { get; set; }
        public float Month { get; set; }
        public float DayOfWeekMon { get; set; }
        public float DayOfWeekSat { get; set; }
        public float DayOfWeekSun { get; set; }
        public float DayOfWeekThu { get; set; }
        public float DayOfWeekTue { get; set; }
        public float DayOfWeekWed { get; set; }
        public float EntryModePIN { get; set; }
        public float EntryModeTap { get; set; }
        public float TypeOfTransactionOnline { get; set; }
        public float TypeOfTransactionPOS { get; set; }
        public float CountryOfTransactionIndia { get; set; }
        public float CountryOfTransactionRussia { get; set; }
        public float CountryOfTransactionUSA { get; set; }
        public float CountryOfTransactionUnitedKingdom { get; set; }
        public float ShippingAddressIndia { get; set; }
        public float ShippingAddressRussia { get; set; }
        public float ShippingAddressUSA { get; set; }
        public float ShippingAddressUnitedKingdom { get; set; }
        public float BankHSBC { get; set; }
        public float BankHalifax { get; set; }
        public float BankLloyds { get; set; }
        public float BankMetro { get; set; }
        public float BankMonzo { get; set; }
        public float BankRBS { get; set; }
        public float TypeOfCardVisa { get; set; }
    }
}
