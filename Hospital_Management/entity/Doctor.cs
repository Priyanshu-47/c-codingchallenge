using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.entity
{
    public class Doctor
    {
        private int doctorId { get; set; }

        private string firstName { get; set; }
        private string lastName { get; set; }
        private string specialization {  get; set; }
        private string contactNumber { get; set; }

        // Default constructor
        public Doctor() { }

        // Parameterized constructor
        public Doctor(int doctorId, string firstName, string lastName, string specialization, string contactNumber)
        {
            this.doctorId = doctorId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.specialization = specialization;
            this.contactNumber = contactNumber;
        }
        public override string ToString()
        {
            // Override the ToString method for a custom string representation of the object.
            return $"Doctor ID: {doctorId}, Name: {firstName} {lastName}, Specialization: {specialization}, Contact: {contactNumber}";
        }
    }
}
