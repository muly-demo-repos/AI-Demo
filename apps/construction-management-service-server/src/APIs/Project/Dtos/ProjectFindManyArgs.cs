using ConstructionManagementService.APIs.Common;
using ConstructionManagementService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagementService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ProjectFindManyArgs : FindManyInput<Project, ProjectWhereInput> { }
