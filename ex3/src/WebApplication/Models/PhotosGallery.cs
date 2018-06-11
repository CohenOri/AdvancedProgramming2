using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace WebApplication.Models
{
    public class PhotosGallery
    {
        private List<PhotoInfo> photosList = new List<PhotoInfo>();
        public List<PhotoInfo> PhotoList { get { return photosList; } }

        private Settings settings;
        private static Regex r = new Regex(":");
        private static PhotosGallery instance = null;
        public static PhotosGallery Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PhotosGallery();
                }
                return instance;
            }
        }

        private PhotosGallery()
        {
            this.settings = Settings.Instance;
            //string outputFolder = this.settings.RelativePath;
            string outputFolder = this.settings.OutPutDur;


            CreatePhotosList(outputFolder + '\\' + "Thumbnails");
            /*foreach (PhotoInfo p in PhotoList)
            {
                Console.WriteLine("photo is:" + p.Path + "//" + p.Name + "//" + p.TimeTaken);
            }*/
        }

        private void CreatePhotosList(String searchFolder)
        {
            //searchFolder = " + searchFolder;
            //String searchFolder = @"C:\MyFolderWithImages";

            var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
            var files = GetFilesFrom(searchFolder, filters, true);
            foreach (String filePath in files)
            {
                // file exist, we need to find out photo "taken time" or at least creation time (which anyfile should have)
                DateTime date = new DateTime();
                string fileNameWithoutExtention = Path.GetFileNameWithoutExtension(filePath);
                string fileExtention = Path.GetExtension(filePath);
                string fullFileName = fileNameWithoutExtention + fileExtention;

                try
                {
                    date = GetDateTakenFromImage(filePath); // try to retrieve taken time
                }
                catch
                {
                    try
                    {
                        date = File.GetCreationTime(filePath); // try to retrieve created time
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Couldn't retrieve photo date: {0}", e.Message);
                        throw;
                    }
                }

                // add photo to photos list
                this.photosList.Add(new PhotoInfo(filePath, date.ToLongDateString(), fileNameWithoutExtention));


                //string relatviePath = ".." + filePath;
                //this.photosList.Add(new PhotoInfo(fullFileName, date.ToLongDateString(), fileNameWithoutExtention));

            }
        }

        /// <summary>
        /// Searches all .extension files in main only / main and sub directories @stackoverflow
        /// </summary>
        /// <param name="searchFolder">given main folder</param>
        /// <param name="filters">array which contains.extnsion names, eg: "jpg" "png" etc.</param>
        /// <param name="searchSubFolders">search also in subfolders or not</param>
        /// <returns>list with all paths to .extension files</returns>
        public String[] GetFilesFrom(String searchFolder, String[] filters, bool searchSubFolders)
        {
            List<string> filesFound = new List<String>();
            var searchOption = searchSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }

        /// <summary>
        /// retrieves the taken time of image WITHOUT loading the whole image (way faster) @ stackoverflow
        /// </summary>
        public static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }

        public string RemovePicFromComp(string path)
        {

            //return path;


            // iterate on the copy of the list and remove the photo with given path 
            // (in order to not edit + iterate on loop the same time)
            PhotoInfo photo = null;
            string temp = null;
            foreach (PhotoInfo p in photosList)
            {
                temp = p.RelativePath.Replace(@"\\", @"\");
                temp = temp.Replace(@"\", "/");
                temp = temp.Replace("~", "");
                //return path + "      p.path:      " + p.RelativePath + "yyy" + temp;                

                if (path == temp)
                {
                    photo = p;
                    break;
                }
            }
            
            if (photo != null)
            {
                photosList.Remove(photo);
                //return photo.Path;
                //return "im here?";
            }

            //return photo.Path;
              // remove thumbnail file from computer
              if (File.Exists(photo.Path))
              {
                  File.Delete(photo.Path);
              }


            // remove the "\\" + "Thumbnails" but keep the rest path
            string cleanPath = photo.Path.Replace(@"\\Thumbnails", "");
            //return cleanPath;
            // now remove actual image [not Thumbnail]
            if (File.Exists(cleanPath))
              {
                  File.Delete(cleanPath);
                return "succesfuly deleted files";
              }
            return "problem";
        }

        /*public static string StringWordsRemove(string stringToClean, string wordsToRemove)
        {
            string[] splitWords = wordsToRemove.Split(new Char[] { ' ' });
            string pattern = " (" + string.Join("|", splitWords) + ") ";
            string cleaned = Regex.Replace(stringToClean, pattern, " ");
            return cleaned;
        }*/

    }


}