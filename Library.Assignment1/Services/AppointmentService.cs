using System;
using System.ComponentModel;
using Library.Assignment1.DTO;
using Library.Assignment1.Models;

namespace Library.Assignment1.Services
{
    public class AppointmentService
    {
        private List<Appointment?> appointments;
        private AppointmentService()
        {
            appointments = new List<Appointment?>();
        }
        private static AppointmentService? instance;
        public static AppointmentService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppointmentService();
                }
                return instance;
            }
        }
        public List<Appointment?> Appointments
        {
            get
            {
                return appointments;
            }
        }

        public bool ScheduleAppointment(DateTime date, PatientDTO patient, Physician physician)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }
            else if (date.Hour < 8 || date.Hour >= 17)
            {
                return false;
            }
            else if (appointments.Any(a => a != null && a.Physician == physician && a.Date == date))
            {
                return false;
            }

            appointments.Add(new Appointment
            {
                Date = date,
                Patient = patient,
                Physician = physician

            });

            return true;
        }
    }
}
