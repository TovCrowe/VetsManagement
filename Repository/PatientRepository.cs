using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Patient;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;
        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<Patient> AddPatientSync(Patient patient)
        {
            await _context.pacientes.AddAsync(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient?> DeletePatientSync(int id)
        {
            var patient = _context.pacientes.FirstOrDefault(x => x.cliente_id == id);
            if (patient == null)
            {
                return null;
            }
            _context.pacientes.Remove(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<List<Patient>> GetAllPatientSync()
        {
            return await _context.pacientes.ToListAsync();
        }

        public async Task<Patient?> GetPatientByIdSync(int id)
        {
            return await _context.pacientes.FirstOrDefaultAsync(x => x.cliente_id == id);
        }

        public async Task<Patient?> UpdatePatientSync(int id, UpdatePatientDTO patientDTO)
        {
            var patient = _context.pacientes.FirstOrDefault(x => x.cliente_id == id);
            if (patient == null)
            {
                return null;
            }
            patient.nombre = patientDTO.nombre;
            patient.email = patientDTO.email;
            patient.fecha_de_alta = patientDTO.fecha_de_alta;
            patient.sintomas = patientDTO.sintomas;
            await _context.SaveChangesAsync();

            return patient;
        }
    }
}