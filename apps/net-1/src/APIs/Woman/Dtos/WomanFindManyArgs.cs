using Microsoft.AspNetCore.Mvc;
using Net1.APIs.Common;
using Net1.Infrastructure.Models;

namespace Net1.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class WomanFindManyArgs : FindManyInput<Woman, WomanWhereInput> { }
