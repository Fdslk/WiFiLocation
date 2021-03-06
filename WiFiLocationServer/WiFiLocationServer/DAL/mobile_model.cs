﻿using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using WiFiLocationServer.Models;
using WiFiLocationServer.Dbconnect;

namespace WiFiLocationServer.DAL
{
    /// <summary>
    /// 数据访问类:手机型号表
    /// </summary>
    public class mobile_model
    {
        #region 公用方法=================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_mobile_model");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Models.mobile_model model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into tb_mobile_model(model_name)");
                strSql.Append(" values (@model_name)");
                SqlParameter[] parameters = {
                        new SqlParameter("@model_name", SqlDbType.VarChar,50)};
                parameters[0].Value = model.model_name;

                object obj = DbHelperSQL.ExecuteSql(strSql.ToString(),parameters); //带事务
                model.id = Convert.ToInt32(obj);

            return model.id;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Models.mobile_model model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {


            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_mobile_model ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Models.mobile_model GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,model_name");
            strSql.Append(" from tb_mobile_model");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Models.mobile_model model = new Models.mobile_model();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        #endregion



        #region 私有方法=================================
        ///<summary>
        /// 将对象转换为实体
        /// </summary>
        private Models.mobile_model DataRowToModel(DataRow row)
        {
            Models.mobile_model model = new Models.mobile_model();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["model_name"] != null && row["model_name"].ToString() != "")
                {
                    model.model_name = row["model_name"].ToString();
                }
            }
            return model;
        }
        #endregion


    }
}