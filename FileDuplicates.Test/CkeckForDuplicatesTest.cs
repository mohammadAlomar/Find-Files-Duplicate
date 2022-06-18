using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace FileDuplicates.Test
{
    public class CkeckForDuplicatesTest
    {
        private string? getRootPath()
        {
            string rootPath = null;
            var fullPath = Directory.GetCurrentDirectory().Split('\\');
            foreach (var diractory in fullPath)
            {
                if (diractory == "bin")
                    break;
                rootPath += diractory + "\\";
            }
            return rootPath;
        }

        [Fact]
        public void CompileCandidatesWithSizeShouldReturnOneDuplicate()
        {
            var rootPath=getRootPath();
            var path = Path.Combine(@rootPath, "NewFolder");
            
            CheckForDuplicates test=new CheckForDuplicates();

            var result=test.Compile_candidates(path, CompareModes.Size);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void CompileCandidatesWithSizeAndNameshouldReturnOneDuplicate()
        {
            var rootPath = getRootPath();
            var path = Path.Combine(@rootPath, "NewFolder");

            CheckForDuplicates test = new CheckForDuplicates();

            var result = test.Compile_candidates(path);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void CompileCandidatesWithSizeAndNameshouldReturnZeroDuplicate()
        {
            var rootPath = getRootPath();
            var path = Path.Combine(@rootPath, "NewFolder\\NewFolder");

            CheckForDuplicates test = new CheckForDuplicates();

            var result = test.Compile_candidates(path);
            
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void CompileCandidatesWithSizeAndNameThrowException()
        {
            string Path = @"";
            CheckForDuplicates test = new CheckForDuplicates();
            
            Assert.Throws<ArgumentNullException>(()=> test.Compile_candidates(Path, CompareModes.Size));
        }

        [Fact]
        public void Check_candidatesTest()
        {
            var rootPath = getRootPath();
            var path = Path.Combine(@rootPath, "NewFolder");

            CheckForDuplicates test = new CheckForDuplicates();
            var candidates = test.Compile_candidates(path);

            var result = test.Check_candidates(candidates);
            List<IDuplicates> listofFilePath = new List<IDuplicates>();
            foreach (var item in result)
            {               
                listofFilePath.Add(item);
            }

            Assert.Equal(1, result.Count);
            Assert.Equal(2, listofFilePath[0].Filepath.Count);
        }
        [Fact]
        public void Check_candidatesNotSameHashTest()
        {
            var rootPath = getRootPath();
            var path = Path.Combine(@rootPath, "NewFolder\\NewFolder");

            CheckForDuplicates test = new CheckForDuplicates();
            var candidates = test.Compile_candidates(path);

            var result = test.Check_candidates(candidates);

            Assert.NotEqual(1,result.Count);
        }
    }
}