using System.ComponentModel.DataAnnotations;

namespace CalendarSchedule.Models.Dtos;

public class ClientDto
{
    public int Id { get; set; } = 0;
    [Required(ErrorMessage = "Nome obrigatório")]
    [MaxLength(250, ErrorMessage = "Máximo 250 digito")]
    [MinLength(3, ErrorMessage = "Mínino 3 digito")]
    public string? Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefone Obrigatório")]
    [MaxLength(20, ErrorMessage = "Máximo 20 digito")]
    [MinLength(10, ErrorMessage = "Mínino 10 digito")]
    public string? Telephone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Estado obrigatório")]
    [MaxLength(20, ErrorMessage = "Máximo 20 digito")]
    [MinLength(2, ErrorMessage = "Mínino 2 digito")]
    public string? State { get; set; }

    [Required(ErrorMessage = "Cidade obrigatório")]
    [MaxLength(50, ErrorMessage = "Máximo 50 digito")]
    [MinLength(2, ErrorMessage = "Mínino 2 digito")]
    public string? City { get; set; }

    [Required(ErrorMessage = "Rua obrigatório")]
    [MaxLength(50, ErrorMessage = "Máximo 50 digito")]
    [MinLength(2, ErrorMessage = "Mínino 2 digito")]
    public string? Road { get; set; }

    [Required(ErrorMessage = "Número obrigatório")]
    [MaxLength(10, ErrorMessage = "Máximo 10 digito")]
    [MinLength(2, ErrorMessage = "Mínino 2 digito")]
    public string? Number { get; set; }

    [Required(ErrorMessage = "Jardim obrigatório")]
    [MaxLength(50, ErrorMessage = "Máximo 50 digito")]
    [MinLength(2, ErrorMessage = "Mínino 2 digito")]
    public string? Garden { get; set; }

    [MaxLength(500, ErrorMessage = "Máximo 500 digito")]
    public string? Description { get; set; } = string.Empty;

    public bool Active { get; set; } = true;
    public bool Prospection { get; set; } = false;
}
