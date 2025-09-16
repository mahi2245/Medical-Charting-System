using System;
using System.ComponentModel;
using Library.Assignment1.Models;

namespace Library.Assignment1.Services
{
    public class PatientService
    {
        private List<Patient?> patients;
        private PatientService()
        {
            patients = new List<Patient?>();
        }
        private static PatientService? instance;
        public static PatientService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientService();
                }
                return instance;
            }
        }
        public List<Patient?> Patients
        {
            get
            {
                return patients;
            }
        }

        public void AddPatient(Patient? patient)
        {
            patients.Add(patient);
        }

        public Patient? FindPatient(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            return patients.FirstOrDefault(a => a != null && a.Name == name);
        }
    }
}