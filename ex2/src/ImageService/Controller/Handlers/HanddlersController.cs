﻿using Infrastructure.Enums;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller.Handlers
{
    class HanddlersController
    {
        private static HanddlersController instance;
        private HanddlersController() { }
        public static HanddlersController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HanddlersController();
                }
                return instance;
            }
        }
        public List<IDirectoryHandler> Handdlers { get; set; }
        public void AddHanler(IDirectoryHandler handler)
        {
            this.Handdlers.Add(handler);
        }
        /// <summary>
        /// find the handler to delete, and delete from list.
        /// </summary>
        /// <param name="path"> path of the folder handler listen</param>
        /// <returns>string to send back to client if item deleted or no such path</returns>
        public string DeleteHandller(string path)
        {
            IDirectoryHandler handletToDelete = null;
            string status = "no such path";
            foreach (IDirectoryHandler handler in this.Handdlers)
            {
                if(handler.GetDirectory().Equals(path))
                {
                    handletToDelete = handler;
                    handler.OnCommandRecieved(this, new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, null, path));
                    status = "Deleted seccsecfully!";
                    break;
                }
            }
            if(handletToDelete != null)
            {
                this.Handdlers.Remove(handletToDelete);
            }
            return status;
        }
    
    }
}
