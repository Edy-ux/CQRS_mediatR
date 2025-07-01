using System.ComponentModel.DataAnnotations;

namespace GamePlayerCQRS.Models.DTOs
{
    public class CreateGamePlayerRequest
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 50 caracteres")]
        public string Password { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Role deve ter no máximo 20 caracteres")]
        public string? Role { get; set; } = "Player";
    }

    public class UpdateGamePlayerRequest
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        [StringLength(20, ErrorMessage = "Role deve ter no máximo 20 caracteres")]
        public string? Role { get; set; }
    }

    public class UpdateGamePlayerPasswordRequest
    {
        [Required(ErrorMessage = "Nova senha é obrigatória")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 50 caracteres")]
        public string NewPassword { get; set; } = string.Empty;
    }

    public class UpdateGamePlayerStatusRequest
    {
        [Required(ErrorMessage = "Status é obrigatório")]
        [RegularExpression("^(active|inactive|suspended)$", ErrorMessage = "Status deve ser: active, inactive ou suspended")]
        public string Status { get; set; } = string.Empty;
    }

    public class GamePlayerFilterRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}