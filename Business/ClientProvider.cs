namespace ProInvestAPI.Business{

    using System.Globalization;
    using Microsoft.EntityFrameworkCore;
    using ProInvestAPI.Domain;
    using ProInvestAPI.Models;

    public class ClientProvider{
        private readonly ProInvestDbContext _connectionModel;
        public ClientProvider(ProInvestDbContext connectionModel){
            string Culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Culture);
            _connectionModel = connectionModel;
        }

        public async Task<string> RegisterClient(ClientDomain client)
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

                    var newClient = new Client
                    {
                        CompanyName = client.CompanyName,
                        Name = client.Name,
                        LastName = client.LastName,
                        Rfc = client.Rfc,
                        BirthDay = client.BirthDay,
                        AcademicDegree = client.AcademicDegree,
                        Profession = client.Profession,
                        PhoneNumber = client.PhoneNumber,
                    };

                    _connectionModel.Clients.Add(newClient);

                    int changes = await _connectionModel.SaveChangesAsync();

                    if (changes > 0)
                    {
                        var userAux = await _connectionModel.Clients
                            .Where(x => x.IdClient == newClient.IdClient)
                            .FirstOrDefaultAsync();

                        if (userAux != null)
                        {
                            await transaction.CommitAsync();
                            return userAux.IdClient.ToString();
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
                    throw new Exception("Error durante la transacción de registro de cliente.", ex);
                }
            }
        }
    }
}

    