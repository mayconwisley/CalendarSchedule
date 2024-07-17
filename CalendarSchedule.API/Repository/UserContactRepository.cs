using Microsoft.EntityFrameworkCore;
using CalendarSchedule.API.Data;
using CalendarSchedule.API.Model;
using CalendarSchedule.API.Repository.Interface;

namespace CalendarSchedule.API.Repository;

public class UserContactRepository(ScheduleContext scheduleContext) : IUserContactRepository
{
    private readonly ScheduleContext _scheduleContext = scheduleContext;

    public async Task<UserContact> Create(UserContact userContact)
    {
        try
        {
            if (userContact is not null)
            {
                _scheduleContext.UserContacts.Add(userContact);
                await _scheduleContext.SaveChangesAsync();

                userContact = await GetById(userContact.Id);

                return userContact;
            }
            return new();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<UserContact> Delete(int id)
    {
        try
        {
            var userContact = await GetById(id);
            if (userContact is not null)
            {
                _scheduleContext.UserContacts.Remove(userContact);
                await _scheduleContext.SaveChangesAsync();
                return userContact;
            }
            return new();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<UserContact>> GetAll(int page, int size, string search)
    {
        try
        {
            var userContacts = await _scheduleContext.UserContacts
                .Include(i => i.User)
                .Skip((page - 1) * size)
                .Take(size)
                .OrderBy(o => o.User.Name)
                .ToListAsync();
            return userContacts;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<UserContact> GetById(int id)
    {
        try
        {
            var userContact = await _scheduleContext.UserContacts
                            .Include(i => i.User)
                            .Where(o => o.Id == id)
                            .FirstOrDefaultAsync();
            if (userContact is not null)
            {
                return userContact;
            }

            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<UserContact>> GetByUserId(int page, int size, int userId)
    {
        try
        {
            var userContacts = await _scheduleContext.UserContacts
                .Include(i => i.User)
                .Where(w => w.UserId == userId)
                .Skip((page - 1) * size)
                .Take(size)
                .OrderBy(o => o.User.Name)
                .ToListAsync();
            return userContacts;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<int> TotalUserContact(string search)
    {
        var totalUserContact = await _scheduleContext.UserContacts
             .Where(w => w.User.Name.Contains(search))
             .CountAsync();
        return totalUserContact;
    }

    public async Task<UserContact> Update(UserContact userContact)
    {
        try
        {
            if (userContact is not null)
            {
                _scheduleContext.UserContacts.Entry(userContact).State = EntityState.Modified;
                await _scheduleContext.SaveChangesAsync();

                userContact = await GetById(userContact.Id);

                return userContact;
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
