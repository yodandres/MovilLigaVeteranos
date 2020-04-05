using FlagMovilPortable.Interfaces;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(FlagMovilPortable.iOS.Config))]

namespace FlagMovilPortable.iOS
{
    public class Config : IConfig
    {
        private string directoryDB;

        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDB))
                {
                    var directory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    directoryDB = System.IO.Path.Combine(directory, "..", "Library");
                }

                return directoryDB;
            }
        }
    }
}