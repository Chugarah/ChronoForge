using Core.Interfaces.Data;
using Domain;
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CustomerRepository(
    DataContext dataContext,
    IEntityFactory<Customers?, CustomersEntity> factory
) : BaseRepository<Customers, CustomersEntity>(dataContext, factory), ICustomerRepository { }
