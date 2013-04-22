//*

//       file: CacheManager.cs
	   
//description: A "in memory" cache. Supports 2 cache cleaning policies : 
//             - periodically (at xx seconds after the item expires).
//             - specific time. 

//             The data structure used for caching is a hashtable. The key is held into 
//             the hashtable 	and the cached item is held into a CacheItemEntry.

//             If a item exists in the cache an you add another with the same key the
//             first item is simply overriden. If the item has the expiration policy 
//             set to "periodically" the expiration will be extended.

			 
//      notes: - By default the cache cleaning is done automatically at every 30 seconds.
//               The value can be modified by setting the CleaningInterval

//     author: Marius Gheorghe.

//*/


//using System;
//using System.Collections;
//using System.Threading;
//using voidsoft.DataBlock.Cache;



//namespace voidsoft.DataBlock.Cache
//{

//    /// <summary>
//    /// Summary description for CacheManager.
//    /// </summary>
//    public class CacheManager
//    {

//        #region fields

//        /// <summary>
//        /// Data structure used to hold the items of the cache.
//        /// </summary>
//        private static Hashtable htData;

//        /// <summary>
//        /// TimeSpan used to spawn the cleaning thread.
//        /// </summary>
//        private static TimeSpan span;


//        /// <summary>
//        /// Delegate used to handle the RemovedItem event
//        /// </summary>
//        public delegate void RemoveItemEventHandler(object key);
//        /// <summary>
//        /// Event which is raised when a item is removed form the cache.
//        /// </summary>
//        public static event RemoveItemEventHandler RemovedItem;

//        #endregion


//        #region constructor

//        /// <summary>
//        /// CacheManager constructor.
//        /// </summary>
//        static CacheManager()
//        {
//            htData = new Hashtable();
//            span = new TimeSpan(0,0,0,30,0); 

//            CacheManager.StartCleanerThread();
//        }
//        #endregion


//        #region public methods

//        /// <summary>
//        /// Add a new item to the cache with the expiration policy set to Periodically.
//        /// </summary>
//        /// <param name="key">Key</param>
//        /// <param name="value">Value</param>
//        /// <param name="time">TimeSpan for this</param>
//        public static void Add(object key, 
//                               object value,
//                               TimeSpan time)
//        {
//            try
//            {
//                int position = CacheManager.GetKeyIndex(key);

//                if(position == -1)
//                {
//                    CacheItemEntry item = new CacheItemEntry();
					
//                    item.EntryDate = DateTime.Now;
//                    item.ExpirationDate = DateTime.Now.Add(time);
//                    item.Value = value;
//                    item.Policy = ExpirationPolicy.Periodically;

//                    lock(CacheManager.htData.SyncRoot)
//                    {
//                        CacheManager.htData.Add(key , item);
//                    }
//                }
//                else
//                {
//                    CacheItemEntry item = new CacheItemEntry();

//                    item.EntryDate = DateTime.Now;
//                    item.ExpirationDate = DateTime.Now.Add(time);
//                    item.Value = value;
//                    item.Policy = ExpirationPolicy.Periodically;

//                    lock(CacheManager.htData)
//                    {
//                        CacheManager.htData[key] = item;
//                    }
//                }
//            }
//            catch(Exception ex)
//            {
//                throw ex;				
//            }
//        }

               
//        /// <summary>
//        /// Add a new item to the cache with the expiration policy set to SpecificTime.
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        /// <param name="expirationDate"></param>
//        public static void Add(object key,
//                               object value,
//                               DateTime expirationDate)
//        {
			
//            try
//            {
//                //check the expiration date
//                int dateCompare =  DateTime.Compare(expirationDate, DateTime.Now);

//                if(dateCompare < 0)
//                {
//                    throw new ArgumentException("Invalid expiration date");
//                }
			
//                //check if we already have a item with the specified key
//                int position = CacheManager.GetKeyIndex(key);

//                if(position == -1)
//                {
//                    CacheItemEntry item = new CacheItemEntry();
					
//                    item.EntryDate = DateTime.Now;
//                    item.ExpirationDate = expirationDate;
//                    item.Value = value;
//                    item.Policy = ExpirationPolicy.SpecificTime;

