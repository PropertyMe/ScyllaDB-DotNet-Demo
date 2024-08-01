using Cassandra.Mapping;
using ScyllaDBDemo.Database;
using ScyllaDBDemo.Models;

namespace ScyllaDBDemo.Services
{
    public class ContactServices(IScyllaDb scyllaDb) : IContactServices
    {
        public Task<List<Contact>> GetContactsAsync()
        {
            var resultList = scyllaDb.Session.Execute("SELECT Id, Username, Email, Address FROM local_dev.contact");
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
            var mapper = new Mapper(scyllaDb.Session);
            return mapper.Single<Contact>($"select * from local_dev.contact where id = :contactId", contactId);
        }
    }
}
