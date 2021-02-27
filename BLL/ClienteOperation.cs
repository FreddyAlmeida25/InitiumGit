using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BLL
{
    public class ClienteOperation
    {
        private static int cola1Segundos = 120;
        private static int cola2Segundos = 180;
        public static List<DAL.Entity.ClienteEntity> GetList()
        {
            List<DAL.Entity.ClienteEntity> myList = new List<DAL.Entity.ClienteEntity>();
            List<SqlParameter> paramList = new List<SqlParameter>();
            try
            {
                paramList.Add(new SqlParameter()
                {
                    ParameterName = "@fecha",
                    Value = DateTime.Now
                });

                myList = DAL.Database.ClienteDB.GetList(paramList);
            }
            catch (Exception ex)
            {
                myList = new List<DAL.Entity.ClienteEntity>();
            }
            return myList;
        }
        public static string Create(string cedula, string nombre)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            try
            {
                DAL.Entity.ClienteEntity input = new DAL.Entity.ClienteEntity();
                input.cedula = cedula;
                input.nombre = nombre;
                input = ActualizarFechas(input);

                paramList.Add(new SqlParameter()
                {
                    ParameterName = "@cedula",
                    Value = input.cedula
                });
                paramList.Add(new SqlParameter()
                {
                    ParameterName = "@nombre",
                    Value = input.nombre
                });
                paramList.Add(new SqlParameter()
                {
                    ParameterName = "@fechainicio",
                    Value = input.fechainicio
                });
                paramList.Add(new SqlParameter()
                {
                    ParameterName = "@fechafin",
                    Value = input.fechafin
                });
                paramList.Add(new SqlParameter()
                {
                    ParameterName = "@idcola",
                    Value = input.idcola
                });

                return DAL.Database.ClienteDB.Create(paramList);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private static DAL.Entity.ClienteEntity ActualizarFechas(DAL.Entity.ClienteEntity input)
        {
            List<DAL.Entity.ClienteEntity> mylist = GetList();
            DAL.Entity.ClienteEntity item_cola1 = mylist.Where(i => i.idcola == 1).OrderByDescending(g => g.fechafin).FirstOrDefault();
            DAL.Entity.ClienteEntity item_cola2 = mylist.Where(i => i.idcola == 2).OrderByDescending(g => g.fechafin).FirstOrDefault();

            if (item_cola1 == null && item_cola2 == null)
            {
                //INICIO : PRIMER ITEM
                input.idcola = 1;
                input.fechainicio = DateTime.Now;
                input.fechafin = DateTime.Now.AddSeconds(1 + cola1Segundos);
            }
            else if (item_cola1 != null && item_cola2 == null)
            {
                if (DateTime.Now > item_cola1.fechafin)
                {
                    //NO HAY CLIENTES EN COLA 1; NO ES EL PRIMER REGISTRO DEL DIA
                    input.idcola = 1;
                    input.fechainicio = DateTime.Now;
                    input.fechafin = DateTime.Now.AddSeconds(1 + cola1Segundos);
                }
                else
                {
                    //HAY CLIENTES EN COLA 1; IR A COLA 2
                    input.idcola = 2;
                    input.fechainicio = item_cola1.fechafin.AddSeconds(1);
                    input.fechafin = item_cola1.fechafin.AddSeconds(1 + cola2Segundos);
                }
            }
            else if (item_cola1 == null && item_cola2 != null)
            {
                input.idcola = 1;
                input.fechainicio = DateTime.Now;
                input.fechafin = DateTime.Now.AddSeconds(1 + cola1Segundos);
            }
            else if (item_cola1.fechafin < item_cola2.fechafin)
            {
                //IR A COLA 1
                input.idcola = 1;                
                if (DateTime.Now > item_cola1.fechafin)
                {
                    //NO HAY CLIENTES EN COLA 2
                    input.fechainicio = DateTime.Now;
                    input.fechafin = DateTime.Now.AddSeconds(1 + cola1Segundos);
                }
                else
                {
                    //HAY CLIENTES EN COLA 2
                    input.fechainicio = item_cola1.fechafin.AddSeconds(1);
                    input.fechafin = item_cola1.fechafin.AddSeconds(1 + cola1Segundos);
                }
                
            }
            else
            {   
                if (DateTime.Now > item_cola2.fechafin)
                {
                    //NO HAY CLIENTES EN COLA 1; IR A COLA 1
                    input.idcola = 1;
                    input.fechainicio = DateTime.Now;
                    input.fechafin = DateTime.Now.AddSeconds(1 + cola1Segundos);
                }
                else
                {
                    //HAY CLIENTES EN COLA 1; IR A COLA 2
                    input.idcola = 2;
                    input.fechainicio = item_cola2.fechafin.AddSeconds(1);
                    input.fechafin = item_cola2.fechafin.AddSeconds(1 + cola2Segundos);
                }
            }            
            return input;
        }
        private static DAL.Entity.ClienteEntity ActualizarFechasOLD(DAL.Entity.ClienteEntity input)
        {
            List<DAL.Entity.ClienteEntity> mylist = GetList();
            DAL.Entity.ClienteEntity item_cola1 = mylist.Where(i => i.idcola == 1).OrderByDescending(g => g.fechafin).FirstOrDefault();
            DAL.Entity.ClienteEntity item_cola2 = mylist.Where(i => i.idcola == 2).OrderByDescending(g => g.fechafin).FirstOrDefault();

            if (item_cola1 == null && item_cola2 == null)
            {
                //INICIO : PRIMER ITEM
                input.idcola = 1;
                input.fechainicio = DateTime.Now;
                input.fechafin = DateTime.Now.AddSeconds(1 + cola1Segundos);
            }
            else if (item_cola1 != null && item_cola2 == null)
            {
                //SEGUNDO ITEM EN COLA
                if (DateTime.Now > item_cola1.fechafin)
                {
                    //NO HAY CLIENTES EN COLA; NO ES EL PRIMER REGISTRO DEL DIA
                    input.idcola = 1;
                    input.fechainicio = DateTime.Now;
                    input.fechafin = item_cola1.fechafin.AddSeconds(1 + cola1Segundos);
                }
                else
                {
                    //HAY CLIENTES EN COLA; IR A COLA 2
                    input.idcola = 2;
                    input.fechainicio = item_cola1.fechafin.AddSeconds(1);
                    input.fechafin = item_cola1.fechafin.AddSeconds(1 + cola2Segundos);
                }
            }
            else if (item_cola1.fechafin > item_cola2.fechafin)
            {
                if (DateTime.Now > item_cola1.fechafin) 
                {
                    //NO HAY CLIENTES EN COLA; NO ES EL PRIMER REGISTRO DEL DIA
                    input.idcola = 1;
                    input.fechainicio = DateTime.Now;
                    input.fechafin = item_cola1.fechafin.AddSeconds(1 + cola1Segundos);
                }
                else
                {
                    //HAY CLIENTES EN COLA; IR A COLA 1
                    input.idcola = 1;
                    input.fechainicio = item_cola1.fechafin.AddSeconds(1);
                    input.fechafin = item_cola1.fechafin.AddSeconds(1 + cola1Segundos);
                }
            }
            else
            {
                //HAY CLIENTES EN COLA; IR A COLA 2
                input.idcola = 2;
                input.fechainicio = item_cola2.fechafin.AddSeconds(1);
                input.fechafin = item_cola2.fechafin.AddSeconds(1 + cola2Segundos);
            }
            return input;
        }
    }
}
