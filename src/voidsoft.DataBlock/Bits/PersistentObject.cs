        #region syncronization

        ///// <summary>
        ///// 
        ///// </summary>
        //protected virtual int Synchronize(TableMetadata[] data)
        //{
        //    List<ExecutionQuery> listQueries = null;
        //    IDbConnection iconnection = null;
        //    IDbCommand icommand = null;

        //    int resultCounter = 0;
        //    //list of indexes used to keep the index of first TableMetadata generated ExecutionQuery

        //    try
        //    {
        //        listQueries = new List<ExecutionQuery>();
        //        DataFactory.InitializeConnection(this.database, ref iconnection);
        //        DataFactory.InitializeCommand(this.database, ref icommand);

        //        iconnection.ConnectionString = this.connectionString;
        //        icommand.Connection = iconnection;

        //        foreach (TableMetadata currentObject in data)
        //        {
        //            switch (currentObject.State)
        //            {   
        //                case ObjectState.Unchanged:
        //                    continue;

        //                case ObjectState.Deleted:
        //                   resultCounter = this.Delete(currentObject);  
        //                    break;

        //                case ObjectState.Modified:
        //                   resultCounter = this.Update(currentObject);
        //                    break;

        //                case ObjectState.New:
        //                   resultCounter = this.Create(currentObject);
        //                    break;
        //            }

        //            //change the state
        //            currentObject.State = ObjectState.Unchanged;
        //        }

        //        return resultCounter;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogMessage(ex.Message + " " + ex.StackTrace);
        //        throw new DataBlockException(ex.Message,ex);
        //    }
        //    finally
        //    {
        //        if (iconnection != null && iconnection.State != ConnectionState.Closed)
        //        {
        //            iconnection.Close();
        //        }

        //        if (icommand != null)
        //        {
        //            icommand.Dispose();
        //        }
        //    }
        //}

        #endregion