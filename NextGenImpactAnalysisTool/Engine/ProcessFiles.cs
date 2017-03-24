using System;
using System.Xml;
using System.IO;
using NextGenImpactAnalysisTool.Model;
namespace NextGenImpactAnalysisTool.Engine
{
    public class ProcessFiles
    {
        ConfigModel _config;
        public ProcessFiles(ConfigModel Config)
        {
            _config = Config;
        }
        public string _fileVersioncheckmessage;
        public string FileVersioncheckmessage { get { return _fileVersioncheckmessage; } }
        public bool CheckLatestVersion()
        {
            if(File.Exists(_config.LocalFilename) == false)
            {
                Directory.CreateDirectory(@"C:\ImpactAnalysisFile");
                _fileVersioncheckmessage = "XML File is Old";
                return true;
            }
            {
                if (File.Exists(_config.Serverfilename) == false)
                {
                    _fileVersioncheckmessage = "Server not Connected";
                    return false;
                }
                if (FileCompare(_config.Serverfilename, _config.LocalFilename))
                {
                    _fileVersioncheckmessage = "XML File is Up-to-date";
                    return false;
                }
                else
                {
                    _fileVersioncheckmessage = "XML File is Old";
                    return true;
                }
            }
        }

        public void UpdateLocalXMLFile()
        {
            try
            {
                if (_config.Serverfilename.Length > 0 && _config.LocalFilename.Length > 0)
                {
                   File.Copy(_config.Serverfilename, _config.LocalFilename, true);
                }                
            }
            catch(Exception ex)
            {
                throw (new Exception(ex.Message));
            }

        }


        #region File Utility

        private bool CopyFile(string oldFile, string newFile)
        {
            using (var inputFile = new FileStream(
                                                   oldFile,
                                                   FileMode.Open,
                                                   FileAccess.Read,
                                                   FileShare.ReadWrite))
            {
                using (var outputFile = new FileStream(newFile,  FileMode.Create))
                {
                    var buffer = new byte[0x10000];
                    int bytes;

                    while ((bytes = inputFile.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        outputFile.Write(buffer, 0, bytes);
                    }
                }
            }
            return true;

        }
        // This method accepts two strings the represent two files to 
        // compare. A return value of 0 indicates that the contents of the files
        // are the same. A return value of any other value indicates that the 
        // files are not the same.
        private bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;
            try
            {
                // Determine if the same file was referenced two times.
                if (file1 == file2)
                {
                    // Return true to indicate that the files are the same.
                    return true;
                }

                // Open the two files.
                fs1 = new FileStream(file1, FileMode.Open);

                fs2 = new FileStream(file2, FileMode.Open);

                // Check the file sizes. If they are not the same, the files 
                // are not the same.
                if (fs1.Length != fs2.Length)
                {
                    // Close the file
                    fs1.Close();
                    fs2.Close();

                    // Return false to indicate files are different
                    return false;
                }

                // Read and compare a byte from each file until either a
                // non-matching set of bytes is found or until the end of
                // file1 is reached.
                do
                {
                    // Read one byte from each file.
                    file1byte = fs1.ReadByte();
                    file2byte = fs2.ReadByte();
                }
                while ((file1byte == file2byte) && (file1byte != -1));

                // Close the files.
                fs1.Close();
                fs2.Close();

                // Return the success of the comparison. "file1byte" is 
                // equal to "file2byte" at this point only if the files are 
                // the same.
                return ((file1byte - file2byte) == 0);
            }
            catch(Exception ex)
            { return false; }
            }


        #endregion
    }
}
