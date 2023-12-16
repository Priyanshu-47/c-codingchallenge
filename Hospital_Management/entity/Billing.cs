using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.entity
{
    public class Billing
    {
        private int billId { get; set; }
        private int patientId { get; set; }
        private int doctorId { get; set; }
        private int appointmentId { get; set; }
        private DateTime billDate { get; set; }
        private decimal amount { get; set; }
        private string paymentStatus { get; set; }

        // Default constructor
        public Billing() { }

        // Parameterized constructor
        public Billing(int billId, int patientId, int doctorId, int appointmentId, DateTime billDate, decimal amount, string paymentStatus)
        {
            this.billId = billId;
            this.patientId = patientId;
            this.doctorId = doctorId;
            this.appointmentId = appointmentId;
            this.billDate = billDate;
            this.amount = amount;
            this.paymentStatus = paymentStatus;
        }

        public override string ToString()
        {
            // Override the ToString method for a custom string representation of the object.
            return $"Bill ID: {billId}, Patient ID: {patientId}, Doctor ID: {doctorId}, Appointment ID: {appointmentId}, Date: {billDate}, Amount: {amount}, Payment Status: {paymentStatus}";
        }

    }
}
