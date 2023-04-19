using System;
using System.Collections.Generic;

namespace EMS.Models;

public partial class EmployeeTable
{
    public int EmployeeId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string Address { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public string Email { get; set; } = null!;

    public decimal? Salary { get; set; }

    public virtual DepartmentTable? Department { get; set; }

    public virtual DesignationTable? Designation { get; set; }

    public virtual ICollection<LeaveTable> LeaveTables { get; } = new List<LeaveTable>();
}
