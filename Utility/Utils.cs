namespace ProInvestAPI.Utility
{
    public static class Utility
    {
        public static string GenerateFolio(int ClientId)
        {
            // Obtiene la fecha y hora actual
            string fechaActual = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Combina la fecha, hora y el id del cliente para crear el folio
            string folio = $"{fechaActual}_{ClientId}";

            return folio;
        }
    }
}
