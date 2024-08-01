using Cassandra.Mapping;
using ScyllaDBDemo.Models;
using ScyllaDBDemo.Repositories;

namespace ScyllaDBDemo.Services
{
    public class ContactServices : IContactServices
    {
        private readonly ScyllaConnect _db = new();
        
        public Task<List<Contact>> GetContactsAsync()
        {
            var resultList = _db.Session.Execute("SELECT Id, Username, Email, Address FROM local_dev.contact");
            var contactList = new List<Contact>();
            foreach (var item in resultList)
            {
                contactList.Add(new Contact
                {
                    Id = item.GetValue<Guid>("id"),
                    Username = item.GetValue<string>("username"),
                    Email = item.GetValue<string>("email"),
                    Address = item.GetValue<string>("address"),
                });
            }
            return Task.FromResult(contactList);
        }

        public Contact GetContact(Guid contactId)
        {
            var mapper = new Mapper(_db.Session);
            return mapper.Single<Contact>($"select * from local_dev.contact where id = :contactId", contactId);
        }
    }
}
