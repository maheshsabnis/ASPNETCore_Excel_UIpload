using System;
using System.Collections.Generic;

namespace MVC_Read_Excel.Models
{
    public partial class CustomerResponseDetail
    {
        public int ResponseId { get; set; }
        public string ServiceEngineerName { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ComplaintType { get; set; } = null!;
        public string DeviceName { get; set; } = null!;
        public DateTime ComplaintDate { get; set; }
        public DateTime VisitDate { get; set; }
        public string? ComplaintDetails { get; set; }
        public string? RepairDetails { get; set; }
        public DateTime ResolveDate { get; set; }
        public decimal Fees { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
