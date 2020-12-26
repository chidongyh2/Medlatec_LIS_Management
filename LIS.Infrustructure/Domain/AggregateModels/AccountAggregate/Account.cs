﻿using LIS.Infrastructure.SeedWorks;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIS.Infrastructure.Domain.AccountAggregate
{
    public class Account : IdentityUser<Guid>, IAggregateRoot
    {
        public bool IsActive { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string ProfileImageUrl { get; private set; }
        public string Address { get; private set; }
        public Guid TenantId { get; private set; }
        public bool IsDelete { get; private set; }

        [MaxLength(16)]
        public byte[] PasswordSalt { get; private set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        public virtual IList<AccountRole> AccountRoles { get; private set; } = new List<AccountRole>();
        public virtual IList<UserConnection> UserConnections { get; private set; } = new List<UserConnection>();
        public virtual IList<UserSetting> UserSettings { get; private set; } = new List<UserSetting>();

        public Account()
        {

        }

        public Account(string email, string firstName, string lastName, string profileImageUrl, string address, bool isActive) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            ProfileImageUrl = profileImageUrl;
            IsActive = isActive;
            Email = email;
            UserName = email;
            Address = address;
        }

        public Account(string email, string firstName, string lastName, string profileImageUrl, string phoneNumber, string address, bool isActive) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            ProfileImageUrl = profileImageUrl;
            IsActive = isActive;
            Email = email;
            UserName = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public Account(string email, string firstName, string lastName, string profileImageUrl, string phoneNumber, string address, bool isActive, string passwordHash, byte[] passwordSalt) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            ProfileImageUrl = profileImageUrl;
            IsActive = isActive;
            Email = email;
            UserName = email;
            PhoneNumber = phoneNumber;
            Address = address;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;

        }

        public void UpdateInfo(string firstName, string lastName, string profileImageUrl, string phoneNumber, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            ProfileImageUrl = profileImageUrl;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public void ConfirmEmail()
        {
            EmailConfirmed = true;
        }

        public void Update(string firstName, string lastName, string profileImageUrl, string phoneNumber, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            ProfileImageUrl = profileImageUrl;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public void SetActive(bool active)
        {
            IsActive = active;
        }

        public void SetDelete()
        {
            IsDelete = true;
        }
        public void SetRole(Guid roleId)
        {
            AccountRoles.Add(new AccountRole(Id, roleId));
        }
    }
}