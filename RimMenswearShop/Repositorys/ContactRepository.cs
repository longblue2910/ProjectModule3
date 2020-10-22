using RimMenswearShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Repositorys
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext context;

        public ContactRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Contact Create(Contact contact)
        {
            context.Contacts.Add(contact);
            context.SaveChanges();
            return contact;
        }
    }
}
