using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Intex2Backend.Models;

namespace Intex2Backend.Models
{
    public static class OnnxModelHelper
    {
        public static Tensor<float> InputToTensor(FraudDetectionInput input)
        {
            // Create a tensor based on the input data
            var inputTensor = new DenseTensor<float>(new[] { 1, 30 });

            // Set the input values
            inputTensor[0, 0] = input.Time;
            inputTensor[0, 1] = input.Amount;
            inputTensor[0, 2] = input.ProductId;
            inputTensor[0, 3] = input.Qty;
            inputTensor[0, 4] = input.Month;
            inputTensor[0, 5] = input.DayOfWeekMon;
            inputTensor[0, 6] = input.DayOfWeekSat;
            inputTensor[0, 7] = input.DayOfWeekSun;
            inputTensor[0, 8] = input.DayOfWeekThu;
            inputTensor[0, 9] = input.DayOfWeekTue;
            inputTensor[0, 10] = input.DayOfWeekWed;
            inputTensor[0, 11] = input.EntryModePIN;
            inputTensor[0, 12] = input.EntryModeTap;
            inputTensor[0, 13] = input.TypeOfTransactionOnline;
            inputTensor[0, 14] = input.TypeOfTransactionPOS;
            inputTensor[0, 15] = input.CountryOfTransactionIndia;
            inputTensor[0, 16] = input.CountryOfTransactionRussia;
            inputTensor[0, 17] = input.CountryOfTransactionUSA;
            inputTensor[0, 18] = input.CountryOfTransactionUnitedKingdom;
            inputTensor[0, 19] = input.ShippingAddressIndia;
            inputTensor[0, 20] = input.ShippingAddressRussia;
            inputTensor[0, 21] = input.ShippingAddressUSA;
            inputTensor[0, 22] = input.ShippingAddressUnitedKingdom;
            inputTensor[0, 23] = input.BankHSBC;
            inputTensor[0, 24] = input.BankHalifax;
            inputTensor[0, 25] = input.BankLloyds;
            inputTensor[0, 26] = input.BankMetro;
            inputTensor[0, 27] = input.BankMonzo;
            inputTensor[0, 28] = input.BankRBS;
            inputTensor[0, 29] = input.TypeOfCardVisa;

            return inputTensor;
        }
    }
}
