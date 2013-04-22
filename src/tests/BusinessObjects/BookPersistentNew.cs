using System;
using System.Collections.Generic;
using System.Text;
using Extender;
using voidsoft.DataBlock;


namespace BusinessObjects
{
    class BookPersistentNew 
    {
        private Book bok = new Book();
        private PersistentObject pojo;// = new PersistentObject(bok);


        public BookPersistentNew()
        {
            pojo = new PersistentObject(bok);
        }

        public BookPersistentNew(Session session)
        {
            pojo = new PersistentObject(session, bok);
        }

        public BookPersistentNew(DatabaseServer server, string connectionString)
        {
            pojo = new PersistentObject(server, connectionString, bok);
        }


        #region overrides

        public int Delete(QueryCriteria c)
        {
            return this.pojo.Delete(c);
        }

        public int Delete(Book bk)
        {
            return this.pojo.Delete(bk);
        }
        #endregion


    }
}
