using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Patient;
using api.Models;

namespace api.Mappers
{
    public static class PatientMappers
    {
        public static PatientDTO ToPatientDTO(this Patient patient)
        {
            return new PatientDTO
            {
                cliente_id = patient.cliente_id,
                nombre = patient.nombre,
                email = patient.email,
                fecha_de_alta = patient.fecha_de_alta,
                sintomas = patient.sintomas,

            };
        }
        public static Patient ToPatientFromCreateDTO(this CreatePatientDTO patientDTO)
        {
            return new Patient
            {
                nombre = patientDTO.nombre,
                email = patientDTO.email,
                fecha_de_alta = patientDTO.fecha_de_alta,
                sintomas = patientDTO.sintomas,
            };
        }
    }
}