//                    lock(CacheManager.htData.SyncRoot)
//                    {
//                        CacheManager.htData.Add(key , item);
//                    }
//                }
//                else
//                {

//                    CacheItemEntry item = new CacheItemEntry();

//                    item.ExpirationDate = expirationDate;
//                    item.EntryDate = System.DateTime.Now;
//                    item.Value = value;
//                    item.Policy = ExpirationPolicy.SpecificTime;

//                    lock(CacheManager.htData)
//                    {
//                        CacheManager.htData[key] = item;
//                    }
//                }

//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }



//        /// <summary>
//        /// Add a new item to the cache with the expiration policy set to NeverExpire.
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        public static void Add(object key,
//                               object value)
//        {
//            try
//            {
//                //check if we already have a item with the specified key
//                int position = CacheManager.GetKeyIndex(key);

//                if(position == -1)
//                {
//                    CacheItemEntry item = new CacheItemEntry();
					
//                    item.EntryDate = DateTime.Now;
//                    item.Value = value;
//                    item.Policy = ExpirationPolicy.Never;

//                    lock(CacheManager.htData.SyncRoot)
//                    {
//                        CacheManager.htData.Add(key , item);
//                    }
//                }
//                else
//                {

//                    CacheItemEntry item = new CacheItemEntry();

//                    item.EntryDate = DateTime.Now;
//                    item.Value = value;
//                    item.Policy = ExpirationPolicy.Never;

//                    lock(CacheManager.htData)
//                    {
//                        CacheManager.htData[key] = item;
//                    }
//                }
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }




//        /// <summary>
//        /// Add a new item to the cache.
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        /// <param name="policy"></param>
//        /// <param name="span"></param>
//        public static void Add(object key,
//                               object value,
//                               ExpirationPolicy policy,
//                               TimeSpan span)
//        {
			
//            //check if we already have a item with the specified key
//            int position = CacheManager.GetKeyIndex(key);

//            if(position == -1)
//            {
//                CacheItemEntry item = new CacheItemEntry();
					
//                item.EntryDate = DateTime.Now;
//                item.Value = value;
//                item.Policy = policy;
//                item.ExpirationDate = DateTime.Now.Add(span);

//                lock(CacheManager.htData.SyncRoot)
//                {
//                    CacheManager.htData.Add(key , item);
//                }
//            }
//            else
//            {

//                CacheItemEntry item = new CacheItemEntry();

//                item.EntryDate = DateTime.Now;
//                item.Value = value;
//                item.Policy = policy;
//                item.ExpirationDate = DateTime.Now.Add(span);

//                lock(CacheManager.htData)
//                {
//                    CacheManager.htData[key] = item;
//                }
//            }




			
////			CacheItemEntry item = new CacheItemEntry();
////			item.Value = value;
////			item.Policy = policy;
////			item.EntryDate = DateTime.Now;
//        }



//        /// <summary>
//        /// Removes the data from the cache.
//        /// </summary>
//        /// <param name="index">Index of the item which will be removed</param>
//        public static void Remove(int index)
//        {
//            try
//            {
//                if(index < 0 || index > CacheManager.htData.Count)
//                {
//                    throw new IndexOutOfRangeException("Invalid index");
//                }

//                int position = -1;
//                object key = null;

                
//                lock(CacheManager.htData.SyncRoot)
//                {
//                    IDictionaryEnumerator ienum = htData.GetEnumerator();

//                    while(ienum.MoveNext())
//                    {
//                        ++position;

//                        if(position == index)
//                        {
//                            key = ienum.Key;
//                            break;
//                        }
//                    }

//                    if(key != null)
//                    {
//                        CacheManager.htData.Remove(key);

//                        if(CacheManager.RemovedItem != null)
//                        {
//                            CacheManager.RemovedItem(key);
//                        }
//                    }
//                }
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }


	
		
//        /// <summary>
//        /// Removes the data from the cache.
//        /// </summary>
//        /// <param name="key">Key of the item which will be removed</param>
//        public static void Remove(object key)
//        {
			
