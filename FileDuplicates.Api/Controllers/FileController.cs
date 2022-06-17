using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileDuplicates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly ICheckForDuplicates _checkForDuplicates;

        public FileController(ICheckForDuplicates checkForDuplicates)
        {
            _checkForDuplicates = checkForDuplicates;
        }


        [HttpGet]
        [Route("getFiles")]
        public ActionResult getFiles(string path, bool SizeMode)
        {
            //When we have a big Project better to move all those Logic to Repository and then call methode from here 
            // but becouse its just for testing and just one class in class Library we call them from here direct.
            path = string.Join("@", path);
            IReadOnlyCollection<IDuplicates> result = new List<IDuplicates>();
            if (SizeMode)
            {
                 result = _checkForDuplicates.Compile_candidates(path, CompareModes.Size);
            }else
            {
                result = _checkForDuplicates.Compile_candidates(path);
            }

            if(result.Count == 0)
                return NoContent();
            return new OkObjectResult(result);
        }
        [HttpPost]
        [Route("CheckCandidates")]
        public ActionResult CheckCandidates(List<string> pathes)
        {
            List<string> candidate=new List<string>();
            foreach (var path in pathes)
            {
               
                candidate.Add(string.Join("@", path));
            }
            var candidates = new Duplicates(candidate);
            List<IDuplicates> listToCheckHash = new List<IDuplicates>();
            listToCheckHash.Add(candidates);
            var result = _checkForDuplicates.Check_candidates(listToCheckHash);
            if (result.Count == 0)
                return NoContent();
            return new OkObjectResult(result);
        }

    }
}
