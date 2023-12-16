using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.entity
{
    public class Appointment
    {
        public int appointmentId { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public DateTime appointmentDate { get; set; }
        public string description { get; set; }

        // Default constructor
        public Appointment() { }

        // Parameterized constructor
        public Appointment(int appointmentId, int patientId, int doctorId, DateTime appointmentDate, string description)
        {
            this.appointmentId = appointmentId;
            this.patientId = patientId;
            this.doctorId = doctorId;
            this.appointmentDate = appointmentDate;
            this.description = description;
        }

        public override string ToString()
        {
            // Override the ToString method for a custom string representation of the object.
            return $"Appointment ID: {appointmentId}, Patient ID: {patientId}, Doctor ID: {doctorId}, Date: {appointmentDate}, Description: {description}";
        }
    }
}
