using AgendaSalas.API.Model;
using AgendaSalas.Models.Dtos;

namespace AgendaSalas.API.MappingDto
{
    public static class MappingDtos
    {
        public static IEnumerable<SalaDto> ConverterSalasParaDto(this IEnumerable<Sala> salas)
        {
            return (from sala in salas
                    select new SalaDto
                    {
                        Id = sala.Id,
                        Nome = sala.Nome,
                        Descricao = sala.Descricao,
                        Ramal = sala.Ramal
                    }).ToList();
        }

        public static IEnumerable<AgendaDto> ConverterAgendasParaDto(this IEnumerable<Agenda> agendas)
        {
            return (from agenda in agendas
                    select new AgendaDto
                    {
                        Id = agenda.Id,
                        Descricao = agenda.Descricao,
                        DataInicio = agenda.DataInicio,
                        DataFinal = agenda.DataFinal,
                        PermitirLigar = agenda.PermitirLigar,
                        PermitirChamar = agenda.PermitirLigar,
                        Sala = agenda?.Sala?.Nome,
                        SalaId = agenda!.Sala!.Id

                    }).ToList();
        }

        public static IEnumerable<Sala> ConverterDtoParaSalas(this IEnumerable<SalaDto> salasDto)
        {
            return (from sala in salasDto
                    select new Sala
                    {
                        Id = sala.Id,
                        Nome = sala.Nome,
                        Descricao = sala.Descricao,
                        Ramal = sala.Ramal

                    }).ToList();
        }

        public static IEnumerable<Agenda> ConverterAgendasParaDto(this IEnumerable<AgendaDto> agendasDto)
        {
            return (from agenda in agendasDto
                    select new Agenda
                    {
                        Id = agenda.Id,
                        Descricao = agenda.Descricao,
                        DataInicio = agenda.DataInicio,
                        DataFinal = agenda.DataFinal,
                        PermitirLigar = agenda.PermitirLigar,
                        PermitirChamar = agenda.PermitirLigar,
                        SalaId = agenda!.SalaId

                    }).ToList();
        }

        public static SalaDto ConverterSalaParaDto(this Sala sala)
        {
            return new SalaDto
            {
                Id = sala.Id,
                Nome = sala.Nome,
                Descricao = sala.Descricao,
                Ramal = sala.Ramal
            };
        }
        public static AgendaDto ConverterAgendaParaDto(this Agenda agenda)
        {
            return new AgendaDto
            {
                Id = agenda.Id,
                Descricao = agenda.Descricao,
                DataInicio = agenda.DataInicio,
                DataFinal = agenda.DataFinal,
                PermitirChamar = agenda.PermitirChamar,
                PermitirLigar = agenda.PermitirLigar,
                Sala = agenda?.Sala?.Nome,
                SalaId = agenda!.Sala!.Id
            };
        }
        public static Sala ConverterDtoParaSala(this SalaDto salaDto)
        {
            return new Sala
            {
                Id = salaDto.Id,
                Nome = salaDto.Nome,
                Descricao = salaDto.Descricao,
                Ramal = salaDto.Ramal

            };
        }
        public static Agenda ConverterDtoParaAgenda(this AgendaDto agendaDto)
        {
            return new Agenda
            {
                Id = agendaDto.Id,
                Descricao = agendaDto.Descricao,
                DataInicio = agendaDto.DataInicio,
                DataFinal = agendaDto.DataFinal,
                PermitirChamar = agendaDto.PermitirChamar,
                PermitirLigar = agendaDto.PermitirLigar,
                SalaId = agendaDto!.SalaId
            };
        }

    }
}
