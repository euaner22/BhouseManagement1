﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace housemanagement1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class bhousemanagementEntities2 : DbContext
    {
        public bhousemanagementEntities2()
            : base("name=bhousemanagementEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminAccounts> AdminAccounts { get; set; }
        public virtual DbSet<CustomerAccount> CustomerAccount { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
    
        public virtual int InsertReservation(Nullable<System.DateTime> startTime, Nullable<System.DateTime> endTime, Nullable<int> roomId)
        {
            var startTimeParameter = startTime.HasValue ?
                new ObjectParameter("StartTime", startTime) :
                new ObjectParameter("StartTime", typeof(System.DateTime));
    
            var endTimeParameter = endTime.HasValue ?
                new ObjectParameter("EndTime", endTime) :
                new ObjectParameter("EndTime", typeof(System.DateTime));
    
            var roomIdParameter = roomId.HasValue ?
                new ObjectParameter("RoomId", roomId) :
                new ObjectParameter("RoomId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertReservation", startTimeParameter, endTimeParameter, roomIdParameter);
        }
    
        public virtual ObjectResult<MultiLogin1_Result> MultiLogin1(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MultiLogin1_Result>("MultiLogin1", usernameParameter, passwordParameter);
        }
    
        public virtual int SavePayment1(string cardHolderName, Nullable<int> paymentAmount, string expiryMonth)
        {
            var cardHolderNameParameter = cardHolderName != null ?
                new ObjectParameter("CardHolderName", cardHolderName) :
                new ObjectParameter("CardHolderName", typeof(string));
    
            var paymentAmountParameter = paymentAmount.HasValue ?
                new ObjectParameter("PaymentAmount", paymentAmount) :
                new ObjectParameter("PaymentAmount", typeof(int));
    
            var expiryMonthParameter = expiryMonth != null ?
                new ObjectParameter("ExpiryMonth", expiryMonth) :
                new ObjectParameter("ExpiryMonth", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SavePayment1", cardHolderNameParameter, paymentAmountParameter, expiryMonthParameter);
        }
    
        public virtual int usp_Login(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_Login", usernameParameter, passwordParameter);
        }
    
        public virtual int SavePayment(string cardHolderName, Nullable<decimal> paymentAmount, Nullable<int> expiryMonth)
        {
            var cardHolderNameParameter = cardHolderName != null ?
                new ObjectParameter("CardHolderName", cardHolderName) :
                new ObjectParameter("CardHolderName", typeof(string));
    
            var paymentAmountParameter = paymentAmount.HasValue ?
                new ObjectParameter("PaymentAmount", paymentAmount) :
                new ObjectParameter("PaymentAmount", typeof(decimal));
    
            var expiryMonthParameter = expiryMonth.HasValue ?
                new ObjectParameter("ExpiryMonth", expiryMonth) :
                new ObjectParameter("ExpiryMonth", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SavePayment", cardHolderNameParameter, paymentAmountParameter, expiryMonthParameter);
        }
    
        public virtual ObjectResult<sp_GetAllReservations_Result> sp_GetAllReservations()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAllReservations_Result>("sp_GetAllReservations");
        }
    }
}
