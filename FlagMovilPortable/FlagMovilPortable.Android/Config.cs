using FlagMovilPortable.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(FlagMovilPortable.Droid.Config))]

namespace FlagMovilPortable.Droid
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
                    directoryDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }

                return directoryDB;
            }
        }
    }
}