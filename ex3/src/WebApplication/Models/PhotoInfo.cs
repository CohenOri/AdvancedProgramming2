using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class PhotoInfo
    {
        public string Path { get; }
        public string TimeTaken { get; }
        public string Name { get; }

        public PhotoInfo (string path, string timeTaken, string name)
        {
            this.Path = path;
            //this.Path = ".." + "exp_outputs\2018\6\a (1).png";
            this.TimeTaken = timeTaken;
            this.Name = name;
        }

    }
}