using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcunMedyaAkademiAgency.Entities
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Adınızı giriniz.")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "E-posta adresinizi giriniz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon Numaranızı Giriniz.")]
        public string TelNo { get; set; }

        [Required(ErrorMessage = "Mesajınızı giriniz.")]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}