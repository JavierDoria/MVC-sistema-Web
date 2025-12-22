using MoneyFlow.Context;
using MoneyFlow.Models;
using MoneyFlow.Models.Entities;
using MoneyFlow.Utilities;

namespace MoneyFlow.Managers
{
    public class UserManager(AppDbContext _dbContext)
    {
        public UserVM Login(LoginVM viewModel)
        {
            var found = _dbContext.User.Where(item =>
            item.Email == viewModel.Email &&
            item.Password ==Sha256Hasher.ComputerHast( viewModel.Password)
            ).FirstOrDefault();
            var user = new UserVM();
            if (found != null)
            {
                user.UserId = found.UserId;
                user.FullName = found.FullName;
                user.Email = found.Email;
            }
            return user;
        }
        public int Register(UserVM viewModel)
        {
            if (viewModel.Password != viewModel.RepeatPassword)
                throw new InvalidOperationException("LAS CONTRASEÑAS NO COINCIDEN");
            
            var foundEmail = _dbContext.User.Any(i => i.Email == viewModel.Email);
            if (foundEmail)
                throw new InvalidOperationException("LA EMAIL YA SE ENCUENTRA REGUSTRADO");

                var entity = new User
                {
                    FullName = viewModel.FullName,
                    Email = viewModel.Email,
                    Password = Sha256Hasher.ComputerHast(viewModel.Password)
                };
                _dbContext.User.Add(entity);
                var affected_rows= _dbContext.SaveChanges();
                return affected_rows;
        }
    }
}
