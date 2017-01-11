namespace MoneyBox.Services
{
    using System;
    using System.Collections.Generic;

    using MoneyBox.DataAccess;
    using MoneyBox.Domain;

    public interface IBoxService
    {
        IEnumerable<Box> LoadAllBoxes();
        Guid GetGuid();
    }
    public class BoxService : IBoxService
    {
    
        private readonly IRepository<Box> _repositoryBox;

        public BoxService(IRepository<Box> repositoryBox)
        {
            _repositoryBox = repositoryBox;
          
        }

        public IEnumerable<Box> LoadAllBoxes()
        {
            return _repositoryBox.Get();
        }

        public Guid GetGuid()
        {
            return _repositoryBox.GetMyGuid();
        }
    }
}
