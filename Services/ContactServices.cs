using ScyllaDBDemo.Models;
using ScyllaDBDemo.Repositories;

namespace ScyllaDBDemo.Services
{
    public class ContactServices : IContactServices
    {
        public Task<List<Contact>> GetContactsAsync()
        {
            var scyllaSession = new ScyllaConnect().GetSession();
            var resultList = scyllaSession.Execute("SELECT Id, Username, Email, Address FROM contact");
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
    }
}
