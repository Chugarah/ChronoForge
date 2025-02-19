﻿using Domain;

namespace Core.Interfaces.Data;

public interface ICustomerRepository : IBaseRepository<Customers>
{
    // If we want to override the methods from the BaseRepository
    // using override keyword, remember that the method needs to be virtual in the BaseRepository
}