using Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entities
{
    public class BookingEntity : ValueObject
    {
        public string Reference { get; set; }
        public long? Amount { get; set; }
        public long? Amount_Received { get; set; }
        public string Country_From { get; set; }
        public string Sender_Full_Name { get; set; }
        public string Sender_Address { get; set; }
        public string School { get; set; }
        public string Currency_From { get; set; }
        public int? Student_Id { get; set; }
        public string Email { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Reference;
            yield return Amount;
            yield return Amount_Received;
            yield return Country_From;
            yield return Sender_Full_Name;
            yield return Sender_Address;
            yield return School;
            yield return Currency_From;
            yield return Student_Id;
            yield return Email;
        }
    }
}
