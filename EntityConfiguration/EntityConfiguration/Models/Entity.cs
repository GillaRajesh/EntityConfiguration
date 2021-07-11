using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

 
namespace EntityConfiguration.Models
{
    [DataContract]
    public class Entity
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string EntityName { get; set; }


        [DataMember]
        public List<Field> Fields { get; set; }

    }
}