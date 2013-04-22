//*


//       file: CacheItemEntry.cs
//description: Structure used to hold the value which will be cached.
//     author: Marius Gheorghe


//*/

//using System;


//namespace voidsoft.DataBlock.Cache
//{

//    /// <summary>
//    /// Represents a cached item entry
//    /// </summary>
//    internal class CacheItemEntry
//    {
		
//        private object item;
//        private DateTime entryDate;
//        private ExpirationPolicy policy;
//        private DateTime expirationDate;


//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="value">Value which will be cached</param>
//        /// <param name="entryDate">Item's entry date into the cache</param>
//        /// <param name="policy">Item's expiration policy</param>
//        /// <param name="expirationDate">Item's expiration date. This isn't  </param>
//        public CacheItemEntry(object value,
//                              DateTime entryDate,
//                              ExpirationPolicy policy,
//                              DateTime expirationDate)
//        {
		
//            this.item = value;
//            this.entryDate = entryDate;
//            this.policy = policy;
//            this.expirationDate = expirationDate;
//        }


//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public CacheItemEntry()
//        {
			
//        }

//        /// <summary>
//        /// Get/sets the current value
//        /// </summary>
//        public object Value
//        {
//            get
//            {
//                return item;
//            }

//            set
//            {
//                item = value;
//            }
//        }


//        /// <summary>
//        /// Get/sets the item's entry date
//        /// </summary>
//        public DateTime EntryDate
//        {
//            get
//            {
//                return entryDate;
//            }
//            set
//            {
//                entryDate = value;
//            }
//        }



//        /// <summary>
//        /// Get/sets the items expiration policy
//        /// </summary>
//        public ExpirationPolicy Policy
//        {
//            get
//            {
//                return policy;
//            }

//            set
//            {
//                policy = value;
//            }
//        }


//        /// <summary>
//        /// Gets/sets the item's expiration date
//        /// </summary>
//        public DateTime ExpirationDate
//        {
//            get
//            {
//                return expirationDate;
//            }

//            set
//            {
//                expirationDate = value;
//            }
//        }
        

//    }
//}
