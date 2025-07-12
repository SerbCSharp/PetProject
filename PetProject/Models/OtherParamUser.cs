using System.ComponentModel.DataAnnotations;

namespace PetProject.Models
{
    public class OtherParamUser : ParamUser
    {
        public string UserName { get; set; }
        public bool SeniorManager { get; set; }

        [Required]
        public Professions Profession { get; set; }
    }
}
