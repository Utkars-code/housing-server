﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using webApi_build_Real.ApplictionContext;
using webApi_build_Real.Models;
using webApi_build_Real.Repository.implementation;

namespace webApi_build_Real.Repository.Adstract
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dc;
        public UserRepository(DataContext dc)
        {
            _dc = dc;
        }
        public async Task<Usercs> Authenticate(string userName, string passwordText)
        {
            var user= await _dc.Usercs.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null || user.PasswordKey == null)
            {
                return null;
                if (!MatchPasswordHash(passwordText, user.Password, user.PasswordKey))
                    return null;         
            }
            return user;
        }

        //using for matching password-->and hash code for security paspers.-
        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));
                for(int i=0;i<passwordHash.Length;i++) 
                {
                    if (passwordHash[i] != password[i])
                      return false;                               
                }
                return true;
            }
        }
          
        public void Register(string userName, string password)
        {
            byte[] passwordHash, passwordKey;
            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            Usercs user = new Usercs();
            user.UserName = userName;
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;
            _dc.Usercs.Add(user);
        }

       public  async Task<bool> UserAlreadyExists(string userName)
        {
            return await _dc.Usercs.AnyAsync(x =>x.UserName==userName);
        }
    }
}
