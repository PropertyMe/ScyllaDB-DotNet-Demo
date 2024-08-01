using ScyllaDBDemo.Models;

namespace ScyllaDBDemo.Services
{
    public interface IContactServices
    {
        Task<List<Contact>> GetContactsAsync();

        Contact GetContact(Guid contactId);
    }
}
