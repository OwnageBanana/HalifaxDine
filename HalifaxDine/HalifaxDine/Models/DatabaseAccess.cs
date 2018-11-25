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

        public IEnumerable<BranchModel> GetBranchData(int? branch_Id = null)
        {
            string sql = "select * from branch";

            if (branch_Id != null)
                sql += " where branch_id = @branch_Id";

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

        public IEnumerable<BranchModel> GetEmployeeData(int? branch_Id = null)
        {
            string sql = "select * from Employee";

            if (branch_Id != null)
                sql += " where branch_id = @branch_Id";
            IEnumerable<BranchModel> model = Enumerable.Empty<BranchModel>();

            try
            {
                model = conn.Query<BranchModel>(sql, new { branch_Id });
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

        public IEnumerable<MenuItemModel> GetClientOrder(string account_id)
        {

            string sql = @"select MI.* from transaction T
                            inner join client C on C.client_id = T.CLIENT_ID
                            INNER JOIN Transaction_Item TI ON TI.TRANS_ID = T.TRANS_ID
                            INNER JOIN menu_item MI ON MI.MENU_ID = TI.MENU_ID
                            where trans_status = 'UNPAID'
                            AND C.Account_Id = @account_id";

            IEnumerable<MenuItemModel> model = Enumerable.Empty<MenuItemModel>();

            try
            {
                model = conn.Query<MenuItemModel>(sql, new { account_id });
            }
            catch
            {
                ;
            }

            return model;
        }

        public IEnumerable<MenuItemModel> CloseClientOrder(int Trans_Id)
        {

            string sql = @"UPDATE `halifaxdine`.`transaction`
SET
`TRANS_STATUS` = 'PAID'
WHERE `TRANS_ID` = @Trans_Id";

            IEnumerable<MenuItemModel> model = Enumerable.Empty<MenuItemModel>();

            try
            {
                conn.Execute(sql, new { Trans_Id });
            }
            catch
            {
                ;
            }

            return model;
        }

        public TransactionModel getclientTransaction(string account_id)
        {
            TransactionModel model = new TransactionModel();

            string sql = @"select * from transaction T
                            inner join client C on C.client_id = t.client_id
                            where t.trans_status = 'UNPAID' and C.account_id = @account_id";

            try
            {
                model = conn.Query<TransactionModel>(sql, new { account_id }).FirstOrDefault();
            }
            catch
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
            bool success = false;
            conn.Open();
            using (IDbTransaction trans = conn.BeginTransaction())
            {
                string sql = @"INSERT INTO feedback VALUES (DEFAULT, @Client_Id, @Feedback_Comment)";

                try
                {
                    success = 1 == conn.Execute(sql, new { model.Client_Id, model.Feedback_Comment });
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                }
            }
            conn.Close();
            return success;
        }

        public bool InsertClientRow(ClientModel model)
        {
            bool success = false;
            conn.Open();
            using (IDbTransaction trans = conn.BeginTransaction())
            {
                string sql = @"INSERT INTO `halifaxdine`.`client`
                            (`CLIENT_ID`,
                            `CLIENT_FNAME`,
                            `CLIENT_LNAME`,
                            `CLIENT_CONTACT`,
                            `ACCOUNT_ID`)
 VALUES (DEFAULT, @Client_FName, @Client_LName, @Client_Contact, @Account_Id)";


                try
                {
                    success = 1 == conn.Execute(sql, new { model.Client_FName, model.Client_LName, model.Client_Contact, model.Account_Id }, trans);
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                }
            }
            conn.Close();
            return success;
        }

        public bool AddEmployeeRow(EmployeeModel model)
        {
            bool success = false;

            conn.Open();
            using (IDbTransaction trans = conn.BeginTransaction())
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

                try
                {
                    success = 1 == conn.Execute(sql, new { model.Branch_id, model.Emp_Email, model.Emp_FName, model.Emp_Hire_Date, model.Emp_Hourly_Rate, model.Emp_LName, model.Emp_Phone, model.Privilege_id }, trans);
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                }
            }
            conn.Close();
            return success;
        }

        public bool InsertClientOrderRow(TransactionModel model)
        {
            bool success = false;
            conn.Open();
            using (IDbTransaction trans = conn.BeginTransaction())
            {
                string sql = @"INSERT INTO `halifaxdine`.`transaction`
                                (`TRANS_ID`,
                                `CLIENT_ID`,
                                `BRANCH_ID`,
                                `TRANS_DATETIME`,
                                `TRANS_STATUS`)
                                VALUES
                                (DEFAULT
                                ,@Client_Id
                                ,@Branch_Id
                                ,@Trans_Date
                                ,@Trans_Status); ";

                try
                {
                    success = 1 == conn.Execute(sql, new { model.Client_Id, model.Branch_Id, model.Trans_Date, Trans_Status = model.Trans_Status.ToString() });
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                }
            }
            conn.Close();

            return success;
        }

        public bool InsertClientOrderItemRow(int Trans_Id, int Menu_Id)
        {
            bool success = false;
            conn.Open();
            using (IDbTransaction trans = conn.BeginTransaction())
            {
                string sql = @"INSERT INTO `halifaxdine`.`transaction_item`
                                (`TRANS_ID`,
                                `MENU_ID`)
                                VALUES
                                (@Trans_Id
                                ,@Menu_Id) ";

                try
                {
                    success = 1 == conn.Execute(sql, new { Trans_Id, Menu_Id });
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                }
            }
            conn.Close();

            return success;
        }
    }
}