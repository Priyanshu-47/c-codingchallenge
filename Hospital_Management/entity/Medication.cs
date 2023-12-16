using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.entity
{
    public class Medication
    {
        private int medicationId { get; set; }
        private int appointmentId { get; set; }
        private string medicationName { get; set; }
        private string dosage { get; set; }

        // Default constructor
        public Medication() { }

        // Parameterized constructor
        public Medication(int medicationId, int appointmentId, string medicationName, string dosage)
        {
            this.medicationId = medicationId;
            this.appointmentId = appointmentId;
            this.medicationName = medicationName;
            this.dosage = dosage;
        }

        public override string ToString()
        {
            // Override the ToString method for a custom string representation of the object.
            return $"Medication ID: {medicationId}, Appointment ID: {appointmentId}, Medication Name: {medicationName}, Dosage: {dosage}";
        }
    }
}