using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.Domain_Models.Dto;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class VaccinationCenterService : IVaccinationCenterService
    {
        private readonly IRepository<VaccinationCenter> _vaccinationCenter_repository;
        private readonly IRepository<Patient> _patientRepository;

        public VaccinationCenterService(IRepository<VaccinationCenter> vaccinationCenter_repository, IRepository<Patient> patientRepository)
        {
            _vaccinationCenter_repository = vaccinationCenter_repository;
            _patientRepository = patientRepository;
        }

        public VaccinationCenter AddVaccinatedPatient(VaccineDto vaccineDto)
        {
            var patient = _patientRepository.Get(vaccineDto.PatientId);
            var center = _vaccinationCenter_repository.Get(vaccineDto.VaccinationCenterId);
            center.MaxCapacity = center.MaxCapacity - 1;

            var vaccine = new Vaccine
            {
                Manufacturer = vaccineDto.Manufacturer,
                Certificate = Guid.NewGuid(),
                DateTaken = vaccineDto.DateTaken,
                PatientId = vaccineDto.PatientId,
                PatientFor = patient,
                VaccinationCenter = vaccineDto.VaccinationCenterId,
                Center = center
            };

            patient.VaccinationSchedule.Add(vaccine);
            _patientRepository.Update(patient);

            center.Vaccines.Add(vaccine);
            return _vaccinationCenter_repository.Update(center);
        }

        public VaccinationCenter CreateNewVaccinationCenter(VaccinationCenter vaccinationCenter)
        {
            vaccinationCenter.Vaccines = new List<Vaccine>(vaccinationCenter.MaxCapacity);
            return _vaccinationCenter_repository.Insert(vaccinationCenter);

        }

        public VaccinationCenter DeleteVaccinationCenter(Guid id)
        {
            var center_to_delete = this.GetVaccinationCenterById(id);
            return _vaccinationCenter_repository.Delete(center_to_delete);

        }

        public VaccinationCenter GetVaccinationCenterById(Guid? id)
        {
            return _vaccinationCenter_repository.Get(id);
        }

        public List<VaccinationCenter> GetVaccinationCenters()
        {
            return _vaccinationCenter_repository.GetAll().ToList();
        }

        public VaccinationCenter UpdateVaccinationCenter(VaccinationCenter vaccinationCenter)
        {
            return _vaccinationCenter_repository.Update(vaccinationCenter);
        }
    }
}
