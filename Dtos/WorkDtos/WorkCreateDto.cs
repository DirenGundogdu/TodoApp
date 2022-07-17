using Dtos.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Dtos.WorkDtos
{
    public class WorkCreateDto : IDto
    {
        //[Required(ErrorMessage ="Definition is required")]
        public string Definition { get; set; }

        public bool IsCompleted { get; set; }
    }
}
