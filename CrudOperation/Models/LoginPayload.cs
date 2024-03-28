using CrudOperation.Utility;
using System.ComponentModel.DataAnnotations;

namespace CrudOperation.Models
{
    public class LoginPayload
    {
        [Required(ErrorMessage = ErrrorMessageEnum.IsRequired)]
        [StringLength(30)]
        public string LoginUser { get; set; }

        [Required(ErrorMessage = ErrrorMessageEnum.IsRequired)]
        public string Password { get; set; }
    }
}
