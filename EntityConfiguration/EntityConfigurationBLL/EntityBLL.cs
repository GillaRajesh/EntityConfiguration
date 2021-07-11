using EntityConfiguration.Models;
using EntityConfigurationData;
using System.Collections.Generic;


namespace EntityConfigurationBLL
{
    public interface IEntityBLL
    {
        string GetFields(string entityName, bool isDefault);

        string SaveFields(string input);
    }

    public class EntityBLL : IEntityBLL
    {       
        public string GetFields(string entityName,bool isDefault)
        {
            EntityData entityData = new EntityData();
            return entityData.GetFields(entityName, isDefault);
        }

        public string SaveFields(string input)
        {
            EntityData entityData = new EntityData();
            return entityData.SaveFields(input);
        }
    }
}
