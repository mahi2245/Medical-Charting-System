using Library.Assignment1.Services;
using Library.Assignment1.Models;
using System;

namespace CLI.Assignment1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Medical Charting System!");

            var patients = PatientService.Current;
            var physicians = PhysicianService.Current;
            var appointments = AppointmentService.Current;
            var cont = true;

            do
            {
                Console.WriteLine("----Menu----");
                Console.WriteLine("1. Add a patient");
                Console.WriteLine("2. List all patients");
                Console.WriteLine("3. Add a physician");
                Console.WriteLine("4. List all physicians");
                Console.WriteLine("5. Schedule an appointment");
                Console.WriteLine("6. List all appointments");
                Console.WriteLine("7. Quit");

                var userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        // add a patient
                        var patient = new Patient();
                        patient.Name = Console.ReadLine();
                        patient.Address = Console.ReadLine();
                        patient.Birthdate = Console.ReadLine();
                        patient.Race = Console.ReadLine();
                        patient.Gender = Console.ReadLine();
                        patients.AddPatient(patient);
                        break;
                    case "2":
                        // list all patients
                        PatientService.Current.Patients.ForEach(Console.WriteLine);
                        break;
                    case "3":
                        // add a physician
                        var physician = new Physician();
                        physician.Name = Console.ReadLine();
                        physician.LicenseNumber = Console.ReadLine();
                        physician.GraduationDate = Console.ReadLine();
                        physician.Specialization = Console.ReadLine();
                        physicians.AddPhysician(physician);
                        break;
                    case "4":
                        // list all physicians
                        PhysicianService.Current.Physicians.ForEach(Console.WriteLine);
                        break;
                    case "5":
                        // schedule an appointment
                        var appointment = new Appointment();
                        var dateInput = Console.ReadLine();
                        DateTime date;
                        if (!DateTime.TryParse(dateInput, out date))
                        {
                            date = DateTime.Today;
                        }
                        string? patientNameInput = Console.ReadLine();
                        var patientName = PatientService.Current.FindPatient(patientNameInput);

                        string? physicianNameInput = Console.ReadLine();
                        var physicianName = PhysicianService.Current.FindPhysician(physicianNameInput);

                        if (patientName == null || physicianName == null)
                        {
                            Console.WriteLine("Patient or physician not found.");
                        }
                        bool scheduled = AppointmentService.Current.ScheduleAppointment(date, patientName, physicianName);
                        if (!scheduled)
                        {
                            Console.WriteLine("Appointment could not be scheduled.");
                        }
                        break;
                    case "6":
                        // list all appointments
                        AppointmentService.Current.Appointments.ForEach(Console.WriteLine);
                        break;
                    case "7":
                        // quit
                        cont = false;
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }

            } while (cont);
        }
    }
}