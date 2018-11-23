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

        public IEnumerable<BranchModel> GetBranchData()
        {
            string sql = "select * from branch";


            IEnumerable<BranchModel> model = Enumerable.Empty<BranchModel>();
            try
            {
                model = conn.Query<BranchModel>(sql);
            }
            catch (Exception e)
            {
                ;
            }

            return model;
        }

        public IEnumerable<MenuItemModel> GetMenuData()
        {
            string sql = "select * from Menu_Item";


            IEnumerable<MenuItemModel> model = Enumerable.Empty<MenuItemModel>();
            try
            {
                model = conn.Query<MenuItemModel>(sql);
            }
            catch (Exception e)
            {
                ;
            }

            return model;
        }

        public IEnumerable<FeedbackModel> GetFeedbackData()
        {
            string sql = "select * from feedback";


            IEnumerable<FeedbackModel> model = Enumerable.Empty<FeedbackModel>();
            try
            {
                model = conn.Query<FeedbackModel>(sql);
            }
            catch (Exception e)
            {
                ;
            }

            return model;
        }


        public ClientModel GetClientRow(string Account_Id)
        {
            string sql = "select * from client where Account_Id = @Account_Id";


            ClientModel model = new ClientModel();
            try
            {
                model = conn.Query<ClientModel>(sql, new { Account_Id }).FirstOrDefault();
            }
            catch (Exception e)
            {
                ;
            }

            return model;
        }

        public bool InsertFeedbackRow(FeedbackModel model)
        {
            string sql = @"INSERT INTO feedback VALUES (DEFAULT, @Client_Id, @Feedback_Comment)";

            bool success = false;
            try
            {
                success = 1 == conn.Execute(sql, new { model.Client_Id, model.Feedback_Comment });
            }
            catch (Exception e)
            {
                ;
            }

            return success;
        }

        public bool InsertClientRow(ClientModel model)
        {
            string sql = @"INSERT INTO `halifaxdine`.`client`
                            (`CLIENT_ID`,
                            `CLIENT_FNAME`,
                            `CLIENT_LNAME`,
                            `CLIENT_CONTACT`,
                            `ACCOUNT_ID`)
 VALUES (DEFAULT, @Client_FName, @Client_LName, @Client_Contact, @Account_Id)";

            bool success = false;
            try
            {
                success = 1 == conn.Execute(sql, new { model.Client_FName, model.Client_LName, model.Client_Contact, model.Account_Id });
            }
            catch (Exception e)
            {
                ;
            }

            return success;
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