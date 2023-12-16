using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.entity
{
    public class LabTest
    {
        private int test_id { get; set; }
        private int appointment_id { get; set; }
        private string test_name { get; set; }
        private string test_result { get; set; }

        // Default constructor
        public LabTest() { }

        // Parameterized constructor
        public LabTest(int test_id, int appointment_id, string test_name, string test_result)
        {
            this.test_id = test_id;
            this.appointment_id = appointment_id;
            this.test_name = test_name;
            this.test_result = test_result;
        }

        public override string ToString()
        {
            // Override the ToString method for a custom string representation of the object.
            return $"Test ID: {test_id}, Appointment ID: {appointment_id}, Test Name: {test_name}, Test Result: {test_result}";
        }
    }
}