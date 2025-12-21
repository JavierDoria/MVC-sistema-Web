using Microsoft.EntityFrameworkCore;
using MoneyFlow.Context;
using MoneyFlow.Models;
using MoneyFlow.Models.Entities;

namespace MoneyFlow.Managers
{
    public class ServiceManager(AppDbContext _dbContext)
    {
        //listar
        public List<ServiceVM> GetAll(int userId)
        {
            var list = _dbContext.Service
                .Where(item=>item.UserId == userId)
                .Select(item => new ServiceVM
                {
                    ServiceId=item.ServiceId,
                    UserId = item.UserId,
                    Name = item.Name,
                    Type = item.Type,
                })
                .ToList();

            return list;
        }
        //crear
        public int New(ServiceVM viewModel)
        {
            var entity = new Service
            {
                Name = viewModel.Name,
                Type = viewModel.Type,
                UserId=viewModel.UserId,
            };
            _dbContext.Service.Add(entity);
            var afeccted_rows = _dbContext.SaveChanges();
            return afeccted_rows;
        }
        public ServiceVM GetById(int id)
        {
            var entity = _dbContext.Service.FirstOrDefault(s => s.ServiceId == id);
            if (entity == null)
                return null;
            var model = new ServiceVM
            {
                ServiceId = entity.ServiceId,
                Name = entity.Name,
                Type = entity.Type,
            };
            return model;
        }
        //Editar
        public int Edit(ServiceVM model)
        {
            var entity=_dbContext.Service.Find(model.ServiceId);

            entity.Name = model.Name;
            entity.Type = model.Type;

            _dbContext.Service.Update(entity);
            var affected_rows = _dbContext.SaveChanges();
            return affected_rows;
        }
        //Eliminar
        public int Delete(int id)
        {
            var entity = _dbContext.Service.Find(id);
            _dbContext.Service.Remove(entity);
            var affected_rows = _dbContext.SaveChanges();
            return affected_rows;
        }
        public List<ServiceVM>GetByType(int userId, string type)
        {
            var list = _dbContext.Service.Where(item => item.UserId==userId && item.Type==type).Select(item => new ServiceVM
            {
                ServiceId = item.ServiceId,
                    Name = item.Name,
            }).ToList();
            return list;
        }
    }
}