//            if(CacheManager.Contains(key))
//            {
//                try
//                {
//                    lock(CacheManager.htData.SyncRoot)
//                    {
//                        CacheManager.htData.Remove(key);

//                        //raise the event
//                        if(CacheManager.RemovedItem != null)
//                        {
//                            CacheManager.RemovedItem(key);
//                        }
//                    }
//                }
//                catch(Exception ex)
//                {
//                    throw ex;
//                }
//            }
//            else
//            {
//                throw new ArgumentException("Invalid key");
//            }
//        }



//        /// <summary>
//        /// Check if the cache contains a item with the specified key
//        /// </summary>
//        /// <param name="key">Key.</param>
//        /// <returns></returns>
//        public static bool Contains(object key)
//        {
//            try
//            {
//                lock(CacheManager.htData.SyncRoot)
//                {
//                    IEnumerator ienum = CacheManager.htData.Keys.GetEnumerator();

//                    while(ienum.MoveNext())
//                    {
//                        if(ienum.Current == key)
//                        {
//                            return true;
//                        }
//                    }

//                    return false;
//                }
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }



//        /// <summary>
//        /// Clears the cache.
//        /// </summary>
//        public static void Clear()
//        {
//            try
//            {
//                lock(CacheManager.htData.SyncRoot)
//                {
//                    CacheManager.htData.Clear();
//                }
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }




//        /// <summary>
//        /// Returns the key at the specified index
//        /// </summary>
//        /// <param name="index"></param>
//        /// <returns></returns>
//        public static object GetKey(int index)
//        {
//            if(index < 0 || index > CacheManager.htData.Count)
//            {
//                throw new IndexOutOfRangeException("Invalid index");
//            }
//            else
//            {
//                try
//                {
//                    lock(CacheManager.htData.SyncRoot)
//                    {
//                        IDictionaryEnumerator ienum = CacheManager.htData.GetEnumerator();

//                        int position = -1;

//                        while(ienum.MoveNext())
//                        {
//                            if(position == index)
//                            {
//                                return ienum.Key;
//                            }
//                        }
//                    }
//                }
//                catch(Exception ex)
//                {
//                    throw ex;
//                }
//            }
//            return null;
//        }



//        /// <summary>
//        /// Returns the value at the specified index
//        /// </summary>
//        /// <param name="index"></param>
//        /// <returns></returns>
//        public static object GetValue(int index)
//        {
//            if(index < 0 || index > CacheManager.htData.Count)
//            {
//                throw new IndexOutOfRangeException("Invalid index");
//            }
//            else
//            {
//                try
//                {
//                    lock(CacheManager.htData.SyncRoot)
//                    {
//                        IDictionaryEnumerator ienum = CacheManager.htData.GetEnumerator();

//                        int position = -1;

//                        while(ienum.MoveNext())
//                        {
//                            if(position == index)
//                            {
//                                return ((CacheItemEntry) ienum.Value).Value;
//                            }
//                        }
//                    }
//                }
//                catch(Exception ex)
//                {
//                    throw ex;
//                }
//            }
//            return null;
//        }



//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public static object GetValue(object key)
//        {
//            int index = CacheManager.GetKeyIndex(key);

//            if(index == -1)
//            {
//                throw new ArgumentException("invalid key");
//            }
//            else
//            {
//                return GetValue(index);
//            }
//        }



//        #endregion


//        #region indexers

//        /// <summary>
//        /// Gets/sets the value at the specified index
//        /// </summary>
//        public object this [int index]
//        {
//            get
//            {
//                if(index < 0 || index > CacheManager.htData.Count )
//                {
//                    throw new IndexOutOfRangeException("Invalid index");
//                }

//                int position = -1;

//                lock(CacheManager.htData.SyncRoot)
//                {
//                    IEnumerator ienum = CacheManager.htData.GetEnumerator();

//                    while(ienum.MoveNext())
//                    {
//                        ++position;

//                        if(position == index)
//                        {
//                            return ((CacheItemEntry) ienum.Current).Value;
//                        }
//                    }
//                }

//                return null;
//            }

