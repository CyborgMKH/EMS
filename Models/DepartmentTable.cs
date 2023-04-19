using System;
using System.Collections.Generic;

namespace EMS.Models;

public partial class DepartmentTable
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<EmployeeTable> EmployeeTables { get; } = new List<EmployeeTable>();

    public virtual ICollection<VacancyTable> VacancyTables { get; } = new List<VacancyTable>();
}
