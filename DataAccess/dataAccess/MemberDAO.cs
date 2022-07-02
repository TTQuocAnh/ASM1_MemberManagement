
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObject;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class MemberDAO : BaseDAL
    {   


        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }

        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<MemberObject> GetMemberObjectsList()
        {
            IDataReader dataReader = null;
            string SQLselect = "Select MemberID, MemberName, Email,Password, City, Country from Member";
            var MemberList = new List<MemberObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLselect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    MemberList.Add(new MemberObject
                    {
                        MemberID = dataReader.GetString(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5),
                    });
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } finally
            {
                dataReader.Close();
                CloseConnection();
            }

            return MemberList;
        }
        
        

       

        /* public MemberObject GetMemberByMemberID(string memberID)
{
MemberObject memberObject = MemberList.SingleOrDefault(mem => mem.MemberID == memberID);
return memberObject;
}*/

        public MemberObject GetMemberByID(string MemberID)
        {
            MemberObject M = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select * from Member where MemberID = @MemberID";
            
            try
            {
                var param = dataProvider.CreateParameter("@MemberID", 50, MemberID, DbType.String);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    M = new MemberObject
                    {
                        MemberID = dataReader.GetString(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5)
                    };

                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } finally { dataReader.Close();
                CloseConnection();
            } return M;
        }
        //Add a new member
        public void AddNewMember(MemberObject addMem)
        {
            try {
                MemberObject mem = GetMemberByID(addMem.MemberID);
                if (mem == null)
                {
                    if (CheckEmailDuplicated(addMem.Email))
                    {
                        throw new Exception("Email is already used!");
                    }
                    else
                    {
                        string SQLInsert = "Insert Member values(@MemberID, @MemberName, @Email, @Password, @City, @Country)";
                        var parameter = new List<SqlParameter>();
                        parameter.Add(dataProvider.CreateParameter("@MemberID", 50, addMem.MemberID, DbType.String));
                        parameter.Add(dataProvider.CreateParameter("@MemberName", 50, addMem.MemberName, DbType.String));
                        parameter.Add(dataProvider.CreateParameter("@Email", 50, addMem.Email, DbType.String));
                        parameter.Add(dataProvider.CreateParameter("@Password", 50, addMem.Password, DbType.String));
                        parameter.Add(dataProvider.CreateParameter("@City", 50, addMem.City, DbType.String));
                        parameter.Add(dataProvider.CreateParameter("@Country", 50, addMem.Country, DbType.String));
                        dataProvider.Insert(SQLInsert, CommandType.Text, parameter.ToArray());

                    }

                }
                else if (mem != null)
                {
                    throw new Exception("Member is already exist!");
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            }


        //Update a member

        public void UpdateAMember(MemberObject updateMem)
        {
            try
            {
                MemberObject mem = GetMemberByID(updateMem.MemberID);
                if (mem != null)
                {
                    if (CheckUpdateEmailDuplicated(updateMem.MemberID, updateMem.Email))
                    {
                        throw new Exception("Email is already used!");
                    }
                    string SQLUpdate = "Update Member set MemberName = @MemberName, Email = @Email," +
                        "Password = @Password, City = @City, Country = @Country where MemberID = @MemberID";
                    var parameter = new List<SqlParameter>();
                    parameter.Add(dataProvider.CreateParameter("@MemberID", 50, updateMem.MemberID, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@MemberName", 50, updateMem.MemberName, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@Email", 50, updateMem.Email, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@Password", 50, updateMem.Password, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@City", 50, updateMem.City, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@Country", 50, updateMem.Country, DbType.String));
                    dataProvider.Update(SQLUpdate, CommandType.Text, parameter.ToArray());
                    
                }
                else
                {
                    throw new Exception("Member doesn't exist!");
                }
            }
            catch (Exception ex)
            {throw new Exception(ex.Message);
            }
            finally { CloseConnection(); }
        }

        //Remove a member

        public void RemoveAMember(string memberID)
        {
            try {
                MemberObject mem = GetMemberByID(memberID);
                if (mem != null)
                {
                    string SqlDelete = "Delete Member where MemberID = @MemberID ";
                    var param = dataProvider.CreateParameter("@MemberID", 50, memberID, DbType.String);
                    dataProvider.Delete(SqlDelete, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("Member doesn't exist!");
                }
            } 
            catch (Exception ex){
                throw new Exception(ex.Message);
            } 
            finally { CloseConnection(); }
        }
                
                public MemberObject GetMemberByEmail(string email)
                {
                     MemberObject n = null;
                     IDataReader dataReader = null;
                     string SQLSelect = "Select * from Member where Email = @Email";
                try
                {
                var param = dataProvider.CreateParameter("@Email", 50, @email, DbType.String);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    n = new MemberObject
                    {
                        MemberID = dataReader.GetString(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5)
                    };

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return n;
            
                }

                public Boolean CheckEmailDuplicated(string email)
                {
                    Boolean check;
                    MemberObject memberObject = GetMemberByEmail(@email);
                    if (memberObject == null)
                    {
                        check = false;
                    }
                    else
                    {
                        check = true;
                    }
                    return check;
                }

                public Boolean CheckUpdateEmailDuplicated(string userID, string email)
                {
                    Boolean check;
                    MemberObject memberObject = GetMemberByEmail(@email);
                    if (memberObject == null)
                    {
                        check = false;
                    }
                    else
                    {
                        if (memberObject.MemberID == userID)
                        {
                            check = false;
                        }
                        else
                        {
                            check = true;
                        }
                    }
                    return check;
                }

        public IEnumerable<MemberObject> SearchMemberByName(string memberName)
        {
            IDataReader dataReader = null;
            string SQLselect = "Select MemberID, MemberName, Email,Password, City, Country from Member";
            var MemberList = new List<MemberObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLselect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    MemberList.Add(new MemberObject
                    {
                        MemberID = dataReader.GetString(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5),
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }

            return (List<MemberObject>)MemberList.Where(m => m.MemberName.Contains(memberName)).ToList();
        }
        /*public List<MemberObject> SearchMemberByName(string memberName)
                {
                    return (List<MemberObject>)MemberList.Where(m => m.MemberName.Contains(memberName)).ToList();
                }*/

        public IEnumerable<MemberObject> SearchMemberByID(string memberID)
        {
            IDataReader dataReader = null;
            string SQLselect = "Select MemberID, MemberName, Email,Password, City, Country from Member";
            var MemberList = new List<MemberObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLselect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    MemberList.Add(new MemberObject
                    {
                        MemberID = dataReader.GetString(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5),
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }

            return (List<MemberObject>)MemberList.Where(m => m.MemberID.Contains(memberID)).ToList();
        }
        /*public List<MemberObject> SearchMemberByID(string memberID)
        {
            return (List<MemberObject>)MemberList.Where(m => m.MemberID.Contains(memberID)).ToList();
        }*/
        public IEnumerable<MemberObject> FilterMemberByCity(string city)
        {
            IDataReader dataReader = null;
            string SQLselect = "Select MemberID, MemberName, Email,Password, City, Country from Member";
            var MemberList = new List<MemberObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLselect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    MemberList.Add(new MemberObject
                    {
                        MemberID = dataReader.GetString(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5),
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }

            return (List<MemberObject>)MemberList.Where(m => m.City.Equals(city)).ToList();
        }
        
        public IEnumerable<MemberObject> FilterMemberByCountry(string country)
        {
            IDataReader dataReader = null;
            string SQLselect = "Select MemberID, MemberName, Email,Password, City, Country from Member";
            var MemberList = new List<MemberObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLselect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    MemberList.Add(new MemberObject
                    {
                        MemberID = dataReader.GetString(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5),
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }

            return (List<MemberObject>)MemberList.Where(m => m.Country.Equals(country)).ToList();
        }
       

            }
        }
    

