using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.entity
{
    public class LabTest
    {
        private int testId { get; set; }
        private int appointmentId { get; set; }
        private string testName { get; set; }
        private string testResult { get; set; }

        // Default constructor
        public LabTest() { }

        // Parameterized constructor
        public LabTest(int testId, int appointmentId, string testName, string testResult)
        {
            this.testId = testId;
            this.appointmentId = appointmentId;
            this.testName = testName;
            this.testResult = testResult;
        }

        public override string ToString()
        {
            // Override the ToString method for a custom string representation of the object.
            return $"Test ID: {testId}, Appointment ID: {appointmentId}, Test Name: {testName}, Test Result: {testResult}";
        }
    }
}