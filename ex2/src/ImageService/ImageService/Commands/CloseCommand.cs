﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;


namespace ImageService.ImageService.Commands
{
    class CloseCommand : ICommand
    {
        /// <summary>
        /// NewFile recevied into logged folder, we need to execute something (in this case AddFile method)
        /// </summary>
        /// <param name="args">args for the command</param>
        /// <param name="result">reference to result flag to update</param>
        /// <returns>an *error message* / image path (if you use it it will open the image)</returns>
        public string Execute(string[] args, out bool result)
        {
            result = true;
            return "nooooo";
        }
    }
}