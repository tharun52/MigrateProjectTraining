using ShoppingApp.Models;
using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Interfaces
{
    public interface IContactUService
    {
        public Task<ContactU> AddContactU(ContactURequestDto Dto);
        public Task<bool> DeleteContactUsById(int id);
    }
}