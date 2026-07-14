using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Services
{
    public class DoctorDomainService
    {
        private readonly ILogger<DoctorDomainService> _logger;
        private readonly IDoctorRepository _doctorRepository;
        public DoctorDomainService(ILogger<DoctorDomainService> logger, IDoctorRepository doctorRepository)
        {
            _logger = logger;
            _doctorRepository = doctorRepository;
        }
        public async Task<Doctor> RegistrarDoctor(string nombre, string especialidad)
        {
            var doctor = new Doctor(nombre, especialidad);
            await _doctorRepository.RegistrarDoctor(doctor);
            await _doctorRepository.GuardarCambios();
            _logger.LogInformation("Doctor registrado: {DoctorNombre} - Especialidad: {DoctorEspecialidad}", doctor.Nombre, doctor.Especialidad);
            return doctor;
        }
        public async Task<Doctor> ActualizarDoctor(Guid doctorId, string nombre, string especialidad)
        {
            var doctor = await ObtenerDoctor(doctorId);
            doctor.Actualizar(nombre, especialidad);
            await _doctorRepository.GuardarCambios();
            _logger.LogInformation("Doctor actualizado: {DoctorNombre} - Especialidad: {DoctorEspecialidad}", doctor.Nombre, doctor.Especialidad);
            return doctor;
        }
        public async Task<Doctor> ObtenerDoctor(Guid doctorId)
        {
            var doctor = await _doctorRepository.ConsultarDoctor(doctorId) ?? throw new Exception($"No se encontró el doctor con id {doctorId}.");
            _logger.LogInformation("Doctor obtenido: {DoctorNombre} - Especialidad: {DoctorEspecialidad}", doctor.Nombre, doctor.Especialidad);
            return doctor;
        }

        public IQueryable<Doctor> ConsultarDoctores()
        {
            var doctores = _doctorRepository.ConsultarDoctor();
            _logger.LogInformation($"Doctores consultados");
            return doctores;
        }
    }
}
