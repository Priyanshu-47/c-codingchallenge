using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.entity
{
    public class Appointment
    {
        public int appointment_id { get; set; }
        public int patient_id { get; set; }
        public int doctor_id { get; set; }
        public DateTime appointment_date { get; set; }
        public string description { get; set; }

        // Default constructor
        public Appointment() { }

        // Parameterized constructor
        public Appointment(int appointment_id, int patient_id, int doctor_id, DateTime appointment_date, string description)
        {
            this.appointment_id = appointment_id;
            this.patient_id = patient_id;
            this.doctor_id = doctor_id;
            this.appointment_date = appointment_date;
            this.description = description;
        }

        public override string ToString()
        {
            // Override the ToString method for a custom string representation of the object.
            return $"Appointment ID: {appointment_id}, Patient ID: {patient_id}, Doctor ID: {doctor_id}, Date: {appointment_date}, Description: {description}";
        }
    }
}
