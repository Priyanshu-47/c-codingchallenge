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
    internal class HospitalServiceDao : IHospitalService
    {
        public List<Patient> patient;

        public string connectionString;
        SqlCommand cmd = null;

        public HospitalServiceDao()
        {
            patient = new List<Patient>();
            connectionString = DBConnection.GetConnectionString();
            cmd = new SqlCommand();
        }

        public HospitalServiceDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Patient getPatientById(int patientId)
        {
            Patient patient = null;
            try
            {


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    
                    connection.Open();

                    string query = "SELECT * FROM Patients WHERE patient_id = @PatientId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PatientId", patientId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                patient = new Patient
                                {
                                    patientId = Convert.ToInt32(reader["patientId"]),
                                    firstName = reader["firstName"].ToString(),
                                    lastName = reader["lastName"].ToString(),
                                    dateOfBirth = Convert.ToDateTime(reader["dateOfBirth"]),
                                    gender = reader["gender"].ToString(),
                                    contactNumber = reader["contactNumber"].ToString(),
                                    address = reader["address"].ToString(),
                                };
                            }
                            else
                            {

                                throw new PatientNumberNotFoundException($"Patient with ID {patientId} not found.");
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An SQL exception occurred: {ex.Message}");
                throw; // Re-throw the exception to maintain the original exception details
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An exception occurred: {ex.Message}");
                throw;
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

            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                cmd.Connection = sqlconnection;

                sqlconnection.Open();

                string query = "SELECT * FROM Patients";
                using (SqlCommand command = new SqlCommand(query, sqlconnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Patient patient = new Patient
                            {
                                patientId = (int)reader["patient_id"],
                                firstName = reader["first_name"].ToString(),
                                lastName = reader["last_name"].ToString(),
                                dateOfBirth = Convert.ToDateTime(reader["date_of_birth"]),
                                gender = reader["gender"].ToString(),
                                contactNumber = reader["contact_number"].ToString(),
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
                SqlCommand command = new SqlCommand("INSERT INTO Patients (FirstName, LastName, DateOfBirth, Gender, ContactNumber, Address) " +
                                                    "VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @ContactNumber, @Address)", sqlconnection);

                command.Parameters.AddWithValue("@FirstName", patient.firstName);
                command.Parameters.AddWithValue("@LastName", patient.lastName);
                command.Parameters.AddWithValue("@DateOfBirth", patient.dateOfBirth);
                command.Parameters.AddWithValue("@Gender", patient.gender);
                command.Parameters.AddWithValue("@ContactNumber", patient.contactNumber);
                command.Parameters.AddWithValue("@Address", patient.address);

                command.ExecuteNonQuery();
            }
        }

        public void updatePatient(Patient patient)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlCommand command = new SqlCommand("UPDATE Patients SET FirstName = @FirstName, LastName = @LastName, " +
                                                    "DateOfBirth = @DateOfBirth, Gender = @Gender, ContactNumber = @ContactNumber, Address = @Address " +
                                                    "WHERE PatientId = @PatientId", sqlconnection);

                command.Parameters.AddWithValue("@FirstName", patient.firstName);
                command.Parameters.AddWithValue("@LastName", patient.lastName);
                command.Parameters.AddWithValue("@DateOfBirth", patient.dateOfBirth);
                command.Parameters.AddWithValue("@Gender", patient.gender);
                command.Parameters.AddWithValue("@ContactNumber", patient.contactNumber);
                command.Parameters.AddWithValue("@Address", patient.address);
                command.Parameters.AddWithValue("@PatientId", patient.patientId);

                command.ExecuteNonQuery();
            }
        }

        public void deletePatient(int patientId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Patients WHERE PatientId = @PatientId", connection);
                command.Parameters.AddWithValue("@PatientId", patientId);

                command.ExecuteNonQuery();
            }
        }

        // Appointment-related operations

        public Appointment getAppointmentById(int appointmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                connection.Open();

                string query = "SELECT * FROM Appointments WHERE AppointmentId = @AppointmentId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Map data from SqlDataReader to Appointment object
                            Appointment appointment = new Appointment
                            {
                                appointmentId = reader.GetInt32(reader.GetOrdinal("AppointmentId")),
                                patientId = reader.GetInt32(reader.GetOrdinal("PatientId")),
                                doctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                                appointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                                description = reader.GetString(reader.GetOrdinal("Description")),
                                // Map other properties
                            };

                            return appointment;
                        }
                    }
                }
            }

            return null;
        }

        public List<Appointment> getAppointmentsForPatient(int patientId)
        {
            List<Appointment> appointments = new List<Appointment>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                connection.Open();

                string query = "SELECT * FROM Appointments WHERE PatientId = @PatientId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientId", patientId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Map data from SqlDataReader to Appointment object
                            Appointment appointment = new Appointment
                            {
                                appointmentId = reader.GetInt32(reader.GetOrdinal("AppointmentId")),
                                patientId = reader.GetInt32(reader.GetOrdinal("PatientId")),
                                doctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                                appointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                                description = reader.GetString(reader.GetOrdinal("Description")),
                                // Map other properties
                            };

                            appointments.Add(appointment);
                        }
                    }
                }
            }

            return appointments;
        }

        public List<Appointment> getAppointmentsForDoctor(int doctorId)
        {
            List<Appointment> appointments = new List<Appointment>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                connection.Open();

                string query = "SELECT * FROM Appointments WHERE DoctorId = @DoctorId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorId", doctorId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Map data from SqlDataReader to Appointment object
                            Appointment appointment = new Appointment
                            {
                                appointmentId = reader.GetInt32(reader.GetOrdinal("AppointmentId")),
                                patientId = reader.GetInt32(reader.GetOrdinal("PatientId")),
                                doctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                                appointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                                description = reader.GetString(reader.GetOrdinal("Description")),
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

                string query = "INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Description) VALUES (@PatientId, @DoctorId, @AppointmentDate, @Description)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters and set values
                    command.Parameters.AddWithValue("@PatientId", appointment.patientId);
                    command.Parameters.AddWithValue("@DoctorId", appointment.doctorId);
                    command.Parameters.AddWithValue("@AppointmentDate", appointment.appointmentDate);
                    command.Parameters.AddWithValue("@Description", appointment.description);

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

                string query = "UPDATE Appointments SET PatientId = @PatientId, DoctorId = @DoctorId, AppointmentDate = @AppointmentDate, Description = @Description WHERE AppointmentId = @AppointmentId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters and set values
                    command.Parameters.AddWithValue("@AppointmentId", appointment.appointmentId);
                    command.Parameters.AddWithValue("@PatientId", appointment.patientId);
                    command.Parameters.AddWithValue("@DoctorId", appointment.doctorId);
                    command.Parameters.AddWithValue("@AppointmentDate", appointment.appointmentDate);
                    command.Parameters.AddWithValue("@Description", appointment.description);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
        }

        public void cancelAppointment(int appointmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd.Connection = connection;
                connection.Open();

                string query = "DELETE FROM Appointments WHERE AppointmentId = @AppointmentId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters and set values
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);

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

