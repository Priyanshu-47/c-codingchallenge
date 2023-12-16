using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.entity
{
    public class Medication
    {
        private int medication_id { get; set; }
        private int appointment_id { get; set; }
        private string medication_name { get; set; }
        private string dosage { get; set; }

        // Default constructor
        public Medication() { }

        // Parameterized constructor
        public Medication(int medication_id, int appointment_id, string medication_name, string dosage)
        {
            this.medication_id = medication_id;
            this.appointment_id = appointment_id;
            this.medication_name = medication_name;
            this.dosage = dosage;
        }

        public override string ToString()
        {
            // Override the ToString method for a custom string representation of the object.
            return $"Medication ID: {medication_id}, Appointment ID: {appointment_id}, Medication Name: {medication_name}, Dosage: {dosage}";
        }
    }
}