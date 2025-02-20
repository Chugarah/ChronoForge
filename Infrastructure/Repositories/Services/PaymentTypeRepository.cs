using Core.Interfaces.Data;
using Domain;
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.Services;

/// <summary>
/// PaymentTypeRepository
/// using BaseRepository and IEntityFactory to convert from domain
/// to entity and vice versa
/// </summary>
/// <param name="dataContext"></param>
/// <param name="factory"></param>
public class PaymentTypeRepository(
    DataContext dataContext,
    IEntityFactory<PaymentType?, PaymentTypeEntity> factory
) : BaseRepository<PaymentType, PaymentTypeEntity>(dataContext, factory), IPaymentTypeRepository { }
