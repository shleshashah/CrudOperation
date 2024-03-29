using CrudOperation.Utility;
using System;
using System.ComponentModel.DataAnnotations;

namespace CrudOperation.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = ErrrorMessageEnum.IsRequired)]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = ErrrorMessageEnum.IsRequired)]
        [StringLength(30)]
        public string LastName { get; set; }
        [Required(ErrorMessage = ErrrorMessageEnum.IsRequired)]
        public string Password { get; set; }
        [Required(ErrorMessage = ErrrorMessageEnum.IsRequired)]
        [Compare("Password", ErrorMessage = ErrrorMessageEnum.ConfirmPassword)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = ErrrorMessageEnum.IsRequired)]
        [StringLength(30)]
        public string LoginUser { get; set; }
        [Required(ErrorMessage = ErrrorMessageEnum.IsRequired)]
        [EmailAddress(ErrorMessage = ErrrorMessageEnum.InvalidEmail)]
        public string Email { get; set; }
        [Required(ErrorMessage = ErrrorMessageEnum.IsRequired)]
        [StringLength(12)]
        [Phone]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Token { get; set; }
    }
}
