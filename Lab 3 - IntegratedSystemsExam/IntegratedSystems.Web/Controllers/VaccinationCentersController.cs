using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;
using IntegratedSystems.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using IntegratedSystems.Domain.Domain_Models.Dto;

namespace IntegratedSystems.Web.Controllers
{
    public class VaccinationCentersController : Controller
    {
        private readonly IVaccinationCenterService _vaccinationCenterService;
        private readonly IPatientService _patientService;

        public VaccinationCentersController(IVaccinationCenterService vaccinationCenterService, IPatientService patientService)
        {
            _vaccinationCenterService = vaccinationCenterService;
            _patientService = patientService;
        }

        // GET: VaccinationCenters
        public IActionResult Index()
        {
            return View(_vaccinationCenterService.GetVaccinationCenters());
        }

        // GET: VaccinationCenters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var center = _vaccinationCenterService.GetVaccinationCenterById(id);

            if (center == null)
            {
                return NotFound();
            }

            return View(center);

        }

        // GET: VaccinationCenters/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: VaccinationCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (ModelState.IsValid)
            {
                _vaccinationCenterService.CreateNewVaccinationCenter(vaccinationCenter);
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);

        }

        // GET: VaccinationCenters/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var center = _vaccinationCenterService.GetVaccinationCenterById(id);
            if (center == null)
            {
                return NotFound();
            }
            return View(center);

        }

        // POST: VaccinationCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Guid id, [Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (id != vaccinationCenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _vaccinationCenterService.UpdateVaccinationCenter(vaccinationCenter);
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);

        }

        // GET: VaccinationCenters/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var center = _vaccinationCenterService.GetVaccinationCenterById(id);
            if (center == null)
            {
                return NotFound();
            }

            return View(center);

        }

        // POST: VaccinationCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _vaccinationCenterService.DeleteVaccinationCenter(id);
            return RedirectToAction(nameof(Index));

        }

        private bool VaccinationCenterExists(Guid id)
        {
            return _vaccinationCenterService.GetVaccinationCenterById(id) != null;
        }

        public IActionResult NoMoreCapacity()
        {
            return View();
        }

        public IActionResult AddVaccinatedPatient(Guid id)
        {
            var center = _vaccinationCenterService.GetVaccinationCenterById(id);
            if(center.MaxCapacity <= 0)
            {
                return RedirectToAction(nameof(NoMoreCapacity));
            }

            var allPatients = _patientService.GetPatients();
            var vaccineDto = new VaccineDto
            {
                AllPatients = allPatients,
                VaccinationCenterId = id
            };
            return View(vaccineDto);
        }
        

        [HttpPost,ActionName("AddVaccinatedPatient")]
        [Authorize]
        public IActionResult AddVaccinatedPatient(VaccineDto vaccineDto)
        {
            var result = _vaccinationCenterService.AddVaccinatedPatient(vaccineDto);
            if(result != null)
            {
                return RedirectToAction(nameof(Details), new { id = vaccineDto.VaccinationCenterId });

            }
            return View(vaccineDto);
        }
    }
}
