using System.Threading.Tasks;

namespace Coqueta.BusinessLayer
{
    using System;
    using BusinessInterfaces;
    using DataInterfaces;
    using Types;
    using System.Collections.Generic;

    public class DataProcessor : IDataProcessor
    {
        private readonly IDataRepository _dataRepository;

        public DataProcessor(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        async Task<IEnumerable<User>> IDataProcessor.GetAll()
        {
            try
            {
                return await _dataRepository.RetrieveAll();
            }
            catch (Exception)
            {
                throw;
            }

        }

        async Task<User> IDataProcessor.GetById(int id) {
            try
            {
               return await _dataRepository.RetrieveById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task IDataProcessor.AddUser(User model)
        {
            try
            {
                model.Validate();

                if (await _dataRepository.IsUserRegistered(model.Email))
                {
                    throw new UserExistsExeption("", new[] { "El usuario se encuentra registrado"});
                }

                await _dataRepository.Save(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task IDataProcessor.RemoveUser(int id)
        {
            try
            {
                await _dataRepository.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }
        }


        async Task IDataProcessor.UpdateUser(User model)
        {
            try
            {
                model.Validate();
                await _dataRepository.Update(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
