﻿using System.Configuration;
using SharpRepository.Repository.Configuration;

namespace SharpRepository.Repository
{
    public static class RepositoryFactory
    {
        public static IRepository<T, TKey> GetInstance<T, TKey>(string repositoryName = null) where T : class, new()
        {
            return GetInstance<T, TKey>("sharpRepository", repositoryName);
        }

        public static IRepository<T, TKey> GetInstance<T, TKey>(string configSection, string repositoryName) where T : class, new()
        {
            return GetInstance<T, TKey>(GetConfiguration(configSection), repositoryName);
        }

        public static IRepository<T, TKey> GetInstance<T, TKey>(SharpRepositorySection configuration, string repositoryName) where T : class, new()
        {
            return configuration.GetInstance<T, TKey>(repositoryName);
        }

        private static SharpRepositorySection GetConfiguration(string sectionName)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = config.GetSection(sectionName) as SharpRepositorySection;
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");

            return section;
        }
    }
}
