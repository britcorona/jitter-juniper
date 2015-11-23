using System;
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
            // Select * from JitterUsers;
            var query = from users in _context.JitterUsers select users; //This is a table. The users is just a name, I could name it anything such as shoe. Selecting things from a database and returning it to a list.
            return query.ToList();
        }
    }
}