using AgendaSalas.Data;
using AgendaSalas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSalas.Repositorio
{
    public class ReuniaoRepositorio
    {
        readonly AgendaContext agendaContext = new();

        public async Task<IEnumerable<Reuniao>> ListarTudo()
        {
            return await agendaContext.Reunioes
                .OrderBy(or => or.Descricao)
                .ToListAsync();
        }
        public async Task<IEnumerable<Reuniao>> ListarPorSala(int salaId)
        {
            var listaPorSala = await agendaContext.Reunioes
                    .Include(i => i.Sala)
                    .Where(w => w.SalaId == salaId)
                    .OrderBy(or => or.Descricao)
                    .ToListAsync();
            return listaPorSala;
        }

        public async Task<Reuniao> BuscarPorId(int id)
        {
            var reuniao = await agendaContext.Reunioes
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (reuniao is not null)
            {
                return reuniao;
            }
            return new Reuniao();
        }

        public async Task Adicionar(Reuniao reuniao)
        {
            try
            {
                agendaContext.Reunioes.Add(reuniao);
                await agendaContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Alterar(Reuniao reuniao)
        {
            try
            {
                Reuniao reuniao1 = await BuscarPorId(reuniao.Id);
                if (reuniao1 is not null)
                {
                    agendaContext.Reunioes.Entry(reuniao1).CurrentValues.SetValues(reuniao);
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
            Reuniao reuniao = await BuscarPorId(id);
            try
            {
                agendaContext.Reunioes.Remove(reuniao);
                await agendaContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
