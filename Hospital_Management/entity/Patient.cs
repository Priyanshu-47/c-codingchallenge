using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.entity
{
    public class Patient
    {
        public int patientId { get; set; }
        public string firstName {  get; set; }
        public string lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string gender { get; set; }
        public string contactNumber { get; set; }
        public string address {  get; set; }

        // Default constructor
        public Patient() { }
        // Parameterized constructor
        public Patient(int patientId, string firstName, string lastName, DateTime dateOfBirth, string gender, string contactNumber, string address)
        {
            this.patientId = patientId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.contactNumber = contactNumber;
            this.address = address;
        }

        public override string ToString()
        {
            // Override the ToString method for a custom string representation of the object.
            return $"Patient ID: {patientId}, Name: {firstName} {lastName}, DOB: {dateOfBirth.ToShortDateString()}, Gender: {gender}, Contact: {contactNumber}, Address: {address}";
        }
    }
}
