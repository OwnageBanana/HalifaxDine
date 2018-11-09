using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using MySql;
using MySql.Data.MySqlClient;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using HalifaxDine.Models;

namespace HalifaxDine.Models
{
    public class DatabaseAccess
    {
        private IDbConnection conn;

        public DatabaseAccess()
        {
            //Added from web.config file at project root
            conn = new ProfiledDbConnection(new MySqlConnection("server = localhost; user id = user; password=pass;  database = HalifaxDine;"), MiniProfiler.Current);

        }

        public IEnumerable<ClientModel> GetCustomerData()
        {
            string sql = "select * from client";


            IEnumerable<ClientModel> model = Enumerable.Empty<ClientModel>();
            try
            {
                model = conn.Query<ClientModel>(sql);
            }
            catch (Exception e)
            {
                ;
            }

            return model;
        }

        public bool AddEmployeeRow(EmployeeModel model)
        {
            string sql = @"INSERT INTO `halifaxdine`.`employee`
                            (`EMP_ID`,
                            `PRIVILEGE_ID`,
                            `BRANCH_ID`,
                            `EMP_FNAME`,
                            `EMP_LNAME`,
                            `EMP_HOURLY_RATE`,
                            `EMP_HIRE_DATE`,
                            `EMP_EMAIL`,
                            `EMP_PHONE`)
                            VALUES
                            (DEFAULT,
                            @PRIVILEGE_ID,
                            @BRANCH_ID,
                            @EMP_FNAME,
                            @EMP_LNAME,
                            @EMP_HOURLY_RATE,
                            @EMP_HIRE_DATE,
                            @EMP_EMAIL,
                            @EMP_PHONE) ";

            bool success = false;
            try
            {
                success = 1 == conn.Execute(sql, new { model.Branch_id,model.Emp_Email,model.Emp_FName,model.Emp_Hire_Date,model.Emp_Hourly_Rate,model.Emp_LName,model.Emp_Phone,model.Privilege_id});
            }
            catch (Exception e)
            {
                ;
            }

            return success;
        }
    }
}