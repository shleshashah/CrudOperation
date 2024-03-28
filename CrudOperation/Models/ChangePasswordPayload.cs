using CrudOperation.Utility;
using System.ComponentModel.DataAnnotations;

namespace CrudOperation.Models
{
    public class ChangePasswordPayload
    {

        [Required(ErrorMessage = ErrrorMessageEnum.IsRequired)]
        [StringLength(30)]
        public string LoginUser { get; set; }
        [Required(ErrorMessage = ErrrorMessageEnum.IsRequired)]
        public string Password { get; set; }
        [Required(ErrorMessage = ErrrorMessageEnum.IsRequired)]
        [Compare("Password", ErrorMessage = ErrrorMessageEnum.ConfirmPassword)]
        public string ConfirmPassword { get; set; }
    }
}
