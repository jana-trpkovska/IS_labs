using EShop.Domain.Domain;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class ApartmentService : IApartmentService
    {
        private readonly IRepository<Apartment> _repository;

        public ApartmentService(IRepository<Apartment> repository)
        {
            _repository = repository;
        }

        public Apartment CreateNewApartment(Apartment apartment)
        {
            return _repository.Insert(apartment);
        }

        public Apartment DeleteApartment(Guid id)
        {
            return _repository.Delete(GetApartmentById(id));
        }

        public Apartment GetApartmentById(Guid? id)
        {
            return _repository.Get(id);
        }

        public List<Apartment> GetApartments()
        {
            return _repository.GetAll().ToList();
        }

        public Apartment UpdateApartment(Apartment apartment)
        {
            return _repository.Update(apartment);
        }
    }
}
