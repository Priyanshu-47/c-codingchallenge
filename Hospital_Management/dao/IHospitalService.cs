using Hospital_Management.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management.dao
{
    public interface IHospitalService
    {
        // Patient-related operations
        Patient getPatientById(int patientId);
        List<Patient> getAllPatients();
        void addPatient(Patient patient);
        void updatePatient(Patient patient);
        void deletePatient(int patientId);
        // Appointment-related operations
        Appointment getAppointmentById(int appointmentId);
        List<Appointment> getAppointmentsForPatient(int patientId);
        List<Appointment> getAppointmentsForDoctor(int doctorId);
        void scheduleAppointment(Appointment appointment);
        void updateAppointment(Appointment appointment);
        void cancelAppointment(int appointmentId);
    }
}
