﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;


namespace ImageService.Commands
{
    class GetConfigCommand : ICommand
    {
   
        /// <summary>
        /// AppCongigSettings-recived operation from new client to send him current settings.
        /// </summary>
        /// <param name="args">args for the command</param>
        /// <param name="result">reference to result flag to update</param>
        /// <returns>an app config settings in json format)</returns>
        public string Execute(string[] args, out bool result)
        {
            result = true;
            return AppCongigSettings.Instance.ToJSON();
        }
    }
}
