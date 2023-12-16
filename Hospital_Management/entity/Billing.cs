using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.entity
{
    public class Billing
    {
        private int bill_id { get; set; }
        private int patient_id { get; set; }
        private int doctor_id { get; set; }
        private int appointment_id { get; set; }
        private DateTime bill_date { get; set; }
        private decimal amount { get; set; }
        private string payment_status { get; set; }

        // Default constructor
        public Billing() { }

        // Parameterized constructor
        public Billing(int bill_id, int patient_id, int doctor_id, int appointment_id, DateTime bill_date, decimal amount, string payment_status)
        {
            this.bill_id = bill_id;
            this.patient_id = patient_id;
            this.doctor_id = doctor_id;
            this.appointment_id = appointment_id;
            this.bill_date = bill_date;
            this.amount = amount;
            this.payment_status = payment_status;
        }

        public override string ToString()
        {
            // Override the ToString method for a custom string representation of the object.
            return $"Bill ID: {bill_id}, Patient ID: {patient_id}, Doctor ID: {doctor_id}, Appointment ID: {appointment_id}, Date: {bill_date}, Amount: {amount}, Payment Status: {payment_status}";
        }

    }
}
