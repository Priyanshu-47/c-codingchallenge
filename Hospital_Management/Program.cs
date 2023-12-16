using Hospital_Management.dao;
using Hospital_Management.entity;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string connectionString = "your_connection_string_here";
        IHospitalService hospitalService = new HospitalServiceDao(connectionString);

        while (true)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Get patient by ID");
            Console.WriteLine("2. Get all patients");
            Console.WriteLine("3. Add patient");
            Console.WriteLine("4. Update patient");
            Console.WriteLine("5. Delete patient");
            Console.WriteLine("6. Get appointment by ID");
            Console.WriteLine("7. Get appointments for patient");
            Console.WriteLine("8. Get appointments for doctor");
            Console.WriteLine("9. Schedule appointment");
            Console.WriteLine("10. Update appointment");
            Console.WriteLine("11. Cancel appointment");
            Console.WriteLine("0. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter patient ID: ");
                    if (int.TryParse(Console.ReadLine(), out int patientId))
                    {
                        Patient patient = hospitalService.getPatientById(patientId);
                        PrintPatient(patient);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer for patient ID.");
                    }
                    break;

                case "2":
                    List<Patient> patients = hospitalService.getAllPatients();
                    PrintPatients(patients);
                    break;

                case "3":
                    Patient newPatient = ReadPatientDetailsFromUser();
                    hospitalService.addPatient(newPatient);
                    Console.WriteLine("Patient added successfully.");
                    break;

                case "4":
                    Console.Write("Enter patient ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int updatePatientId))
                    {
                        Patient existingPatient = hospitalService.getPatientById(updatePatientId);
                        if (existingPatient != null)
                        {
                            Patient updatedPatient = ReadPatientDetailsFromUser();
                            updatedPatient.patientId = existingPatient.patientId;
                            hospitalService.updatePatient(updatedPatient);
                            Console.WriteLine("Patient updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Patient with ID {updatePatientId} not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer for patient ID.");
                    }
                    break;

                case "5":
                    Console.Write("Enter patient ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int deletePatientId))
                    {
                        hospitalService.deletePatient(deletePatientId);
                        Console.WriteLine("Patient deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer for patient ID.");
                    }
                    break;

                case "6":
                    Console.Write("Enter appointment ID: ");
                    if (int.TryParse(Console.ReadLine(), out int appointmentId))
                    {
                        Appointment appointment = hospitalService.getAppointmentById(appointmentId);
                        PrintAppointment(appointment);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer for appointment ID.");
                    }
                    break;

                case "7":
                    Console.Write("Enter patient ID: ");
                    if (int.TryParse(Console.ReadLine(), out int appointmentsForPatientId))
                    {
                        List<Appointment> appointmentsForPatient = hospitalService.getAppointmentsForPatient(appointmentsForPatientId);
                        PrintAppointments(appointmentsForPatient);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer for patient ID.");
                    }
                    break;

                case "8":
                    Console.Write("Enter doctor ID: ");
                    if (int.TryParse(Console.ReadLine(), out int appointmentsForDoctorId))
                    {
                        List<Appointment> appointmentsForDoctor = hospitalService.getAppointmentsForDoctor(appointmentsForDoctorId);
                        PrintAppointments(appointmentsForDoctor);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer for doctor ID.");
                    }
                    break;

                case "9":
                    Appointment newAppointment = ReadAppointmentDetailsFromUser();
                    hospitalService.scheduleAppointment(newAppointment);
                    Console.WriteLine("Appointment scheduled successfully.");
                    break;

                case "10":
                    Console.Write("Enter appointment ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int updateAppointmentId))
                    {
                        Appointment existingAppointment = hospitalService.getAppointmentById(updateAppointmentId);
                        if (existingAppointment != null)
                        {
                            Appointment updatedAppointment = ReadAppointmentDetailsFromUser();
                            updatedAppointment.appointmentId = existingAppointment.appointmentId;
                            hospitalService.updateAppointment(updatedAppointment);
                            Console.WriteLine("Appointment updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Appointment with ID {updateAppointmentId} not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer for appointment ID.");
                    }
                    break;

                case "11":
                    Console.Write("Enter appointment ID to cancel: ");
                    if (int.TryParse(Console.ReadLine(), out int cancelAppointmentId))
                    {
                        hospitalService.cancelAppointment(cancelAppointmentId);
                        Console.WriteLine("Appointment canceled successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer for appointment ID.");
                    }
                    break;

                case "0":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static void PrintPatient(Patient patient)
    {
        if (patient != null)
        {
            Console.WriteLine($"Patient ID: {patient.patientId}");
            Console.WriteLine($"First Name: {patient.firstName}");
            Console.WriteLine($"Last Name: {patient.lastName}");
            Console.WriteLine($"Date of Birth: {patient.dateOfBirth.ToShortDateString()}");
            Console.WriteLine($"Gender: {patient.gender}");
            Console.WriteLine($"Contact Number: {patient.contactNumber}");
            Console.WriteLine($"Address: {patient.address}");
        }
        else
        {
            Console.WriteLine("Patient not found.");
        }
    }

    static void PrintPatients(List<Patient> patients)
    {
        if (patients.Count > 0)
        {
            foreach (var patient in patients)
            {
                PrintPatient(patient);
                Console.WriteLine("-----------------------------------------");
            }
        }
        else
        {
            Console.WriteLine("No patients found.");
        }
    }

    static Patient ReadPatientDetailsFromUser()
    {
        Patient patient = new Patient();

        Console.Write("Enter First Name: ");
        patient.firstName = Console.ReadLine();

        Console.Write("Enter Last Name: ");
        patient.lastName = Console.ReadLine();

        Console.Write("Enter Date of Birth (yyyy-MM-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
        {
            patient.dateOfBirth = dateOfBirth;
        }
        else
        {
            Console.WriteLine("Invalid date format. Setting Date of Birth to default.");
        }

        Console.Write("Enter Gender: ");
        patient.gender = Console.ReadLine();

        Console.Write("Enter Contact Number: ");
        patient.contactNumber = Console.ReadLine();

        Console.Write("Enter Address: ");
        patient.address = Console.ReadLine();

        return patient;
    }

    static void PrintAppointment(Appointment appointment)
    {
        if (appointment != null)
        {
            Console.WriteLine($"Appointment ID: {appointment.appointmentId}");
            Console.WriteLine($"Patient ID: {appointment.patientId}");
            Console.WriteLine($"Doctor ID: {appointment.doctorId}");
            Console.WriteLine($"Appointment Date: {appointment.appointmentDate}");
            Console.WriteLine($"Description: {appointment.description}");
        }
        else
        {
            Console.WriteLine("Appointment not found.");
        }
    }

    static void PrintAppointments(List<Appointment> appointments)
    {
        if (appointments.Count > 0)
        {
            foreach (var appointment in appointments)
            {
                PrintAppointment(appointment);
                Console.WriteLine("-----------------------------------------");
            }
        }
        else
        {
            Console.WriteLine("No appointments found.");
        }
    }

    static Appointment ReadAppointmentDetailsFromUser()
    {
        Appointment appointment = new Appointment();

        Console.Write("Enter Patient ID: ");
        if (int.TryParse(Console.ReadLine(), out int patientId))
        {
            appointment.patientId = patientId;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer for Patient ID.");
        }

        Console.Write("Enter Doctor ID: ");
        if (int.TryParse(Console.ReadLine(), out int doctorId))
        {
            appointment.doctorId = doctorId;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer for Doctor ID.");
        }

        Console.Write("Enter Appointment Date (yyyy-MM-dd HH:mm): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime appointmentDate))
        {
            appointment.appointmentDate = appointmentDate;
        }
        else
        {
            Console.WriteLine("Invalid date format. Setting Appointment Date to default.");
        }

        Console.Write("Enter Description: ");
        appointment.description = Console.ReadLine();

        return appointment;
    }
}
