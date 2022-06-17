using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace FileDuplicates
{
    public class CheckForDuplicates : ICheckForDuplicates
    {
        public IReadOnlyCollection<IDuplicates> Check_candidates(IEnumerable<IDuplicates> candidates)
        {
            List<IDuplicates> listOfDuplicatesFiles = new();

            // Dictionary of Hash Values
            Dictionary<string, List<string>> dictionaryHash = new();

            foreach (Duplicates dublette in candidates)
            {
                foreach (string file in dublette.Filepath)
                {
                    using (var stream = File.OpenRead(file))
                    {
                        System.Text.StringBuilder sb = new();

                        string string_hash;

                        using (MD5 md5 = MD5.Create())
                        {
                            var hash = md5.ComputeHash(stream);

                            for (int i = 0; i < hash.Length; i++)
                            {
                                sb.Append(hash[i].ToString("X2"));
                            }
                            string_hash = sb.ToString();
                        }
                        //same work in Other methode
                        if (dictionaryHash.ContainsKey(string_hash))
                        {
                            dictionaryHash[string_hash].Add(file);
                        }
                        else
                        {
                            dictionaryHash.Add(string_hash, new List<string> { file });
                        }
                    }
                }
            }

            foreach (var item in dictionaryHash.Where(el => el.Value.Count > 1))
            {
                listOfDuplicatesFiles.Add(new Duplicates(item.Value));
            }
            return listOfDuplicatesFiles.AsReadOnly();
        }

       

        public IReadOnlyCollection<IDuplicates> Compile_candidates(string folderpath)
        {
          return Compile_candidates(folderpath, CompareModes.Size_and_name);
        }

        public IReadOnlyCollection<IDuplicates> Compile_candidates(string folderpath, CompareModes mode)
        {
            // Dictionary for mode size
            Dictionary<long, List<string>> dictionarySize = new();

            // Dictionary for mode size and name
            Dictionary<(long, string), List<string>> dictionarySizeWithName = new();

            
            List<IDuplicates> listOfDuplicatesFiles = new();
            if(folderpath == null|| folderpath==string.Empty)
                throw new ArgumentNullException(nameof(folderpath));

            // Get All files
            var files = Directory.EnumerateFiles(folderpath, "*", SearchOption.AllDirectories);
            switch (mode)
            {
                case CompareModes.Size:
                    {
                        foreach (string file in files)
                        {
                            var fileInfo = new FileInfo(file);

                            // here we will check if we have  this Key
                            if (dictionarySize.ContainsKey(fileInfo.Length))
                            {
                                // if we have it, we will add the value or file to same list for this key
                                dictionarySize[fileInfo.Length].Add(fileInfo.FullName);
                            }
                            else
                            {
                                // if don't have it we will create a new List and add this value to its
                                dictionarySize.Add(fileInfo.Length, new List<string> { fileInfo.FullName });
                            }
                        }

                        // Copy all value for same Key if has more than 1 
                        foreach (var item in dictionarySize.Where(el => el.Value.Count > 1))
                        {
                            listOfDuplicatesFiles.Add(new Duplicates(item.Value));
                        }
                        break;
                    }
                case CompareModes.Size_and_name:
                    {
                        foreach (string file in files)
                        {
                            var fileInfo = new FileInfo(file);

                            // for compaire with 2 or more Value (file and size)
                            (long, string) group = (fileInfo.Length, fileInfo.Name);

                            
                            if (dictionarySizeWithName.ContainsKey(group))
                            {

                                dictionarySizeWithName[group].Add(fileInfo.FullName);
                            }
                            else
                            {

                                dictionarySizeWithName.Add(group, new List<string> { fileInfo.FullName });
                            }
                        }
                        
                        foreach (var item in dictionarySizeWithName.Where(el => el.Value.Count > 1))
                        {
                            listOfDuplicatesFiles.Add(new Duplicates(item.Value));
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return listOfDuplicatesFiles.AsReadOnly();
        }
    }
       
           
    

}
