using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Database
{
    public class ClienteDB
    {
        public static List<Entity.ClienteEntity> GetList(List<SqlParameter> paramList)
        {
            List<Entity.ClienteEntity> myList = new List<Entity.ClienteEntity>();
            Entity.ClienteEntity item = new Entity.ClienteEntity();
            try
            {
                DataSet ds = DbActions.GetDataSet("ConsultaClientes", paramList);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    item = new Entity.ClienteEntity();
                    item.id = Convert.ToInt32(dr["id"]);
                    item.cedula = dr["cedula"].ToString();
                    item.nombre = dr["nombre"].ToString();
                    item.fechainicio = Convert.ToDateTime(dr["fechainicio"].ToString());
                    item.fechafin = Convert.ToDateTime(dr["fechafin"].ToString());
                    item.idcola = Convert.ToInt32(dr["idcola"]);
                    myList.Add(item);
                }
            }
            catch (Exception ex)
            {
                myList = new List<Entity.ClienteEntity>();
            }
            return myList;
        }
        public static string Create(List<SqlParameter> paramList)
        {
            try
            {
                return DbActions.InsertUpdate("IngresaClientes", paramList);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
