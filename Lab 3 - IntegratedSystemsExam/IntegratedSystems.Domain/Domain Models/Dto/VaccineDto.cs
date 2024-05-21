using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.Domain_Models.Dto
{
    public class VaccineDto
    {
        public List<Patient>? AllPatients { get; set; }
        public string? Manufacturer { get; set; }
        public Guid PatientId { get; set; }
        public DateTime DateTaken { get; set; }
        public Guid VaccinationCenterId { get; set; }
    }
}
