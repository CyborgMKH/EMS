using System;
using System.Collections.Generic;

namespace EMS.Models;

public partial class LeaveTable
{
    public int LeaveId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? LeaveDate { get; set; }

    public string? Reason { get; set; }

    public string? LeaveStatus { get; set; }

    public string? Remarks { get; set; }

    public virtual EmployeeTable? Employee { get; set; }
}
