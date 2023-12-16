using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital_Management.entity;
using Hospital_Management.util;
using System.Data.SqlClient;
using Hospital_Management.myexception;

namespace Hospital_Management.dao
{
    public class HospitalServiceDao : IHospitalService
    {

        public string connectionString;
        SqlCommand cmd = null;

        public HospitalServiceDao()
        {
            connectionString = DBConnection.GetConnectionString();
            cmd = new SqlCommand();
        }
        public Patient getPatientById(int patient_id)
        {
            Patient patient = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Patients WHERE patient_id = @patient_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@patient_id", patient_id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patient = new Patient
                            {
                                patient_id = (int)reader["patient_id"],
                                first_name = reader["first_name"].ToString(),
                                last_name = reader["last_name"].ToString(),
                                date_of_birth = Convert.ToDateTime(reader["date_of_birth"]),
                                gender = reader["gender"].ToString(),
                                contact_number = reader["contact_number"].ToString(),
                                address = reader["address"].ToString()
                            };
                        }
                        else
                        {

                            throw new PatientNumberNotFoundException($"Patient with ID {patient_id} not found.");
                        }
                    }
                }
            }

            return patient;
        }

        //public Patient getPatientById(int patientId)
        //{
        //    Patient patient = null;
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        SqlCommand command = new SqlCommand("SELECT * FROM Patients WHERE PatientId = @PatientId", connection);
        //        command.Parameters.AddWithValue("@PatientId", patientId);

        //        SqlDataReader reader = command.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            // Assuming Patient class has a constructor that takes a SqlDataReader
        //            patient = new Patient
        //            {
        //                patientId = Convert.ToInt32(reader["patientId"]),
        //                firstName = reader["firstName"].ToString(),
        //                lastName = reader["lastName"].ToString(),
        //                dateOfBirth = Convert.ToDateTime(reader["dateOfBirth"]),
        //                gender = reader["gender"].ToString(),
        //                contactNumber = reader["contactNumber"].ToString(),
        //                address = reader["address"].ToString(),
        //            };
        //        }

        //        return patient; // Patient with the specified ID not found
        //    }
        //}

        //    public List<Patient> getAllPatients()
        //{
        //    List<Patient> patients = new List<Patient>();
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        SqlCommand command = new SqlCommand("SELECT * FROM Patients", connection);

        //        SqlDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            patients.Add(new Patient(reader));
        //        }

        //        return patients;
        //    }
        //}
        public List<Patient> getAllPatients()
        {
            List<Patient> patients = new List<Patient>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Patients";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Patient patient = new Patient
                            {
                                patient_id = (int)reader["patient_id"],
                                first_name = reader["first_name"].ToString(),
                                last_name = reader["last_name"].ToString(),
                                date_of_birth = Convert.ToDateTime(reader["date_of_birth"]),
                                gender = reader["gender"].ToString(),
                                contact_number = reader["contact_number"].ToString(),
                                address = reader["address"].ToString()
                            };

                            patients.Add(patient);
                        }
                    }
                }
            }

            return patients;
        }

        public void addPatient(Patient patient)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Patients (first_name, last_name, date_of_birth, gender, contact_number, address) " +
                                                    "VALUES (@first_name, @last_name, @date_of_birth, @gender, @contact_number, @address)", sqlconnection);

                command.Parameters.AddWithValue("@first_name", patient.first_name);
                command.Parameters.AddWithValue("@last_name", patient.last_name);
                command.Parameters.AddWithValue("@date_of_birth", patient.date_of_birth);
                command.Parameters.AddWithValue("@gender", patient.gender);
                command.Parameters.AddWithValue("@contact_number", patient.contact_number);
                command.Parameters.AddWithValue("@address", patient.address);

                command.ExecuteNonQuery();
            }
        }

        public void updatePatient(Patient patient)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlCommand command = new SqlCommand("UPDATE Patients SET first_name = @first_name, last_name = @last_name, " +
                                                    "date_of_birth = @date_of_birth, gender = @gender, contact_number = @contact_number, address = @address " +
                                                    "WHERE patient_id = @patient_id", sqlconnection);

                command.Parameters.AddWithValue("@first_name", patient.first_name);
                command.Parameters.AddWithValue("@last_name", patient.last_name);
                command.Parameters.AddWithValue("@date_of_birth", patient.date_of_birth);
                command.Parameters.AddWithValue("@gender", patient.gender);
                command.Parameters.AddWithValue("@contact_number", patient.contact_number);
                command.Parameters.AddWithValue("@address", patient.address);
                command.Parameters.AddWithValue("@patient_id", patient.patient_id);

                command.ExecuteNonQuery();
            }
        }

        public void deletePatient(int patient_id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Patients WHERE patient_id = @patient_id", connection);
                command.Parameters.AddWithValue("@patient_id", patient_id);

                command.ExecuteNonQuery();
            }
        }

        // Appointment-related operations

        public Appointment getAppointmentById(int appointment_id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                connection.Open();

                string query = "SELECT * FROM Appointments WHERE appointment_id = @appointment_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@appointment_id", appointment_id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Map data from SqlDataReader to Appointment object
                            Appointment appointment = new Appointment
                            {
                                appointment_id = reader.GetInt32(reader.GetOrdinal("appointment_id")),
                                patient_id = reader.GetInt32(reader.GetOrdinal("patient_id")),
                                doctor_id = reader.GetInt32(reader.GetOrdinal("doctor_id")),
                                appointment_date = reader.GetDateTime(reader.GetOrdinal("appointment_date")),
                                description = reader.GetString(reader.GetOrdinal("description")),

                            };

                            return appointment;
                        }
                    }
                }
            }

            return null;
        }

        public List<Appointment> getAppointmentsForPatient(int patient_id)
        {
            List<Appointment> appointments = new List<Appointment>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                connection.Open();

                string query = "SELECT * FROM Appointments WHERE Patient_id = @patient_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@patient_id", patient_id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Map data from SqlDataReader to Appointment object
                            Appointment appointment = new Appointment
                            {
                                appointment_id = reader.GetInt32(reader.GetOrdinal("appointment_id")),
                                patient_id = reader.GetInt32(reader.GetOrdinal("Patient_id")),
                                doctor_id = reader.GetInt32(reader.GetOrdinal("doctor_id")),
                                appointment_date = reader.GetDateTime(reader.GetOrdinal("appointment_date")),
                                description = reader.GetString(reader.GetOrdinal("sescription")),
                                // Map other properties
                            };

                            appointments.Add(appointment);
                        }
                    }
                }
            }

            return appointments;
        }

        public List<Appointment> getAppointmentsForDoctor(int doctor_id)
        {
            List<Appointment> appointments = new List<Appointment>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                connection.Open();

                string query = "SELECT * FROM Appointments WHERE doctor_id = @doctor_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@doctor_id", doctor_id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Map data from SqlDataReader to Appointment object
                            Appointment appointment = new Appointment
                            {
                                appointment_id = reader.GetInt32(reader.GetOrdinal("appointment_id")),
                                patient_id = reader.GetInt32(reader.GetOrdinal("patient_id")),
                                doctor_id = reader.GetInt32(reader.GetOrdinal("doctor_id")),
                                appointment_date = reader.GetDateTime(reader.GetOrdinal("appointment_date")),
                                description = reader.GetString(reader.GetOrdinal("description")),
                                // Map other properties
                            };

                            appointments.Add(appointment);
                        }
                    }
                }
            }

            return appointments;
        }

        public void scheduleAppointment(Appointment appointment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                connection.Open();

                string query = "INSERT INTO Appointments (patient_id, doctor_id, appointment_date, description) VALUES (@patient_id, @doctor_id, @appointment_date, @description)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters and set values
                    command.Parameters.AddWithValue("@patient_id", appointment.patient_id);
                    command.Parameters.AddWithValue("@doctor_id", appointment.doctor_id);
                    command.Parameters.AddWithValue("@appointment_date", appointment.appointment_date);
                    command.Parameters.AddWithValue("@description", appointment.description);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
        }

        public void updateAppointment(Appointment appointment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                connection.Open();

                string query = "UPDATE Appointments SET patient_id = @patient_id, doctor_id = @doctor_id, appointment_date = @appointment_date, description = @description WHERE appointment_id = @appointment_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters and set values
                    command.Parameters.AddWithValue("@appointment_id", appointment.appointment_id);
                    command.Parameters.AddWithValue("@patient_id", appointment.patient_id);
                    command.Parameters.AddWithValue("@doctor_id", appointment.doctor_id);
                    command.Parameters.AddWithValue("@appointment_date", appointment.appointment_date);
                    command.Parameters.AddWithValue("@description", appointment.description);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
        }

        public void cancelAppointment(int appointment_id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                connection.Open();

                string query = "DELETE FROM Appointments WHERE appointment_id = @appointment_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters and set values
                    command.Parameters.AddWithValue("@appointment_id", appointment_id);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }

            //public Appointment getAppointmentById(int appointmentId)
            //{
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();

            //        SqlCommand command = new SqlCommand("SELECT * FROM Appointments WHERE AppointmentId = @AppointmentId", connection);
            //        command.Parameters.AddWithValue("@AppointmentId", appointmentId);

            //        SqlDataReader reader = command.ExecuteReader();

            //        if (reader.Read())
            //        {
            //            // Create a new Appointment object based on the database result
            //            Appointment appointment = new Appointment
            //            {
            //                appointmentId = Convert.ToInt32(reader["AppointmentId"]),
            //                // Map other properties
            //            };

            //            return appointment;
            //        }

            //        return null;
            //    }
            //}

            //public List<Appointment> getAppointmentsForPatient(int patientId)
            //{
            //    List<Appointment> appointments = new List<Appointment>();

            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();

            //        SqlCommand command = new SqlCommand("SELECT * FROM Appointments WHERE PatientId = @PatientId", connection);
            //        command.Parameters.AddWithValue("@PatientId", patientId);

            //        SqlDataReader reader = command.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            // Create a new Appointment object for each row in the result
            //            Appointment appointment = new Appointment
            //            {
            //                appointmentId = Convert.ToInt32(reader["AppointmentId"]),
            //                // Map other properties
            //            };

            //            appointments.Add(appointment);
            //        }
            //    }

            //    return appointments;
            //}

            //public List<Appointment> getAppointmentsForDoctor(int doctorId)
            //{
            //    List<Appointment> appointments = new List<Appointment>();

            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();

            //        SqlCommand command = new SqlCommand("SELECT * FROM Appointments WHERE DoctorId = @DoctorId", connection);
            //        command.Parameters.AddWithValue("@DoctorId", doctorId);

            //        SqlDataReader reader = command.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            // Create a new Appointment object for each row in the result
            //            Appointment appointment = new Appointment
            //            {
            //                appointmentId = Convert.ToInt32(reader["AppointmentId"]),
            //                // Map other properties
            //            };

            //            appointments.Add(appointment);
            //        }
            //    }

            //    return appointments;
            //}

            //public void scheduleAppointment(Appointment appointment)
            //{
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();

            //        SqlCommand command = new SqlCommand("INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Description) VALUES (@PatientId, @DoctorId, @AppointmentDate, @Description)", connection);
            //        command.Parameters.AddWithValue("@PatientId", appointment.patientId);
            //        command.Parameters.AddWithValue("@DoctorId", appointment.doctorId);
            //        command.Parameters.AddWithValue("@AppointmentDate", appointment.appointmentDate);
            //        command.Parameters.AddWithValue("@Description", appointment.description);

            //        command.ExecuteNonQuery();
            //    }
            //}

            //public void updateAppointment(Appointment appointment)
            //{
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();

            //        SqlCommand command = new SqlCommand("UPDATE Appointments SET PatientId = @PatientId, DoctorId = @DoctorId, AppointmentDate = @AppointmentDate, Description = @Description WHERE AppointmentId = @AppointmentId", connection);
            //        command.Parameters.AddWithValue("@AppointmentId", appointment.appointmentId);
            //        command.Parameters.AddWithValue("@PatientId", appointment.patientId);
            //        command.Parameters.AddWithValue("@DoctorId", appointment.doctorId);
            //        command.Parameters.AddWithValue("@AppointmentDate", appointment.appointmentDate);
            //        command.Parameters.AddWithValue("@Description", appointment.description);

            //        command.ExecuteNonQuery();
            //    }
            //}

            //public void cancelAppointment(int appointmentId)
            //{
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();

            //        SqlCommand command = new SqlCommand("DELETE FROM Appointments WHERE AppointmentId = @AppointmentId", connection);
            //        command.Parameters.AddWithValue("@AppointmentId", appointmentId);

            //        command.ExecuteNonQuery();
            //    }
            //}
        }
    }
}

