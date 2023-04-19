using System;
using System.Collections.Generic;

namespace EMS.Models;

public partial class VacancyTable
{
    public int VacancyId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public DateTime? VacancyFrom { get; set; }

    public DateTime? VacancyTo { get; set; }

    public virtual DepartmentTable? Department { get; set; }

    public virtual DesignationTable? Designation { get; set; }
}
