using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Patient;
using api.Models;

namespace api.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatientSync();
        Task<Patient?> GetPatientByIdSync(int id);
        Task<Patient> AddPatientSync(Patient patient);
        Task<Patient?> UpdatePatientSync(int id, UpdatePatientDTO patientDTO);
        Task<Patient?> DeletePatientSync(int id);
    }
}