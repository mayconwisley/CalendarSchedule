﻿using AgendaSalas.Models.Dtos;
using AgendaSalas.Web.Models;

namespace AgendaSalas.Web.Service;

public interface IAgendaService
{
    Task<AgendaView> GetAll(int page = 1, int size = 25, string search = "");
    Task<IEnumerable<AgendaDto>> GetByDate();
    Task<AgendaDto> GetById(int id);
    Task<AgendaDto> Create(AgendaDto agendaDto);
    Task<AgendaDto> Update(AgendaDto agendaDto);
    Task<bool> Delete(int id);
}