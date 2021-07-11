using EntityConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EntityConfigurationData
{
    public class EntityData
    {
       
        public string GetFields(string entityName,bool isDefault)
        {            
            string constr = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\SharedFiles\ConfigurationAPI\EntityConfiguration\EntityConfiguration\App_Data\EntityDB.mdf; Integrated Security = True";
            StringBuilder stringBuilder = new StringBuilder();
            DataSet dataSet = new DataSet("JSON");

            using (SqlConnection con = new SqlConnection(constr))
            {

                using (SqlDataAdapter da = new SqlDataAdapter())
                {

                    string sql = "pICg_GetEntityFields";
                    da.SelectCommand = new SqlCommand(sql, con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    da.SelectCommand.Parameters.Add("@aENTITYNAME", SqlDbType.VarChar).Value = entityName;
                    da.SelectCommand.Parameters.Add("@aISDEFAULT", SqlDbType.Bit).Value = isDefault;
                    da.Fill(dataSet);
                   
                }

                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows.Count == 1)
                    {
                        stringBuilder.Append(Convert.ToString(dataSet.Tables[0].Rows[0][0]));
                    }
                    else
                    {
                        foreach (DataRow dr in dataSet.Tables[0].Rows)
                        {
                            stringBuilder.Append(dr.ItemArray[0].ToString());
                        }
                    }
                }

                return Convert.ToString(stringBuilder);
            }
        }

        public string SaveFields(string input)
        {
            string constr = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\SharedFiles\ConfigurationAPI\EntityConfiguration\EntityConfiguration\App_Data\EntityDB.mdf; Integrated Security = True";
            StringBuilder stringBuilder = new StringBuilder();
            DataSet dataSet = new DataSet("JSON");

            using (SqlConnection con = new SqlConnection(constr))
            {

                using (SqlDataAdapter da = new SqlDataAdapter())
                {

                    string sql = "pICs_SaveEntityFields";
                    da.SelectCommand = new SqlCommand(sql, con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    da.SelectCommand.Parameters.Add("@aJsonInputString", SqlDbType.VarChar).Value = input;                    
                    da.Fill(dataSet);

                }

                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows.Count == 1)
                    {
                        stringBuilder.Append(Convert.ToString(dataSet.Tables[0].Rows[0][0]));
                    }
                    else
                    {
                        foreach (DataRow dr in dataSet.Tables[0].Rows)
                        {
                            stringBuilder.Append(dr.ItemArray[0].ToString());
                        }
                    }
                }

                return Convert.ToString(stringBuilder);
            }
        }
    }
}
