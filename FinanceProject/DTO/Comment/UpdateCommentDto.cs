using System.ComponentModel.DataAnnotations;

namespace FinanceProject.DTO.Comment
{
    public class UpdateCommentDto
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Title cannot exceed 20 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(200, ErrorMessage = "Content cannot exceed 200 characters.")]
        public string Content { get; set; } = string.Empty;
    }
}
