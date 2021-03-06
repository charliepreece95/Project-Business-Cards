﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class clsUserCollection
    {

        //private data member that stores the count of records found
        private Int32 recordCount;
        //create a private list data member to store the data from the database
        private List<clsUser> userList = new List<clsUser>();
        //private data member to connect to the database
        private clsDataConnection myDB = new clsDataConnection();



        //public property returning the count of records
        public Int32 Count
        {
            get
            {
                //return record count;
                return recordCount;
            }
        }

        //public list of users
        public List<clsUser> Users
        {
            //getter for the property
            get
            {
                //return the list of users
                return userList;
            }
        }

        public void FindAllUsers()
        {
            //re-set the connection
            myDB = new clsDataConnection();
            //var to store the index
            Int32 Index = 0;
            //var to store the user number of the current record
            Int32 UserNo;
            //var to flag that user was found
            Boolean UserFound;
            //execute the stored procedure
            myDB.Execute("sproc_tblUser_SelectAll");
            //get the count of records
            recordCount = myDB.Count;
            //while there are still records to process
            while (Index < myDB.Count)
            {
                //create an instance of the user class
                clsUser NewUser = new clsUser();
                //get the user number from the database
                UserNo = Convert.ToInt32(myDB.DataTable.Rows[Index]["UserNo"]);
                //find the user by invoking the find method
                UserFound = NewUser.Find(UserNo);
                if (UserFound == true)
                {
                    //add the user to the list
                    userList.Add(NewUser);
                }
                //increment the index
                Index++;
            }
        }
    }
}
