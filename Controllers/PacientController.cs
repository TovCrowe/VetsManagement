using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Patient;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers

{

    [Route("api/pacient")]
    [ApiController]
    public class PacientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PacientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatient()
        {
            try
            {
                var patients = await _context.pacientes.ToListAsync();
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
                var patient = await _context.pacientes.FirstOrDefaultAsync(x => x.cliente_id == id);
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
                _context.pacientes.Add(patientModel);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPatient), new { id = patientModel.cliente_id }, patientModel.ToPatientDTO()); 
            } catch (Exception ex){
                return StatusCode(500, ex);
            }   
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePatient([FromRoute] int id, [FromBody] UpdatePacientDTO updatePacientDTO){
            try{
                var patient = await _context.pacientes.FirstOrDefaultAsync(x => x.cliente_id == id);
                if(patient == null){
                    return NotFound();
                }
                patient.nombre = updatePacientDTO.nombre;
                patient.email = updatePacientDTO.email;
                patient.fecha_de_alta = updatePacientDTO.fecha_de_alta;
                patient.sintomas = updatePacientDTO.sintomas;

                await _context.SaveChangesAsync();
                return Ok(patient.ToPatientDTO());

            } catch (Exception ex){
                return StatusCode(500, ex);
            }
        }  
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePatient([FromRoute] int id){
            try{
                var patient = await _context.pacientes.FirstOrDefaultAsync(x => x.cliente_id == id);
                if(patient == null){
                    return NotFound();
                }
                _context.pacientes.Remove(patient);
                await _context.SaveChangesAsync();
                return NoContent(); 
            } catch (Exception ex){
                return StatusCode(500, ex);
            }
        }
    }
}