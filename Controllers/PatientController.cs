using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Patient;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers

{

    [Route("api/pacient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        public PatientController( IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            try
            {
                var patients = await _patientRepository.GetAllPatientSync();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, ex);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            try
            {
                var patient = await _patientRepository.GetPatientByIdSync(id); 
                if(patient == null)
                {
                    return NotFound();
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] CreatePatientDTO patientDTO){
            try{
                var patientModel = patientDTO.ToPatientFromCreateDTO();
                await _patientRepository.AddPatientSync(patientModel);
                return CreatedAtAction(nameof(GetPatient), new { id = patientModel.cliente_id }, patientModel.ToPatientDTO()); 
            } catch (Exception ex){
                return StatusCode(500, ex);
            }   
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePatient([FromRoute] int id, [FromBody] UpdatePatientDTO updatePacientDTO){
            try{
                var patient = await _patientRepository.UpdatePatientSync(id, updatePacientDTO);
                if(patient == null){
                    return NotFound();
                }
                return Ok(patient.ToPatientDTO());

            } catch (Exception ex){
                return StatusCode(500, ex);
            }
        }  
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePatient([FromRoute] int id){
            try{
                var patient = await _patientRepository.DeletePatientSync(id);
                if(patient == null){
                    return NotFound();
                }
                return NoContent(); 
            } catch (Exception ex){
                return StatusCode(500, ex);
            }
        }
    }
}