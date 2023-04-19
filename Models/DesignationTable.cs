using System;
using System.Collections.Generic;

namespace EMS.Models;

public partial class DesignationTable
{
    public int DesignationId { get; set; }

    public string DesignationName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<EmployeeTable> EmployeeTables { get; } = new List<EmployeeTable>();

    public virtual ICollection<VacancyTable> VacancyTables { get; } = new List<VacancyTable>();
}
