using System;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using SpringHeroBank.controller;
using SpringHeroBank.entity;
using SpringHeroBank.helper;
using SpringHeroBank.view;

namespace SpringHeroBank
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ApplicationMenu applicationMenu = new ApplicationMenu();
            applicationMenu.menu();
        }
    }
}