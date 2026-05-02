using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs
{
    public class ClientDto
    {
        public Guid Id { get; set; }
        public string ClientCode { get; set; }
        public string CompanyName { get; set; }
        public string? CompanyLogo { get; set; }
        public string ClientName { get; set; }
        public string Domain { get; set; }
        public string? ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(15);
        public string? Address { get; set; }
    }
}
