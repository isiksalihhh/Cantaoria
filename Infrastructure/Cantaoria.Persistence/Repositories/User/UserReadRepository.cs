﻿using Cantaoria.Application.Repositories;
using Cantaoria.Domain.Entities;
using Cantaoria.Persistence.Concrete;

namespace Cantaoria.Persistence.Repositories
{
    public class UserReadRepository : ReadRepository<User>, IUserReadRepository
    {
        public UserReadRepository(CantaoriaDbContext context) : base(context)
        {
        }
       public async Task<User> GetByEmail(string email)
        {
           return await GetSingleAsync(x => x.Email == email);
       }
    }
}
