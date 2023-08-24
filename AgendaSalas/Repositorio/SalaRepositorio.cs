using AgendaSalas.Data;
using AgendaSalas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaSalas.Repositorio
{
    public class SalaRepositorio
    {
        readonly AgendaContext agendaContext = new();
      
        public async Task<IEnumerable<Sala>> ListarTudo()
        {
            return await agendaContext.Salas
                .OrderBy(or => or.Descricao)
                .ToListAsync();
        }
        
        public async Task<Sala> BuscarPorId(int id)
        {
            var sala = await agendaContext.Salas
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (sala is not null)
            {
                return sala;
            }
            return new Sala();
        }
     
        public async Task Adicionar(Sala sala)
        {
            try
            {
                agendaContext.Salas.Add(sala);
                await agendaContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Alterar(Sala sala)
        {
            try
            {
                Sala sala1 = await BuscarPorId(sala.Id);
                if (sala1 is not null)
                {
                    agendaContext.Salas.Entry(sala1).CurrentValues.SetValues(sala);
                    await agendaContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Excluir(int id)
        {
            Sala sala = await BuscarPorId(id);
            try
            {
                agendaContext.Remove(sala);
                await agendaContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
