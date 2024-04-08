using EShop.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface IApartmentService
    {
        public List<Apartment> GetApartments();
        public Apartment GetApartmentById(Guid? id);
        public Apartment CreateNewApartment(Apartment apartment);
        public Apartment UpdateApartment(Apartment apartment);
        public Apartment DeleteApartment(Guid id);

    }
}
