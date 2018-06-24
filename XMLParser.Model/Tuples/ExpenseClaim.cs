using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using XMLParser.Model.Constants;

namespace XMLParser.Model.Tuples {
    public class ExpenseClaim : IValidatableObject{
        [XmlElement("cost_centre")]
        public CostCentre CostCentre { get; set; }

        [XmlElement("total")]
        public decimal? Total { get; set; }

        [XmlElement("payment_method")]
        public PaymentMethod PaymentMethod { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (Total == null) {
                yield return new ValidationResult($"'{nameof(Total)}' is missing.");
            }
            if (PaymentMethod.Equals(PaymentMethod.Unknown)) {
                yield return new ValidationResult($"'{nameof(PaymentMethod)}' is missing.");
            }
        }
    }
}