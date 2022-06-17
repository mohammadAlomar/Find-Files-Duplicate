namespace FileDuplicates
{
    public interface ICheckForDuplicates
    {
        IReadOnlyCollection<IDuplicates> Compile_candidates(string folderpath);
        IReadOnlyCollection<IDuplicates> Compile_candidates(string folderpath,
                                                      CompareModes mode);
        IReadOnlyCollection<IDuplicates> Check_candidates(IEnumerable<IDuplicates> candidates);
    }
}
