using System;
using System.Collections.Generic;
using Xunit;

namespace FileDuplicates.Test
{
    public class CkeckForDuplicatesTest
    {
        [Fact]
        public void CompileCandidatesWithSizeShouldReturnOneDuplicate()
        {
            string Path = @"F:\Dev\Schleupen SE\FileDuplicates.Test\NewFolder";
            CheckForDuplicates test=new CheckForDuplicates();

            var result=test.Compile_candidates(Path,CompareModes.Size);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void CompileCandidatesWithSizeAndNameshouldReturnOneDuplicate()
        {
            string Path = @"F:\Dev\Schleupen SE\FileDuplicates.Test\NewFolder";
            CheckForDuplicates test = new CheckForDuplicates();

            var result = test.Compile_candidates(Path);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void CompileCandidatesWithSizeAndNameshouldReturnZeroDuplicate()
        {
            string Path = @"F:\Dev\Schleupen SE\FileDuplicates.Test\NewFolder\NewFolder";
            CheckForDuplicates test = new CheckForDuplicates();

            var result = test.Compile_candidates(Path);
            
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
            string Path = @"F:\Dev\Schleupen SE\FileDuplicates.Test\NewFolder";
            CheckForDuplicates test = new CheckForDuplicates();
            var candidates = test.Compile_candidates(Path);

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
            string Path = @"F:\Dev\Schleupen SE\FileDuplicates.Test\NewFolder\NewFolder";
            CheckForDuplicates test = new CheckForDuplicates();
            var candidates = test.Compile_candidates(Path);

            var result = test.Check_candidates(candidates);

            Assert.NotEqual(1,result.Count);
        }
    }
}