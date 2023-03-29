using WebAPI.Context;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class ContactsRepository : IRepository<Contact>
    {
        private ApplicationContext Context;
        public ContactsRepository(ApplicationContext context)
        {
            Context = context;
        }
        public void Create(Contact item)
        {
            item.Id = 0;
            Context.Contacts.Add(item);
            Context.SaveChanges();
        }

        public Contact Delete(int id)
        {
            var contact = Context.Contacts.Find(id);
            if (contact != null)
            {
               Context.Contacts.Remove(contact);
               Context.SaveChanges();
            }

            return contact;
        }

        public IEnumerable<Contact> Get()
        {
            return Context.Contacts;
        }

        public Contact Get(int id)
        {
            return Context.Contacts.Find(id);
        }

        public void Update(Contact updatedItem)
        {
            Contact currentItem = Get(updatedItem.Id);

            var props = (typeof(Contact)).GetProperties();
            foreach (var prop in props)
            {
                prop.SetValue(currentItem, prop.GetValue(updatedItem));
            }

            Context.Contacts.Update(currentItem);
            Context.SaveChanges();
        }
    }
}
