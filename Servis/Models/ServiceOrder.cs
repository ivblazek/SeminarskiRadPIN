using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Servis.Models
{
    public class ServiceOrder
    {
        [Display(Name = "Broj")]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Zaprimljeno")]
        [Required]
        public DateTime DateReceived { get; set; }

        [Display(Name = "Zadnja promjena")]
        [Required]
        public DateTime DateLastChange { get; set; }

        [Display(Name = "Kupac")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Potrebno odabrati kupca")]
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual AppUser Customer { get; set; }

        [Display(Name = "Predmet")]
        [Required(ErrorMessage = "Potrebno je unjeti predmet servisa")]
        public string Item { get; set; }

        [Display(Name = "Model")]
        [Required(ErrorMessage = "Potrebno je unjeti model")]
        public string Model { get; set; }

        [Display(Name = "Serijski broj")]
        [Required(ErrorMessage = "Potrebno je unjeti serijski broj")]
        public string SerialNumber { get; set; }

        [Display(Name = "Opis kvara")]
        [Required(ErrorMessage = "Potrebno je navesti kvar")]
        public string FaultDesc { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int Status { get; set; }

        [Display(Name = "Bilješke")]
        public string Notes { get; set; }

        [Display(Name = "Opis servisa")]
        public string RepairDesc { get; set; }

        public string StatusDescription
        {
            get
            {
                switch (this.Status)
                {
                    case 0:
                        return "U obradi";
                    case 1:
                        return "Čeka dijelove";
                    case 2:
                        return "Čeka preuzimanje";
                    case 3:
                        return "Preuzet";
                    default:
                        return "Greška";
                }
            }
        }

        public string GetColor
        {
            get
            {
                switch (this.Status)
                {
                    case 0:
                        return "primary";
                    case 1:
                        return "danger";
                    case 2:
                        return "warning";
                    case 3:
                        return "success";
                    default:
                        return "danger";
                }
            }
        }

        public string GetReceivedDate
        {
            get
            {
                return this.DateReceived.ToString("dd.MM.yyyy. HH:mm");
            }
        }

        public string GetChangedDate
        {
            get
            {
                return this.DateLastChange.ToString("dd.MM.yyyy. HH:mm");
            }
        }

    }
}
