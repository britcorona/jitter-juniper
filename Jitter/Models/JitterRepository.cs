﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jitter.Models
{
    public class JitterRepository
    {
        private JitterContext _context;
        public JitterContext Context { get {return _context;}}

        public JitterRepository()
        {
            _context = new JitterContext();
        }

        public JitterRepository(JitterContext a_context)
        {
            _context = a_context;
        }

        public List<JitterUser> GetAllUsers()
        {
            var query = from users in _context.JitterUsers select users;
            return query.ToList();
        }

        public JitterUser GetUserByHandle(string handle)
        {
            //How it would look in SQL: SELECT * FROM JitterUsers WHERE JitterUser.Handle = handle. Select is always put at the end when writing it out below.
            var query = from user in _context.JitterUsers where user.Handle == handle select user;
            //This one way: IQueryable<JitterUser> query = from user in _context.JitterUsers where user.Handle == handle select user;
            //We need to make sure there is exaclty one user returned.

            //return query.Single(); //query.Single<JitterUser>(); //JitterUser is grayed out here because it already knew that it needed JitterUser.
            return query.SingleOrDefault();
        }

        public bool IsHandleAvailable(string handle)
        {
            bool available = false;
            try
            {
                JitterUser some_user = GetUserByHandle(handle);
                if (some_user == null)
                {
                    available = true;
                }
            }
            catch (InvalidOperationException) //From the test
            {

                //Nothing needs to be put here because its already set to false on line 42
            }
            return available;
        }

        public List<JitterUser> SearchByHandle(string handle)
        {
            //How it would look in SQL: SELECT * FROM JitterUsers AS users WHERE users.Handle LIKE '%handle%'
            var query = from user in _context.JitterUsers select user;
            List<JitterUser> found_users = query.Where(user => user.Handle.Contains(handle)).ToList();
            found_users.Sort();
            return found_users;
        }
    }
}