//            set
//            {
//                if(index < 0 || index > CacheManager.htData.Count )
//                {
//                    throw new IndexOutOfRangeException("Invalid index");
//                }

//                int position = -1;

//                lock(CacheManager.htData.SyncRoot)
//                {

//                    IDictionaryEnumerator ienum = CacheManager.htData.GetEnumerator();

//                    while(ienum.MoveNext())
//                    {
//                        ++position;
						
//                        if(position == index)
//                        {
//                            ((CacheItemEntry) CacheManager.htData[ienum.Key]).Value = value;
//                            break;
//                        }
//                    }
//                }
//            }

//        }

		
		
//        #endregion


//        #region properties
//        /// <summary>
//        /// Gets the number of items from the cache.
//        /// </summary>
//        public static int Count
//        {
//            get
//            {
//                lock(CacheManager.htData.SyncRoot)
//                {
//                    return CacheManager.htData.Count;
//                }
//            }
//        }


//        /// <summary>
//        /// Get/sets the time interval at which the cache cleaning is done.
//        /// </summary>
//        public static TimeSpan CleaningInterval
//        {
//            get
//            {
//                return span;
//            }
//            set
//            {
//                CacheManager.span = value;	
//            }
//        }


//        #endregion


//        #region private implementation
//        /// <summary>
//        /// Returns the index of the specified key
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        internal static int GetKeyIndex(object key)
//        {
//            int position = -1;

//            try
//            {
//                bool founded = false;

//                lock(CacheManager.htData.SyncRoot)
//                {

//                    IDictionaryEnumerator ienum = CacheManager.htData.GetEnumerator();

//                    while(ienum.MoveNext())
//                    {
//                        ++position;

//                        if(ienum.Key == key)
//                        {
//                            founded = true;
//                            break;
//                        }
//                    }
//                }

//                if(founded)
//                {
//                    return position;
//                }
//                else
//                {
//                    return -1;
//                }
//            }
//            catch
//            {
//                return -1;				
//            }
//        }

//        #endregion
	

//        #region cache cleaner

//        /// <summary>
//        /// Starts the cache cleaner thread.
//        /// </summary>
//        private static void StartCleanerThread()
//        {
//            //System.Threading.ThreadPool.QueueUserWorkItem(new WaitCallback(ClearCache));
		
//            System.Threading.Thread thCleaner = new Thread(new ThreadStart(ClearCache));
//            thCleaner.Name = "CleanerThread";
//            thCleaner.IsBackground = true;
//            thCleaner.Start();
//        }


//        /// <summary>
//        /// Clears the cache of expired items.
//        /// </summary>
//        private static void ClearCache()
//        {

//            //data structure used to hold the items which will be removed from cache
//            ArrayList alData = null;

//            try
//            {

//                bool flag = true;

//                alData = new ArrayList();


//                //endless loop. The thread will wake up and clean the cache periodically.
//                //The timp span can be specified with the CleaningInterval property
//                while(flag)
//                {
//                    lock(CacheManager.htData.SyncRoot)
//                    {
//                        IDictionaryEnumerator ienum = CacheManager.htData.GetEnumerator();

//                        while(ienum.MoveNext())
//                        {
//                            CacheItemEntry item = (CacheItemEntry) ienum.Value;

//                            if(item.Policy != ExpirationPolicy.Never)
//                            {
//                                int intermediateValue = DateTime.Compare(DateTime.Now, item.ExpirationDate);

//                                if(intermediateValue  > 0 )
//                                {
//                                    alData.Add(ienum.Key);
//                                }
//                            }
//                        }

//                        for(int i = 0 ; i < alData.Count; i++)
//                        {
//                            CacheManager.htData.Remove(alData[i]);
							
//                            if(CacheManager.RemovedItem != null)
//                            {
//                                CacheManager.RemovedItem(alData[i]);
//                            }
//                        }
//                    }

//                    //clear the data
//                    alData.Clear();

//                    //sleep
//                    Thread.Sleep(CacheManager.span);
//                }
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                if(alData != null)
//                {
//                    alData.Clear();
//                }
//            }
//        }

//        #endregion

//    }
//}
