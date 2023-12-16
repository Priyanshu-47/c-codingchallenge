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
        Patient getPatientById(int patient_id);
        List<Patient> getAllPatients();
        void addPatient(Patient patient);
        void updatePatient(Patient patient);
        void deletePatient(int patient_id);
        // Appointment-related operations
        Appointment getAppointmentById(int appointment_id);
        List<Appointment> getAppointmentsForPatient(int patient_id);
        List<Appointment> getAppointmentsForDoctor(int doctor_id);
        void scheduleAppointment(Appointment appointment);
        void updateAppointment(Appointment appointment);
        void cancelAppointment(int appointment_id);
    }
}
