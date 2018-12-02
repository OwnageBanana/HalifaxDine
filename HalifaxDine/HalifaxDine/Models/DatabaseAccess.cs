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
                model = conn.Query<BranchModel>(sql, new { branch_Id });
            }
            catch (Exception e)
            {
                ;
            }

            return model;
        }

        public IEnumerable<EmployeeModel> GetEmployeeData(int? branch_Id = null, int? Employee_Id = null)
        {
            string sql = "select * from Employee";



            if (branch_Id != null)
            {
                sql += " where branch_id = @branch_Id";
                if (Employee_Id != null)
                    sql += " EMP_Id = @Employee_Id ";
            }
            else if (Employee_Id != null)
                sql += " where EMP_Id = @Employee_Id ";

            IEnumerable<EmployeeModel> model = Enumerable.Empty<EmployeeModel>();

            try
            {
                model = conn.Query<EmployeeModel>(sql, new { branch_Id, Employee_Id });
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

        public EmployeeModel GetEmployeeRow(string Account_Id)
        {
            string sql = "select * from Employee where ACCOUNT_ID = @Account_Id";


            EmployeeModel model = new EmployeeModel();
            try
            {
                model = conn.Query<EmployeeModel>(sql, new { Account_Id }).FirstOrDefault();
            }
            catch (Exception e)
            {
                ;
            }

            return model;
        }

        public IEnumerable<PopularityModel> GetBranchMonthlyPopularity()
        {
            string sql = @"select stats.Month, stats.Branch_Id, b.Branch_Province ,avg(stats.FeedBack_Rating) as avg_rating from (
                            Select extract(MONTH from datetime) as Month, Branch_ID, Feedback_rating from feedback
                            )stats
                            join branch b on stats.Branch_Id = b.Branch_Id
                            group by  month, branch_id
                            ;
                            ";


            IEnumerable<PopularityModel> model = Enumerable.Empty<PopularityModel>();
            try
            {
                model = conn.Query<PopularityModel>(sql );
            }
            catch (Exception e)
            {
                ;
            }

            return model;
        }

        public IEnumerable<PopularityModel> GetBranchPopularity()
        {
            string sql = @"Select b.Branch_ID, b.branch_description, b.branch_province, avg(Feedback_rating)as avg_rating  from feedback f
                            join branch b on f.Branch_Id = b.Branch_Id
                            group by b.Branch_ID, b.branch_description, b.branch_province
                            ";

            IEnumerable<PopularityModel> model = Enumerable.Empty<PopularityModel>();
            try
            {
                model = conn.Query<PopularityModel>(sql);
            }
            catch (Exception e)
            {
                ;
            }

            return model;
        }

        public IEnumerable<PopularityModel> GetBranchItemPopularity()
        {
            string sql = @"select MI.MENU_Name as Menu_Desc, B.Branch_Id, B.BRANCH_PROVINCE, count(TI.MENU_ID) as Item_Count
                             from transaction_item TI
		                        inner join menu_item MI on MI.Menu_Id = TI.Menu_Id
		                        join transaction T on T.Trans_Id = TI.Trans_Id
		                        join Branch B on B.Branch_Id = T.Branch_Id
                             group by MI.MENU_DESC, B.Branch_Id, B.BRANCH_PROVINCE
                        ";

            IEnumerable<PopularityModel> model = Enumerable.Empty<PopularityModel>();
            try
            {
                model = conn.Query<PopularityModel>(sql);
            }
            catch (Exception e)
            {
                ;
            }

            return model;
        }
        // UPDATE BRANCH INFO
        public bool UpdateBranch(BranchModel model)
        {

            bool success = false;
            conn.Open();
            using (IDbTransaction trans = conn.BeginTransaction())
            {
                string sql = @"UPDATE `halifaxdine`.`Branch`
                                  SET
                                  `BRANCH_City` = @Branch_City,
                                  `BRANCH_Province` = @Branch_Province,
                                  `BRANCH_Is_Best_Resturant` = @Branch_Is_Best_Resturant,
                                  `BRANCH_Tax` = @Branch_Tax,
                                  `BRANCH_Description` = @Branch_Description
                                  WHERE `BRANCH_ID` = @Branch_Id
                                  ";
                
                try
                {
                    success = 1 == conn.Execute(sql, new { model.Branch_Id, model.Branch_City, model.Branch_Description, model.Branch_Is_Best_Resturant, model.Branch_Province, model.Branch_Tax });
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


        public bool UpdateEmployee(EmployeeModel model)
        {
            bool success = false;
            conn.Open();
            using (IDbTransaction trans = conn.BeginTransaction())
            {
                string sql = @"UPDATE `halifaxdine`.`employee`
                                SET
                                `PRIVILEGE_ID` = @Privilege_Id,
                                `BRANCH_ID` = @Branch_Id,
                                `EMP_FNAME` = @Emp_FName,
                                `EMP_LNAME` = @Emp_LName,
                                `EMP_HOURLY_RATE` = @Emp_Hourly_Rate,
                                `EMP_HIRE_DATE` = @Emp_Hire_Date,
                                `EMP_EMAIL` = @Emp_Email,
                                `EMP_PHONE` = @Emp_Phone
                                WHERE `EMP_ID` = @Emp_Id;";

                try
                {
                    success = 1 == conn.Execute(sql, new { model.Emp_Id, model.Privilege_Id, model.Branch_Id, model.Emp_Email, model.Emp_FName, model.Emp_LName, model.Emp_Phone, model.Emp_Hourly_Rate, model.Emp_Hire_Date });
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

        public bool InsertFeedbackRow(FeedbackModel model)
        {
            bool success = false;
            conn.Open();
            using (IDbTransaction trans = conn.BeginTransaction())
            {
                string sql = @"INSERT INTO `halifaxdine`.`feedback`
                                (`FEEDBACK_ID`,
                                `CLIENT_ID`,
                                `BRANCH_ID`,
                                `FEEDBACK_COMMENT`,
                                `FEEDBACK_RATING`,
                                `DATETIME`)
                                VALUES (DEFAULT, @Client_Id, @Branch_Id, @Feedback_Comment, @Feedback_Rating,@DateTime)";

                try
                {
                    success = 1 == conn.Execute(sql, new { model.Client_Id, model.Feedback_Comment, model.Branch_Id, model.Feedback_Rating, model.DateTime });
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

        public bool InsertEmployeeRow(EmployeeModel model, Role role)
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
                            `EMP_PHONE`,
                            `ACCOUNT_ID`)
                            VALUES
                            (DEFAULT,
                            @PRIVILEGE_ID,
                            @BRANCH_ID,
                            @EMP_FNAME,
                            @EMP_LNAME,
                            @EMP_HOURLY_RATE,
                            @EMP_HIRE_DATE,
                            @EMP_EMAIL,
                            @EMP_PHONE,
                            @ACCOUNT_ID) ";

                try
                {
                    success = 1 == conn.Execute(sql, new { model.Branch_Id, model.Emp_Email, model.Emp_FName, model.Emp_Hire_Date, model.Emp_Hourly_Rate, model.Emp_LName, model.Emp_Phone, Privilege_Id = role, model.Account_Id }, trans);
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


        public bool DeleteClientItemRow(int Trans_Id, int Menu_Id)
        {

            bool success = false;
            conn.Open();
            using (IDbTransaction trans = conn.BeginTransaction())
            {
                string sql = @"delete from `halifaxdine`.`transaction_item`
                                  WHERE `MENU_ID` = @Menu_Id
                                    and `TRANS_ID` = @Trans_Id
                                    limit 1
                                  ";

                try
                {
                    success = 1 == conn.Execute(sql, new { Menu_Id, Trans_Id });
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



        public bool InsertMenuItemRow(MenuItemModel model)
        {

            bool success = false;
            conn.Open();
            using (IDbTransaction trans = conn.BeginTransaction())
            {
                string sql = @"INSERT INTO `halifaxdine`.`menu_item`
                                (`MENU_ID`,
                                `MENU_PRICE`,
                                `MENU_DESC`,
                                `MENU_NAME`)
                                VALUES
                                (Default,
                                @Menu_Price,
                                @Menu_Desc,
                                @Menu_Name)";

                try
                {
                    success = 1 == conn.Execute(sql, new { model.Menu_Price,model.Menu_Name, model.Menu_Desc });
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

        public bool UpdateMenuItemRow(MenuItemModel model)
        {

            bool success = false;
            conn.Open();
            using (IDbTransaction trans = conn.BeginTransaction())
            {
                string sql = @"UPDATE `halifaxdine`.`menu_item`
                                  SET
                                  `MENU_PRICE` = @Menu_Price,
                                  `MENU_DESC` = @Menu_Desc,
                                  `MENU_NAME` = @Menu_Name
                                  WHERE `MENU_ID` = @Menu_Id
                                  ";

                try
                {
                    success = 1 == conn.Execute(sql, new { model.Menu_Id, model.Menu_Price, model.Menu_Name, model.Menu_Desc });
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

        public bool DeleteMenuItemRow(MenuItemModel model)
        {

            bool success = false;
            conn.Open();
            using (IDbTransaction trans = conn.BeginTransaction())
            {
                string sql = @"delete from `halifaxdine`.`menu_item`
                                  WHERE `MENU_ID` = @Menu_Id
                                  ";

                try
                {
                    success = 1 == conn.Execute(sql, new { model.Menu_Id });
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


        public IEnumerable<IngredientModel> GetIngredientData()
        {
            string sql = "select * from Ingredient";


            IEnumerable<IngredientModel> model = Enumerable.Empty<IngredientModel>();
            try
            {
                model = conn.Query<IngredientModel>(sql);
            }
            catch (Exception e)
            {
                ;
            }

            return model;
        }
        //GET: menu_ingredient_item for chef
        public IEnumerable<MenuIngredientModel> GetMenuIngredientData()
        {
            string sql = "select * from MENU_Ingredient";


            IEnumerable<MenuIngredientModel> model = Enumerable.Empty<MenuIngredientModel>();
            try
            {
                model = conn.Query<MenuIngredientModel>(sql);
            }
            catch (Exception e)
            {
                ;
            }

            return model;
        }
    }
}