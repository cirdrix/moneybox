namespace MoneyBox.Services
{
    using System.Collections.Generic;

    using MoneyBox.Domain;

    public interface IRegistrationService
    {
        IEnumerable<BoxRegistration> GetRegistrationsPaged(string identityName, string searchTerm, Box seatchBox, int pageNumber, int pageRows, string sortorder, string sortname, out int tableRows);
    }

    public class RegistrationService : IRegistrationService
    {
        public IEnumerable<BoxRegistration> GetRegistrationsPaged(
            string identityName,
            string searchTerm,
            Box seatchBox,
            int pageNumber,
            int pageRows,
            string sortorder,
            string sortname,
            out int tableRows)
        {
            var box = new Box { Description = "Box desc"};
            var rows = new List<BoxRegistration>();
            for (int i = 1; i <= 100; i++)
            {
                var row = new BoxRegistration();
                row.Id = i;
                row.Description = string.Format("Desc " + i);
                row.Amount = i*100;
                row.Box = box;

                rows.Add(row);
            }

            tableRows = rows.Count;
            return rows;
        }
    }
}