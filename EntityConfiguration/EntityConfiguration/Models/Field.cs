using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace EntityConfiguration.Models
{
    [DataContract]
    public class Field : Entity
    {
        [DataMember]
        public int FieldID { get; set; }

        [DataMember]
        public string FieldName { get; set; }

        [DataMember]
        public bool IsRequired { get; set; }

        [DataMember]
        public string MaxLength { get; set; }

        [DataMember]
        public bool IsCustom { get; set; }

        

    }
}