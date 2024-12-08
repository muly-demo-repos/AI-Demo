using Microsoft.AspNetCore.Mvc;
using RealEstateCrm.APIs.Common;
using RealEstateCrm.Infrastructure.Models;

namespace RealEstateCrm.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ClientFindManyArgs : FindManyInput<Client, ClientWhereInput> { }
