using Azure;
using Azure.Data.Tables;
using System;

namespace st10269378.Models
{
    // customer profile model for storing customer information
    public class CustomerProfile : ITableEntity
    {
        // partition key for customer profiles (required for azure table)
        public string PartitionKey { get; set; }
        // row key for customer profiles (required for azure table)
        public string RowKey { get; set; }
        // timestamp for customer profiles (required for azure table)
        public DateTimeOffset? Timestamp { get; set; }
        // etag for customer profiles (required for azure table)
        public ETag ETag { get; set; }

        // customer first name
        public string FirstName { get; set; }
        // customer last name
        public string LastName { get; set; }
        // customer email
        public string Email { get; set; }
        // customer phone number
        public string PhoneNumber { get; set; }

        // initializes customer profile with default values
        public CustomerProfile()
        {
            // sets partition key to a constant value for all customer profiles
            PartitionKey = "CustomerProfile";
            // sets row key to a unique guid for each customer profile
            RowKey = Guid.NewGuid().ToString();
        }
    }
}