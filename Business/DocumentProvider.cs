namespace ProInvestAPI.Business{

    using System.Globalization;
    using Microsoft.EntityFrameworkCore;
    using ProInvestAPI.Domain;
    using ProInvestAPI.Models;

    public class DocumentProvider{
        private readonly ProInvestDbContext _connectionModel;
        public DocumentProvider(ProInvestDbContext connectionModel){
            string Culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Culture);
            _connectionModel = connectionModel;
        }

       

        public (int, List<UserDomain>, string) GetUsers()
        {
            int code = 200;
            List<UserDomain> userList = new List<UserDomain>();
            string report = "";
            try
            {
                var listTemp = _connectionModel.Users.ToList();
                foreach (var item in listTemp)
                {
                    UserDomain user = new UserDomain
                    {
                        Username = item.Username,
                        IdUser = item.IdUser
                    };
                    userList.Add(user);
                }
            }
            catch (Exception e)
            {
                code = 500;
                report = e.Message;
            }
            return (code, userList, report);
        }

        public async Task<string> Register(UserDomain user)
        {
            using (var transaction = await _connectionModel.Database.BeginTransactionAsync())
            {
                try
                {
                    bool canConnect = await _connectionModel.Database.CanConnectAsync();

                    if (!canConnect)
                    {
                        throw new Exception("No se pudo establecer conexión con la base de datos.");
                    }

                    var newUser = new User
                    {
                        Username = user.Username,
                        Email = user.Email,
                        Password = user.Password,
                    };

                    _connectionModel.Users.Add(newUser);

                    int changes = await _connectionModel.SaveChangesAsync();

                    if (changes > 0)
                    {
                        var userAux = await _connectionModel.Users
                            .Where(x => x.Email == newUser.Email)
                            .FirstOrDefaultAsync();

                        if (userAux != null)
                        {
                            await transaction.CommitAsync();
                            return userAux.IdUser.ToString();
                        }
                        else
                        {
                            await transaction.RollbackAsync();
                            return null;
                        }
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Error durante la transacción de registro de usuario.", ex);
                }
            }
        }
    }
}

    