using System;
using System.ComponentModel;
using Library.Assignment1.Models;

namespace Library.Assignment1.Services
{
    public class PhysicianService
    {
        private List<Physician?> physicians;
        private PhysicianService()
        {
            physicians = new List<Physician?>();
        }
        private static PhysicianService? instance;
        public static PhysicianService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new PhysicianService();
                }
                return instance;
            }
        }
        public List<Physician?> Physicians
        {
            get
            {
                return physicians;
            }
        }

        public void AddPhysician(Physician? physician)
        {
            physicians.Add(physician);
        }

        public Physician? FindPhysician(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            return physicians.FirstOrDefault(a => a != null && a.Name == name);
        }
    }
}