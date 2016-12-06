namespace MoneyBox.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;

    using MoneyBox.Domain;

    public interface IRegistrationService
    {
        IEnumerable<BoxRegistration> GetRegistrationsPaged(string identityName, string searchTerm, Box seatchBox, int pageNumber, int pageRows, string sortorder, string sortname, out int tableRows);

        void Import(DataTable dt, int idBox);
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

        public void Import(DataTable dt, int idBox)
        {
            foreach (DataRow row in dt.Rows)
            {
                BoxRegistration registration = new BoxRegistration();
                registration.Box = new Box { Id = idBox};
                DateTime createdAt = DateTime.MinValue;
                if (DateTime.TryParseExact(row[0].ToString(), "s", CultureInfo.InvariantCulture, DateTimeStyles.None, out createdAt))
                {
                    registration.CreatedAt = createdAt;
                }

                registration.Description = row[1].ToString();
                decimal amount = 0;
                if (decimal.TryParse(row[2].ToString(), out amount))
                {
                    registration.Amount = amount;
                }
            }
        }
    }
}